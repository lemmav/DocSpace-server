import React, { memo } from "react";
import { withRouter } from "react-router";
import PropTypes from "prop-types";

import ModalDialog from "@docspace/components/modal-dialog";
import Button from "@docspace/components/button";
import Text from "@docspace/components/text";

import { withTranslation } from "react-i18next";
import toastr from "@docspace/components/toast/toastr";
import { EmployeeStatus } from "@docspace/common/constants";
import ModalDialogContainer from "../ModalDialogContainer";
import { inject, observer } from "mobx-react";

class ChangeUserStatusDialogComponent extends React.Component {
  constructor(props) {
    super(props);

    this.state = { isRequestRunning: false };
  }

  onChangeUserStatus = () => {
    const {
      updateUserStatus,
      status,
      t,
      setSelected,
      onClose,
      userIDs,
      getPeopleListItem,
      setSelection,
      infoPanelVisible,
    } = this.props;

    let usersCount = 0;

    this.setState({ isRequestRunning: true }, () => {
      updateUserStatus(status, userIDs)
        .then((users) => {
          if (users.length === 1 && infoPanelVisible) {
            const user = getPeopleListItem(users[0]);

            setSelection(user);
          }

          usersCount = users.length;
        })
        .catch((error) => toastr.error(error))
        .finally((users) => {
          this.setState({ isRequestRunning: false }, () => {
            toastr.success(t("PeopleTranslations:SuccessChangeUserStatus"));
            (!infoPanelVisible || usersCount !== 1) && setSelected("close");
            onClose();
          });
        });
    });
  };

  render() {
    const { t, tReady, onClose, visible, status, userIDs } = this.props;
    const { isRequestRunning } = this.state;

    const statusTranslation =
      status === EmployeeStatus.Active
        ? t("ChangeUsersActiveStatus")
        : t("ChangeUsersDisableStatus");

    const userStatusTranslation =
      status === EmployeeStatus.Active
        ? t("PeopleTranslations:DisabledEmployeeStatus")
        : t("Common:Active");

    return (
      <ModalDialogContainer
        isLoading={!tReady}
        visible={visible}
        onClose={onClose}
        autoMaxHeight
      >
        <ModalDialog.Header>
          {t("ChangeUserStatusDialogHeader")}
        </ModalDialog.Header>
        <ModalDialog.Body>
          <Text>
            {t("ChangeUserStatusDialog", {
              status: statusTranslation,
              userStatus: userStatusTranslation,
            })}
          </Text>
          <Text>{t("ChangeUserStatusDialogMessage")}</Text>
        </ModalDialog.Body>
        <ModalDialog.Footer>
          <Button
            label={t("ChangeUsersStatusButton")}
            size="normal"
            primary
            scale
            onClick={this.onChangeUserStatus}
            isLoading={isRequestRunning}
            isDisabled={userIDs.length === 0}
          />
          <Button
            label={t("Common:CancelButton")}
            size="normal"
            scale
            onClick={onClose}
            isDisabled={isRequestRunning}
          />
        </ModalDialog.Footer>
      </ModalDialogContainer>
    );
  }
}

const ChangeUserStatusDialog = withTranslation([
  "ChangeUserStatusDialog",
  "Common",
  "PeopleTranslations",
])(ChangeUserStatusDialogComponent);

ChangeUserStatusDialog.propTypes = {
  visible: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  setSelected: PropTypes.func.isRequired,
  userIDs: PropTypes.arrayOf(PropTypes.string).isRequired,
};

export default withRouter(
  inject(({ peopleStore, auth }) => {
    const setSelected = peopleStore.selectionStore.setSelected;

    const { getPeopleListItem, updateUserStatus } = peopleStore.usersStore;

    const { setSelection, isVisible: infoPanelVisible } = auth.infoPanelStore;

    return {
      updateUserStatus,

      setSelected,

      getPeopleListItem,

      setSelection,
      infoPanelVisible,
    };
  })(observer(ChangeUserStatusDialog))
);
