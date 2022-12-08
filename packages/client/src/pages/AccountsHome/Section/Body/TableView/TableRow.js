import React from "react";
import styled from "styled-components";
import { withRouter } from "react-router";
import { withTranslation } from "react-i18next";

import TableRow from "@docspace/components/table-container/TableRow";
import TableCell from "@docspace/components/table-container/TableCell";
import Link from "@docspace/components/link";
import Text from "@docspace/components/text";
import Checkbox from "@docspace/components/checkbox";
import ComboBox from "@docspace/components/combobox";
import DropDownItem from "@docspace/components/drop-down-item";

import withContent from "SRC_DIR/HOCs/withPeopleContent";

import Badges from "../Badges";
import { Base } from "@docspace/components/themes";

const StyledWrapper = styled.div`
  display: contents;
`;

const StyledPeopleRow = styled(TableRow)`
  :hover {
    .table-container_cell {
      cursor: pointer;
      background: ${(props) =>
        `${props.theme.filesSection.tableView.row.backgroundActive} !important`};
      border-top: ${(props) =>
        `1px solid ${props.theme.filesSection.tableView.row.borderColor}`};
      margin-top: -1px;
    }

    .table-container_user-name-cell {
      margin-left: -24px;
      padding-left: 24px;
    }
    .table-container_row-context-menu-wrapper {
      margin-right: -20px;
      padding-right: 18px;
    }
  }

  .table-container_cell {
    height: 48px;
    max-height: 48px;

    background: ${(props) =>
      (props.checked || props.isActive) &&
      `${props.theme.filesSection.tableView.row.backgroundActive} !important`};
  }

  .table-container_row-checkbox-wrapper {
    padding-right: 0px;
    min-width: 48px;

    .table-container_row-checkbox {
      margin-left: -4px;
      padding: 16px 0px 16px 12px;
    }
  }

  .link-with-dropdown-group {
    margin-right: 12px;
  }

  .table-cell_username {
    margin-right: 12px;
  }

  .table-container_row-context-menu-wrapper {
    padding-right: 0px;
  }

  .table-cell_type,
  .table-cell_room {
    margin-left: -8px;
  }

  .type-combobox,
  .room-combobox {
    padding-left: 8px;
    .combo-button {
      padding-left: 8px;
      margin-left: -8px;

      .combo-button-label {
        font-size: 13px;
        font-weight: 600;
      }
    }
  }

  .room-combobox {
    .combo-buttons_arrow-icon {
      display: none;
    }
  }
`;

StyledPeopleRow.defaultProps = { theme: Base };

const fakeRooms = [
  {
    name: "Room 1",
    role: "Viewer",
  },
  {
    name: "Room 2",
    role: "Co-worker",
  },
];

const PeopleTableRow = (props) => {
  const {
    t,
    item,
    contextOptionsProps,
    element,
    checkedProps,
    onContentRowSelect,
    onEmailClick,

    isOwner,
    theme,
    changeUserType,

    setBufferSelection,
    isActive,
    isSeveralSelection,
    canChangeUserType,
  } = props;

  const {
    displayName,
    email,
    statusType,

    position,
    rooms,

    role,
    isVisitor,
  } = item;

  const isPending = statusType === "pending" || statusType === "disabled";

  const nameColor = isPending
    ? theme.peopleTableRow.pendingNameColor
    : theme.peopleTableRow.nameColor;
  const sideInfoColor = theme.peopleTableRow.sideInfoColor;

  const onChange = (e) => {
    onContentRowSelect && onContentRowSelect(e.target.checked, item);
  };

  const getTypesOptions = React.useCallback(() => {
    const options = [];

    const adminOption = {
      key: "admin",
      title: t("Common:DocSpaceAdmin"),
      label: t("Common:DocSpaceAdmin"),
      action: "admin",
    };
    const managerOption = {
      key: "manager",
      title: t("Common:RoomAdmin"),
      label: t("Common:RoomAdmin"),
      action: "manager",
    };
    const userOption = {
      key: "user",
      title: t("Common:User"),
      label: t("Common:User"),
      action: "user",
    };

    isOwner && options.push(adminOption);

    options.push(managerOption);

    isVisitor && options.push(userOption);

    return options;
  }, [t, isOwner, isVisitor]);

  const onTypeChange = React.useCallback(
    ({ action }) => {
      changeUserType(action, [item], t, true);
    },
    [item, changeUserType, t]
  );

  const getRoomsOptions = React.useCallback(() => {
    const options = [];

    fakeRooms.forEach((room) => {
      options.push(
        <DropDownItem key={room.name} noHover={true}>
          {room.name} &nbsp;
          <Text fontSize="13px" fontWeight={600} color={sideInfoColor} truncate>
            ({room.role})
          </Text>
        </DropDownItem>
      );
    });

    return <>{options.map((option) => option)}</>;
  }, []);

  const getUserTypeLabel = React.useCallback((role) => {
    switch (role) {
      case "owner":
        return t("Common:Owner");
      case "admin":
        return t("Common:DocSpaceAdmin");
      case "manager":
        return t("Common:RoomAdmin");
      case "user":
        return t("Common:User");
    }
  }, []);

  const typeLabel = getUserTypeLabel(role);

  const isChecked = checkedProps.checked;

  const userContextClick = React.useCallback(() => {
    if (isSeveralSelection && isChecked) {
      return;
    }

    setBufferSelection(item);
  }, [isSeveralSelection, isChecked, item, setBufferSelection]);

  const renderTypeCell = () => {
    const typesOptions = getTypesOptions();

    const combobox = (
      <ComboBox
        className="type-combobox"
        selectedOption={
          typesOptions.find((option) => option.key === role) || {}
        }
        options={typesOptions}
        onSelect={onTypeChange}
        scaled={false}
        size="content"
        displaySelectedOption
        modernView
        manualWidth={"fit-content"}
      />
    );

    const text = (
      <Text
        type="page"
        title={position}
        fontSize="13px"
        fontWeight={600}
        color={sideInfoColor}
        truncate
        noSelect
        style={{ paddingLeft: "8px" }}
      >
        {typeLabel}
      </Text>
    );

    const canChange = canChangeUserType(item);

    return canChange ? combobox : text;
  };

  const typeCell = renderTypeCell();

  return (
    <StyledWrapper
      className={`user-item ${
        isChecked || isActive ? "table-row-selected" : ""
      }`}
    >
      <StyledPeopleRow
        key={item.id}
        className="table-row"
        sideInfoColor={sideInfoColor}
        checked={isChecked}
        fileContextClick={userContextClick}
        isActive={isActive}
        {...contextOptionsProps}
      >
        <TableCell className={"table-container_user-name-cell"}>
          <TableCell
            hasAccess={true}
            className="table-container_row-checkbox-wrapper"
            checked={isChecked}
          >
            <div className="table-container_element">{element}</div>
            <Checkbox
              className="table-container_row-checkbox"
              onChange={onChange}
              isChecked={isChecked}
            />
          </TableCell>

          <Link
            type="page"
            title={displayName}
            fontWeight="600"
            fontSize="13px"
            color={nameColor}
            isTextOverflow
            className="table-cell_username"
            noHover
          >
            {statusType === "pending"
              ? email
              : displayName?.trim()
              ? displayName
              : email}
          </Link>
          <Badges statusType={statusType} isPaid={!isVisitor} />
        </TableCell>

        <TableCell className={"table-cell_type"}>{typeCell}</TableCell>

        {/* <TableCell className="table-cell_room">
          {!rooms?.length ? (
            <Text
              type="page"
              title={position}
              fontSize="13px"
              fontWeight={400}
              color={sideInfoColor}
              truncate
              noSelect
              style={{ paddingLeft: "8px" }}
            >
              —
            </Text>
          ) : rooms?.length === 1 ? (
            <Text
              type="page"
              title={position}
              fontSize="13px"
              fontWeight={400}
              color={sideInfoColor}
              truncate
              style={{ paddingLeft: "8px" }}
            >
              {rooms[0].name} ({rooms[0].role})
            </Text>
          ) : (
            <ComboBox
              className="room-combobox"
              selectedOption={{ key: "length", label: `${fakeRooms.length}` }}
              options={[]}
              onSelect={onTypeChange}
              advancedOptions={getRoomsOptions()}
              scaled={false}
              size="content"
              displaySelectedOption
              modernView
            />
          )}
        </TableCell> */}

        <TableCell>
          <Link
            type="page"
            title={email}
            fontSize="13px"
            fontWeight={600}
            color={sideInfoColor}
            onClick={onEmailClick}
            isTextOverflow
          >
            {email}
          </Link>
        </TableCell>
      </StyledPeopleRow>
    </StyledWrapper>
  );
};

export default withTranslation(["People", "Common", "Settings"])(
  withRouter(withContent(PeopleTableRow))
);
