﻿using System;
using System.Security.Cryptography;
using Codeworx.Identity.OpenId.Model;
using Microsoft.IdentityModel.Tokens;

namespace Codeworx.Identity.Cryptography.Internal
{
    public class EcdJwkSerializer : IJwkInformationSerializer
    {
        public bool Supports(SecurityKey key) => key is ECDsaSecurityKey;

        public KeyParameter SerializeKeyToJsonWebKey(SecurityKey key, string keyId)
        {
            if (key is ECDsaSecurityKey ecdKey)
            {
                var parameters = ecdKey.ECDsa.ExportParameters(false);

                return new EllipticKeyParameter(keyId, Constants.KeyUse.Signature, ecdKey.KeySize, Base64UrlEncoder.Encode(parameters.Q.X), Base64UrlEncoder.Encode(parameters.Q.Y));
            }

            throw new NotSupportedException("Key type not supported!");
        }

        public string GetAlgorithm(SecurityKey key, HashAlgorithm hashAlgorithm)
        {
            if (key is ECDsaSecurityKey ecdKey)
            {
                return $"ES{hashAlgorithm.HashSize}";
            }

            throw new NotSupportedException("Key type not supported!");
        }
    }
}
