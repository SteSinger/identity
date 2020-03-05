﻿using System.Net;

namespace System
{
    public static class CodeworxIdentityPrimitivesUriBuilderExtensions
    {
        public static void AppendPath(this UriBuilder uriBuilder, string path)
        {
            uriBuilder.Path = $"{uriBuilder.Path.TrimEnd('/')}/{path.TrimStart('/')}";
        }

        public static void AppendQueryPart(this UriBuilder uriBuilder, string parameterName, string parameterValue)
        {
            parameterName = WebUtility.UrlEncode(parameterName.Trim('&', '?'));
            parameterValue = WebUtility.UrlEncode(parameterValue);

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            var queryPart = $"{parameterName}={parameterValue}";

            if (uriBuilder.Query.Length > 1)
            {
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + queryPart;
            }
            else
            {
                uriBuilder.Query = queryPart;
            }
        }
    }
}