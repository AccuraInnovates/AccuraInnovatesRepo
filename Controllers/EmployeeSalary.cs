using Accura_Innovatives.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using cloudscribe.Pagination.Models;
using System.Data;
using System.Drawing.Printing;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Accura_Innovatives.Controllers
{

    public class EmployeeSalary : Controller
    {
        private readonly EmployeeMaster1Context _context;
        private readonly IWebHostEnvironment hostingenvironment;

        public EmployeeSalary(EmployeeMaster1Context context, IWebHostEnvironment hc)
        {
            _context = context;
            hostingenvironment = hc;
        }
        SalaryCalculation sc=new SalaryCalculation();

        // GET: EmployeeSalary
        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            ViewBag.CurrentFilter = searchString;
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var employeeMaster1Context = from b in _context.SalaryCalculations.Include(e => e.EmpNav) select b;

            var SalCalCount = employeeMaster1Context.Count();


            if (!System.String.IsNullOrEmpty(searchString))
            {

                int code = Convert.ToInt32(searchString);
                employeeMaster1Context = _context.SalaryCalculations.Where(x => x.EmpCode == code || searchString == null);
                SalCalCount = employeeMaster1Context.Count();

            }


            employeeMaster1Context = employeeMaster1Context
            .Skip(ExcludeRecords)
            .Take(pageSize);

            var result = new PagedResult<SalaryCalculation>
            {
                Data = employeeMaster1Context.AsNoTracking().ToList(),
                TotalItems = SalCalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: EmployeeSalary/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sal = await _context.SalaryCalculations.FindAsync(id);
            if (sal == null)
            {
                return NotFound();
            }
            return View(sal);
        }

        // GET: EmployeeSalary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeSalary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalaryCalculation sal)
        {
            try
            {
                if (sal.LastEditedDate == null || sal.LastEditedDate == "NULL" || sal.LastEditedDate == "")
                {
                    sal.LastEditedDate = DateOnly.FromDateTime(DateTime.Now).ToString();
                }
                _context.Add(sal);
                _context.SaveChanges();
              //  EditSalaryDetails(sal.EmpCode, sal);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred:Kindly check the entered values.";
                return View();
            }
        }

        // GET: EmployeeSalary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var e = await _context.EmployeeMasterData1s.FindAsync(id);
            var emp = e.SalaryCalculation;

            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // POST: EmployeeSalary/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<IActionResult> CalculateSalary(string MonthYear)
        {

            try
            {
                int i = 0;
                List<SalaryCalculation> sal = _context.SalaryCalculations.ToList();
                foreach (var s in sal)
                {
                    ConsolidatedModel co;
                    if (_context.ConsolidatedModel.Count(x => x.EmpCode == s.EmpCode) == 0)
                    {
                        co = new ConsolidatedModel();
                        i = 1;
                    }
                    else
                        co = _context.ConsolidatedModel.Where(x => x.EmpCode == s.EmpCode).FirstOrDefault();
                    float deductions = 0;
                    double OtHoursPayment = 0, OtMinutesPayment = 0, OTSecsPayment = 0 ;
                    float WorkedDays = 0,GrossWithoutOT = 0;
                    float GrossAfterTDS = 0, esiEmployee = 0, esiEmployer = 0,
                      pfEmployee = 0, pfEmployer = 0, eps = 0, bas = 0;

                    if (s.EmpCode != null)
                    {
                        var y = _context.YearlyLeaves.Where(x => x.EmpCode == s.EmpCode).FirstOrDefault();
                        var c = _context.CompOffAdvancesOnes.Where(x => x.EmpCode == s.EmpCode && x.Month == MonthYear ).ToList();
                        var e = _context.Attendances.Where(x => x.EmpCode == s.EmpCode && x.Month == MonthYear).ToList();
                        var m = _context.EmployeeMasterData1s.Where(x => x.EmpCode == s.EmpCode).FirstOrDefault();

                        co.EmpCode = s.EmpCode;
                        co.EmpName = s.EmpName;

                        foreach (var attendance in e)
                        {

                            co.MonthYear = attendance.Month;
                            co.TotalWorkingDays = attendance.NoOfCalenderDaysInCurrentMonth - attendance.MonthlyWeekOffDays;
                            co.NoOfPresentDays = attendance.TotalShift;
                            co.OtherDeductions = attendance.OtherDeductions;
                            if (attendance.OtherEarnings != null)
                            {
                                co.OtherEarnings = attendance.OtherEarnings;
                            }
                            else { co.OtherEarnings = 0; }
                            if (attendance.OTDay > 0)
                            {
                                co.OTHrs = attendance.OTDay.ToString() + "." + attendance.TotalOTHrs.ToString();
                            }
                            else
                            {
                                co.OTHrs = attendance.TotalOTHrs.ToString();
                            }
                            co.NH = attendance.NationalHolidays;
                            co.TDS = attendance.TDS;
                            co.TotalWeekOffDays = attendance.MonthlyWeekOffDays;
                            if (y == null)
                            {
                                co.TotalAllowedCL = 0;
                            }
                            else if (y.CL == null)
                            {
                                co.TotalAllowedCL = 0;
                            }

                            else
                            {
                                co.TotalAllowedCL = y.CL;
                            }
                            if (y == null)
                            {
                                co.TotalAllowedSL = 0;
                            }
                            else if (y.SL == null)
                            {
                                co.TotalAllowedSL = 0;
                            }
                            else
                            {
                                co.TotalAllowedSL = y.SL;
                            }
                            co.CurrentCL = attendance.ClDays;
                            co.CurrentSL = attendance.SlDays;
                            co.AccountNumber = m.SalAccNo;
                            co.BankName = m.SalBankName;
                            co.Department = m.EmpDept;
                            co.Designation = m.EmpDesignation;
                            co.CompanyName = m.SalaryPaidBy;
                            co.Gender = m.Gender;
                            co.Grade = m.EmpCtg;
                            co.Qualification = m.HighQuali;
                            co.MaritalStatus = m.MaritalStatus;
                            co.EsiEpfEligibility = m.EsiEpfEligibility;
                            co.EsiNo = m.EsiNo;
                            co.EpfNo = m.EpfNo;
                            co.LOPDays = attendance.LOPDays;
                            //
                            //co.OpeningAdvance1 = 0;
                            //co.OpeningBalanceCompOff = 0;
                            co.WagesPerShift = m.EmpWageShift;
                            if (m.ContractorId != null)
                            {
                                co.Contractor = _context.ContractorMs.Where(x => x.ContractorId == m.ContractorId).Select(x => x.ContractorName).FirstOrDefault();
                            }
                            if (DateTime.TryParse(m.Dob, out DateTime dt1))
                            {
                                
                                co.DOB = dt1.ToString("dd-MM-yyyy");
                                
                            }
                            if (DateTime.TryParse(m.EmpDoj, out DateTime dt2))
                            {
                               
                                co.DOJ = dt2.ToString("dd-MM-yyyy");
                                
                            }

                            

                            co.Mess = Math.Round(attendance.MessDeductionAmount);
                            co.PaymentMode = m.PaymentMode;
                            co.Penalty = Math.Round(attendance.PenaltyDeductionAmount);
                            co.ActualGross = Math.Round(s.GrossPay);
                            //co.OpeningAdvance2 = 0;
                            co.Advance1 = Math.Round(attendance.Advance1Amount);
                            co.Advance2 = Math.Round(attendance.Advance2Amount);
                            co.Incentive1 = 0;
                            co.Incentive2 = 0;

                            // double WorkingMinutes = (double)(co.TotalWorkingDays * 600);

                            double OtDaytoHours = attendance.OTDay * 24;
                            if (co.Gender != "Male" && (s.EmpCode / 10000) == 2)
                            {
                                OtHoursPayment = (attendance.TotalOTHrs.Hour + OtDaytoHours) * (s.GrossPay / 9.5);
                                OtMinutesPayment = attendance.TotalOTHrs.Minute * (s.GrossPay / 570);
                                OTSecsPayment = attendance.TotalOTHrs.Second * (s.GrossPay / 34200);
                            }
                            else
                            {
                                OtHoursPayment = (attendance.TotalOTHrs.Hour + OtDaytoHours) * (s.GrossPay / 10);
                                OtMinutesPayment = attendance.TotalOTHrs.Minute * (s.GrossPay / 600);
                                OTSecsPayment = attendance.TotalOTHrs.Second * (s.GrossPay / 36000);
                            }
                            //int OtDaysInMins = 0;
                            // if (OtDay > 0)
                            //  {
                            //     OtDaysInMins = OtDay * 10 * 60;
                            //  }
                            //TimeOnly dateTime = attendance.TotalOTHrs;
                            // int totalOTMinutes = CalculateTotalMinutes(dateTime.Hour, dateTime.Minute) + OtDaysInMins;
                            //if (float.TryParse((s.GrossPay / 600).ToString(), out float SalMin))
                            //{
                            //    SalPerMinute = SalMin;
                            //}
                            double OTPayment = OtHoursPayment + OtMinutesPayment + OTSecsPayment;
                            co.Incentive1 = Math.Round(OTPayment);
                            co.Incentive2 = Math.Round(attendance.IncentiveAmountSalesOthers);

                            WorkedDays = (float)attendance.TotalShift + (float)attendance.SlDays + (float)attendance.ClDays + attendance.NationalHolidays + (float)attendance.CompOffDays;
                            co.NoOfPresentDays = WorkedDays;
                            if ((s.EmpCode / 10000) == 2)
                            {
                                co.WagesPerShift = s.GrossPay;
                                co.TotalWorkingDays = 1;
                            }
                            else { co.WagesPerShift = 0; }
                            GrossWithoutOT = (float)(WorkedDays * (s.GrossPay / co.TotalWorkingDays));
                            double GrossPayForTheMonth = (double)(WorkedDays * (s.GrossPay / co.TotalWorkingDays)) + (double)co.Incentive1 + (double)co.Incentive2 + (double)co.OtherEarnings;

                            s.Gross = GrossPayForTheMonth;

                           
                            co.GrossPay = (double)Math.Round((decimal)s.Gross);
                            s.BasicPay = Convert.ToDouble(Math.Round((decimal)(0.64 * GrossWithoutOT)));
                            co.BasicPay = (double)Math.Round((decimal)s.BasicPay);
                            s.OtherAllowances = (double)Math.Round(Convert.ToDecimal(GrossWithoutOT - s.BasicPay));
                            co.HRA = (double)Math.Round((decimal)s.OtherAllowances);

                            string em = _context.EmployeeMasterData1s.Where(x => x.EmpCode == s.EmpCode).Select(x => x.EsiEpfEligibility).FirstOrDefault();

                            int result = (int)_context.EsiPfs.Max(e => e.EsiPfID);

                            float EsiEC = _context.EsiPfs.Where(x => x.EsiPfID == result).Select(x => (float)x.EsiEmployeeContribution).FirstOrDefault();
                            float EsiErC = _context.EsiPfs.Where(x => x.EsiPfID == result).Select(x => (float)x.EsiEmployerContribution).FirstOrDefault();
                            float EpfEC = _context.EsiPfs.Where(x => x.EsiPfID == result).Select(x => (float)x.EpfEmployeeContribution).FirstOrDefault();
                            float EpfErC = _context.EsiPfs.Where(x => x.EsiPfID == result).Select(x => (float)x.EpfEmployerContribution).FirstOrDefault();
                            float EpsErC = _context.EsiPfs.Where(x => x.EsiPfID == result).Select(x => (float)x.EpsEmployerContribution).FirstOrDefault();

                            if (em == "ESI & EPF")
                            {
                                if (float.TryParse(s.Gross.ToString(), out float G1))
                                {
                                    esiEmployee = G1 * (EsiEC / 100);
                                    esiEmployer = G1 * (EsiErC / 100);
                                }

                                co.EsiEmployeeContribution = Math.Round(esiEmployee);
                                co.EsiEmployerContribution = Math.Round(esiEmployer);

                                if (s.BasicPay > 15000)
                                {
                                    bas = 15000;
                                }
                                else
                                {
                                    if (float.TryParse(s.BasicPay.ToString(), out float bas1))
                                    {
                                        bas = bas1;
                                    }

                                }
                                pfEmployee = bas * (EpfEC / 100);
                                pfEmployer = bas * (EpfErC / 100);
                                eps = bas * (EpsErC / 100);
                            }

                            else if (em == "Only EPF")
                            {
                                if (s.BasicPay > 15000)
                                {
                                    bas = 15000;
                                }
                                else
                                {
                                    if (float.TryParse(s.BasicPay.ToString(), out float bas1))
                                    {
                                        bas = bas1;
                                    }

                                }

                                pfEmployee = bas * (EpfEC / 100);
                                pfEmployer = bas * (EpfErC / 100);
                                eps = bas * (EpsErC / 100);
                            }
                            double deduction = attendance.PenaltyDeductionAmount + attendance.MessDeductionAmount + attendance.Advance1Amount + attendance.Advance2Amount + attendance.OtherDeductions + attendance.TDS + pfEmployee + esiEmployee;

                            co.Deductions = Math.Round(deduction);
                            s.NetPay = (double)Math.Round((decimal)s.Gross - (decimal)deduction);
                            co.NetPayBeforeCeiling = s.NetPay;
                            //co.NetPayAfterCeiling = (double)Math.Ceiling((decimal)co.NetPayBeforeCeiling);
                            if (co.NetPayBeforeCeiling % 10 > 5)
                            {
                                co.NetPayAfterCeiling = (double)Math.Round((decimal)(co.NetPayBeforeCeiling + 5) / 10) * 10;
                            }
                            else if(co.NetPayBeforeCeiling % 10 > 0)
                            {
                                co.NetPayAfterCeiling = (double)Math.Round((decimal)(co.NetPayBeforeCeiling + 9) / 10) * 10;
                            }
                            else co.NetPayAfterCeiling = (double)Math.Round((decimal)co.NetPayBeforeCeiling);
                            //if (float.TryParse(deduction.ToString(), out float d))
                            //{
                            //    deductions = d;
                            //}
                            //co.GrossPay = (double)Math.Round((decimal)s.Gross);
                            
                            if(co.BalanceCL == null)
                            {
                                co.BalanceCL = 0;
                            }
                            if( co.BalanceSL == null)
                            {
                                co.BalanceSL = 0;
                            }
                           
                            if (co.PreMonthsConsumedCL == null)
                            {
                                co.PreMonthsConsumedCL = 0;
                            }
                            else
                            {
                                List<double> attCL = _context.Attendances.Where(x => x.EmpCode == s.EmpCode && x.Month != MonthYear).Select(x => x.ClDays).ToList();
                                foreach (var val in attCL)
                                {
                                    co.PreMonthsConsumedCL = co.PreMonthsConsumedCL + val;
                                 }
                            }
                            if (co.PreMonthsConsumedSL == null)
                            {
                                co.PreMonthsConsumedSL = 0;
                            }
                            else { 
                                List<double> attSL = _context.Attendances.Where(x => x.EmpCode == s.EmpCode && x.Month != MonthYear).Select(x => x.SlDays).ToList();
                                foreach (var val in attSL)
                                {
                                    co.PreMonthsConsumedSL = co.PreMonthsConsumedSL + val;
                                }
                            }
                            //co.PreMonthsConsumedCL = (double)Math.Round((decimal)co.PreMonthsConsumedCL - (decimal)co.CurrentCL);
                            //co.PreMonthsConsumedSL = (double)Math.Round((decimal)co.PreMonthsConsumedSL - (decimal)co.CurrentSL);
                            co.BalanceCL = (double)((decimal)co.TotalAllowedCL - ((decimal)co.PreMonthsConsumedCL + (decimal)co.CurrentCL));
                            co.BalanceSL = (double)((decimal)co.TotalAllowedSL - ((decimal)co.PreMonthsConsumedSL + (decimal)co.CurrentSL));
                            co.ActualBasic = Convert.ToDouble(Math.Round((decimal)(0.64 * s.GrossPay)));
                            co.ActualHRA = (double)Math.Round(Convert.ToDecimal(s.GrossPay - co.ActualBasic));
                            co.ActualNetPay = (double)Math.Round((decimal)co.ActualBasic + (decimal)co.ActualHRA);
                            co.ActualIncentive1 = 0;
                            co.ActualIncentive2 = 0;

                            co.ActualOtherEarnings = 0;
                            co.ActualSalary = (double)Math.Round(Convert.ToDecimal(co.BasicPay + co.ActualHRA + co.ActualIncentive1 + co.ActualIncentive2 + co.ActualOtherEarnings));
                            //if (float.TryParse((s.Gross - attendance.TDS).ToString(), out float Gross1))
                            //{
                            //    GrossAfterTDS = Gross1;
                            //}
                            co.CurrentMonthCompOffToBeAdded = 0;
                            if (co.ClosingBalanceAdvance1 == null)
                            {
                                co.ClosingBalanceAdvance1 = 0;
                            }
                            if (co.ClosingBalanceAdvance2 == null)
                            {
                                co.ClosingBalanceAdvance2 = 0;
                            }
                            if(co.OpeningAdvance1 == null)
                            {
                                co.OpeningAdvance1 = 0;
                            }
                            if ( co.OpeningAdvance2 == null)
                            {
                                co.OpeningAdvance2 = 0;
                            }
                            if( co.ClosingBalanceAdvance1 == null)
                            {
                                co.ClosingBalanceAdvance1 = 0;
                            }
                            if(co.ClosingBalanceAdvance2 == null)
                            {
                                co.ClosingBalanceAdvance2= 0;
                            }
                            if(co.OpeningBalanceCompOff == null)
                            {
                                co.OpeningBalanceCompOff = 0;
                            }
                            if( co.BalanceCompOff == null)
                            {
                                co.BalanceCompOff = 0;
                            }
                            else
                            {
                                co.OpeningBalanceCompOff = co.BalanceCompOff;
                            }
                            if (c != null)
                            {
                                foreach (var c1 in c)
                                {
                                    if (c1.HeadOpfAccount == "Salary Advance1" && c1.Value != "0" && c1.Month == MonthYear)
                                    {
                                        co.OpeningAdvance1 = (double)Math.Round((decimal)co.ClosingBalanceAdvance1 + Convert.ToDecimal(c1.Value));
                                        co.ClosingBalanceAdvance1 = co.OpeningAdvance1;
                                    }

                                   
                                    if (c1.HeadOpfAccount == "Salary Advance2" && c1.Value != "0" && c1.Month == MonthYear)
                                    {
                                        co.OpeningAdvance2 = (double)Math.Round((decimal)co.ClosingBalanceAdvance2 + Convert.ToDecimal(c1.Value));
                                        co.ClosingBalanceAdvance2 = co.OpeningAdvance2;
                                    }
                                    
                                    if (c1.HeadOpfAccount == "Compensation Leave" && c1.Value != "0" && c1.Month == MonthYear)
                                    {
                                       
                                        co.CurrentMonthCompOffToBeAdded = Convert.ToDouble(c1.Value);
                                        co.BalanceCompOff = co.BalanceCompOff + Convert.ToDouble(c1.Value);
                                    }
                                    
                                }
                            }
                            if (co.ClosingBalanceAdvance1 != 0)
                            {
                                co.ClosingBalanceAdvance1 = (double)Math.Round((decimal)co.ClosingBalanceAdvance1 - (decimal)attendance.Advance1Amount);
                                co.DeductedAdvance1 = (double)Math.Round((decimal)co.OpeningAdvance1 - (decimal)co.ClosingBalanceAdvance1 - (decimal)attendance.Advance1Amount);
                            }
                            if (co.ClosingBalanceAdvance2 != 0)
                            {
                                co.ClosingBalanceAdvance2 = (double)Math.Round((decimal)co.ClosingBalanceAdvance2 - (decimal)attendance.Advance2Amount);
                                co.DeductedAdvance2 = (double)Math.Round((decimal)co.OpeningAdvance2 - (decimal)co.ClosingBalanceAdvance2 - (decimal)attendance.Advance2Amount);
                            }

                                //if (attendance.LOPDays != 0 && attendance.NationalHolidays != 0)
                                //{
                                //    attendance.LOPDays = attendance.LOPDays - attendance.NationalHolidays;
                                //}
                                //if (co.BalanceCompOff == null)
                                //{
                                //    co.BalanceCompOff = 0;
                                //}
                                co.CurrentMonthCompOffConsumed = attendance.CompOffDays;

                                //if (attendance.CompOffDays != 0)
                                //{
                                //    //attendance.LOPDays = attendance.LOPDays - attendance.CompOffDays;
                                //    if (c != null)
                                //    {
                                //        foreach (var c1 in c)
                                //        {
                                //            if (c1.HeadOpfAccount == "Compensation Leave" && c1.Value != "0")
                                //            {
                                //                co.OpeningBalanceCompOff = co.BalanceCompOff + Convert.ToDouble(c1.Value);
                                //                co.CurrentMonthCompOffToBeAdded = Convert.ToDouble(c1.Value);
                                //            }
                                //        }
                                //    }
                                //}

                                //if (co.OpeningBalanceCompOff != 0)
                                //{
                                co.BalanceCompOff = (double)( (decimal)co.OpeningBalanceCompOff + (decimal)co.CurrentMonthCompOffToBeAdded - (decimal)co.CurrentMonthCompOffConsumed);
                                //}

                                //else
                                //{
                                //    co.BalanceCompOff = 0;
                                //}
                                //if (co.BalanceCompOff != 0)
                                //{ 
                                //co.BalanceCompOff = (int)Math.Round((decimal)co.BalanceCompOff -(decimal)co.CurrentMonthCompOffConsumed);
                                //}

                                //TotalSalary = (float)GrossAfterTDS + (float)OTPayment - (float)((attendance.LOPDays * 24 * 60) * SalPerMinute) - deductions;



                                co.EPSEmployerContribution = Math.Round(eps);
                                co.PfEmployeeContribution = Math.Round(pfEmployee);
                                co.PfEmployerContribution = Math.Round(pfEmployer);
                                //  co.Deductions = Math.Round(deductions + pfEmployee + esiEmployee + attendance.TDS);
                                //  s.NetPay = Math.Round(TotalSalary - pfEmployee - esiEmployee);

                                //  co.NetPayBeforeCeiling = (double)Math.Round((decimal)(s.NetPay));
                                s.CTC = (double)Math.Round(Convert.ToDecimal(co.GrossPay + esiEmployer + pfEmployer + eps));

                                co.CTC = (double)Math.Round((decimal)s.CTC);
                                s.LastEditedDate = (DateTime.Now).ToString();
                            }
                        }
                    
                    if (i == 1)
                    {
                        _context.Add(co);
                    }
                    else
                    {
                        _context.Update(co);
                    }
                    _context.Update(s);
                    await _context.SaveChangesAsync();

                }

                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");

            }
        }
    
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeSalary/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
