﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeworx.Identity.OAuth
{
    public class OAuthClaimsProvider : ISystemClaimsProvider
    {
        private readonly IBaseUriAccessor _baseUri;

        public OAuthClaimsProvider(IBaseUriAccessor baseUri)
        {
            _baseUri = baseUri;
        }

        public Task<IEnumerable<AssignedClaim>> GetClaimsAsync(IIdentityDataParameters parameters)
        {
            var result = new List<AssignedClaim>();

            var subjectClaim = AssignedClaim.Create(Constants.Claims.Subject, parameters.User.GetUserId());
            result.Add(subjectClaim);

            var issuerClaim = AssignedClaim.Create(Constants.Claims.Issuer, _baseUri.BaseUri.ToString().TrimEnd('/'));
            result.Add(issuerClaim);

            var audienceClaim = AssignedClaim.Create(Constants.Claims.Audience, parameters.Client.ClientId);
            result.Add(audienceClaim);

            if (parameters.Scopes.Any())
            {
                var scopeClaim = AssignedClaim.Create(Constants.OAuth.ScopeName, parameters.Scopes);
                result.Add(scopeClaim);
            }

            if (parameters is IAuthorizationParameters authorizationParameters)
            {
                if (!string.IsNullOrWhiteSpace(authorizationParameters.Nonce))
                {
                    var nonceClaim = AssignedClaim.Create(Constants.OAuth.NonceName, authorizationParameters.Nonce);
                    result.Add(nonceClaim);
                }

                if (!string.IsNullOrWhiteSpace(authorizationParameters.State))
                {
                    var stateClaim = AssignedClaim.Create(Constants.OAuth.StateName, authorizationParameters.State);
                    result.Add(stateClaim);
                }
            }

            return Task.FromResult<IEnumerable<AssignedClaim>>(result);
        }
    }
}
