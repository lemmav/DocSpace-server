import React from "react";
import PropTypes from "prop-types";
import Backdrop from "@appserver/components/backdrop";
import { desktop, size, tablet } from "@appserver/components/utils/device";
import { Provider } from "@appserver/components/utils/context";
import { isMobile, isFirefox, isMobileOnly } from "react-device-detect";
import Article from "./sub-components/article";
import SubArticleHeader from "./sub-components/article-header";
import SubArticleMainButton from "./sub-components/article-main-button";
import SubArticleBody from "./sub-components/article-body";
import ArticlePinPanel from "./sub-components/article-pin-panel";
import Section from "./sub-components/section";
import SubSectionHeader from "./sub-components/section-header";
import SubSectionFilter from "./sub-components/section-filter";
import SubSectionBody from "./sub-components/section-body";
import SubSectionBodyContent from "./sub-components/section-body-content";
import SubSectionBar from "./sub-components/section-bar";
import SubSectionPaging from "./sub-components/section-paging";
import SectionToggler from "./sub-components/section-toggler";
import ReactResizeDetector from "react-resize-detector";
import FloatingButton from "../FloatingButton";
import { inject, observer } from "mobx-react";
import Selecto from "react-selecto";
import styled, { css } from "styled-components";
import { LayoutContextConsumer } from "studio/Layout/context";
import classnames from "classnames";

const StyledSelectoWrapper = styled.div`
  .selecto-selection {
    z-index: 200;
  }
`;

const StyledMainBar = styled.div`
  box-sizing: border-box;
  @media ${desktop} {
    margin-left: -24px;
    max-width: ${(props) => props.width + 40 + "px"};
  }

  @media ${tablet} {
    margin-left: -16px;
    max-width: ${(props) => props.width + 36 + "px"};
  }
  ${isMobile &&
  css`
    z-index: 149;
    //  min-width: 100%;
    position: fixed;
    // padding-left: 8px;
    top: 48px;
  `};
`;

function ArticleHeader() {
  return null;
}
ArticleHeader.displayName = "ArticleHeader";

function ArticleMainButton() {
  return null;
}
ArticleMainButton.displayName = "ArticleMainButton";

function ArticleBody() {
  return null;
}
ArticleBody.displayName = "ArticleBody";

function SectionHeader() {
  return null;
}
SectionHeader.displayName = "SectionHeader";

function SectionBar() {
  return null;
}

SectionBar.displayName = "SectionBar";

function SectionFilter() {
  return null;
}
SectionFilter.displayName = "SectionFilter";

function SectionBody() {
  return null;
}
SectionBody.displayName = "SectionBody";

function SectionPaging() {
  return null;
}
SectionPaging.displayName = "SectionPaging";

class PageLayout extends React.Component {
  static ArticleHeader = ArticleHeader;
  static ArticleMainButton = ArticleMainButton;
  static ArticleBody = ArticleBody;
  static SectionHeader = SectionHeader;
  static SectionFilter = SectionFilter;
  static SectionBody = SectionBody;
  static SectionBar = SectionBar;
  static SectionPaging = SectionPaging;

  constructor(props) {
    super(props);

    this.timeoutHandler = null;
    this.intervalHandler = null;

    this.scroll = null;
  }

  componentDidUpdate(prevProps) {
    if (!this.scroll) {
      this.scroll = document.getElementsByClassName("section-scroll")[0];
    }

    if (
      this.props.hideAside &&
      !this.props.isArticlePinned &&
      this.props.hideAside !== prevProps.hideAside
    ) {
      this.backdropClick();
    }
  }

  componentDidMount() {
    window.addEventListener("orientationchange", this.orientationChangeHandler);

    this.orientationChangeHandler();
  }

  componentWillUnmount() {
    window.removeEventListener(
      "orientationchange",
      this.orientationChangeHandler
    );

    if (this.intervalHandler) clearInterval(this.intervalHandler);
    if (this.timeoutHandler) clearTimeout(this.timeoutHandler);
  }

  orientationChangeHandler = () => {
    const isValueExist = !!this.props.isArticlePinned;
    const isEnoughWidth = screen.availWidth > size.smallTablet;
    const isPortrait =
      isFirefox &&
      isMobileOnly &&
      screen.orientation.type === "portrait-primary";

    if ((!isEnoughWidth && isValueExist) || isPortrait) {
      this.backdropClick();
      return;
    }
    if (isEnoughWidth && isValueExist) {
      this.pinArticle();
    }
  };

  backdropClick = () => {
    this.props.setArticlePinned(false);
    this.props.setIsBackdropVisible(false);
    this.props.setIsArticleVisible(false);
    isMobile && this.props.setArticleVisibleOnUnpin(false);
  };

  pinArticle = () => {
    this.props.setIsBackdropVisible(false);
    this.props.setIsArticleVisible(true);
    this.props.setArticlePinned(true);
    isMobile && this.props.setArticleVisibleOnUnpin(false);
  };

  unpinArticle = () => {
    this.props.setIsBackdropVisible(true);
    this.props.setIsArticleVisible(true);
    this.props.setArticlePinned(false);
    isMobile && this.props.setArticleVisibleOnUnpin(true);
  };

  showArticle = () => {
    this.props.setArticlePinned(false);
    this.props.setIsBackdropVisible(true);
    this.props.setIsArticleVisible(true);
    isMobile && this.props.setArticleVisibleOnUnpin(true);
  };

  onSelect = (e) => {
    if (this.props.dragging) return;
    const items = e.selected;
    this.props.setSelections(items);
  };

  dragCondition = (e) => {
    const path = e.inputEvent.composedPath();
    const isBackdrop = path.some(
      (x) => x.classList && x.classList.contains("backdrop-active")
    );
    const notSelectablePath = path.some(
      (x) => x.classList && x.classList.contains("not-selectable")
    );

    const isDraggable = path.some(
      (x) => x.classList && x.classList.contains("draggable")
    );

    if (notSelectablePath || isBackdrop || isDraggable) {
      return false;
    } else return true;
  };

  onScroll = (e) => {
    this.scroll.scrollBy(e.direction[0] * 10, e.direction[1] * 10);
  };

  render() {
    const {
      onDrop,
      showPrimaryProgressBar,
      primaryProgressBarIcon,
      primaryProgressBarValue,
      showPrimaryButtonAlert,
      showSecondaryProgressBar,
      secondaryProgressBarValue,
      secondaryProgressBarIcon,
      showSecondaryButtonAlert,
      uploadFiles,
      viewAs,
      //withBodyAutoFocus,
      withBodyScroll,
      children,
      isHeaderVisible,
      //headerBorderBottom,
      onOpenUploadPanel,
      isTabletView,
      firstLoad,
      dragging,
      isArticleVisible,
      isBackdropVisible,
      isArticlePinned,
      isDesktop,
      isHomepage,
      maintenanceExist,
    } = this.props;
    let articleHeaderContent = null;
    let articleMainButtonContent = null;
    let articleBodyContent = null;
    let sectionHeaderContent = null;
    let sectionBarContent = null;
    let sectionFilterContent = null;
    let sectionPagingContent = null;
    let sectionBodyContent = null;

    React.Children.forEach(children, (child) => {
      const childType =
        child && child.type && (child.type.displayName || child.type.name);

      switch (childType) {
        case ArticleHeader.displayName:
          articleHeaderContent = child;
          break;
        case ArticleMainButton.displayName:
          articleMainButtonContent = child;
          break;
        case ArticleBody.displayName:
          articleBodyContent = child;
          break;
        case SectionHeader.displayName:
          sectionHeaderContent = child;
          break;
        case SectionFilter.displayName:
          sectionFilterContent = child;
          break;
        case SectionBar.displayName:
          sectionBarContent = child;
          break;
        case SectionPaging.displayName:
          sectionPagingContent = child;
          break;
        case SectionBody.displayName:
          sectionBodyContent = child;
          break;
        default:
          break;
      }
    });

    const isArticleHeaderAvailable = !!articleHeaderContent,
      isArticleMainButtonAvailable = !!articleMainButtonContent,
      isArticleBodyAvailable = !!articleBodyContent,
      isArticleAvailable =
        isArticleHeaderAvailable ||
        isArticleMainButtonAvailable ||
        isArticleBodyAvailable,
      isSectionHeaderAvailable = !!sectionHeaderContent,
      isSectionFilterAvailable = !!sectionFilterContent,
      isSectionPagingAvailable = !!sectionPagingContent,
      isSectionBodyAvailable =
        !!sectionBodyContent ||
        isSectionFilterAvailable ||
        isSectionPagingAvailable,
      isSectionAvailable =
        isSectionHeaderAvailable ||
        isSectionFilterAvailable ||
        isSectionBodyAvailable ||
        isSectionPagingAvailable ||
        isArticleAvailable,
      isBackdropAvailable = isArticleAvailable;

    const renderPageLayout = () => {
      return (
        <>
          {isBackdropAvailable && (
            <Backdrop
              zIndex={400}
              visible={isBackdropVisible}
              onClick={this.backdropClick}
            />
          )}
          {isArticleAvailable && (
            <Article
              visible={isArticleVisible}
              pinned={isArticlePinned}
              firstLoad={firstLoad}
            >
              {isArticleHeaderAvailable && (
                <SubArticleHeader>
                  {articleHeaderContent
                    ? articleHeaderContent.props.children
                    : null}
                </SubArticleHeader>
              )}
              {isArticleMainButtonAvailable && (
                <SubArticleMainButton>
                  {articleMainButtonContent
                    ? articleMainButtonContent.props.children
                    : null}
                </SubArticleMainButton>
              )}
              {isArticleBodyAvailable && (
                <SubArticleBody pinned={isArticlePinned} isDesktop={isDesktop}>
                  {articleBodyContent
                    ? articleBodyContent.props.children
                    : null}
                </SubArticleBody>
              )}
              {isArticleBodyAvailable && (
                <ArticlePinPanel
                  pinned={isArticlePinned}
                  onPin={this.pinArticle}
                  onUnpin={this.unpinArticle}
                />
              )}
            </Article>
          )}
          {isSectionAvailable && (
            <ReactResizeDetector
              refreshRate={100}
              refreshMode="debounce"
              refreshOptions={{ trailing: true }}
            >
              {({ width, height }) => (
                <Provider
                  value={{
                    sectionWidth: width,
                    sectionHeight: height,
                  }}
                >
                  <Section
                    widthProp={width}
                    unpinArticle={this.unpinArticle}
                    pinned={isArticlePinned}
                    visible={isArticleVisible}
                  >
                    <LayoutContextConsumer>
                      {(value) => (
                        <StyledMainBar
                          width={width}
                          id="main-bar"
                          className={classnames("main-bar", {
                            "main-bar--hidden":
                              value.isVisible === undefined
                                ? false
                                : !value.isVisible,
                          })}
                        >
                          <SubSectionBar>
                            {sectionBarContent
                              ? sectionBarContent.props.children
                              : null}
                          </SubSectionBar>
                        </StyledMainBar>
                      )}
                    </LayoutContextConsumer>

                    {isSectionHeaderAvailable && (
                      <SubSectionHeader
                        maintenanceExist={maintenanceExist}
                        isHeaderVisible={isHeaderVisible}
                        isArticlePinned={isArticlePinned}
                        viewAs={viewAs}
                      >
                        {sectionHeaderContent
                          ? sectionHeaderContent.props.children
                          : null}
                      </SubSectionHeader>
                    )}

                    {isSectionFilterAvailable && (
                      <>
                        <SubSectionFilter
                          className="section-header_filter"
                          viewAs={viewAs}
                        >
                          {sectionFilterContent
                            ? sectionFilterContent.props.children
                            : null}
                        </SubSectionFilter>
                      </>
                    )}
                    {isSectionBodyAvailable && (
                      <>
                        <SubSectionBody
                          onDrop={onDrop}
                          uploadFiles={uploadFiles}
                          withScroll={withBodyScroll}
                          autoFocus={isMobile || isTabletView ? false : true}
                          pinned={isArticlePinned}
                          viewAs={viewAs}
                          isHomepage={isHomepage}
                        >
                          {isSectionFilterAvailable && (
                            <SubSectionFilter className="section-body_filter">
                              {sectionFilterContent
                                ? sectionFilterContent.props.children
                                : null}
                            </SubSectionFilter>
                          )}
                          <SubSectionBodyContent>
                            {sectionBodyContent
                              ? sectionBodyContent.props.children
                              : null}
                          </SubSectionBodyContent>
                          {isSectionPagingAvailable && (
                            <SubSectionPaging>
                              {sectionPagingContent
                                ? sectionPagingContent.props.children
                                : null}
                            </SubSectionPaging>
                          )}
                        </SubSectionBody>
                      </>
                    )}

                    {showPrimaryProgressBar && showSecondaryProgressBar ? (
                      <>
                        <FloatingButton
                          className="layout-progress-bar"
                          icon={primaryProgressBarIcon}
                          percent={primaryProgressBarValue}
                          alert={showPrimaryButtonAlert}
                          onClick={onOpenUploadPanel}
                        />
                        <FloatingButton
                          className="layout-progress-second-bar"
                          icon={secondaryProgressBarIcon}
                          percent={secondaryProgressBarValue}
                          alert={showSecondaryButtonAlert}
                        />
                      </>
                    ) : showPrimaryProgressBar && !showSecondaryProgressBar ? (
                      <FloatingButton
                        className="layout-progress-bar"
                        icon={primaryProgressBarIcon}
                        percent={primaryProgressBarValue}
                        alert={showPrimaryButtonAlert}
                        onClick={onOpenUploadPanel}
                      />
                    ) : !showPrimaryProgressBar && showSecondaryProgressBar ? (
                      <FloatingButton
                        className="layout-progress-bar"
                        icon={secondaryProgressBarIcon}
                        percent={secondaryProgressBarValue}
                        alert={showSecondaryButtonAlert}
                      />
                    ) : (
                      <></>
                    )}

                    {isArticleAvailable && (
                      <SectionToggler
                        visible={!isArticleVisible}
                        onClick={this.showArticle}
                      />
                    )}
                  </Section>
                </Provider>
              )}
            </ReactResizeDetector>
          )}
        </>
      );
    };

    const scrollOptions = this.scroll
      ? {
          container: this.scroll,
          throttleTime: 0,
          threshold: 100,
        }
      : {};

    return (
      <>
        {renderPageLayout()}
        {!isMobile && uploadFiles && !dragging && (
          <StyledSelectoWrapper>
            <Selecto
              boundContainer={".section-wrapper"}
              dragContainer={".section-body"}
              selectableTargets={[".files-item"]}
              hitRate={0}
              selectByClick={false}
              selectFromInside={true}
              ratio={0}
              continueSelect={false}
              onSelect={this.onSelect}
              dragCondition={this.dragCondition}
              scrollOptions={scrollOptions}
              onScroll={this.onScroll}
            />
          </StyledSelectoWrapper>
        )}
      </>
    );
  }
}

PageLayout.propTypes = {
  children: PropTypes.any,
  withBodyScroll: PropTypes.bool,
  withBodyAutoFocus: PropTypes.bool,
  showPrimaryProgressBar: PropTypes.bool,
  primaryProgressBarValue: PropTypes.number,
  showPrimaryButtonAlert: PropTypes.bool,
  progressBarDropDownContent: PropTypes.any,
  primaryProgressBarIcon: PropTypes.string,
  showSecondaryProgressBar: PropTypes.bool,
  secondaryProgressBarValue: PropTypes.number,
  secondaryProgressBarIcon: PropTypes.string,
  showSecondaryButtonAlert: PropTypes.bool,
  onDrop: PropTypes.func,
  setSelections: PropTypes.func,
  uploadFiles: PropTypes.bool,
  hideAside: PropTypes.bool,
  viewAs: PropTypes.string,
  uploadPanelVisible: PropTypes.bool,
  onOpenUploadPanel: PropTypes.func,
  isTabletView: PropTypes.bool,
  isHeaderVisible: PropTypes.bool,
  firstLoad: PropTypes.bool,
  isHomepage: PropTypes.bool,
};

PageLayout.defaultProps = {
  withBodyScroll: true,
  withBodyAutoFocus: false,
};

PageLayout.ArticleHeader = ArticleHeader;
PageLayout.ArticleMainButton = ArticleMainButton;
PageLayout.ArticleBody = ArticleBody;
PageLayout.SectionHeader = SectionHeader;
PageLayout.SectionBar = SectionBar;
PageLayout.SectionFilter = SectionFilter;
PageLayout.SectionBody = SectionBody;
PageLayout.SectionPaging = SectionPaging;

export default inject(({ auth }) => {
  const { isLoaded, settingsStore } = auth;
  const {
    isHeaderVisible,
    isTabletView,
    isArticlePinned,
    isArticleVisible,
    isBackdropVisible,
    setArticlePinned,
    setArticleVisibleOnUnpin,
    setIsArticleVisible,
    setIsBackdropVisible,
    isDesktopClient,
    maintenanceExist,
  } = settingsStore;

  return {
    isLoaded,
    isTabletView,
    isHeaderVisible,
    isArticlePinned,
    isArticleVisible,
    setArticlePinned,
    setArticleVisibleOnUnpin,
    setIsArticleVisible,
    isBackdropVisible,
    setIsBackdropVisible,
    maintenanceExist,
    isDesktop: isDesktopClient,
  };
})(observer(PageLayout));
