using System.Data;

namespace Accura_Innovatives.Models
{
    public class ExportEsiReport
    {
        public string? IPNumber { get; set; }
        public string? IPName { get; set; }
        public int? Days { get; set; }
        public double? Wages {  get; set; }
        public string? ReasonCode { get; set; }
        public DateOnly? LastWorkingDate { get; set; }

    }
}
