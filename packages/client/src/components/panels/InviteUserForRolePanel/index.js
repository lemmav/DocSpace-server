import React, { useState, useEffect } from "react";
import styled, { css } from "styled-components";

import ModalDialog from "@docspace/components/modal-dialog";
import Backdrop from "@docspace/components/backdrop";
import Aside from "@docspace/components/aside";
import Header from "@docspace/components/selector/sub-components/Header";
import Body from "@docspace/components/selector/sub-components/Body";

import Selector from "@docspace/components/selector";
import PeopleSelector from "@docspace/client/src/components/PeopleSelector";
import Link from "@docspace/components/link";
import { AddUserToRoomPanel } from "../index";

import { inject, observer } from "mobx-react";
import { withTranslation } from "react-i18next";

const StyledBlock = styled.div`
  display: flex;
  justify-content: space-between;
  padding: 4px 16px 16px 16px;

  .role {
    font-weight: 700;
    font-size: 16px;
    line-height: 22px;
    color: #657077;
  }
`;

const InviteUserForRolePanel = ({
  visible,
  members,
  currentRole,
  onClose,
  onSelectUserForRole,
  onCloseInviteUserForRolePanel,
  addUserToRoomVisible,
  onOpenAddUserToRoom,
  onCloseAddUserToRoom,
  fetchMembers,
}) => {
  const blockNode = (
    <StyledBlock>
      <div className="role">({currentRole.title})</div>
      <Link
        fontWeight="600"
        type="action"
        isHovered
        onClick={onOpenAddUserToRoom}
      >
        Add user to room
      </Link>
    </StyledBlock>
  );

  return (
    <>
      <Aside
        className="header_aside-panel"
        visible={visible}
        onClose={onClose}
        withoutBodyScroll
        zIndex={410}
        isCloseable={false}
      >
        {!addUserToRoomVisible && (
          // <PeopleSelector
          //   headerLabel="Invite user for role"
          //   onBackClickAction={onBackClickAction}
          //   items={members}
          //   placeholder="Search users"
          //   zIndex={410}
          //   selectByClick={true}
          //   onSelectUserForRole={onSelectUserForRole}
          //   blockNode={blockNode}
          // />

          <Selector
            headerLabel={"Invite user for role"}
            onBackClick={onCloseInviteUserForRolePanel}
            items={members}
            placeholder="Search users"
            zIndex={410}
            selectByClick={true}
            onSelectUserForRole={onSelectUserForRole}
            blockNode={blockNode}
          />
        )}
      </Aside>

      {addUserToRoomVisible && (
        <AddUserToRoomPanel
          visible={addUserToRoomVisible}
          onClose={onCloseAddUserToRoom}
          existUsers={members}
          fetchMembers={fetchMembers}
        />
      )}
    </>
  );
};

export default InviteUserForRolePanel;
