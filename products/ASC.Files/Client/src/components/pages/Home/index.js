import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { withRouter } from "react-router";
import { RequestLoader, Checkbox, toastr } from "asc-web-components";
import { PageLayout, utils, api } from "asc-web-common";
import { withTranslation, I18nextProvider } from 'react-i18next';
import i18n from "./i18n";

import {
  ArticleHeaderContent,
  ArticleBodyContent,
  ArticleMainButtonContent
} from "../../Article";
import {
  SectionHeaderContent,
  SectionBodyContent,
  SectionFilterContent,
  SectionPagingContent
} from "./Section";
import { setSelected, fetchFiles, setTreeFolders } from "../../../store/files/actions";
import { loopTreeFolders } from "../../../store/files/selectors";
import store from "../../../store/store";
const { changeLanguage } = utils;

class PureHome extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isHeaderVisible: false,
      isHeaderIndeterminate: false,
      isHeaderChecked: false,
      isLoading: false,

      showProgressBar: false,
      progressBarValue: 0,
      progressBarLabel: "",
      overwriteSetting: false,
      uploadOriginalFormatSetting: false,
      hideWindowSetting: false,

      files: [],
      uploadedFiles: 0,
      totalSize: 0,
      percent: 0,
    };
  }

  renderGroupButtonMenu = () => {
    const { files, selection, selected, setSelected, folders } = this.props;

    const headerVisible = selection.length > 0;
    const headerIndeterminate =
      headerVisible &&
      selection.length > 0 &&
      selection.length < files.length + folders.length;
    const headerChecked =
      headerVisible && selection.length === files.length + folders.length;

    /*console.log(`renderGroupButtonMenu()
      headerVisible=${headerVisible} 
      headerIndeterminate=${headerIndeterminate} 
      headerChecked=${headerChecked}
      selection.length=${selection.length}
      files.length=${files.length}
      selected=${selected}`);*/

    let newState = {};

    if (headerVisible || selected === "close") {
      newState.isHeaderVisible = headerVisible;
      if (selected === "close") {
        setSelected("none");
      }
    }

    newState.isHeaderIndeterminate = headerIndeterminate;
    newState.isHeaderChecked = headerChecked;

    this.setState(newState);
  };

  //TODO: Refactor this block

  updateFiles = () => {
    const { filter, currentFolderId, treeFolders, setTreeFolders } = this.props;

    this.onLoading(true);
    const newFilter = filter.clone();
    fetchFiles(currentFolderId, newFilter, store.dispatch, treeFolders)
      .then((data) => {
        const path = data.selectedFolder.pathParts;
        const newTreeFolders = treeFolders;
        const folders = data.selectedFolder.folders;
        const foldersCount = data.selectedFolder.foldersCount;
        loopTreeFolders(path, newTreeFolders, folders, foldersCount);
        setTreeFolders(newTreeFolders);
      })
      .catch((err) => toastr.error(err))
      .finally(() => {
        this.onLoading(false);
      });
  };

  sendChunk = (files, location, requestsDataArray, isLatestFile, indexOfFile) => {
    const sendRequestFunc = (index) => {
      let newState = {};
      api.files
        .uploadFile(location, requestsDataArray[index])
        .then((res) => {
          let newPercent = this.state.percent;
          const percent = (newPercent +=
            (files[indexOfFile].size / this.state.totalSize) * 100);
          if (res.data.data && res.data.data.uploaded) {
            files[indexOfFile].uploaded = true;
            newState = { files, percent };
          }
          if (index + 1 !== requestsDataArray.length) {
            sendRequestFunc(index + 1);
          } else if (isLatestFile) {
            this.updateFiles();
            newState = Object.assign({}, newState, {
              uploadedFiles: this.state.uploadedFiles + 1,
            });
            return;
          } else {
            newState = Object.assign({}, newState, {
              uploadedFiles: this.state.uploadedFiles + 1,
            });
            this.startSessionFunc(indexOfFile + 1);
          }
        })
        .catch((err) => toastr.error(err))
        .finally(() => {
          if (
            newState.hasOwnProperty("files") ||
            newState.hasOwnProperty("percent") ||
            newState.hasOwnProperty("uploadedFiles")
          ) {
            let progressVisible = true;
            let uploadedFiles = newState.uploadedFiles;
            let percent = newState.percent;
            if (newState.uploadedFiles === files.length) {
              percent = 100;
              newState.percent = 0;
              newState.uploadedFiles = 0;
              progressVisible = false;
            }
            newState.progressBarValue = percent;
            newState.progressBarLabel = this.props.t("UploadingLabel", {
              file: uploadedFiles,
              totalFiles: files.length,
            });

            this.setState(newState, () => {
              if (!progressVisible) {
                this.setProgressVisible(false);
              }
            });
          }
        });
    };

    sendRequestFunc(0);
  };

  //TODO: Refactor this block

  startSessionFunc = (indexOfFile) => {
    const { files } = this.state;
    const { currentFolderId } = this.props;
    const file = files[indexOfFile];
    const isLatestFile = indexOfFile === files.length - 1;

    const fileName = file.name;
    const fileSize = file.size;
    const relativePath = file.relativePath
      ? file.relativePath.slice(1, -file.name.length)
      : file.webkitRelativePath
      ? file.webkitRelativePath.slice(0, -file.name.length)
      : "";

    let location;
    const requestsDataArray = [];
    const chunkSize = 1024 * 1023; //~0.999mb
    const chunks = Math.ceil(file.size / chunkSize, chunkSize);
    let chunk = 0;

    api.files
      .startUploadSession(currentFolderId, fileName, fileSize, relativePath)
      .then((res) => {
        location = res.data.location;
        while (chunk < chunks) {
          const offset = chunk * chunkSize;
          //console.log("current chunk..", chunk);
          //console.log("file blob from offset...", offset);
          //console.log(file.slice(offset, offset + chunkSize));

          const formData = new FormData();
          formData.append("file", file.slice(offset, offset + chunkSize));
          requestsDataArray.push(formData);
          chunk++;
        }
      })
      .then(
        () => this.sendChunk(files, location, requestsDataArray, isLatestFile, indexOfFile)
      )
      .catch((err) => {
        this.setProgressVisible(false, 0);
        toastr.error(err);
      });
  };

  onDrop = (e) => {
    // ev.currentTarget.style.background = "lightyellow";
    const items = e.dataTransfer.items;
    let files = [];

    const inSeries = (queue, callback) => {
      let i = 0;
      let length = queue.length;

      if (!queue || !queue.length) {
        callback();
      }

      const callNext = (i) => {
        if (typeof queue[i] === "function") {
          queue[i](() => i+1 < length ? callNext(i+1) : callback());
        }
      };
      callNext(i);
    };

    const readDirEntry = (dirEntry, callback) => {
      let entries = [];
      const dirReader = dirEntry.createReader();

      // keep quering recursively till no more entries
      const getEntries = (func) => {
        dirReader.readEntries((moreEntries) => {
          if (moreEntries.length) {
            entries = [...entries, ...moreEntries];
            getEntries(func);
          } else {
            func();
          }
        });
      };

      getEntries(() => readEntries(entries, callback));
    };

    const readEntry = (entry, callback) => {
      if (entry.isFile) {
        entry.file(file => {
          addFile(file, entry.fullPath);
          callback();
        });
      } else if (entry.isDirectory) {
        readDirEntry(entry, callback);
      }
    };

    const readEntries = (entries, callback) => {
      const queue = [];
      loop(entries, (entry) => {
        queue.push((func) => readEntry(entry, func));
      });
      inSeries(queue, () => callback());
    };

    const addFile = (file, relativePath) => {
      file.relativePath = relativePath || "";
      files.push(file);
    };

    const loop = (items, callback) => {
      let length;

      if (items) {
        length = items.length;
        // Loop array items
        for (let i = 0; i < length; i++) {
          callback(items[i], i);
        }
      }
    };

    const readItems = (items, func) => {
      const entries = [];
      loop(items, (item) => {
        const entry = item.webkitGetAsEntry();
        if (entry) {
          if (entry.isFile) {
            addFile(item.getAsFile(), entry.fullPath);
          } else {
            entries.push(entry);
          }
        }
      });

      if (entries.length) {
        readEntries(entries, func);
      } else {
        func();
      }
    };

    this.setState({ isLoading: true }, () =>
      readItems(items, () => this.startUpload(files))
    );
  };

  startUpload = files => {
    const newFiles = [];
    let total = 0;

    for (let item of files) {
      if (item.size !== 0) {
        newFiles.push(item);
        total += item.size;
      } else {
        toastr.error(this.props.t("ErrorUploadMessage"));
      }
    }
    this.startUploadFiles(newFiles, total);
  }

  startUploadFiles = (files, totalSize) => {
    if (files.length > 0) {
      const progressBarLabel = this.props.t("UploadingLabel", {
        file: 0,
        totalFiles: files.length,
      });
      this.setState({ files, totalSize, progressBarLabel, showProgressBar: true, isLoading: true },
        () => { this.startSessionFunc(0); });
    } else if(this.state.isLoading) {
      this.setState({ isLoading: false });
    }
  };

  componentDidUpdate(prevProps) {
    if (this.props.selection !== prevProps.selection) {
      this.renderGroupButtonMenu();
    }
  }

  onSectionHeaderContentCheck = (checked) => {
    this.props.setSelected(checked ? "all" : "none");
  };

  onSectionHeaderContentSelect = (selected) => {
    this.props.setSelected(selected);
  };

  onClose = () => {
    const { selection, setSelected } = this.props;

    if (!selection.length) {
      setSelected("none");
      this.setState({ isHeaderVisible: false });
    } else {
      setSelected("close");
    }
  };

  onLoading = (status) => {
    this.setState({ isLoading: status });
  };

  setProgressVisible = (visible, timeout) => {
    const newTimeout = timeout ? timeout : 10000;
    if (visible) {
      this.setState({ showProgressBar: visible });
    } else {
      setTimeout(
        () => this.setState({ showProgressBar: visible, progressBarValue: 0 }),
        newTimeout
      );
    }
  };
  setProgressValue = (value) => this.setState({ progressBarValue: value });
  setProgressLabel = (label) => this.setState({ progressBarLabel: label });

  onChangeOverwrite = () =>
    this.setState({ overwriteSetting: !this.state.overwriteSetting });
  onChangeOriginalFormat = () =>
    this.setState({
      uploadOriginalFormatSetting: !this.state.uploadOriginalFormatSetting,
    });
  onChangeWindowVisible = () =>
    this.setState({ hideWindowSetting: !this.state.hideWindowSetting });

  startFilesOperations = (progressBarLabel) => {
    this.setState({ isLoading: true, progressBarLabel, showProgressBar: true });
  };

  finishFilesOperations = (err) => {
    const timeout = err ? 0 : null;
    err && toastr.error(err);
    this.onLoading(false);
    this.setProgressVisible(false, timeout);
  };

  render() {
    const {
      isHeaderVisible,
      isHeaderIndeterminate,
      isHeaderChecked,
      selected,
      isLoading,
      showProgressBar,
      progressBarValue,
      progressBarLabel,
      overwriteSetting,
      uploadOriginalFormatSetting,
      hideWindowSetting,
    } = this.state;
    const { t } = this.props;

    const progressBarContent = (
      <div>
        <Checkbox
          onChange={this.onChangeOverwrite}
          isChecked={overwriteSetting}
          label={t("OverwriteSetting")}
        />
        <Checkbox
          onChange={this.onChangeOriginalFormat}
          isChecked={uploadOriginalFormatSetting}
          label={t("UploadOriginalFormatSetting")}
        />
        <Checkbox
          onChange={this.onChangeWindowVisible}
          isChecked={hideWindowSetting}
          label={t("HideWindowSetting")}
        />
      </div>
    );

    return (
      <>
        <RequestLoader
          visible={isLoading}
          zIndex={256}
          loaderSize="16px"
          loaderColor={"#999"}
          label={`${t("LoadingProcessing")} ${t("LoadingDescription")}`}
          fontSize="12px"
          fontColor={"#999"}
        />
        <PageLayout
          withBodyScroll
          withBodyAutoFocus
          uploadFiles
          onDrop={this.onDrop}
          showProgressBar={showProgressBar}
          progressBarValue={progressBarValue}
          progressBarDropDownContent={progressBarContent}
          progressBarLabel={progressBarLabel}
          articleHeaderContent={<ArticleHeaderContent />}
          articleMainButtonContent={
            <ArticleMainButtonContent
              onLoading={this.onLoading}
              startUpload={this.startUpload}
              setProgressVisible={this.setProgressVisible}
              setProgressValue={this.setProgressValue}
              setProgressLabel={this.setProgressLabel}
            />
          }
          articleBodyContent={
            <ArticleBodyContent
              onLoading={this.onLoading}
              isLoading={isLoading}
            />
          }
          sectionHeaderContent={
            <SectionHeaderContent
              isHeaderVisible={isHeaderVisible}
              isHeaderIndeterminate={isHeaderIndeterminate}
              isHeaderChecked={isHeaderChecked}
              onCheck={this.onSectionHeaderContentCheck}
              onSelect={this.onSectionHeaderContentSelect}
              onClose={this.onClose}
              onLoading={this.onLoading}
              isLoading={isLoading}
              setProgressValue={this.setProgressValue}
              startFilesOperations={this.startFilesOperations}
              finishFilesOperations={this.finishFilesOperations}
            />
          }
          sectionFilterContent={
            <SectionFilterContent onLoading={this.onLoading} />
          }
          sectionBodyContent={
            <SectionBodyContent
              selected={selected}
              isLoading={isLoading}
              onLoading={this.onLoading}
              onChange={this.onRowChange}
              setProgressValue={this.setProgressValue}
              startFilesOperations={this.startFilesOperations}
              finishFilesOperations={this.finishFilesOperations}
            />
          }
          sectionPagingContent={
            <SectionPagingContent onLoading={this.onLoading} />
          }
        />
      </>
    );
  }
}

const HomeContainer = withTranslation()(PureHome);

const Home = (props) => {
  changeLanguage(i18n);
  return (<I18nextProvider i18n={i18n}><HomeContainer {...props} /></I18nextProvider>);
}

Home.propTypes = {
  files: PropTypes.array,
  history: PropTypes.object.isRequired,
  isLoaded: PropTypes.bool
};

function mapStateToProps(state) {
  return {
    files: state.files.files,
    folders: state.files.folders,
    selection: state.files.selection,
    selected: state.files.selected,
    isLoaded: state.auth.isLoaded,

    currentFolderId: state.files.selectedFolder.id,
    filter: state.files.filter,
    treeFolders: state.files.treeFolders
  };
}

export default connect(
  mapStateToProps,
  { setSelected, setTreeFolders }
)(withRouter(Home));
