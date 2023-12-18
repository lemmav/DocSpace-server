/**
 *
 */
package com.onlyoffice.authorization.api.core.usecases.service.client;

import com.onlyoffice.authorization.api.web.client.transfer.PersonDTO;
import com.onlyoffice.authorization.api.web.client.transfer.TenantDTO;
import com.onlyoffice.authorization.api.web.server.messaging.messages.ClientMessage;
import com.onlyoffice.authorization.api.web.server.transfer.request.CreateClientDTO;
import com.onlyoffice.authorization.api.web.server.transfer.response.ClientDTO;

import java.util.List;

/**
 *
 */
public interface ClientCreationUsecases {
    ClientDTO saveClient(ClientMessage message);
    List<String> saveClients(Iterable<ClientMessage> messages);
    ClientDTO createClientAsync(CreateClientDTO clientDTO, TenantDTO tenant,
                                PersonDTO person, String tenantUrl);
}
