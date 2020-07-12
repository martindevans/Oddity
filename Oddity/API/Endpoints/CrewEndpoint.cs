﻿using System.Net.Http;
using Oddity.API.Builders;
using Oddity.API.Events;
using Oddity.API.Models.Crew;

namespace Oddity.API.Endpoints
{
    /// <summary>
    /// Represents an entry point for /crew endpoint.
    /// </summary>
    public class CrewEndpoint : EndpointBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrewEndpoint"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="context">The Oddity context which will be used for lazy properties in models.</param>
        /// <param name="builderDelegatesContainer">The builder delegates container.</param>
        public CrewEndpoint(HttpClient httpClient, OddityCore context, BuilderDelegatesContainer builderDelegatesContainer)
            : base(httpClient, context, builderDelegatesContainer)
        {

        }

        /// <summary>
        /// Gets data about the specified crew member from the /crew/:id endpoint.
        /// </summary>
        /// <param name="id">ID of the specified crew member.</param>
        /// <returns>Deserialized JSON returned from the API.</returns>
        public SimpleBuilder<CrewInfo> Get(string id)
        {
            return new SimpleBuilder<CrewInfo>(HttpClient, "crew", id, Context, BuilderDelegatesContainer);
        }

        /// <summary>
        /// Gets data about crew from the /crew endpoint.
        /// </summary>
        /// <returns>Deserialized JSON returned from the API.</returns>
        public ListBuilder<CrewInfo> GetAll()
        {
            return new ListBuilder<CrewInfo>(HttpClient, "crew", Context, BuilderDelegatesContainer);
        }
    }
}