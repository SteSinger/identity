﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Codeworx.Identity.OAuth;
using Codeworx.Identity.OAuth.Authorization;
using Codeworx.Identity.OpenId;

namespace Codeworx.Identity.AspNetCore.OpenId
{
    public class AuthorizationCodeFlowService : IOpenIdAuthorizationFlowService
    {
        private readonly IClientService _clientService;
        private readonly IScopeService _scopeService;

        public AuthorizationCodeFlowService(IClientService clientService, IScopeService scopeService)
        {
            _clientService = clientService;
            _scopeService = scopeService;
        }

        public bool IsSupported(string responseType)
        {
            return Equals(responseType, Identity.OAuth.Constants.ResponseType.Code);
        }

        public async Task<IAuthorizationResult> AuthorizeRequest(OpenIdAuthorizationRequest request, ClaimsIdentity user)
        {
            var client = await _clientService.GetById(request.ClientId)
                .ConfigureAwait(false);

            if (client == null)
            {
                return InvalidRequestResult.CreateInvalidClientId(request.State);
            }

            if (!client.SupportedFlow.Any(p => p.IsSupported(request.ResponseType)))
            {
                return new UnauthorizedClientResult(request.State, request.RedirectionTarget);
            }

            var containsOpenId = request.Scope
                .Split(' ')
                .Any(p => p.Equals(Identity.OpenId.Constants.Scopes.OpenId));

            if (containsOpenId == false)
            {
                return new MissingOpenidScopeResult(request.State, request.RedirectionTarget);
            }

            var scopes = await _scopeService.GetScopes()
                .ConfigureAwait(false);

            var scopeKeys = scopes
                .Select(s => s.ScopeKey)
                .ToList();

            var containsKey = request.Scope
                                  .Split(' ')
                                  .Any(p => !scopeKeys.Contains(p)) == true;

            if (!string.IsNullOrEmpty(request.Scope) && containsKey)
            {
                return new UnknownScopeResult(request.State, request.RedirectionTarget);
            }

            throw new NotImplementedException();
        }
    }
}