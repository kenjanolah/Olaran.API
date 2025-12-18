namespace Dapper.Web.Models
{
    public class JobPositions
    {
        public int id { get; set; }
        public string position_name { get; set; } = string.Empty;   
        public decimal beginning_salary { get; set; }
    }
}