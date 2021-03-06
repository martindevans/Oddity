﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oddity.Models.Roadster
{
    public class RoadsterInfo : ModelBase, IIdentifiable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double? Eccentricity { get; set; }
        public double? Inclination { get; set; }
        public double? Longitude { get; set; }
        public string Wikipedia { get; set; }
        public string Video { get; set; }
        public string Details { get; set; }

        [JsonProperty("launch_date_utc")]
        public DateTime? DateTimeUtc { get; set; }

        [JsonProperty("launch_date_unix")]
        public ulong? DateTimeUnix { get; set; }

        [JsonProperty("launch_mass_kg")]
        public double? LaunchMassKilograms { get; set; }

        [JsonProperty("launch_mass_lbs")]
        public double? LaunchMassPounds { get; set; }

        [JsonProperty("norad_id")]
        public uint? NoradId { get; set; }

        [JsonProperty("epoch_jd")]
        public double? EpochJd { get; set; }

        [JsonProperty("orbit_type")]
        public string OrbitType { get; set; }

        [JsonProperty("apoapsis_au")]
        public double? ApoapsisAu { get; set; }

        [JsonProperty("periapsis_au")]
        public double? PeriapsisAu { get; set; }

        [JsonProperty("semi_major_axis_au")]
        public double? SemiMajorAxisAu { get; set; }

        [JsonProperty("periapsis_arg")]
        public double? PeriapsisArg { get; set; }

        [JsonProperty("period_days")]
        public double? PeriodDays { get; set; }

        [JsonProperty("speed_kph")]
        public double? SpeedKph { get; set; }

        [JsonProperty("speed_mph")]
        public double? SpeedMph { get; set; }

        [JsonProperty("earth_distance_km")]
        public double? EarthDistanceKilometers { get; set; }

        [JsonProperty("earth_distance_mi")]
        public double? EarthDistanceMiles { get; set; }

        [JsonProperty("mars_distance_km")]
        public double? MarsDistanceKilometers { get; set; }

        [JsonProperty("mars_distance_mi")]
        public double? MarsDistanceMiles { get; set; }

        [JsonProperty("flickr_images")]
        public List<string> FlickrImages { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
