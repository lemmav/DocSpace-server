// (c) Copyright Ascensio System SIA 2009-2024
//
// This program is a free software product.
// You can redistribute it and/or modify it under the terms
// of the GNU Affero General Public License (AGPL) version 3 as published by the Free Software
// Foundation. In accordance with Section 7(a) of the GNU AGPL its Section 15 shall be amended
// to the effect that Ascensio System SIA expressly excludes the warranty of non-infringement of
// any third-party rights.
//
// This program is distributed WITHOUT ANY WARRANTY, without even the implied warranty
// of MERCHANTABILITY or FITNESS FOR A PARTICULAR  PURPOSE. For details, see
// the GNU AGPL at: http://www.gnu.org/licenses/agpl-3.0.html
//
// You can contact Ascensio System SIA at Lubanas st. 125a-25, Riga, Latvia, EU, LV-1021.
//
// The  interactive user interfaces in modified source and object code versions of the Program must
// display Appropriate Legal Notices, as required under Section 5 of the GNU AGPL version 3.
//
// Pursuant to Section 7(b) of the License you must retain the original Product logo when
// distributing the program. Pursuant to Section 7(e) we decline to grant you any rights under
// trademark law for use of our trademarks.
//
// All the Product's GUI elements, including illustrations and icon sets, as well as technical
// writing
// content are licensed under the terms of the Creative Commons Attribution-ShareAlike 4.0
// International. See the License terms at http://creativecommons.org/licenses/by-sa/4.0/legalcode

package com.asc.registration.service.mapper;

import com.asc.common.core.domain.entity.Consent;
import com.asc.common.core.domain.value.enums.ConsentStatus;
import com.asc.registration.service.transfer.response.ClientInfoResponse;
import com.asc.registration.service.transfer.response.ConsentResponse;
import org.springframework.stereotype.Component;

/**
 * ConsentDataMapper is responsible for mapping consent domain objects to data transfer objects
 * (DTOs). This component handles the transformation logic for creating consent response DTOs.
 */
@Component
public class ConsentDataMapper {

  /**
   * Maps a Consent domain object and a ClientResponse DTO to a ConsentResponse DTO.
   *
   * @param consent The consent domain object.
   * @param client The client info response DTO associated with the consent.
   * @return The mapped ConsentResponse DTO.
   * @throws IllegalArgumentException if consent or client is null
   */
  public ConsentResponse toConsentResponse(Consent consent, ClientInfoResponse client) {
    if (consent == null) throw new IllegalArgumentException("Consent cannot be null");
    if (client == null) throw new IllegalArgumentException("ClientResponse cannot be null");

    return ConsentResponse.builder()
        .registeredClientId(consent.getId().getRegisteredClientId())
        .principalName(consent.getId().getPrincipalName())
        .scopes(String.join(",", consent.getScopes()))
        .invalidated(consent.getStatus().equals(ConsentStatus.INVALIDATED))
        .modifiedOn(consent.getModifiedOn())
        .client(client)
        .build();
  }
}
