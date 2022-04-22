import styled, { css } from "styled-components";
import { isMobile, isMobileOnly } from "react-device-detect";
import { tablet, desktop, mobile } from "@appserver/components/utils/device";

const StyledContainer = styled.div`
  width: 100% !important;
  display: grid;
  align-items: center;
  grid-template-columns: ${(props) =>
    props.isRootFolder ? "auto 1fr" : "29px auto 1fr"};

  padding: ${(props) => (props.isDropBox ? "10px 0 5px" : "10px 0 11px")};

  .arrow-button {
    width: 17px;
    min-width: 17px;
  }

  @media ${tablet} {
    width: 100%;
    grid-template-columns: ${(props) =>
      props.isRootFolder ? "auto 1fr" : "29px 1fr auto"};
    padding: ${(props) => (props.isDropBox ? "14px 0 5px" : "14px 0 15px")};
  }
  ${isMobile &&
  css`
    width: 100%;
    grid-template-columns: ${(props) =>
      props.isRootFolder ? "auto 1fr" : "29px 1fr auto"};
    padding: ${(props) =>
      props.isDropBox ? "12px 0 5px" : " 12px 0 13px"} !important;
  `}

  @media ${mobile} {
    padding: ${(props) =>
      props.isDropBox ? "10px 0 5px" : "10px 0 11px"} !important;
  }

  ${isMobileOnly &&
  css`
    width: 100% !important;
    padding: ${(props) =>
      props.isDropBox ? "10px 0 5px" : "10px 0 11px"} !important;
  `}
`;

export default StyledContainer;
