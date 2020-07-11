﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Oddity.API.Events;
using Oddity.API.Exceptions;

namespace Oddity.API.Builders
{
    /// <summary>
    /// Represents a list builder used to retrieve data (collection of objects) without any filters.
    /// </summary>
    /// <typeparam name="TReturn">Type which will be returned after successful API request.</typeparam>
    public class ListBuilder<TReturn> : BuilderBase<List<TReturn>>
    {
        private readonly string _endpoint;
        private readonly string _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBuilder{TReturn}"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endpoint">The endpoint used in this instance to retrieve data from API.</param>
        /// <param name="builderDelegates">The builder delegates container.</param>
        public ListBuilder(HttpClient httpClient, string endpoint, BuilderDelegatesContainer builderDelegates)
            : this(httpClient, endpoint, null, builderDelegates)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBuilder{TReturn}"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endpoint">The endpoint used in this instance to retrieve data from API.</param>
        /// <param name="id">The ID of the specified object to retrieve from API.</param>
        /// <param name="builderDelegates">The builder delegates container.</param>
        public ListBuilder(HttpClient httpClient, string endpoint, string id, BuilderDelegatesContainer builderDelegates)
            : base(httpClient, builderDelegates)
        {
            _endpoint = endpoint;
            _id = id;
        }

        /// <inheritdoc />
        public override List<TReturn> Execute()
        {
            return ExecuteAsync().GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public override async Task<List<TReturn>> ExecuteAsync()
        {
            var content = await GetResponseFromEndpoint($"{_endpoint}/{_id}");
            return DeserializeJson(content);
        }
    }
}
