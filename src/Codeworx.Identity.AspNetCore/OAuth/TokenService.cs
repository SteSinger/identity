﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codeworx.Identity.OAuth;
using Codeworx.Identity.OAuth.Token;

namespace Codeworx.Identity.AspNetCore.OAuth
{
    public class TokenService : ITokenService
    {
        private readonly IClientAuthenticationService _clientAuthenticationService;
        private readonly IRequestValidator<TokenRequest, TokenErrorResponse> _requestValidator;
        private readonly IEnumerable<ITokenFlowService> _tokenFlowServices;

        public TokenService(IEnumerable<ITokenFlowService> tokenFlowServices, IRequestValidator<TokenRequest, TokenErrorResponse> requestValidator, IClientAuthenticationService clientAuthenticationService)
        {
            _tokenFlowServices = tokenFlowServices;
            _requestValidator = requestValidator;
            _clientAuthenticationService = clientAuthenticationService;
        }

        public async Task<ITokenResult> AuthorizeRequest(
            TokenRequest request,
            string clientId,
            string clientSecret)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var validationResult = await _requestValidator.IsValid(request)
                                                          .ConfigureAwait(false);
            if (validationResult != null)
            {
                return new InvalidRequestResult(validationResult);
            }

            var tokenFlowService = _tokenFlowServices.FirstOrDefault(p => p.SupportedGrantType == request.GrantType);
            if (tokenFlowService == null)
            {
                return new UnsupportedGrantTypeResult();
            }

            var clientAuthenticationResult = await _clientAuthenticationService.AuthenticateClient(request, clientId, clientSecret)
                                                                                 .ConfigureAwait(false);
            if (clientAuthenticationResult.TokenResult != null)
            {
                return clientAuthenticationResult.TokenResult;
            }

            if (clientAuthenticationResult.ClientRegistration == null)
            {
                return new InvalidClientResult();
            }

            if (!clientAuthenticationResult.ClientRegistration.SupportedFlow.Any(p => p.IsSupported(request.GrantType)))
            {
                return new UnauthorizedClientResult();
            }

            // ToDo: Check scopes (invalid_scope)
            var tokenResult = await tokenFlowService.AuthorizeRequest(request)
                                                    .ConfigureAwait(false);

            return tokenResult;
        }
    }
}