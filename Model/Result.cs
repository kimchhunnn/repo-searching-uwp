using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace repo_searching_uwp.Model
{
    public class Repo
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class Result
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<Repo> Items { get; set; }
    }
}
