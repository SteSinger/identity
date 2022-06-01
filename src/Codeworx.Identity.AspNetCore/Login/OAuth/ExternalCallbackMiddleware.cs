﻿using System;
using System.Threading.Tasks;
using Codeworx.Identity.Login;
using Codeworx.Identity.Login.OAuth;
using Codeworx.Identity.Model;
using Codeworx.Identity.Resources;
using Microsoft.AspNetCore.Http;

namespace Codeworx.Identity.AspNetCore
{
    public class ExternalCallbackMiddleware
    {
        private readonly RequestDelegate _next;

        public ExternalCallbackMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context,
            IRequestBinder<ExternalCallbackRequest> requestBinder,
            IResponseBinder<SignInResponse> signInBinder,
            IResponseBinder<LoginRedirectResponse> loginRedirectResponseBinder,
            ILoginService loginService,
            IStringResources stringResources)
        {
            ExternalCallbackRequest callbackRequest = null;

            try
            {
                callbackRequest = await requestBinder.BindAsync(context.Request);
                SignInResponse signInResponse = await loginService.SignInAsync(callbackRequest.ProviderId, callbackRequest.LoginRequest);
                await signInBinder.BindAsync(signInResponse, context.Response);
            }
            catch (ErrorResponseException ex)
            {
                IResponseBinder binder = context.GetResponseBinder(ex.ResponseType);
                await binder.BindAsync(ex.Response, context.Response);
            }
            catch (Exception ex) when (ex is IErrorWithReturnUrl returnUrlException)
            {
                var returnUrl = returnUrlException.ReturnUrl;
                var message = returnUrlException.GetMessage();
                var response = new LoginRedirectResponse(callbackRequest?.ProviderId, providerError: message, redirectUri: returnUrl);
                await loginRedirectResponseBinder.BindAsync(response, context.Response);
            }
            catch (Exception ex)
            {
                string message = stringResources.GetResource(StringResource.GenericLoginError);
                if (ex is IEndUserErrorMessage endUserError)
                {
                    message = endUserError.GetMessage();
                }

                var response = new LoginRedirectResponse(callbackRequest?.ProviderId, providerError: message);
                await loginRedirectResponseBinder.BindAsync(response, context.Response);
            }
        }
    }
}