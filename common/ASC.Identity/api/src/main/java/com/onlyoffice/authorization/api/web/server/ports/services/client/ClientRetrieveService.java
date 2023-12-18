package com.onlyoffice.authorization.api.web.server.ports.services.client;

import com.onlyoffice.authorization.api.core.usecases.repository.client.ClientPersistenceRetrievalUsecases;
import com.onlyoffice.authorization.api.core.usecases.service.client.ClientRetrieveUsecases;
import com.onlyoffice.authorization.api.web.security.crypto.Cipher;
import com.onlyoffice.authorization.api.web.server.transfer.response.ClientDTO;
import com.onlyoffice.authorization.api.web.server.transfer.response.PaginationDTO;
import com.onlyoffice.authorization.api.web.server.utilities.mappers.ClientMapper;
import jakarta.persistence.EntityNotFoundException;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.slf4j.MDC;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.stream.Collectors;

@Slf4j
@Service
@RequiredArgsConstructor
public class ClientRetrieveService implements ClientRetrieveUsecases {
    private final ClientPersistenceRetrievalUsecases retrievalUsecases;

    private final Cipher cipher;

    @Cacheable("clients")
    @Transactional(readOnly = true, rollbackFor = Exception.class, timeout = 1250)
    public ClientDTO getClient(String clientId) {
        MDC.put("clientId", clientId);
        log.info("Trying to get a client", clientId);
        MDC.clear();

        return retrievalUsecases
                .findById(clientId)
                .filter(c -> !c.isInvalidated())
                .map(c -> {
                    try {
                        var query = ClientMapper.INSTANCE.fromEntityToQuery(c);
                        query.setClientSecret(cipher.decrypt(query.getClientSecret()));
                        return query;
                    } catch (Exception e) {
                        MDC.put("clientId", clientId);
                        log.error("Could not map a client", e);
                        MDC.clear();
                        throw new EntityNotFoundException(String.
                                format("could not find and decrypt client secret: %s", e.getMessage()));
                    }
                })
                .orElseThrow(() -> new EntityNotFoundException(String
                        .format("could not find client with id %s", clientId)));
    }

    @Transactional(readOnly = true, rollbackFor = Exception.class, timeout = 2250)
    public PaginationDTO getTenantClients(int tenant, int page, int limit) {
        MDC.put("page", String.valueOf(page));
        MDC.put("limit", String.valueOf(limit));
        log.info("Trying to get tenant clients", tenant, page, limit);
        MDC.clear();

        var data = retrievalUsecases.findAllByTenant(tenant, Pageable
                .ofSize(limit).withPage(page));

        var builder = PaginationDTO
                .<ClientDTO>builder()
                .page(page)
                .limit(limit)
                .data(data.stream()
                        .filter(c -> !c.isInvalidated())
                        .map(c -> ClientMapper.INSTANCE.fromEntityToQuery(c))
                        .collect(Collectors.toList()));

        if (data.hasPrevious())
            builder.previous(page - 1);

        if (data.hasNext())
            builder.next(page + 1);

        return builder.build();
    }
}
