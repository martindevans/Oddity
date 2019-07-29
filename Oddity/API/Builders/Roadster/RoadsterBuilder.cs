﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Oddity.API.Models.Roadster;
using Oddity.API.Models.Rocket;

namespace Oddity.API.Builders.Roadster
{
    /// <summary>
    /// Represents a set of methods to Roadster information and download them from API.
    /// </summary>
    public class RoadsterBuilder : BuilderBase<RoadsterInfo>
    {
        private const string RoadsterInfoEndpoint = "info/roadster";

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsterBuilder"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="builderDelegatesContainer">The builder delegates container.</param>
        public RoadsterBuilder(HttpClient httpClient, BuilderDelegatesContainer builderDelegatesContainer) : base(httpClient, builderDelegatesContainer)
        {

        }

        /// <inheritdoc />
        protected override async Task<RoadsterInfo> ExecuteBuilder()
        {
            var link = BuildLink(RoadsterInfoEndpoint);
            return await SendRequestToApi(link).ConfigureAwait(false);
        }
    }
}
