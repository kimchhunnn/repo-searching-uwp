using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace repo_searching_uwp.Model
{
    //{"message":"Validation Failed","errors":[{"message":"None of the search qualifiers apply to this search type.","resource":"Search","field":"q","code":"invalid"}],"documentation_url":"https://docs.github.com/v3/search/"}

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
