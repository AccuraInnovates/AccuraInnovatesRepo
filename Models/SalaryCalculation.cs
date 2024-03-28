using Microsoft.Data.SqlClient;
using System.Data;

namespace Accura_Innovatives.Models
{
    public class SalaryCalculation
    {
        public int SalCalId { get; set; } = 0;
        public int EmpCode { get; set; } = 0;
        public string EmpName { get; set; }
        public double GrossPay { get; set; }
        public double? Gross { get; set; }
        public double? BasicPay { get; set; } = 0;
        public double? OtherAllowances { get; set; } = 0;      
        public double? NetPay { get; set; } 
        public double? CTC  { get; set; }
        public string LastEditedDate { get; set; } = (DateTime.Now).ToString();
        public virtual EmployeeMasterData1? EmpNav { get; set; }

    }
}
