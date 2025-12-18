namespace Dapper.Web.Dto
{
    public class CreateEmployeeDTO
    {
        public string fullname { get; init; } = string.Empty;
        public string address { get; init; } = string.Empty;
        public int age { get; init; }
        public string birthday { get; init; } = string.Empty;
    }
}