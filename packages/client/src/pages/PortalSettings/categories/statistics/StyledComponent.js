import styled from "styled-components";
import Row from "@docspace/components/row";

const StyledBaseQuotaComponent = styled.div`
  .quotas_label {
    margin-bottom: 20px;
    p:first-child {
      margin-bottom: 8px;
    }
  }
  .toggle-container {
    margin-bottom: 32px;
    max-width: 700px;
    .quotas_toggle-button {
      position: static;
    }
    .toggle_label {
      margin-top: 10px;
      margin-bottom: 16px;
    }
  }
`;

const StyledDiscSpaceUsedComponent = styled.div`
  margin-top: 16px;
  .disk-space_title {
    margin-bottom: 16px;
  }
  .button-container {
    display: flex;
    gap: 16px;
    margin-top: 16px;
    .text-container {
      display: grid;
    }
  }
`;

const StyledDiagramComponent = styled.div`
  .diagram_slider,
  .diagram_description {
    margin-top: 16px;
  }
  .diagram_slider {
    width: 100%;
    max-width: ${(props) => props.maxWidth}px;
    display: flex;
    background: #f3f4f4;
    border-radius: 29px;
  }
  .diagram_description {
    display: flex;

    flex-wrap: wrap;

    .diagram_folder-tag {
      display: flex;
      margin-right: 24px;
      padding-bottom: 8px;
    }
  }
`;

const StyledFolderTagSection = styled.div`
  height: 12px;
  border-right: 1px solid #f3f4f4;
  background: ${(props) => props.color};
  width: ${(props) => props.width + "%"};

  &:first-of-type {
    border-radius: 46px 0px 0px 46px;
  }
`;

const StyledFolderTagColor = styled.div`
  margin: auto 0;

  width: 12px;
  height: 12px;
  background: ${(props) => props.color};
  border-radius: 50%;
  margin-right: 4px;
`;

const StyledStatistics = styled.div`
  max-width: 700px;

  .statistics-description {
    margin-bottom: 20px;
  }
  .statistics-container {
    margin-bottom: 40px;
  }
  .item-statistic {
    margin-bottom: 4px;
  }
  .statistics_title {
    margin-bottom: 8px;
  }

  .button-element {
    margin-top: 20px;
  }
`;

const StyledDivider = styled.div`
  height: 1px;
  width: 100%;
  background-color: #ddd;
  margin: 28px 0 28px 0;
`;

const StyledSimpleFilesRow = styled(Row)`
  .row_content {
    gap: 12px;
    align-items: center;
    height: 56px;
    .row_name {
      width: 100%;
      overflow: hidden;

      p {
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
      }
    }

    .user-icon {
      .react-svg-icon {
        height: 32px;
        border-radius: 50%;
      }
    }
  }
`;
export {
  StyledBaseQuotaComponent,
  StyledDiscSpaceUsedComponent,
  StyledFolderTagSection,
  StyledFolderTagColor,
  StyledDiagramComponent,
  StyledStatistics,
  StyledDivider,
  StyledSimpleFilesRow,
};
