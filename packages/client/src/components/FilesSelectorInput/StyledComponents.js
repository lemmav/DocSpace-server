import styled, { css } from "styled-components";

const StyledBodyWrapper = styled.div`
  max-width: ${(props) => (props.maxWidth ? props.maxWidth : "350px")};
`;

export { StyledBodyWrapper };
