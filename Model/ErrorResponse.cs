using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace repo_searching_uwp.Model
{

    class Errors
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    class ErrorResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public Errors[] Errors { get; set; }
    }
}
