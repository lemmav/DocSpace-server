import React from "react";
import PropTypes from "prop-types";
import styled from "styled-components";
import { tablet } from "@appserver/components/utils/device";
import CatalogButtonIcon from "../../../../../public/images/catalog.button.react.svg";
import Base from "@appserver/components/themes/base";

const StyledSectionToggler = styled.div`
  height: 64px;
  position: fixed;
  bottom: 0;
  right: 16px;
  display: none;
  -webkit-tap-highlight-color: rgba(0, 0, 0, 0);

  @media ${tablet} {
    display: ${(props) => (props.visible ? "block" : "none")};
  }

  div {
    width: 48px;
    height: 48px;
    padding: 14px 12px 14px 16px;
    box-shadow: ${(props) => props.theme.section.toggler.boxShadow};
    border-radius: 48px;
    cursor: pointer;
    background: ${(props) => props.theme.section.toggler.background};
    box-sizing: border-box;
    line-height: 14px;
    svg {
      path {
        fill: ${(props) => props.theme.section.toggler.fill};
      }
    }
  }
`;

StyledSectionToggler.defaultProps = { theme: Base };

const iconStyle = {
  width: "20px",
  height: "20px",
  minWidth: "20px",
  minHeight: "20px",
};

const SectionToggler = React.memo((props) => {
  //console.log("PageLayout SectionToggler render");
  const { visible, onClick } = props;

  return (
    <StyledSectionToggler className="not-selectable" visible={visible}>
      <div onClick={onClick}>
        <CatalogButtonIcon style={iconStyle} />
      </div>
    </StyledSectionToggler>
  );
});

SectionToggler.displayName = "SectionToggler";

SectionToggler.propTypes = {
  visible: PropTypes.bool,
  onClick: PropTypes.func,
};

export default SectionToggler;
