import React from "react";
import { withTranslation } from "react-i18next";
import styled from "styled-components";
import Text from "@appserver/components/text";
import Box from "@appserver/components/box";
import Link from "@appserver/components/link";
import { combineUrl } from "@appserver/common/utils";
import { inject, observer } from "mobx-react";
import { AppServerConfig } from "@appserver/common/constants";
import withCultureNames from "@appserver/common/hoc/withCultureNames";
import history from "@appserver/common/history";
import { Base } from "@appserver/components/themes";

import { StyledArrowRightIcon } from "../common/settingsCustomization/StyledSettings";

const StyledComponent = styled.div`
  padding-top: 13px;

  .combo-button-label {
    max-width: 100%;
  }

  .category-item-wrapper {
    padding-bottom: 20px;

    .category-item-heading {
      padding-bottom: 8px;
      svg {
        padding-bottom: 5px;
      }
    }

    .category-item-description {
      color: #657077;
      font-size: 13px;
      max-width: 1024px;
    }

    .inherit-title-link {
      margin-right: 4px;
      font-size: 16px;
      font-weight: 700;
    }
  }
`;

StyledComponent.defaultProps = { theme: Base };

const CustomizationNavbar = ({ t, theme, helpUrlCommonSettings }) => {
  const onClickLink = (e) => {
    e.preventDefault();
    history.push(e.target.pathname);
  };
  return (
    <StyledComponent>
      <div className="category-item-wrapper">
        <div className="category-item-heading">
          <Link
            className="inherit-title-link header"
            onClick={onClickLink}
            truncate={true}
            href={combineUrl(
              AppServerConfig.proxyURL,
              "/settings/common/customization/language-and-time-zone"
            )}
          >
            {t("StudioTimeLanguageSettings")}
          </Link>
          <StyledArrowRightIcon size="small" color="#333333" />
        </div>
        <Text className="category-item-description">
          {t("LanguageAndTimeZoneSettingsDescription")}
        </Text>
        <Box paddingProp="10px 0 3px 0">
          <Link
            color={theme.studio.settings.common.linkColorHelp}
            target="_blank"
            isHovered={true}
            href={helpUrlCommonSettings}
          >
            {t("Common:LearnMore")}
          </Link>
        </Box>
      </div>
      <div className="category-item-wrapper">
        <div className="category-item-heading">
          <Link
            truncate={true}
            className="inherit-title-link header"
            onClick={onClickLink}
            href={combineUrl(
              AppServerConfig.proxyURL,
              "/settings/common/customization/welcome-page-settings"
            )}
          >
            {t("CustomTitlesWelcome")}
          </Link>
          <StyledArrowRightIcon size="small" color="#333333" />
        </div>
        <Text className="category-item-description">
          {t("CustomTitlesSettingsDescription")}
        </Text>
      </div>
      <div className="category-item-wrapper">
        <div className="category-item-heading">
          <Link
            truncate={true}
            className="inherit-title-link header"
            onClick={onClickLink}
            href={combineUrl(
              AppServerConfig.proxyURL,
              "/settings/common/customization/portal-renaming"
            )}
          >
            {t("PortalRenaming")}
          </Link>
          <StyledArrowRightIcon size="small" color="#333333" />
        </div>
        <Text className="category-item-description">
          {t("PortalRenamingDescription")}
        </Text>
      </div>
    </StyledComponent>
  );
};

export default inject(({ auth }) => {
  const { helpUrlCommonSettings, theme } = auth.settingsStore;
  return {
    theme,
    helpUrlCommonSettings,
  };
})(
  withCultureNames(
    observer(withTranslation(["Settings", "Common"])(CustomizationNavbar))
  )
);
