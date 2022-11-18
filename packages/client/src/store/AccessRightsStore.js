import { makeAutoObservable } from "mobx";

import {
  EmployeeActivationStatus,
  EmployeeStatus,
  FolderType,
  ShareAccessRights,
} from "@docspace/common/constants";
import {
  getFileRoleActions,
  getRoomRoleActions,
} from "@docspace/common/utils/actions";

class AccessRightsStore {
  authStore = null;
  selectedFolderStore = null;
  treeFoldersStore = null;

  constructor(authStore, selectedFolderStore) {
    this.authStore = authStore;
    this.selectedFolderStore = selectedFolderStore;

    makeAutoObservable(this);
  }

  canInviteUserInRoom(room) {
    const { access, rootFolderType } = room;

    if (rootFolderType === FolderType.Archive) return false;

    return getRoomRoleActions(access).inviteUsers;
  }

  canChangeUserRole = (room) => {
    const { access, rootFolderType, currentUserInList } = room;
    const { userStore } = this.authStore;
    const { user } = userStore;

    const isMyProfile = user.id === currentUserInList.id;
    const isOwnerRoleRoom =
      currentUserInList.access === ShareAccessRights.FullAccess;

    if (rootFolderType === FolderType.Archive || isMyProfile || isOwnerRoleRoom)
      return false;

    return getRoomRoleActions(access).changeUserRole;
  };

  canLockFile = (file) => {
    const { rootFolderType, access } = file;

    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH
    )
      return false;

    return getFileRoleActions(access).block;
  };

  canChangeVersionHistory = (file) => {
    const { rootFolderType, editing, providerKey, access } = file;

    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH ||
      rootFolderType === FolderType.Privacy ||
      editing ||
      providerKey
    )
      return false;

    return getFileRoleActions(access).changeVersionHistory;
  };
  canViewVersionHistory = (file) => {
    const { rootFolderType, access, providerKey } = file;

    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH ||
      rootFolderType === FolderType.Privacy ||
      providerKey
    )
      return false;

    return getFileRoleActions(access).viewVersionHistory;
  };

  canEditFile = (file) => {
    const { rootFolderType, access } = file;

    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH ||
      rootFolderType === FolderType.Privacy
    )
      return false;

    return getFileRoleActions(access).edit;
  };

  canRename = (item = {}) => {
    const { rootFolderType, access, isFile } = item;
    const { isDesktopClient } = this.authStore.settingsStore;
    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH ||
      (!isFile && rootFolderType === FolderType.Privacy && !isDesktopClient)
    )
      return false;

    return getFileRoleActions(access).rename;
  };
  canFillForm = (file) => {
    const { rootFolderType, access } = file;

    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH
    )
      return false;

    return getFileRoleActions(access).fillForm;
  };

  generalCopyProhibitionConditions = (rootFolderType, fileEditing) =>
    rootFolderType === FolderType.TRASH ||
    rootFolderType === FolderType.Favorites ||
    rootFolderType === FolderType.Recent ||
    rootFolderType === FolderType.Privacy ||
    fileEditing;
  canCopy = (item) => {
    const { rootFolderType, access } = item;

    if (this.generalCopyProhibitionConditions(rootFolderType)) return false;

    return getFileRoleActions(access).canCopy;
  };

  canCreateCopy = (item) => {
    const { rootFolderType, access } = item;

    if (
      rootFolderType === FolderType.Archive ||
      this.generalCopyProhibitionConditions(rootFolderType)
    )
      return false;

    return getFileRoleActions(access).canCopy;
  };
  generalDeleteProhibitionConditions = (rootFolderType, fileEditing) =>
    rootFolderType === FolderType.Archive ||
    rootFolderType === FolderType.TRASH ||
    rootFolderType === FolderType.Favorites ||
    rootFolderType === FolderType.Recent ||
    rootFolderType === FolderType.Privacy ||
    fileEditing;
  canDeleteItsItems = (item) => {
    const { rootFolderType, access, editing: fileEditing } = item;

    if (this.generalDeleteProhibitionConditions(rootFolderType, fileEditing))
      return false;

    return getFileRoleActions(access).deleteSelf;
  };

  canDeleteAlienItems = (item) => {
    const { rootFolderType, access, editing: fileEditing } = item;

    if (this.generalDeleteProhibitionConditions(rootFolderType, fileEditing))
      return false;

    return getFileRoleActions(access).deleteAlien;
  };

  generalMoveProhibitionConditions = (rootFolderType, fileEditing) =>
    rootFolderType === FolderType.Archive ||
    rootFolderType === FolderType.TRASH ||
    rootFolderType === FolderType.Favorites ||
    rootFolderType === FolderType.Recent ||
    rootFolderType === FolderType.Privacy ||
    fileEditing;

  canMoveItsItems = (item) => {
    const { rootFolderType, access, editing: fileEditing } = item;

    if (this.generalMoveProhibitionConditions(rootFolderType, fileEditing))
      return false;

    return getFileRoleActions(access).moveSelf;
  };

  canMoveAlienItems = (item) => {
    const {
      rootFolderType,
      access,

      editing: fileEditing,
    } = item;

    if (this.generalMoveProhibitionConditions(rootFolderType, fileEditing))
      return false;

    return getFileRoleActions(access).moveAlien;
  };

  canMakeForm = (item) => {
    const { rootFolderType, access } = item;

    if (
      rootFolderType === FolderType.Archive ||
      rootFolderType === FolderType.TRASH ||
      rootFolderType === FolderType.Privacy ||
      rootFolderType === FolderType.Favorites ||
      rootFolderType === FolderType.Recent
    )
      return false;

    return getFileRoleActions(access).saveAsForm;
  };

  canArchiveRoom = (room) => {
    const { archive } = getRoomRoleActions(room.access);

    return archive;
  };

  canRemoveRoom = (room) => {
    const { access, rootFolderType } = room;
    const { delete: remove } = getRoomRoleActions(access);

    if (rootFolderType !== FolderType.Archive) return false;
    return remove;
  };

  canPinRoom = (room) => {
    const { access, rootFolderType } = room;

    if (rootFolderType === FolderType.Archive) return false;

    return getRoomRoleActions(access).canPin;
  };

  canEditRoom = (room) => {
    const { access, rootFolderType } = room;

    if (rootFolderType === FolderType.Archive) return false;

    return getRoomRoleActions(access).edit;
  };

  get canCreateFiles() {
    const { access, rootFolderType } = this.selectedFolderStore;

    if (rootFolderType === FolderType.Archive) return false;

    const { create } = getFileRoleActions(access);

    return create;
  }

  canMoveFile = (room) => {
    const { rootFolderType } = room;

    if (rootFolderType === FolderType.Archive) return false;

    const { moveSelf, moveAlien } = getFileRoleActions(room.access);

    return moveSelf || moveAlien;
  };

  canDeleteFile = (room) => {
    const { rootFolderType } = room;

    if (rootFolderType === FolderType.Archive) return false;

    const { deleteSelf, deleteAlien } = getFileRoleActions(room.access);

    return deleteSelf || deleteAlien;
  };

  canCopyFile = (room) => {
    const { rootFolderType } = room;

    if (rootFolderType === FolderType.Archive) return false;

    const { copyFromPersonal } = getFileRoleActions(room.access);

    return copyFromPersonal;
  };

  canChangeUserType = (user) => {
    const { id, isOwner } = this.authStore.userStore.user;

    const { id: userId, statusType, role } = user;

    if (userId === id || statusType === EmployeeStatus.Disabled) return false;

    switch (role) {
      case "owner":
        return false;

      case "admin":
      case "manager":
        if (isOwner) {
          return true;
        } else {
          return false;
        }

      case "user":
        return true;

      default:
        return false;
    }
  };

  canMakeEmployeeUser = (user) => {
    const { id, isOwner } = this.authStore.userStore.user;

    const {
      status,
      id: userId,
      isAdmin: userIsAdmin,
      isOwner: userIsOwner,
      isVisitor: userIsVisitor,
    } = user;

    const needMakeEmployee =
      status !== EmployeeStatus.Disabled && userId !== id;

    if (isOwner) return needMakeEmployee;

    return needMakeEmployee && !userIsAdmin && !userIsOwner && userIsVisitor;
  };

  canActivateUser = (user) => {
    const { id, isOwner, isAdmin } = this.authStore.userStore.user;

    const {
      status,
      id: userId,
      isAdmin: userIsAdmin,
      isOwner: userIsOwner,
    } = user;

    const needActivate = status !== EmployeeStatus.Active && userId !== id;

    if (isOwner) return needActivate;

    if (isAdmin) return needActivate && !userIsAdmin && !userIsOwner;

    return false;
  };

  canDisableUser = (user) => {
    const { id, isOwner, isAdmin } = this.authStore.userStore.user;

    const {
      status,
      id: userId,
      isAdmin: userIsAdmin,
      isOwner: userIsOwner,
    } = user;

    const needDisable = status !== EmployeeStatus.Disabled && userId !== id;

    if (isOwner) return needDisable;

    if (isAdmin) return needDisable && !userIsAdmin && !userIsOwner;

    return false;
  };

  canInviteUser = (user) => {
    const { id, isOwner } = this.authStore.userStore.user;

    const {
      activationStatus,
      status,
      id: userId,
      isAdmin: userIsAdmin,
      isOwner: userIsOwner,
    } = user;

    const needInvite =
      activationStatus === EmployeeActivationStatus.Pending &&
      status === EmployeeStatus.Active &&
      userId !== id;

    if (isOwner) return needInvite;

    return needInvite && !userIsAdmin && !userIsOwner;
  };

  canRemoveUser = (user) => {
    const { id, isOwner, isAdmin } = this.authStore.userStore.user;

    const {
      status,
      id: userId,
      isAdmin: userIsAdmin,
      isOwner: userIsOwner,
    } = user;

    const needRemove = status === EmployeeStatus.Disabled && userId !== id;

    if (isOwner) return needRemove;

    if (isAdmin) return needRemove && !userIsAdmin && !userIsOwner;

    return false;
  };
}

export default AccessRightsStore;
