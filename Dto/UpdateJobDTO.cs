using System.Text.Json.Serialization;


namespace Dapper.Web.Dto
{
    public class UpdateJobDTO
    {   
        [JsonIgnore]
        public int id { get; set; }

        public string position_name { get; set; } = String.Empty;
        public decimal beginning_salary { get; set; }
    }
}