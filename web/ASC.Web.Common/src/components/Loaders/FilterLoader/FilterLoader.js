import React from "react";
import styled from "styled-components";
import RectangleLoader from "../RectangleLoader/index";
import PropTypes from "prop-types";
import { utils } from "asc-web-components";
const { mobile } = utils.device;

const StyledFilter = styled.div`
  width: 100%;
  display: grid;
  grid-template-columns: 1fr 95px;
  grid-template-rows: 1fr;
  grid-column-gap: 8px;

  @media ${mobile} {
    grid-template-columns: 1fr 50px;
  }
`;

const FilterLoader = (props) => {
  return (
    <StyledFilter>
      <RectangleLoader width={props.width} height={props.height} {...props} />
      <RectangleLoader width={props.width} height={props.height} {...props} />
    </StyledFilter>
  );
};

FilterLoader.propTypes = {
  width: PropTypes.string,
  height: PropTypes.string,
};

FilterLoader.defaultProps = {
  width: "100%",
  height: "32px",
};

export default FilterLoader;
