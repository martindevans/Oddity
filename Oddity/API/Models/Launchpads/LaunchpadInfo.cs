﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Oddity.API.Models.Launches;
using Oddity.API.Models.Rockets;

namespace Oddity.API.Models.Launchpads
{
    public class LaunchpadInfo : ModelBase
    {
        public string Id { get; set; }
        public LaunchpadStatus? Status { get; set; }
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        public string Locality { get; set; }
        public string Region { get; set; }
        public string TimeZone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [JsonProperty("launch_attempts")]
        public uint? LaunchAttempts { get; set; }

        [JsonProperty("launch_successes")]
        public uint? LaunchSuccesses { get; set; }

        [JsonProperty("rockets")]
        public List<string> RocketsId
        {
            get => _rocketsId;
            set
            {
                _rocketsId = value;
                Rockets = _rocketsId.Select(p => new Lazy<RocketInfo>(() => Context.RocketsEndpoint.Get(p).Execute())).ToList();
            }
        }

        public List<Lazy<RocketInfo>> Rockets { get; private set; }

        private List<string> _rocketsId;

        [JsonProperty("launches")]
        public List<string> LaunchesId
        {
            get => _launchesId;
            set
            {
                _launchesId = value;
                Launches = _launchesId.Select(p => new Lazy<LaunchInfo>(() => Context.LaunchesEndpoint.Get(p).Execute())).ToList();
            }
        }

        public List<Lazy<LaunchInfo>> Launches { get; private set; }

        private List<string> _launchesId;
    }
}
