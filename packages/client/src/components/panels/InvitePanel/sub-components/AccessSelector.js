import React, { useEffect, useState } from "react";
import AccessRightSelect from "@docspace/components/access-right-select";
import { getAccessOptions } from "../utils";
import { isMobileOnly } from "react-device-detect";

import { StyledAccessSelector } from "../StyledInvitePanel";
import { isSmallTablet } from "@docspace/components/utils/device";
const AccessSelector = ({
  t,
  roomType,
  onSelectAccess,
  containerRef,
  defaultAccess,
  isOwner,
  withRemove = false,
  filteredAccesses,
  setIsOpenItemAccess,
}) => {
  const [horizontalOrientation, setHorizontalOrientation] = useState(false);
  const width = containerRef?.current?.offsetWidth - 32;

  const accessOptions = getAccessOptions(
    t,
    roomType,
    withRemove,
    true,
    isOwner
  );

  const selectedOption = accessOptions.filter(
    (access) => access.access === +defaultAccess
  )[0];

  useEffect(() => {
    checkWidth();
    window.addEventListener("resize", checkWidth);
    return () => window.removeEventListener("resize", checkWidth);
  }, []);

  const checkWidth = () => {
    if (!isMobileOnly) return;

    if (!isSmallTablet()) {
      setHorizontalOrientation(true);
    } else {
      setHorizontalOrientation(false);
    }
  };

  const isMobileHorizontalOrientation = isMobileOnly && horizontalOrientation;

  return (
    <StyledAccessSelector className="invite-panel_access-selector">
      <AccessRightSelect
        selectedOption={selectedOption}
        onSelect={onSelectAccess}
        accessOptions={filteredAccesses ? filteredAccesses : accessOptions}
        noBorder={false}
        directionX="right"
        directionY="bottom"
        fixedDirection={true}
        manualWidth={width + "px"}
        isDefaultMode={false}
        isAside={false}
        withBackground={isMobileHorizontalOrientation ? false : isMobileOnly}
        setIsOpenItemAccess={setIsOpenItemAccess}
        hideMobileView={isMobileHorizontalOrientation}
      />
    </StyledAccessSelector>
  );
};

export default AccessSelector;
