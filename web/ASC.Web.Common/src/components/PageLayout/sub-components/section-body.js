import React from "react";
import PropTypes from "prop-types";
import styled from "styled-components";
import { utils, Scrollbar, ProgressBar } from "asc-web-components";
const { tablet } = utils.device;

const StyledSectionBody = styled.div`
  ${props => props.displayBorder && `outline: 1px dotted;`}
  flex-grow: 1;
  height: 100%;
  
  ${props => props.withScroll && `
    margin-left: -24px;
  `}

  .sectionWrapper {
    flex: 1 0 auto;
    padding: 16px 8px 16px 24px;
    outline: none;

    @media ${tablet} {
      padding: 16px 0 16px 24px;
    }
  }
`;

const StyledSectionWrapper = styled.div`
  display: flex;
  flex-direction: column;
  min-height: 100%;

  .layout-progress-bar {
    flex: 0 0 auto;
    margin-right: -16px;
  }
`;

const StyledSpacer = styled.div`
  display: none;
  min-height: 64px;

  @media ${tablet} {
    display: ${props => (props.pinned ? "none" : "block")};
  }
`;

class SectionBody extends React.Component {
  constructor(props) {
    super(props);

    this.focusRef = React.createRef();
    this.ref = React.createRef();
  }
  
  componentDidMount() {
    if (!this.props.autoFocus) return;
    
    this.focusRef.current.focus();
  }

  render() {
    //console.log("PageLayout SectionBody render");
    const { children, withScroll, autoFocus, pinned, showProgressBar, progressBarValue, progressBarDropDownContent, progressBarLabel } = this.props;

    const focusProps = autoFocus ? {
      ref: this.focusRef,
      tabIndex: 1
    } : {};

    const width = this.ref.current && this.ref.current.offsetWidth;

    return (
      <StyledSectionBody ref={this.ref} withScroll={withScroll}>
        {withScroll ? (
          <Scrollbar stype="mediumBlack">
            <StyledSectionWrapper>
              <div className="sectionWrapper" {...focusProps}>
                {children}
                <StyledSpacer pinned={pinned}/>
              </div>
              {showProgressBar && (
                <ProgressBar
                  className="layout-progress-bar"
                  label={progressBarLabel}
                  percent={progressBarValue}
                  width={width}
                  dropDownContent={progressBarDropDownContent}
                />
              )}
            </StyledSectionWrapper>
          </Scrollbar>
        ) : (
          <StyledSectionWrapper>
            {children}
            <StyledSpacer pinned={pinned}/>
          </StyledSectionWrapper>
        )}
      </StyledSectionBody>
    );
  }
}

SectionBody.displayName = "SectionBody";

SectionBody.propTypes = {
  withScroll: PropTypes.bool,
  autoFocus: PropTypes.bool,
  pinned: PropTypes.bool,
  showProgressBar: PropTypes.bool,
  progressBarValue: PropTypes.number,
  progressBarLabel: PropTypes.string,
  progressBarDropDownContent: PropTypes.any,
  children: PropTypes.oneOfType([
    PropTypes.arrayOf(PropTypes.node),
    PropTypes.node,
    PropTypes.any
  ])
};

SectionBody.defaultProps = {
  withScroll: true,
  autoFocus: false,
  pinned: false
};

export default SectionBody;
