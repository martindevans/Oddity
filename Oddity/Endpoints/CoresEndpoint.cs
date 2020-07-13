﻿using System.Net.Http;
using Oddity.Builders;
using Oddity.Events;
using Oddity.Models.Cores;

namespace Oddity.Endpoints
{
    /// <summary>
    /// Represents an entry point for /cores endpoint.
    /// </summary>
    public class CoresEndpoint : EndpointBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoresEndpoint"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="context">The Oddity context which will be used for lazy properties in models.</param>
        /// <param name="builderDelegates">The builder delegates container.</param>
        public CoresEndpoint(HttpClient httpClient, OddityCore context, BuilderDelegates builderDelegates)
            : base(httpClient, context, builderDelegates)
        {

        }

        /// <summary>
        /// Gets data about the specified core from the /cores/:id endpoint.
        /// </summary>
        /// <param name="id">ID of the specified cores.</param>
        /// <returns>Deserialized JSON returned from the API.</returns>
        public SimpleBuilder<CoreInfo> Get(string id)
        {
            return new SimpleBuilder<CoreInfo>(HttpClient, "cores", id, Context, builderDelegates);
        }

        /// <summary>
        /// Gets data about all cores from the /cores endpoint.
        /// </summary>
        /// <returns>Deserialized JSON returned from the API.</returns>
        public ListBuilder<CoreInfo> GetAll()
        {
            return new ListBuilder<CoreInfo>(HttpClient, "cores", Context, builderDelegates);
        }

        /// <summary>
        /// Gets filtered and paginated data about all cores from the /cores/query endpoint.
        /// </summary>
        /// <returns>Deserialized JSON returned from the API.</returns>
        public QueryBuilder<CoreInfo> Query()
        {
            return new QueryBuilder<CoreInfo>(HttpClient, "cores/query", Context, builderDelegates);
        }
    }
}