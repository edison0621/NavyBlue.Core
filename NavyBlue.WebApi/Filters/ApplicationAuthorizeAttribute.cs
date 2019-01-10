﻿// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ApplicationAuthorizeAttribute.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:01
// *****************************************************************************************************************
// <copyright file="ApplicationAuthorizeAttribute.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Authorization;
using System;

namespace NavyBlue.AspNetCore.Web.Filters
{
    /// <summary>
    ///     ApplicationAuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApplicationAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationAuthorizeAttribute" /> class.
        /// </summary>
        public ApplicationAuthorizeAttribute()
        {
            this.Roles = "Application";
        }
    }
}