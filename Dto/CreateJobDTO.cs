namespace Dapper.Web.Dto
{
    public class CreateJobDTO
    {
        public string position_name { get; init; } = string.Empty;
        public decimal beginning_salary { get; init; }
    }
}