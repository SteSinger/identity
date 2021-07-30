using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace Codeworx.Identity.Test.AspNetCore
{
    public class PreventOpenRedirectTest : IntegrationTestBase
    {
        public async Task WindowsLogin_ExternalReturnUrl_Expects_BadRequest()
        {
            var uriBuilder = new UriBuilder(TestClient.BaseAddress.ToString());
            uriBuilder.AppendPath("winlogin/78275C4F-EF6E-46D3-88B6-80CAC6B6D1F8");
            uriBuilder.AppendQueryParameter("returnUrl", "https://example.org/bla");

            var tokenResponse = await this.TestClient.GetAsync(uriBuilder.ToString());

            Assert.AreEqual(StatusCodes.Status400BadRequest, tokenResponse.StatusCode);
        }

        public async Task WindowsLogin_RelativeInternalReturnUrl_Expects_Redirect()
        {
            var uriBuilder = new UriBuilder(TestClient.BaseAddress.ToString());
            uriBuilder.AppendPath("winlogin/78275C4F-EF6E-46D3-88B6-80CAC6B6D1F8");
            uriBuilder.AppendQueryParameter("returnUrl", "/account/login");

            var tokenResponse = await this.TestClient.GetAsync(uriBuilder.ToString());

            Assert.AreEqual(StatusCodes.Status304NotModified, tokenResponse.StatusCode);
        }
    }
}
