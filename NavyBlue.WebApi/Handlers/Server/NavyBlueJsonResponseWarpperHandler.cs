﻿// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NavyBlueJsonResponseWarpperHandler.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:01
// *****************************************************************************************************************
// <copyright file="NavyBlueJsonResponseWarpperHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Server
{
    /// <summary>
    ///     JinyinmaoJsonResponseWapperHandler.
    /// </summary>
    public class NavyBlueJsonResponseWarpperHandler : DelegatingHandler
    {
        /// <summary>
        ///     Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (request.GetQueryString("jw").IsNotNullOrEmpty() && request.Headers.Accept.Any(a => a.MediaType == "application/json" || a.MediaType == "*/*") &&
                (response.Content == null || response.Content.Headers.ContentType.MediaType == "application/json"))
            {
                await WappUpContent(request, response);
            }

            return response;
        }

        private static async Task WappUpContent(HttpRequestMessage request, HttpResponseMessage response)
        {
            string content = null;
            if (response.Content != null)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            if (content.IsNullOrEmpty())
            {
                content = "{}";
            }

            JObject jObject = JObject.Parse(content);
            jObject.Remove("retCode");
            jObject.Remove("retMsg");
            jObject.Add("retCode", (response.IsSuccessStatusCode ? "00" : "10") + (int)response.StatusCode);
            jObject.Add("retMsg", response.IsSuccessStatusCode ? "ok" : (jObject.GetValue("message", StringComparison.OrdinalIgnoreCase)?.Value<string>() ?? ""));

            //response.Content = request.CreateResponse(HttpStatusCode.OK, jObject).Content;

            response.Content = new StringContent(jObject.ToJson());
            response.StatusCode = HttpStatusCode.OK;
        }
    }
}