
namespace Dapper.Web.models
{
    public record Employee
    {
        public int id { get; init; }
        public string fullname { get; init; } = string.Empty;
        public string address { get; init; } = string.Empty;
        public int age { get; init; }
        public string birthday { get; init; } = string.Empty;
        
    }
}