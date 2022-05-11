﻿using System.Threading.Tasks;
using Codeworx.Identity.Response;
using Microsoft.AspNetCore.Http;

namespace Codeworx.Identity.AspNetCore.Binder
{
    public class MethodNotSupportedResponseBinder : ResponseBinder<MethodNotSupportedResponse>
    {
        protected override Task BindAsync(MethodNotSupportedResponse responseData, HttpResponse response, bool headerOnly)
        {
            response.StatusCode = StatusCodes.Status405MethodNotAllowed;

            return Task.CompletedTask;
        }
    }
}