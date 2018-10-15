﻿using Newtonsoft.Json;

namespace PipeDriveApi.Models
{
    public class WebhookPost<TEntity> where TEntity: BaseEntity
    {
        public TEntity Current { get; set; }
        public TEntity Previous { get; set; }
        public string Event { get; set; }
        public WebhookMetadata Meta { get; set; }

    }

    public class WebhookMetadata
    {
        [JsonProperty(PropertyName ="v")]
        public string Version { get; set;}

        [JsonProperty(PropertyName ="matches_filters")]
        public WebhookFilter MatchesFilter { get; set; }
    }

    public class WebhookFilter
    {
        public int[] Current { get; set; }
    }
}
