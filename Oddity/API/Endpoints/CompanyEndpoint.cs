﻿using System.Net.Http;
using Oddity.API.Builders;
using Oddity.API.Events;
using Oddity.API.Models.Company;

namespace Oddity.API.Endpoints
{
    /// <summary>
    /// Represents an entry point for /company endpoint.
    /// </summary>
    public class CompanyEndpoint : EndpointBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyEndpoint"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="context">The Oddity context which will be used for lazy properties in models.</param>
        /// <param name="builderDelegatesContainer">The builder delegates container.</param>
        public CompanyEndpoint(HttpClient httpClient, OddityCore context, BuilderDelegatesContainer builderDelegatesContainer)
            : base(httpClient, context, builderDelegatesContainer)
        {

        }

        /// <summary>
        /// Gets data about company from the /company endpoint.
        /// </summary>
        /// <returns>Deserialized JSON returned from the API.</returns>
        public SimpleBuilder<CompanyInfo> Get()
        {
            return new SimpleBuilder<CompanyInfo>(HttpClient, "company", Context, BuilderDelegatesContainer);
        }
    }
}