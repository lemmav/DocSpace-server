import React from 'react';
import { inject, observer } from 'mobx-react';

import { ShareAccessRights, AppServerConfig } from '@appserver/common/constants';
import toastr from '@appserver/components/toast/toastr';
import { combineUrl } from '@appserver/common/utils';
import { getFileConversationProgress } from '@appserver/common/api/files';

import Badges from '../components/Badges';
import config from '../../package.json';

export default function withBadges(WrappedComponent) {
  class WithBadges extends React.Component {
    onShowVersionHistory = () => {
      const {
        homepage,
        isTabletView,
        item,
        setIsVerHistoryPanel,
        fetchFileVersions,
        history,
        isTrashFolder,
      } = this.props;
      if (isTrashFolder) return;

      if (!isTabletView) {
        fetchFileVersions(item.id + '');
        setIsVerHistoryPanel(true);
      } else {
        history.push(combineUrl(AppServerConfig.proxyURL, homepage, `/${item.id}/history`));
      }
    };

    onBadgeClick = () => {
      const { item, selectedFolderPathParts, markAsRead, setNewFilesPanelVisible } = this.props;
      if (item.fileExst) {
        markAsRead([], [item.id], item);
      } else {
        const newFolderIds = selectedFolderPathParts;
        newFolderIds.push(item.id);
        setNewFilesPanelVisible(true, newFolderIds, item);
      }
    };

    getConvertProgress = (fileId) => {
      const {
        selectedFolderId,
        filter,
        setIsLoading,
        setSecondaryProgressBarData,
        t,
        clearSecondaryProgressData,
        fetchFiles,
      } = this.props;
      getFileConversationProgress(fileId).then((res) => {
        if (res && res[0] && res[0].progress !== 100) {
          setSecondaryProgressBarData({
            icon: 'file',
            visible: true,
            percent: res[0].progress,
            label: t('Convert'),
            alert: false,
          });
          setTimeout(() => this.getConvertProgress(fileId), 1000);
        } else {
          if (res[0].error) {
            setSecondaryProgressBarData({
              visible: true,
              alert: true,
            });
            toastr.error(res[0].error);
            setTimeout(() => clearSecondaryProgressData(), TIMEOUT);
          } else {
            setSecondaryProgressBarData({
              icon: 'file',
              visible: true,
              percent: 100,
              label: t('Convert'),
              alert: false,
            });
            setTimeout(() => clearSecondaryProgressData(), TIMEOUT);
            const newFilter = filter.clone();
            fetchFiles(selectedFolderId, newFilter)
              .catch((err) => {
                setSecondaryProgressBarData({
                  visible: true,
                  alert: true,
                });
                //toastr.error(err);
                setTimeout(() => clearSecondaryProgressData(), TIMEOUT);
              })
              .finally(() => setIsLoading(false));
          }
        }
      });
    };

    setConvertDialogVisible = () => {
      this.props.setConvertItem(this.props.item);
      this.props.setConvertDialogVisible(true);
    };

    render() {
      const {
        t,
        theme,
        item,
        canWebEdit,
        isTrashFolder,
        isPrivacyFolder,
        canConvert,
        onFilesClick,
        isAdmin,
        isDesktopClient,
        sectionWidth,
      } = this.props;
      const { fileStatus, access } = item;

      const newItems = item.new || fileStatus === 2;
      const showNew = !!newItems;

      const accessToEdit =
        access === ShareAccessRights.FullAccess || access === ShareAccessRights.None; // TODO: fix access type for owner (now - None)

      const badgesComponent = (
        <Badges
          t={t}
          theme={theme}
          item={item}
          isAdmin={isAdmin}
          showNew={showNew}
          newItems={newItems}
          sectionWidth={sectionWidth}
          canWebEdit={canWebEdit}
          canConvert={canConvert}
          isTrashFolder={isTrashFolder}
          isPrivacyFolder={isPrivacyFolder}
          isDesktopClient={isDesktopClient}
          accessToEdit={accessToEdit}
          onShowVersionHistory={this.onShowVersionHistory}
          onBadgeClick={this.onBadgeClick}
          setConvertDialogVisible={this.setConvertDialogVisible}
          onFilesClick={onFilesClick}
        />
      );

      return <WrappedComponent badgesComponent={badgesComponent} {...this.props} />;
    }
  }

  return inject(
    (
      {
        auth,
        formatsStore,
        treeFoldersStore,
        filesActionsStore,
        versionHistoryStore,
        selectedFolderStore,
        dialogsStore,
        filesStore,
        uploadDataStore,
      },
      { item },
    ) => {
      const { docserviceStore } = formatsStore;
      const { isRecycleBinFolder, isPrivacyFolder, updateRootBadge } = treeFoldersStore;
      const { markAsRead } = filesActionsStore;
      const { isTabletView, isDesktopClient, theme } = auth.settingsStore;
      const { setIsVerHistoryPanel, fetchFileVersions } = versionHistoryStore;
      const { setNewFilesPanelVisible, setConvertDialogVisible, setConvertItem } = dialogsStore;
      const { filter, setIsLoading, fetchFiles } = filesStore;
      const { secondaryProgressDataStore } = uploadDataStore;
      const {
        setSecondaryProgressBarData,
        clearSecondaryProgressData,
      } = secondaryProgressDataStore;

      const canWebEdit = docserviceStore.canWebEdit(item.fileExst);
      const canConvert = docserviceStore.canConvert(item.fileExst);

      return {
        theme,
        isAdmin: auth.isAdmin,
        canWebEdit,
        canConvert,
        isTrashFolder: isRecycleBinFolder,
        isPrivacyFolder,
        homepage: config.homepage,
        isTabletView,
        setIsVerHistoryPanel,
        fetchFileVersions,
        selectedFolderPathParts: selectedFolderStore.pathParts,
        markAsRead,
        setNewFilesPanelVisible,
        updateRootBadge,
        setSecondaryProgressBarData,
        selectedFolderId: selectedFolderStore.id,
        filter,
        setIsLoading,
        clearSecondaryProgressData,
        fetchFiles,
        setConvertDialogVisible,
        setConvertItem,
        isDesktopClient,
      };
    },
  )(observer(WithBadges));
}
