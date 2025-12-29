using Newtonsoft.Json;

namespace async_vs_sync_benchmark_api.Models
{
    public class SharedModel
    {
        public class RemoveModel
        {
            public int Id { get; set; }
            public string Note { get; set; }
        }
        public class ExistsModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class Sort
        {
            public string Field { get; set; }
            public string Dir { get; set; }
        }
        public class CanRemoveModel
        {
            public bool Can { get; set; }
            public string Message { get; set; }
        }
        public class SearchResult<TResult, TSearch>: MessageResult
        {
            [JsonProperty("results")]
            public List<TResult> Results { get; set; }
            [JsonProperty("param")]
            public TSearch Param { get; set; }
        }
        // TODO: Use ApiResponse instead of ViewResult
        public class ViewResult<TResult>: MessageResult
        {
            [JsonProperty("result")]
            public TResult Result { get; set; }
        }
        public class MessageResult
        {
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }
            [JsonProperty("type")]
            public ResponseType ResponseType { get; set; } = ResponseType.Success;
        }
        public class ApiResponse<T>
        {
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }
            [JsonProperty("type")]
            public ResponseType ResponseType { get; set; } = ResponseType.Success;
            [JsonProperty("result")]
            public T Result { get; set; }
        }
        public enum ResponseType
        {
            Error = 1,
            Success = 2
        }
        public class DateRange
        {
            public DateRange() { }
            public DateRange(string range)
            {
                if (range != "")
                {
                    Date1 = DateTime.Parse(range.Substring(0, range.LastIndexOf('~') - 1));
                    Date2 = DateTime.Parse(range.Substring(range.LastIndexOf('~') + 1));
                    IsEmpty = false;
                }
                else
                {
                    IsEmpty = true;
                }
            }

            public DateTime Date1 { get; set; }
            public DateTime Date2 { get; set; }
            public bool IsEmpty { get; set; }
        }
        public class RefreshTokenInfo
        {
            public string RefreshToken { get; set; }
            public DateTime ExpiredDate { get; set; }
        }
        public class IsExistsModel
        {
            public long Id { get; set; }
            public long ParentId { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }
        public class RecaptchaResponse
        {
            public bool Success { get; set; }
            public IEnumerable<string> ErrorCodes { get; set; }
            public DateTime ChallengeTs { get; set; }
            public string Hostname { get; set; }
        }
        public class SendResetPasswordCodeModel
        {
            public string SendTo { get; set; }
            public string EncodedResponse { get; set; }
        }
        public class Attachment
        {
            public string Uid { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public class ClaimKey
        {
            public const string SessionId = "sid";
            public const string UserId = "uid";
            public const string OrgUnit = "ou";
            public const string OrgUnitId = "ouId";
            public const string Grants = "grants";
        }
    }
}

