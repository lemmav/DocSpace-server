import React from 'react';
import PropTypes from 'prop-types';
import Backdrop from '@appserver/components/backdrop';
import { isTablet, isDesktop, size } from '@appserver/components/utils/device';
import { Provider } from '@appserver/components/utils/context';
import { isMobile } from 'react-device-detect';
import Article from './sub-components/article';
import SubArticleHeader from './sub-components/article-header';
import SubArticleMainButton from './sub-components/article-main-button';
import SubArticleBody from './sub-components/article-body';
import ArticlePinPanel from './sub-components/article-pin-panel';
import Section from './sub-components/section';
import SubSectionHeader from './sub-components/section-header';
import SubSectionFilter from './sub-components/section-filter';
import SubSectionBody from './sub-components/section-body';
import SubSectionBodyContent from './sub-components/section-body-content';
import SubSectionPaging from './sub-components/section-paging';
import SectionToggler from './sub-components/section-toggler';
import ReactResizeDetector from 'react-resize-detector';
import FloatingButton from '../FloatingButton';
import { inject, observer } from 'mobx-react';
import Selecto from 'react-selecto';
import styled from 'styled-components';
import Catalog from './sub-components/catalog';
import SubCatalogBackdrop from './sub-components/catalog-backdrop';
import SubCatalogHeader from './sub-components/catalog-header';
import SubCatalogMainButton from './sub-components/catalog-main-button';
import SubCatalogBody from './sub-components/catalog-body';

const StyledSelectoWrapper = styled.div`
  .selecto-selection {
    z-index: 200;
  }
`;

function CatalogHeader() {
  return null;
}
CatalogHeader.displayName = 'CatalogHeader';

function CatalogMainButton() {
  return null;
}
CatalogMainButton.displayName = 'CatalogMainButton';

function CatalogBody() {
  return null;
}
CatalogBody.displayName = 'CatalogBody';

function ArticleHeader() {
  return null;
}
ArticleHeader.displayName = 'ArticleHeader';

function ArticleMainButton() {
  return null;
}
ArticleMainButton.displayName = 'ArticleMainButton';

function ArticleBody() {
  return null;
}
ArticleBody.displayName = 'ArticleBody';

function SectionHeader() {
  return null;
}
SectionHeader.displayName = 'SectionHeader';

function SectionFilter() {
  return null;
}
SectionFilter.displayName = 'SectionFilter';

function SectionBody() {
  return null;
}
SectionBody.displayName = 'SectionBody';

function SectionPaging() {
  return null;
}
SectionPaging.displayName = 'SectionPaging';

class PageLayout extends React.Component {
  static CatalogHeader = CatalogHeader;
  static CatalogMainButton = CatalogMainButton;
  static CatalogBody = CatalogBody;
  static ArticleHeader = ArticleHeader;
  static ArticleMainButton = ArticleMainButton;
  static ArticleBody = ArticleBody;
  static SectionHeader = SectionHeader;
  static SectionFilter = SectionFilter;
  static SectionBody = SectionBody;
  static SectionPaging = SectionPaging;

  constructor(props) {
    super(props);

    this.timeoutHandler = null;
    this.intervalHandler = null;

    this.scroll = null;
  }

  componentDidUpdate(prevProps) {
    if (!this.scroll) {
      this.scroll = document.getElementsByClassName('section-scroll')[0];
    }

    if (
      this.props.hideAside &&
      !this.props.isArticlePinned &&
      this.props.hideAside !== prevProps.hideAside
    ) {
      this.backdropClick();
    }

    if (isDesktop()) return this.props.setShowText(true);
    if (isTablet() && !this.props.userShowText) return this.props.setShowText(false);
    if (this.props.showText && isTablet() && !this.props.userShowText)
      return this.props.setShowText(false);
    if (this.props.showText && isMobile && !this.props.userShowText)
      return this.props.setShowText(false);
    if (!isTablet() && !isMobile && !this.props.showText && !this.props.userShowText)
      return this.props.setShowText(true);
  }

  componentDidMount() {
    window.addEventListener('orientationchange', this.orientationChangeHandler);

    this.orientationChangeHandler();
  }

  componentWillUnmount() {
    window.removeEventListener('orientationchange', this.orientationChangeHandler);

    if (this.intervalHandler) clearInterval(this.intervalHandler);
    if (this.timeoutHandler) clearTimeout(this.timeoutHandler);
  }

  orientationChangeHandler = () => {
    const isValueExist = !!this.props.isArticlePinned;
    const isEnoughWidth = screen.availWidth > size.smallTablet;

    if (!isEnoughWidth && isValueExist) {
      this.backdropClick();
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
    const isBackdrop = path.some((x) => x.classList && x.classList.contains('backdrop-active'));
    const notSelectablePath = path.some(
      (x) => x.classList && x.classList.contains('not-selectable'),
    );

    const isDraggable = path.some((x) => x.classList && x.classList.contains('draggable'));

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
      showText,
      toggleShowText,
      showCatalog,
    } = this.props;
    let catalogHeaderContent = null;
    let catalogMainButtonContent = null;
    let catalogBodyContent = null;
    let articleHeaderContent = null;
    let articleMainButtonContent = null;
    let articleBodyContent = null;
    let sectionHeaderContent = null;
    let sectionFilterContent = null;
    let sectionPagingContent = null;
    let sectionBodyContent = null;
    React.Children.forEach(children, (child) => {
      const childType = child && child.type && (child.type.displayName || child.type.name);

      switch (childType) {
        case CatalogHeader.displayName:
          catalogHeaderContent = child;
          break;
        case CatalogMainButton.displayName:
          catalogMainButtonContent = child;
          break;
        case CatalogBody.displayName:
          catalogBodyContent = child;
          break;
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
        isArticleHeaderAvailable || isArticleMainButtonAvailable || isArticleBodyAvailable,
      isSectionHeaderAvailable = !!sectionHeaderContent,
      isSectionFilterAvailable = !!sectionFilterContent,
      isSectionPagingAvailable = !!sectionPagingContent,
      isSectionBodyAvailable =
        !!sectionBodyContent || isSectionFilterAvailable || isSectionPagingAvailable,
      isSectionAvailable =
        isSectionHeaderAvailable ||
        isSectionFilterAvailable ||
        isSectionBodyAvailable ||
        isSectionPagingAvailable ||
        isArticleAvailable,
      isBackdropAvailable = isArticleAvailable,
      isCatalogHeaderAvailable = !!catalogHeaderContent,
      isCatalogMainButtonAvailable = !!catalogMainButtonContent,
      isCatalogBodyAvailable = !!catalogBodyContent,
      isCatalogAvailable =
        isCatalogHeaderAvailable || isCatalogMainButtonAvailable || isCatalogBodyAvailable;

    const renderPageLayout = () => {
      return (
        <>
          {showCatalog && isCatalogAvailable && (
            <Catalog showText={showText}>
              {isCatalogHeaderAvailable && (
                <>
                  <SubCatalogBackdrop showText={showText} onClick={toggleShowText} />
                  <SubCatalogHeader showText={showText} onClick={toggleShowText}>
                    {catalogHeaderContent ? catalogHeaderContent.props.children : null}
                  </SubCatalogHeader>
                </>
              )}

              {isCatalogMainButtonAvailable && (
                <SubCatalogMainButton showText={showText}>
                  {catalogMainButtonContent ? catalogMainButtonContent.props.children : null}
                </SubCatalogMainButton>
              )}

              {isCatalogBodyAvailable && (
                <SubCatalogBody showText={showText}>
                  {catalogBodyContent ? catalogBodyContent.props.children : null}
                </SubCatalogBody>
              )}
            </Catalog>
          )}
          {!showCatalog && isBackdropAvailable && (
            <Backdrop zIndex={400} visible={isBackdropVisible} onClick={this.backdropClick} />
          )}
          {!showCatalog && isArticleAvailable && (
            <Article visible={isArticleVisible} pinned={isArticlePinned} firstLoad={firstLoad}>
              {isArticleHeaderAvailable && (
                <SubArticleHeader>
                  {articleHeaderContent ? articleHeaderContent.props.children : null}
                </SubArticleHeader>
              )}
              {isArticleMainButtonAvailable && (
                <SubArticleMainButton>
                  {articleMainButtonContent ? articleMainButtonContent.props.children : null}
                </SubArticleMainButton>
              )}
              {isArticleBodyAvailable && (
                <SubArticleBody pinned={isArticlePinned} isDesktop={isDesktop}>
                  {articleBodyContent ? articleBodyContent.props.children : null}
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
              refreshOptions={{ trailing: true }}>
              {({ width, height }) => (
                <Provider
                  value={{
                    sectionWidth: width,
                    sectionHeight: height,
                  }}>
                  <Section
                    showText={showText}
                    widthProp={width}
                    unpinArticle={this.unpinArticle}
                    pinned={isArticlePinned}>
                    {isSectionHeaderAvailable && (
                      <SubSectionHeader
                        isHeaderVisible={isHeaderVisible}
                        isArticlePinned={isArticlePinned}>
                        {sectionHeaderContent ? sectionHeaderContent.props.children : null}
                      </SubSectionHeader>
                    )}

                    {isSectionFilterAvailable && (
                      <>
                        <div
                          id="main-bar"
                          style={{
                            display: 'grid',
                            paddingRight: '20px',
                          }}></div>
                        <SubSectionFilter className="section-header_filter">
                          {sectionFilterContent ? sectionFilterContent.props.children : null}
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
                          viewAs={viewAs}>
                          {isSectionFilterAvailable && (
                            <SubSectionFilter className="section-body_filter">
                              {sectionFilterContent ? sectionFilterContent.props.children : null}
                            </SubSectionFilter>
                          )}
                          <SubSectionBodyContent>
                            {sectionBodyContent ? sectionBodyContent.props.children : null}
                          </SubSectionBodyContent>
                          {isSectionPagingAvailable && (
                            <SubSectionPaging>
                              {sectionPagingContent ? sectionPagingContent.props.children : null}
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

                    {!showCatalog && isArticleAvailable && (
                      <SectionToggler visible={!isArticleVisible} onClick={this.showArticle} />
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
              boundContainer={'.section-body'}
              dragContainer={'.section-body'}
              selectableTargets={['.files-item']}
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
  showText: PropTypes.bool,
  userShowText: PropTypes.bool,
  setShowText: PropTypes.func,
  toggleShowText: PropTypes.func,
  showCatalog: PropTypes.bool,
};

PageLayout.defaultProps = {
  withBodyScroll: true,
  withBodyAutoFocus: false,
};

PageLayout.CatalogHeader = CatalogHeader;
PageLayout.CatalogMainButton = CatalogMainButton;
PageLayout.ArticleHeader = ArticleHeader;
PageLayout.ArticleMainButton = ArticleMainButton;
PageLayout.ArticleBody = ArticleBody;
PageLayout.SectionHeader = SectionHeader;
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
    showText,
    userShowText,
    setShowText,
    toggleShowText,
    showCatalog,
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
    isDesktop: isDesktopClient,
    showText,
    userShowText,
    setShowText,
    toggleShowText,
    showCatalog,
  };
})(observer(PageLayout));
