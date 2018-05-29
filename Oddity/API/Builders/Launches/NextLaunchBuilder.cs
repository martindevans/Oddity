﻿using System.Net.Http;
using System.Threading.Tasks;
using Oddity.API.Models.Launch;

namespace Oddity.API.Builders.Launches
{
    /// <summary>
    /// Represents a set of methods to filter latest launch information and download them from API.
    /// </summary>
    public class NextLaunchesBuilder : BuilderBase<LaunchInfo>
    {
        private const string LaunchpadInfoEndpoint = "launches/next";

        /// <summary>
        /// Initializes a new instance of the <see cref="NextLaunchesBuilder"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="deserializationError">The deserialization error delegate.</param>
        public NextLaunchesBuilder(HttpClient httpClient, DeserializationError deserializationError) : base(httpClient, deserializationError)
        {

        }

        /// <inheritdoc />
        public override LaunchInfo Execute()
        {
            return ExecuteAsync().Result;
        }

        /// <inheritdoc />
        public override async Task<LaunchInfo> ExecuteAsync()
        {
            var link = BuildLink(LaunchpadInfoEndpoint);
            return await RequestForObject(link);
        }
    }
}