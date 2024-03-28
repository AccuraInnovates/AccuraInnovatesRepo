using Accura_Innovatives.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Humanizer;
using ClosedXML.Excel;


namespace Accura_Innovatives.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeMaster1Context _context;
        private readonly IWebHostEnvironment hostingenvironment;
        public HomeController(ILogger<HomeController> logger, EmployeeMaster1Context context, IWebHostEnvironment hc)
        {
            _logger = logger;
            _context = context;
            hostingenvironment = hc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BulkUpload()
        {
            return View();
        }
        public IActionResult ImageUpload()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult CompOffAdvances()
        {
            return View();
        }
        public IActionResult YearlyLeave()
        {
            return View();
        }
        public IActionResult SalaryDetailsUpload()
        {
            return View();
        }

        public IActionResult ConsolidatedModel()
        {
            return View();
        }
        public IActionResult ExportEsiReport()
        {
            return View();
        }

        public IActionResult ExportEPFReport()
        {
            return View();
        }

        public IActionResult ExportPdfReport()
        {
            return View();
        }

        public IActionResult SalaryProcessMaster()
        {
            return View();
        }

        public IActionResult CalculateSalary()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string code)
        {

            int id = Convert.ToInt32(code);
            var employeeMaster1Context = _context.EmployeeMasterData1s.Include(e => e.BloodGrpNavigation).Include(e => e.Contractor).Include(e => e.EmpCtgNavigation).Include(e => e.EmpRoleNavigation).Include(e => e.EsiEpfEligibilityNavigation).Include(e => e.HighQuali2Navigation).Include(e => e.HighQualiNavigation).Include(e => e.NationalityNavigation).Include(e => e.PerBnkNameNavigation).Include(e => e.SalBankNameNavigation).Include(e => e.SalaryPaidByNavigation);

            var emp = await _context.EmployeeMasterData1s.FindAsync(id);
            if (emp == null)
            {
                ViewBag.ErrorMessage = "Employee Code Not Found";
                return View("ImageUpload");
            }
            ViewData["BloodGrp"] = new SelectList(_context.BloodGrpMs, "BloodGrp", "BloodGrp", emp.BloodGrp);
            ViewData["ContractorId"] = new SelectList(_context.ContractorMs, "ContractorId", "ContractorId", emp.ContractorId);
            ViewData["EmpCtg"] = new SelectList(_context.CategoryMs, "CtgCode", "CtgCode", emp.EmpCtg);
            ViewData["EmpRole"] = new SelectList(_context.UserRightsMs, "EmpRole", "EmpRole", emp.EmpRole);
            ViewData["EsiEpfEligibility"] = new SelectList(_context.LegalMs, "LegName", "LegName", emp.EsiEpfEligibility);
            ViewData["HighQuali2"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName", emp.HighQuali2);
            ViewData["HighQuali"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName", emp.HighQuali);
            ViewData["Nationality"] = new SelectList(_context.NationalityMs, "Nationality", "Nationality", emp.Nationality);
            ViewData["PerBnkName"] = new SelectList(_context.BankMs, "BankName", "BankName", emp.PerBnkName);
            ViewData["SalBankName"] = new SelectList(_context.BankMs, "BankName", "BankName", emp.SalBankName);
            ViewData["SalaryPaidBy"] = new SelectList(_context.CompanyMs, "CompanyName", "CompanyName", emp.SalaryPaidBy);

            EmployeeViewModel e = new EmployeeViewModel();
            e.EmpCtg = emp.EmpCtg;
            e.EmpCode = emp.EmpCode;
            e.EmpName = emp.EmpName;
            e.EmpAadharName = emp.EmpAadharName;
            e.EmpPanName = emp.EmpPanName;
            e.EmpCertifName = emp.EmpCertifName;
            e.EmpNameInBank = emp.EmpNameInBank;
            e.EmpBcardName = emp.EmpBcardName;
            if (emp.EmpPhoto != null && emp.EmpPhoto != "")
            {
                string path = "./wwwroot/Images/" + emp.EmpPhoto;
                using (var stream = System.IO.File.OpenRead(path))
                {
                    e.ProfilePhoto = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.Gender = emp.Gender;
            e.BloodGrp = emp.BloodGrp;
            e.AadharNo = emp.AadharNo;
            if (emp.AadharCard != null && emp.AadharCard != "")
            {
                string path1 = "./wwwroot/Images/" + emp.AadharCard;
                using (var stream = System.IO.File.OpenRead(path1))
                {
                    e.AadharCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.PanNo = emp.PanNo;
            if (emp.PanCard != null && emp.PanCard != "")
            {
                string path2 = "./wwwroot/Images/" + emp.PanCard;
                using (var stream = System.IO.File.OpenRead(path2))
                {
                    e.PanCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }

            e.DrvLinNo = emp.DrvLinNo;
            e.DrvLinVal = emp.DrvLinVal;
            if (emp.DrvLinCard != null && emp.DrvLinCard != "")
            {
                string path3 = "./wwwroot/Images/" + emp.DrvLinCard;
                using (var stream = System.IO.File.OpenRead(path3))
                {
                    e.DrvLinCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.PassportNo = emp.PassportNo;
            e.PassportVal = emp.PassportVal;
            if (emp.PassportCard != null && emp.PassportCard != "")
            {
                string path4 = "./wwwroot/Images/" + emp.PassportCard;
                using (var stream = System.IO.File.OpenRead(path4))
                {
                    e.PassportCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.AadharDob = emp.AadharDob;
            e.Dob = emp.Dob;
            e.PresentAddressLine1 = emp.PresentAddressLine1;
            e.PresentAddressLine2 = emp.PresentAddressLine2;
            e.PresentAddressCity = emp.PresentAddressCity;
            e.PresentAddressState = emp.PresentAddressState;
            e.PresentAddressZipcode = emp.PresentAddressZipcode;
            e.PermAddressLine1 = emp.PermAddressLine1;
            e.PermAddressLine2 = emp.PermAddressLine2;
            e.PermAddressCity = emp.PermAddressCity;
            e.PermAddressState = emp.PermAddressState;
            e.PermAddressZipcode = emp.PermAddressZipcode;
            e.CommAddressLine1 = emp.CommAddressLine1;
            e.CommAddressLine2 = emp.CommAddressLine2;
            e.CommAddressCity = emp.CommAddressCity;
            e.CommAddressState = emp.CommAddressState;
            e.CommAddressZipcode = emp.CommAddressZipcode;
            e.Nationality = emp.Nationality;
            e.MaritalStatus = emp.MaritalStatus;
            e.PerEmail = emp.PerEmail;
            e.PerMobile = emp.PerMobile;
            e.PerAccNo = emp.PerAccNo;
            e.PerBnkName = emp.PerBnkName;
            e.PerBnkIfsc = emp.PerBnkIfsc;
            e.PerBnkBranch = emp.PerBnkBranch;
            e.FamMemName1 = emp.FamMemName1;
            e.FamMemRel1 = emp.FamMemRel1;
            e.FamMemContact1 = emp.FamMemContact1;
            e.FamMemName2 = emp.FamMemName2;
            e.FamMemRel2 = emp.FamMemRel2;
            e.FamMemContact2 = emp.FamMemContact2;
            e.FamMemName3 = emp.FamMemName3;
            e.FamMemRel3 = emp.FamMemRel3;
            e.FamMemContact3 = emp.FamMemContact3;
            e.FamMemName4 = emp.FamMemName4;
            e.FamMemRel4 = emp.FamMemRel4;
            e.FamMemContact4 = emp.FamMemContact4;
            e.FamMemName5 = emp.FamMemName5;
            e.FamMemRel5 = emp.FamMemRel5;
            e.FamMemContact5 = emp.FamMemContact5;
            e.EmerContactName = emp.EmerContactName;
            e.EmerContactRel = emp.EmerContactRel;
            e.EmerContactNo = emp.EmerContactNo;
            e.HighQuali = emp.HighQuali;
            e.HighQualiInstituteName = emp.HighQualiInstituteName;
            e.HighQualiMark = emp.HighQualiMark;
            e.HighQualiPassYear = emp.HighQualiPassYear;
            if (emp.HighQualiCerf1 != null && emp.HighQualiCerf1 != "")
            {
                string path5 = "./wwwroot/Images/" + emp.HighQualiCerf1;
                using (var stream = System.IO.File.OpenRead(path5))
                {
                    e.HighQualiCerf1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.HighQuali2 = emp.HighQuali2;
            e.HighQualiInstituteName2 = emp.HighQualiInstituteName2;
            e.HighQualiMark2 = emp.HighQualiMark2;
            e.HighQualiPassYear2 = emp.HighQualiPassYear2;
            if (emp.HighQualiCerf2 != null && emp.HighQualiCerf2 != "")
            {
                string path6 = "./wwwroot/Images/" + emp.HighQualiCerf2;
                using (var stream = System.IO.File.OpenRead(path6))
                {
                    e.HighQualiCerf2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.HscSchoolName = emp.HscSchoolName;
            e.HscMark = emp.HscMark;
            e.HscPassYear = emp.HscPassYear;
            if (emp.HscCerf != null && emp.HscCerf != "")
            {
                string path7 = "./wwwroot/Images/" + emp.HscCerf;
                using (var stream = System.IO.File.OpenRead(path7))
                {
                    e.HscCerf = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.SslcSchoolName = emp.SslcSchoolName;
            e.SslcMark = emp.SslcMark;
            e.SslcPassYear = emp.SslcPassYear;
            if (emp.SslcCerf != null && emp.SslcCerf != "")
            {
                string path8 = "./wwwroot/Images/" + emp.SslcCerf;
                using (var stream = System.IO.File.OpenRead(path8))
                {
                    e.SslcCerf = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.OtherCerfName1 = emp.OtherCerfName1;
            e.OtherCerfInstitute1 = emp.OtherCerfInstitute1;
            e.OtherCerfMark1 = emp.OtherCerfMark1;
            e.OtherCerfDuration1 = emp.OtherCerfDuration1;
            e.OtherCerfPassYear1 = emp.OtherCerfPassYear1;
            if (emp.OtherCerf1 != null && emp.OtherCerf1 != "")
            {
                string path9 = "./wwwroot/Images/" + emp.OtherCerf1;
                using (var stream = System.IO.File.OpenRead(path9))
                {
                    e.OtherCerf1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.OtherCerfName2 = emp.OtherCerfName2;
            e.OtherCerfInstitute2 = emp.OtherCerfInstitute2;
            e.OtherCerfMark2 = emp.OtherCerfMark2;
            e.OtherCerfDuration2 = emp.OtherCerfDuration2;
            e.OtherCerfPassYear2 = emp.OtherCerfPassYear2;
            if (emp.OtherCerf2 != null && emp.OtherCerf2 != "")
            {
                string path10 = "./wwwroot/Images/" + emp.OtherCerf2;
                using (var stream = System.IO.File.OpenRead(path10))
                {
                    e.OtherCerf2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.OtherCerfName3 = emp.OtherCerfName3;
            e.OtherCerfInstitute3 = emp.OtherCerfInstitute3;
            e.OtherCerfMark3 = emp.OtherCerfMark3;
            e.OtherCerfDuration3 = emp.OtherCerfDuration3;
            e.OtherCerfPassYear3 = emp.OtherCerfPassYear3;
            if (emp.OtherCerf3 != null && emp.OtherCerf3 != "")
            {
                string path11 = "./wwwroot/Images/" + emp.OtherCerf3;
                using (var stream = System.IO.File.OpenRead(path11))
                {
                    e.OtherCerf3 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.ExpYears = emp.ExpYears;
            e.PreWorkCmp1 = emp.PreWorkCmp1;
            e.PreWorkCmpSdt1 = emp.PreWorkCmpSdt1;
            e.PreWorkCmpEdt1 = emp.PreWorkCmpEdt1;
            if (emp.PreWorkCmpDoc1 != null && emp.PreWorkCmpDoc1 != "")
            {
                string path12 = "./wwwroot/Images/" + emp.PreWorkCmpDoc1;
                using (var stream = System.IO.File.OpenRead(path12))
                {
                    e.PreWorkCmpDoc1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.PreWorkCmp2 = emp.PreWorkCmp2;
            e.PreWorkCmpSdt2 = emp.PreWorkCmpSdt2;
            e.PreWorkCmpEdt2 = emp.PreWorkCmpEdt2;
            if (emp.PreWorkCmpDoc2 != null && emp.PreWorkCmpDoc2 != "")
            {
                string path13 = "./wwwroot/Images/" + emp.PreWorkCmpDoc2;
                using (var stream = System.IO.File.OpenRead(path13))
                {
                    e.PreWorkCmpDoc2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.PreWorkCmp3 = emp.PreWorkCmp3;
            e.PreWorkCmpSdt3 = emp.PreWorkCmpSdt3;
            e.PreWorkCmpEdt3 = emp.PreWorkCmpEdt3;
            if (emp.PreWorkCmpDoc3 != null && emp.PreWorkCmpDoc3 != "")
            {
                string path14 = "./wwwroot/Images/" + emp.PreWorkCmpDoc3;
                using (var stream = System.IO.File.OpenRead(path14))
                {
                    e.PreWorkCmpDoc3 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.PreWorkCmp4 = emp.PreWorkCmp4;
            e.PreWorkCmpSdt4 = emp.PreWorkCmpSdt4;
            e.PreWorkCmpEdt4 = emp.PreWorkCmpEdt4;
            if (emp.PreWorkCmpDoc4 != null && emp.PreWorkCmpDoc4 != "")
            {
                string path15 = "./wwwroot/Images/" + emp.PreWorkCmpDoc4;
                using (var stream = System.IO.File.OpenRead(path15))
                {
                    e.PreWorkCmpDoc4 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.PreWorkCmp5 = emp.PreWorkCmp5;
            e.PreWorkCmpSdt5 = emp.PreWorkCmpSdt5;
            e.PreWorkCmpEdt5 = emp.PreWorkCmpEdt5;
            if (emp.PreWorkCmpDoc5 != null && emp.PreWorkCmpDoc5 != "")
            {
                string path16 = "./wwwroot/Images/" + emp.PreWorkCmpDoc5;
                using (var stream = System.IO.File.OpenRead(path16))
                {
                    e.PreWorkCmpDoc5 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.WorkExBreak1 = emp.WorkExBreak1;
            e.WorkExBreakSdt1 = emp.WorkExBreakSdt1;
            e.WorkExBreakEdt1 = emp.WorkExBreakEdt1;
            e.WorkExBreak2 = emp.WorkExBreak2;
            e.WorkExBreakSdt2 = emp.WorkExBreakSdt2;
            e.WorkExBreakEdt2 = emp.WorkExBreakEdt2;
            e.WorkExBreak3 = emp.WorkExBreak3;
            e.WorkExBreakSdt3 = emp.WorkExBreakSdt3;
            e.WorkExBreakEdt3 = emp.WorkExBreakEdt3;
            e.WorkExBreak4 = emp.WorkExBreak4;
            e.WorkExBreakSdt4 = emp.WorkExBreakSdt4;
            e.WorkExBreakEdt4 = emp.WorkExBreakEdt4;
            e.PreWorkCmpCtc = emp.PreWorkCmpCtc;
            e.PreWorkCmpEpfStatus = emp.PreWorkCmpEpfStatus;
            e.PreWorkCmpEsiStatus = emp.PreWorkCmpEsiStatus;
            e.EmpDept = emp.EmpDept;
            e.EmpDesignation = emp.EmpDesignation;
            e.EmpRole = emp.EmpRole;
            e.EmpDoj = emp.EmpDoj;
            e.EmpOnboardCtg = emp.EmpOnboardCtg;
            e.ContractorId = emp.ContractorId;
            e.SalaryPaidBy = emp.SalaryPaidBy;
            e.PaymentMode = emp.PaymentMode;
            e.SalAccEligibility = emp.SalAccEligibility;
            e.SalAccNo = emp.SalAccNo;
            e.SalBankName = emp.SalBankName;
            e.SalBankIfsc = emp.SalBankIfsc;
            e.SalBankBranch = emp.SalBankBranch;
            e.SalBenfCode = emp.SalBenfCode;
            e.EsiEpfEligibility = emp.EsiEpfEligibility;
            e.Form11Willingness = emp.Form11Willingness;
            e.Form11Eligibility = emp.Form11Eligibility;
            e.Form11No = emp.Form11No;
            if (emp.Form11Doc1 != null && emp.Form11Doc1 != "")
            {
                string path17 = "./wwwroot/Images/" + emp.Form11Doc1;
                using (var stream = System.IO.File.OpenRead(path17))
                {
                    e.Form11Doc1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            if (emp.Form11Doc2 != null && emp.Form11Doc2 != "")
            {
                string path18 = "./wwwroot/Images/" + emp.Form11Doc2;
                using (var stream = System.IO.File.OpenRead(path18))
                {
                    e.Form11Doc2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.EsiJdt = emp.EsiJdt;
            e.EpfJdt = emp.EpfJdt;
            e.EsiNo = emp.EsiNo;
            e.EsiNomineeName = emp.EsiNomineeName;
            e.EpfNo = emp.EpfNo;
            e.EpfNomineeName = emp.EpfNomineeName;
            e.SalCriteria = emp.SalCriteria;
            e.EmpSal = emp.EmpSal;
            e.EmpWageShift = emp.EmpWageShift;
            e.EmpWageHr = emp.EmpWageHr;
            e.EmpWageDay = emp.EmpWageDay;
            e.RoomRent = emp.RoomRent;
            e.MessDeduction = emp.MessDeduction;
            e.OffEmail = emp.OffEmail;
            e.OffMobile = emp.OffMobile;
            e.AssetEligibility = emp.AssetEligibility;
            e.Assets = emp.Assets;
            e.OnboardVia = emp.OnboardVia;
            e.OnboardRefNo = emp.OnboardRefNo;
            e.OnboardRefName1 = emp.OnboardRefName1;
            e.OnboardRefName2 = emp.OnboardRefName2;
            if (emp.Attachment1 != null && emp.Attachment1 != "")
            {
                string path19 = "./wwwroot/Images/" + emp.Attachment1;
                using (var stream = System.IO.File.OpenRead(path19))
                {
                    e.Attachment1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            if (emp.Attachment2 != null && emp.Attachment2 != "")
            {
                string path20 = "./wwwroot/Images/" + emp.Attachment2;
                using (var stream = System.IO.File.OpenRead(path20))
                {
                    e.Attachment2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            if (emp.Attachment3 != null && emp.Attachment3 != "")
            {

                string path21 = "./wwwroot/Images/" + emp.Attachment3;
                using (var stream = System.IO.File.OpenRead(path21))
                {
                    e.Attachment3 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            if (emp.Attachment4 != null && emp.Attachment4 != "")
            {
                string path22 = "./wwwroot/Images/" + emp.Attachment4;
                using (var stream = System.IO.File.OpenRead(path22))
                {
                    e.Attachment4 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            if (emp.Attachment5 != null && emp.Attachment5 != "")
            {
                string path23 = "./wwwroot/Images/" + emp.Attachment5;
                using (var stream = System.IO.File.OpenRead(path23))
                {
                    e.Attachment5 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.AadharVerf = emp.AadharVerf;
            if (emp.AadharVerfProof != null && emp.AadharVerfProof != "")
            {
                string path24 = "./wwwroot/Images/" + emp.AadharVerfProof;
                using (var stream = System.IO.File.OpenRead(path24))
                {
                    e.AadharVerfProof = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }

            e.CerfVerf = emp.CerfVerf;
            e.OriginalDocSubmission = emp.OriginalDocSubmission;
            e.OriginalDocList = emp.OriginalDocList;
            e.OriginalDocAck = emp.OriginalDocAck;
            e.OriginalDocAckNo = emp.OriginalDocAckNo;
            if (emp.OriginalDocAckProof != null && emp.OriginalDocAckProof != "")
            {
                string path25 = "./wwwroot/Images/" + emp.OriginalDocAckProof;
                using (var stream = System.IO.File.OpenRead(path25))
                {
                    e.OriginalDocAckProof = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }
            e.EmpCurrentStatus = emp.EmpCurrentStatus;
            e.EmpDoe = emp.EmpDoe;
            e.OriginalDocHandover = emp.OriginalDocHandover;
            e.OriginalDocAckBack = emp.OriginalDocAckBack;
            e.ReleavedReason = emp.ReleavedReason;
            e.EmpRejoinDate = emp.EmpRejoinDate;
            e.ReportingTo = emp.ReportingTo;
            e.EmpOldCode = emp.EmpOldCode;
            e.EmpCreateDate = emp.EmpCreateDate;
            if (emp.TcCard != null && emp.TcCard != "")
            {
                string path26 = "./wwwroot/Images/" + emp.TcCard;
                using (var stream = System.IO.File.OpenRead(path26))
                {
                    e.TcCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                }
            }

            return View("ImageUpload",e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveImage(EmployeeViewModel emp)
        {

            try
            {
                string filename = "";


            if (emp.ProfilePhoto != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + emp.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadFolder, filename);
                emp.ProfilePhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.ProfilePhoto = emp.ProfilePhoto;
            }
            string aadharFilename = "";
            if (emp.AadharCard != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                aadharFilename = Guid.NewGuid().ToString() + "_" + emp.AadharCard.FileName;
                string filePath = Path.Combine(uploadFolder, aadharFilename);
                emp.AadharCard.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.AadharCard = null;
            }
            string panFilename = "";
            if (emp.PanCard != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                panFilename = Guid.NewGuid().ToString() + "_" + emp.PanCard.FileName;
                string filePath = Path.Combine(uploadFolder, panFilename);
                emp.PanCard.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.PanCard = null;
            }
            string drvFilename = "";
            if (emp.DrvLinCard != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                drvFilename = Guid.NewGuid().ToString() + "_" + emp.DrvLinCard.FileName;
                string filePath = Path.Combine(uploadFolder, drvFilename);
                emp.DrvLinCard.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.DrvLinCard = null;
            }
            string passFilename = "";
            if (emp.PassportCard != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                passFilename = Guid.NewGuid().ToString() + "_" + emp.PassportCard.FileName;
                string filePath = Path.Combine(uploadFolder, passFilename);
                emp.PassportCard.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else { emp.PassportCard = null; }
            string PGFilename = "";
            if (emp.HighQualiCerf1 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                PGFilename = Guid.NewGuid().ToString() + "_" + emp.HighQualiCerf1.FileName;
                string filePath = Path.Combine(uploadFolder, PGFilename);
                emp.HighQualiCerf1.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.HighQualiCerf1 = null;
            }
            string UGFilename = "";
            if (emp.HighQualiCerf2 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                UGFilename = Guid.NewGuid().ToString() + "_" + emp.HighQualiCerf2.FileName;
                string filePath = Path.Combine(uploadFolder, UGFilename);
                emp.HighQualiCerf2.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.HighQualiCerf2 = null;
            }
            string HscFilename = "";
            if (emp.HscCerf != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                HscFilename = Guid.NewGuid().ToString() + "_" + emp.HscCerf.FileName;
                string filePath = Path.Combine(uploadFolder, HscFilename);
                emp.HscCerf.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.HscCerf = null;
            }
            string SslcFilename = "";
            if (emp.SslcCerf != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                SslcFilename = Guid.NewGuid().ToString() + "_" + emp.SslcCerf.FileName;
                string filePath = Path.Combine(uploadFolder, SslcFilename);
                emp.SslcCerf.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.SslcCerf = null;
            }
            string Doc1Filename = "";
            if (emp.Form11Doc1 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Doc1Filename = Guid.NewGuid().ToString() + "_" + emp.Form11Doc1.FileName;
                string filePath = Path.Combine(uploadFolder, Doc1Filename);
                emp.Form11Doc1.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.Form11Doc1 = null;
            }
            string Doc2Filename = "";
            if (emp.Form11Doc2 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Doc2Filename = Guid.NewGuid().ToString() + "_" + emp.Form11Doc2.FileName;
                string filePath = Path.Combine(uploadFolder, Doc2Filename);
                emp.Form11Doc2.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.Form11Doc2 = null;
            }
            string OtCer1Filename = "";
            if (emp.OtherCerf1 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                OtCer1Filename = Guid.NewGuid().ToString() + "_" + emp.OtherCerf1.FileName;
                string filePath = Path.Combine(uploadFolder, OtCer1Filename);
                emp.OtherCerf1.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.OtherCerf1 = null;
            }
            string OtCer2Filename = "";
            if (emp.OtherCerf2 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                OtCer2Filename = Guid.NewGuid().ToString() + "_" + emp.OtherCerf2.FileName;
                string filePath = Path.Combine(uploadFolder, OtCer2Filename);
                emp.OtherCerf2.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else { emp.OtherCerf2 = null; }
            string OtCer3Filename = "";
            if (emp.OtherCerf3 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                OtCer3Filename = Guid.NewGuid().ToString() + "_" + emp.OtherCerf3.FileName;
                string filePath = Path.Combine(uploadFolder, OtCer3Filename);
                emp.OtherCerf3.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else { emp.OtherCerf3 = null; }
            string PreWorkDoc1Filename = "";
            if (emp.PreWorkCmpDoc1 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                PreWorkDoc1Filename = Guid.NewGuid().ToString() + "_" + emp.PreWorkCmpDoc1.FileName;
                string filePath = Path.Combine(uploadFolder, PreWorkDoc1Filename);
                emp.PreWorkCmpDoc1.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else { emp.PreWorkCmpDoc1 = null; }
            string PreWorkDoc2Filename = "";
            if (emp.PreWorkCmpDoc2 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                PreWorkDoc2Filename = Guid.NewGuid().ToString() + "_" + emp.PreWorkCmpDoc2.FileName;
                string filePath = Path.Combine(uploadFolder, PreWorkDoc2Filename);
                emp.PreWorkCmpDoc2.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.PreWorkCmpDoc2 = null;
            }
            string PreWorkDoc3Filename = "";
            if (emp.PreWorkCmpDoc3 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                PreWorkDoc3Filename = Guid.NewGuid().ToString() + "_" + emp.PreWorkCmpDoc3.FileName;
                string filePath = Path.Combine(uploadFolder, PreWorkDoc3Filename);
                emp.PreWorkCmpDoc3.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.PreWorkCmpDoc3 = null;
            }
            string PreWorkDoc4Filename = "";
            if (emp.PreWorkCmpDoc4 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                PreWorkDoc4Filename = Guid.NewGuid().ToString() + "_" + emp.PreWorkCmpDoc4.FileName;
                string filePath = Path.Combine(uploadFolder, PreWorkDoc4Filename);
                emp.PreWorkCmpDoc4.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.PreWorkCmpDoc4 = null;
            }
            string PreWorkDoc5Filename = "";
            if (emp.PreWorkCmpDoc5 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                PreWorkDoc5Filename = Guid.NewGuid().ToString() + "_" + emp.PreWorkCmpDoc5.FileName;
                string filePath = Path.Combine(uploadFolder, PreWorkDoc5Filename);
                emp.PreWorkCmpDoc5.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.PreWorkCmpDoc5 = null;
            }
            string Att1Filename = "";
            if (emp.Attachment1 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Att1Filename = Guid.NewGuid().ToString() + "_" + emp.Attachment1.FileName;
                string filePath = Path.Combine(uploadFolder, Att1Filename);
                emp.Attachment1.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.Attachment1 = null;
            }
            string Att2Filename = "";
            if (emp.Attachment2 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Att2Filename = Guid.NewGuid().ToString() + "_" + emp.Attachment2.FileName;
                string filePath = Path.Combine(uploadFolder, Att2Filename);
                emp.Attachment2.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.Attachment2 = null;
            }
            string Att3Filename = "";
            if (emp.Attachment3 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Att3Filename = Guid.NewGuid().ToString() + "_" + emp.Attachment3.FileName;
                string filePath = Path.Combine(uploadFolder, Att3Filename);
                emp.Attachment3.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.Attachment3 = null;
            }
            string Att4Filename = "";
            if (emp.Attachment4 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Att4Filename = Guid.NewGuid().ToString() + "_" + emp.Attachment4.FileName;
                string filePath = Path.Combine(uploadFolder, Att4Filename);
                emp.Attachment4.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else { emp.Attachment4 = null; }
            string Att5Filename = "";
            if (emp.Attachment5 != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                Att5Filename = Guid.NewGuid().ToString() + "_" + emp.Attachment5.FileName;
                string filePath = Path.Combine(uploadFolder, Att5Filename);
                emp.Attachment5.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.Attachment5 = null;
            }
            string AadVerFilename = "";
            if (emp.AadharVerfProof != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                AadVerFilename = Guid.NewGuid().ToString() + "_" + emp.AadharVerfProof.FileName;
                string filePath = Path.Combine(uploadFolder, AadVerFilename);
                emp.AadharVerfProof.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.AadharVerfProof = null;
            }
            string OrgDocFilename = "";
            if (emp.OriginalDocAckProof != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                OrgDocFilename = Guid.NewGuid().ToString() + "_" + emp.OriginalDocAckProof.FileName;
                string filePath = Path.Combine(uploadFolder, OrgDocFilename);
                emp.OriginalDocAckProof.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else
            {
                emp.OriginalDocAckProof = null;
            }
            string TcFilename = "";
            if (emp.TcCard != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                TcFilename = Guid.NewGuid().ToString() + "_" + emp.TcCard.FileName;
                string filePath = Path.Combine(uploadFolder, TcFilename);
                emp.TcCard.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else { emp.TcCard = null; }
            emp.EmpOnboardCtg = emp.EmpCtg;
            EmployeeMasterData1 e = new EmployeeMasterData1
            {
                EmpCtg = emp.EmpCtg,
                EmpCode = emp.EmpCode,
                EmpName = emp.EmpName,
                EmpAadharName = emp.EmpAadharName,
                EmpPanName = emp.EmpPanName,
                EmpCertifName = emp.EmpCertifName,
                EmpNameInBank = emp.EmpNameInBank,
                EmpBcardName = emp.EmpBcardName,
                EmpPhoto = filename,
                Gender = emp.Gender,
                BloodGrp = emp.BloodGrp,
                AadharNo = emp.AadharNo,
                AadharCard = aadharFilename,
                PanNo = emp.PanNo,
                PanCard = panFilename,
                DrvLinNo = emp.DrvLinNo,
                DrvLinVal = emp.DrvLinVal,
                DrvLinCard = drvFilename,
                PassportNo = emp.PassportNo,
                PassportVal = emp.PassportVal,
                PassportCard = passFilename,
                AadharDob = emp.AadharDob,
                Dob = emp.Dob,
                PresentAddressLine1 = emp.PresentAddressLine1,
                PresentAddressLine2 = emp.PresentAddressLine2,
                PresentAddressCity = emp.PresentAddressCity,
                PresentAddressState = emp.PresentAddressState,
                PresentAddressZipcode = emp.PresentAddressZipcode,
                PermAddressLine1 = emp.PermAddressLine1,
                PermAddressLine2 = emp.PermAddressLine2,
                PermAddressCity = emp.PermAddressCity,
                PermAddressState = emp.PermAddressState,
                PermAddressZipcode = emp.PermAddressZipcode,
                CommAddressLine1 = emp.CommAddressLine1,
                CommAddressLine2 = emp.CommAddressLine2,
                CommAddressCity = emp.CommAddressCity,
                CommAddressState = emp.CommAddressState,
                CommAddressZipcode = emp.CommAddressZipcode,
                Nationality = emp.Nationality,
                MaritalStatus = emp.MaritalStatus,
                PerEmail = emp.PerEmail,
                PerMobile = emp.PerMobile,
                PerAccNo = emp.PerAccNo,
                PerBnkName = emp.PerBnkName,
                PerBnkIfsc = emp.PerBnkIfsc,
                PerBnkBranch = emp.PerBnkBranch,
                FamMemName1 = emp.FamMemName1,
                FamMemRel1 = emp.FamMemRel1,
                FamMemContact1 = emp.FamMemContact1,
                FamMemName2 = emp.FamMemName2,
                FamMemRel2 = emp.FamMemRel2,
                FamMemContact2 = emp.FamMemContact2,
                FamMemName3 = emp.FamMemName3,
                FamMemRel3 = emp.FamMemRel3,
                FamMemContact3 = emp.FamMemContact3,
                FamMemName4 = emp.FamMemName4,
                FamMemRel4 = emp.FamMemRel4,
                FamMemContact4 = emp.FamMemContact4,
                FamMemName5 = emp.FamMemName5,
                FamMemRel5 = emp.FamMemRel5,
                FamMemContact5 = emp.FamMemContact5,
                EmerContactName = emp.EmerContactName,
                EmerContactRel = emp.EmerContactRel,
                EmerContactNo = emp.EmerContactNo,
                HighQuali = emp.HighQuali,
                HighQualiInstituteName = emp.HighQualiInstituteName,
                HighQualiMark = emp.HighQualiMark,
                HighQualiPassYear = emp.HighQualiPassYear,
                HighQualiCerf1 = PGFilename,
                HighQuali2 = emp.HighQuali2,
                HighQualiInstituteName2 = emp.HighQualiInstituteName2,
                HighQualiMark2 = emp.HighQualiMark2,
                HighQualiPassYear2 = emp.HighQualiPassYear2,
                HighQualiCerf2 = UGFilename,
                HscSchoolName = emp.HscSchoolName,
                HscMark = emp.HscMark,
                HscPassYear = emp.HscPassYear,
                HscCerf = HscFilename,
                SslcSchoolName = emp.SslcSchoolName,
                SslcMark = emp.SslcMark,
                SslcPassYear = emp.SslcPassYear,
                SslcCerf = SslcFilename,
                OtherCerfName1 = emp.OtherCerfName1,
                OtherCerfInstitute1 = emp.OtherCerfInstitute1,
                OtherCerfMark1 = emp.OtherCerfMark1,
                OtherCerfDuration1 = emp.OtherCerfDuration1,
                OtherCerfPassYear1 = emp.OtherCerfPassYear1,
                OtherCerf1 = OtCer1Filename,
                OtherCerfName2 = emp.OtherCerfName2,
                OtherCerfInstitute2 = emp.OtherCerfInstitute2,
                OtherCerfMark2 = emp.OtherCerfMark2,
                OtherCerfDuration2 = emp.OtherCerfDuration2,
                OtherCerfPassYear2 = emp.OtherCerfPassYear2,
                OtherCerf2 = OtCer2Filename,
                OtherCerfName3 = emp.OtherCerfName3,
                OtherCerfInstitute3 = emp.OtherCerfInstitute3,
                OtherCerfMark3 = emp.OtherCerfMark3,
                OtherCerfDuration3 = emp.OtherCerfDuration3,
                OtherCerfPassYear3 = emp.OtherCerfPassYear3,
                OtherCerf3 = OtCer3Filename,
                ExpYears = emp.ExpYears,
                PreWorkCmp1 = emp.PreWorkCmp1,
                PreWorkCmpSdt1 = emp.PreWorkCmpSdt1,
                PreWorkCmpEdt1 = emp.PreWorkCmpEdt1,
                PreWorkCmpDoc1 = PreWorkDoc1Filename,
                PreWorkCmp2 = emp.PreWorkCmp2,
                PreWorkCmpSdt2 = emp.PreWorkCmpSdt2,
                PreWorkCmpEdt2 = emp.PreWorkCmpEdt2,
                PreWorkCmpDoc2 = PreWorkDoc2Filename,
                PreWorkCmp3 = emp.PreWorkCmp3,
                PreWorkCmpSdt3 = emp.PreWorkCmpSdt3,
                PreWorkCmpEdt3 = emp.PreWorkCmpEdt3,
                PreWorkCmpDoc3 = PreWorkDoc3Filename,
                PreWorkCmp4 = emp.PreWorkCmp4,
                PreWorkCmpSdt4 = emp.PreWorkCmpSdt4,
                PreWorkCmpEdt4 = emp.PreWorkCmpEdt4,
                PreWorkCmpDoc4 = PreWorkDoc4Filename,
                PreWorkCmp5 = emp.PreWorkCmp5,
                PreWorkCmpSdt5 = emp.PreWorkCmpSdt5,
                PreWorkCmpEdt5 = emp.PreWorkCmpEdt5,
                PreWorkCmpDoc5 = PreWorkDoc5Filename,
                WorkExBreak1 = emp.WorkExBreak1,
                WorkExBreakSdt1 = emp.WorkExBreakSdt1,
                WorkExBreakEdt1 = emp.WorkExBreakEdt1,
                WorkExBreak2 = emp.WorkExBreak2,
                WorkExBreakSdt2 = emp.WorkExBreakSdt2,
                WorkExBreakEdt2 = emp.WorkExBreakEdt2,
                WorkExBreak3 = emp.WorkExBreak3,
                WorkExBreakSdt3 = emp.WorkExBreakSdt3,
                WorkExBreakEdt3 = emp.WorkExBreakEdt3,
                WorkExBreak4 = emp.WorkExBreak4,
                WorkExBreakSdt4 = emp.WorkExBreakSdt4,
                WorkExBreakEdt4 = emp.WorkExBreakEdt4,
                PreWorkCmpCtc = emp.PreWorkCmpCtc,
                PreWorkCmpEpfStatus = emp.PreWorkCmpEpfStatus,
                PreWorkCmpEsiStatus = emp.PreWorkCmpEsiStatus,
                EmpDept = emp.EmpDept,
                EmpDesignation = emp.EmpDesignation,
                EmpRole = emp.EmpRole,
                EmpDoj = emp.EmpDoj,
                EmpOnboardCtg = emp.EmpOnboardCtg,
                ContractorId = emp.ContractorId,
                SalaryPaidBy = emp.SalaryPaidBy,
                PaymentMode = emp.PaymentMode,
                SalAccEligibility = emp.SalAccEligibility,
                SalAccNo = emp.SalAccNo,
                SalBankName = emp.SalBankName,
                SalBankIfsc = emp.SalBankIfsc,
                SalBankBranch = emp.SalBankBranch,
                SalBenfCode = emp.SalBenfCode,
                EsiEpfEligibility = emp.EsiEpfEligibility,
                Form11Willingness = emp.Form11Willingness,
                Form11Eligibility = emp.Form11Eligibility,
                Form11No = emp.Form11No,
                Form11Doc1 = Doc1Filename,
                Form11Doc2 = Doc2Filename,
                EsiJdt = emp.EsiJdt,
                EpfJdt = emp.EpfJdt,
                EsiNo = emp.EsiNo,
                EsiNomineeName = emp.EsiNomineeName,
                EpfNo = emp.EpfNo,
                EpfNomineeName = emp.EpfNomineeName,
                SalCriteria = emp.SalCriteria,
                EmpSal = emp.EmpSal,
                EmpWageShift = emp.EmpWageShift,
                EmpWageHr = emp.EmpWageHr,
                EmpWageDay = emp.EmpWageDay,
                RoomRent = emp.RoomRent,
                MessDeduction = emp.MessDeduction,
                OffEmail = emp.OffEmail,
                OffMobile = emp.OffMobile,
                AssetEligibility = emp.AssetEligibility,
                Assets = emp.Assets,
                OnboardVia = emp.OnboardVia,
                OnboardRefNo = emp.OnboardRefNo,
                OnboardRefName1 = emp.OnboardRefName1,
                OnboardRefName2 = emp.OnboardRefName2,
                Attachment1 = Att1Filename,
                Attachment2 = Att2Filename,
                Attachment3 = Att3Filename,
                Attachment4 = Att4Filename,
                Attachment5 = Att5Filename,
                AadharVerf = emp.AadharVerf,
                AadharVerfProof = AadVerFilename,
                CerfVerf = emp.CerfVerf,
                OriginalDocSubmission = emp.OriginalDocSubmission,
                OriginalDocList = emp.OriginalDocList,
                OriginalDocAck = emp.OriginalDocAck,
                OriginalDocAckNo = emp.OriginalDocAckNo,
                OriginalDocAckProof = OrgDocFilename,
                EmpCurrentStatus = emp.EmpCurrentStatus,
                EmpDoe = emp.EmpDoe,
                OriginalDocHandover = emp.OriginalDocHandover,
                OriginalDocAckBack = emp.OriginalDocAckBack,
                ReleavedReason = emp.ReleavedReason,
                EmpRejoinDate = emp.EmpRejoinDate,
                ReportingTo = emp.ReportingTo,
                EmpOldCode = emp.EmpOldCode,
                EmpCreateDate = emp.EmpCreateDate,
                TcCard = TcFilename,
            };
       

           
                _context.Update(e);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
                    ViewBag.ErrorMessage = "An error occurred: " + "Employee Code is Not Entered";
                    return View("ImageUpload");

            }
            ViewBag.Success = "Images are Successfully Uploaded.";
            return View("ImageUpload");
        
        }


        public IActionResult DownloadBulkUploadSampleExcel()
        {

            try
            {

                var builder = new StringBuilder();
                builder.AppendLine("EMP_CTG,EMP_CODE,EMP_NAME,EMP_AADHAR_NAME,EMP_PAN_NAME,EMP_CERTIF_NAME,EMP_NAME_IN_BANK,EMP_BCARD_NAME,EMP_PHOYO,GENDER,BLOOD_GRP,AADHAR_NO,AADHAR_CARD,PAN_NO,PAN_CARD,DRV_LIN_NO,DRV_LIN_VAL,DRV_LIN_CARD,PASSPORT_NO,PASSPORT_VAL,PASSPORT_CARD,AADHAR_DOB,DOB,PRESENT_ADDRESS_LINE_1,PRESENT_ADDRESS_LINE_2,PRESENT_ADDRESS_CITY,PRESENT_ADDRESS_STATE,PRESENT_ADDRESS_ZIPCODE,PERM_ADDRESS_LINE_1,PERM_ADDRESS_LINE_2,PERM_ADDRESS_CITY,PERM_ADDRESS_STATE,PERM_ADDRESS_ZIPCODE,COMM_ADDRESS_LINE_1,COMM_ADDRESS_LINE_2,COMM_ADDRESS_CITY,COMM_ADDRESS_STATE,COMM_ADDRESS_ZIPCODE,NATIONALITY,MARITAL_STATUS,PER_EMAIL,PER_MOBILE,PER_ACC_NO,PER_BNK_NAME,PER_BNK_IFSC,PER_BNK_BRANCH," +
                    "FAM_MEM_NAME_1,FAM_MEM_REL_1,FAM_MEM_CONTACT_1,FAM_MEM_NAME_2,FAM_MEM_REL_2,FAM_MEM_CONTACT_2,FAM_MEM_NAME_3,FAM_MEM_REL_3,FAM_MEM_CONTACT_3,FAM_MEM_NAME_4,FAM_MEM_REL_4,FAM_MEM_CONTACT_4,FAM_MEM_NAME_5,FAM_MEM_REL_5,FAM_MEM_CONTACT_5,EMER_CONTACT_NAME,EMER_CONTACT_REL,EMER_CONTACT_NO," +
                    "HIGH_QUALI,HIGH_QUALI_INSTITUTE_NAME,HIGH_QUALI_MARK,HIGH_QUALI_PASS_YEAR,HIGH_QUALI_CERF_1,HIGH_QUALI_2,HIGH_QUALI_INSTITUTE_NAME_2,HIGH_QUALI_MARK_2,HIGH_QUALI_PASS_YEAR_2,HIGH_QUALI_CERF_2,HSC_SCHOOL_NAME,HSC_MARK,HSC_PASS_YEAR,HSC_CERF,SSLC_SCHOOL_NAME,SSLC_MARK,SSLC_PASS_YEAR,SSLC_CERF,OTHER_CERF_NAME_1,OTHER_CERF_INSTITUTE_1,OTHER_CERF_MARK_1,OTHER_CERF_DURATION_1,OTHER_CERF_PASS_YEAR_1,OTHER_CERF_1,OTHER_CERF_NAME_2,OTHER_CERF_INSTITUTE_2,OTHER_CERF_MARK_2,OTHER_CERF_DURATION_2,OTHER_CERF_PASS_YEAR_2,OTHER_CERF_2,OTHER_CERF_NAME_3,OTHER_CERF_INSTITUTE_3,OTHER_CERF_MARK_3,OTHER_CERF_DURATION_3,OTHER_CERF_PASS_YEAR_3,OTHER_CERF_3," +
                    "EXP_YEARS,PRE_WORK_CMP_1,PRE_WRK_CMP_SDT_1,PRE_WRK_CMP_EDT_1,PRE_WRK_CMP_DOC_1,PRE_WORK_CMP_2,PRE_WRK_CMP_SDT_2,PRE_WRK_CMP_EDT_2,PRE_WRK_CMP_DOC_2,PRE_WORK_CMP_3,PRE_WRK_CMP_SDT_3,PRE_WRK_CMP_EDT_3,PRE_WRK_CMP_DOC_3,PRE_WORK_CMP_4,PRE_WRK_CMP_SDT_4,PRE_WRK_CMP_EDT_4,PRE_WRK_CMP_DOC_4,PRE_WORK_CMP_5,PRE_WRK_CMP_SDT_5,PRE_WRK_CMP_EDT_5,PRE_WRK_CMP_DOC_5,WORK_EX_BREAK_1,WORK_EX_BREAK_SDT_1,WORK_EX_BREAK_EDT_1,WORK_EX_BREAK_2,WORK_EX_BREAK_SDT_2,WORK_EX_BREAK_EDT_2,WORK_EX_BREAK_3,WORK_EX_BREAK_SDT_3,WORK_EX_BREAK_EDT_3,WORK_EX_BREAK_4,WORK_EX_BREAK_SDT_4,WORK_EX_BREAK_EDT_4,PRE_WORK_CMP_CTC,PRE_WORK_CMP_EPF_STATUS,PRE_WORK_CMP_ESI_STATUS," +
                    "EMP_DEPARTMENT,EMP_DESIGNATION,EMP_ROLE,EMP_DOJ,EMP_ONBOARD_CTG,CONTRACTOR_ID,SALARY_PAD_BY,PAYMENT_MODE,SAL_ACC_ELIGIBILITY,SAL_ACC_NO,SAL_BANK_NAME,SAL_BANK_IFSC,SAL_BANK_BRANCH,SAL_BENF_CODE,ESI_EPF_ELIGIBILITY,FORM11_WILLINGNESS,FORM11_ELIGIBILITY,FORM11_NO,FORM11_DOC_1,FORM11_DOC_2,ESI_JDT,EPF_JDT,ESI_NO,ESI_NOMINEE_NAME,EPF_NO,EPF_NOMINEE_NAME,SAL_CRITERIA,EMP_SAL,EMP_WAGE_SHIFT,EMP_WAGE_HR,EMP_WAGW_DAY,ROOM_RENT,MESS_DEDUCTION,OFF_EMAIL,OFF_MOBILE,ASSET_ELIGIBILITY,ASSETS," +
                    "ONBOARD_VIA,ONBOARD_REF_NO,ONBOARD_REF_NAME_1,ONBOARD_REF_NAME_2,ATTCHMENT_1,ATTCHMENT_2,ATTCHMENT_3,ATTCHMENT_4,ATTCHMENT_5,AADHAR_VERF,AADHAR_VERF_PROOF,CERF_VERF,ORIGINAL_DOC_SUBMISSION,ORIGINAL_DOC_LIST,ORIGINAL_DOC_ACK,ORIGINAL_DOC_ACK_NO,ORIGINAL_DOCUMENT_ACT_PROOF,EMP_CURRENT_STATUS,EMP_DOE,ORIGINAL_DOC_HANDOVER,ORIGINAL_DOC_ACK_BACK,RELEAVED_REASON,EMP_REJOIN_DATE,REPORTING_TO,EMP_OLD_CODE,EMP_CREATE_DATE,TC_CARD");

                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "BulkUploadSampleExcel.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> BulkUpload(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file != null && file.Length > 0)
                {
                    var uploadFolders = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

                    if (!Directory.Exists(uploadFolders))
                    {
                        Directory.CreateDirectory(uploadFolders);
                    }
                    var filePath = Path.Combine(uploadFolders, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            do
                            {
                                bool isHeaderSkipped = false;
                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    //int count = reader.FieldCount;
                                    EmployeeMasterData1 e = new EmployeeMasterData1();
                                  //  EmployeeViewModel emp = new EmployeeViewModel();
                                    e.EmpCtg = reader.GetValue(0).ToString();
                                    if (int.TryParse(reader.GetValue(1).ToString(), out int result0))
                                    {
                                        e.EmpCode = result0;
                                    }
                                    e.EmpName = reader.GetValue(2).ToString();
                                    e.EmpAadharName = reader.GetValue(3).ToString();
                                    if(reader.GetValue(4) != null)
                                    e.EmpPanName = reader.GetValue(4).ToString();
                                    if (reader.GetValue(5) != null)
                                        e.EmpCertifName = reader.GetValue(5).ToString();
                                    if (reader.GetValue(6) != null)
                                        e.EmpNameInBank = reader.GetValue(6).ToString();
                                    if (reader.GetValue(7) != null)
                                        e.EmpBcardName = reader.GetValue(7).ToString();
                                    if (reader.GetValue(8) != null)
                                        e.EmpPhoto = reader.GetValue(8).ToString();
                                    
                                    if (reader.GetValue(9) != null)
                                        e.Gender = reader.GetValue(9).ToString();
                                    if (reader.GetValue(10) != null)
                                        e.BloodGrp = reader.GetValue(10).ToString();
                                    if (reader.GetValue(11) != null)
                                        e.AadharNo = reader.GetValue(11).ToString();
                                    if(reader.GetValue(12) != null)
                                        e.AadharCard = reader.GetValue(12).ToString();
                                    if (reader.GetValue(13) != null)
                                        e.PanNo = reader.GetValue(13).ToString();
                                    if (reader.GetValue(14) != null)
                                        e.PanCard = reader.GetValue(14).ToString();
                                      
                                        if (reader.GetValue(15) != null)
                                        e.DrvLinNo = reader.GetValue(15).ToString();
                                    if (reader.GetValue(16) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(16).ToString(), out DateTime dt26))
                                        {

                                            e.DrvLinVal = dt26.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    //if (reader.GetValue(16) != null)
                                    //    e.DrvLinVal = reader.GetValue(16).ToString();
                                    if (reader.GetValue(17) != null)
                                        e.DrvLinCard = reader.GetValue(17).ToString();
                                    if (reader.GetValue(18) != null)
                                        e.PassportNo = reader.GetValue(18).ToString();
                                    if (reader.GetValue(19) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(19).ToString(), out DateTime dt27))
                                        {

                                            e.PassportVal = dt27.ToString("dd-MM-yyyy");

                                        }
                                    }
                                    //if (reader.GetValue(19) != null)
                                    //    e.PassportVal = reader.GetValue(19).ToString();  
                                    if (reader.GetValue(20) != null)
                                        e.PassportCard = reader.GetValue(20).ToString();

                                    if (reader.GetValue(21) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(21).ToString(), out DateTime dt1))
                                        {

                                            e.AadharDob = dt1.ToString("dd-MM-yyyy");

                                        }
    
                                    }
                                    if (reader.GetValue(22) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(22).ToString(), out DateTime dt2))
                                        {
                                            e.Dob = dt2.ToString("dd-MM-yyyy");
                                        }
                                    }
                                    if (reader.GetValue(23) != null)
                                        e.PresentAddressLine1 = reader.GetValue(23).ToString();
                                    if (reader.GetValue(24) != null)
                                        e.PresentAddressLine2 = reader.GetValue(24).ToString();
                                    if (reader.GetValue(25) != null)
                                        e.PresentAddressCity = reader.GetValue(25).ToString();
                                    if (reader.GetValue(26) != null)
                                        e.PresentAddressState = reader.GetValue(26).ToString();
                                    if (reader.GetValue(27) != null)
                                    {
                                        if (int.TryParse(reader.GetValue(27).ToString(), out int result))
                                        {
                                            e.PresentAddressZipcode = result;
                                        }
                                    }
                                    if (reader.GetValue(28) != null)
                                        e.PermAddressLine1 = reader.GetValue(28).ToString();
                                    if (reader.GetValue(29) != null)
                                        e.PermAddressLine2 = reader.GetValue(29).ToString();
                                    if (reader.GetValue(30) != null)
                                        e.PermAddressCity = reader.GetValue(30).ToString();
                                    if (reader.GetValue(31) != null)
                                        e.PermAddressState = reader.GetValue(31).ToString();
                                    if (reader.GetValue(32) != null)
                                    {
                                        if (int.TryParse(reader.GetValue(32).ToString(), out int result1))
                                        {
                                            e.PermAddressZipcode = result1;
                                        }
                                    }
                                    if (reader.GetValue(33) != null)
                                        e.CommAddressLine1 = reader.GetValue(33).ToString();
                                    if (reader.GetValue(34) != null)
                                        e.CommAddressLine2 = reader.GetValue(34).ToString();
                                    if (reader.GetValue(35) != null)
                                        e.CommAddressCity = reader.GetValue(35).ToString();
                                    if (reader.GetValue(36) != null)
                                        e.CommAddressState = reader.GetValue(36).ToString();
                                    if (reader.GetValue(37) != null)
                                    {
                                        if (int.TryParse(reader.GetValue(37).ToString(), out int result2))
                                        {
                                            e.CommAddressZipcode = result2;
                                        }
                                    }
                                    if (reader.GetValue(38) != null)
                                        e.Nationality = reader.GetValue(38).ToString();
                                    if (reader.GetValue(39) != null)
                                        e.MaritalStatus = reader.GetValue(39).ToString();
                                    if (reader.GetValue(40) != null)
                                        e.PerEmail = reader.GetValue(40).ToString();
                                    if (reader.GetValue(41) != null)
                                        e.PerMobile = reader.GetValue(41).ToString();
                                    if (reader.GetValue(42) != null)
                                        e.PerAccNo = reader.GetValue(42).ToString();
                                    if (reader.GetValue(43) != null)
                                        e.PerBnkName = reader.GetValue(43).ToString();
                                    if (reader.GetValue(44) != null)
                                        e.PerBnkIfsc = reader.GetValue(44).ToString();
                                    if (reader.GetValue(45) != null)
                                        e.PerBnkBranch = reader.GetValue(45).ToString();
                                    if (reader.GetValue(46) != null)
                                        e.FamMemName1 = reader.GetValue(46).ToString();
                                    if (reader.GetValue(47) != null)
                                        e.FamMemRel1 = reader.GetValue(47).ToString();
                                    if (reader.GetValue(48) != null)
                                        e.FamMemContact1 = reader.GetValue(48).ToString();
                                    if (reader.GetValue(49) != null)
                                        e.FamMemName2 = reader.GetValue(49).ToString();
                                    if (reader.GetValue(50) != null)
                                        e.FamMemRel2 = reader.GetValue(50).ToString();
                                    if (reader.GetValue(51) != null)
                                        e.FamMemContact2 = reader.GetValue(51).ToString();
                                    if (reader.GetValue(52) != null)
                                        e.FamMemName3 = reader.GetValue(52).ToString();
                                    if (reader.GetValue(53) != null)
                                        e.FamMemRel3 = reader.GetValue(53).ToString();
                                    if (reader.GetValue(54) != null)
                                        e.FamMemContact3 = reader.GetValue(54).ToString();
                                    if (reader.GetValue(55) != null)
                                        e.FamMemName4 = reader.GetValue(55).ToString();
                                    if (reader.GetValue(56) != null)
                                        e.FamMemRel4 = reader.GetValue(56).ToString();
                                    if (reader.GetValue(57) != null)
                                        e.FamMemContact4 = reader.GetValue(57).ToString();
                                    if (reader.GetValue(58) != null)
                                        e.FamMemName5 = reader.GetValue(58).ToString();
                                    if (reader.GetValue(59) != null)
                                        e.FamMemRel5 = reader.GetValue(59).ToString();
                                    if (reader.GetValue(60) != null)
                                        e.FamMemContact5 = reader.GetValue(60).ToString();
                                    if (reader.GetValue(61) != null)
                                        e.EmerContactName = reader.GetValue(61).ToString();
                                    if (reader.GetValue(62) != null)
                                        e.EmerContactRel = reader.GetValue(62).ToString();
                                    if (reader.GetValue(63) != null)
                                        e.EmerContactNo = reader.GetValue(63).ToString();
                                    if (reader.GetValue(64) != null)
                                        e.HighQuali = reader.GetValue(64).ToString();
                                    if (reader.GetValue(65) != null)
                                        e.HighQualiInstituteName = reader.GetValue(65).ToString();
                                    if (reader.GetValue(66) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(66).ToString(), out float result4))
                                        {
                                            e.HighQualiMark = result4;
                                        }
                                    }
                                    if (reader.GetValue(67) != null)
                                        e.HighQualiPassYear = reader.GetValue(67).ToString();
                                   
                                    if (reader.GetValue(68) != null)
                                        e.HighQualiCerf1 = reader.GetValue(68).ToString();
                             
                                    if (reader.GetValue(69) != null)
                                        e.HighQuali2 = reader.GetValue(69).ToString();
                                    if (reader.GetValue(70) != null)
                                        e.HighQualiInstituteName2 = reader.GetValue(70).ToString();
                                    if (reader.GetValue(71) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(71).ToString(), out float result5))
                                        {
                                            e.HighQualiMark2 = result5;
                                        }
                                    }
                                    if (reader.GetValue(72) != null)
                                        e.HighQualiPassYear2 = reader.GetValue(72).ToString();
                                    if (reader.GetValue(73) != null)
                                        e.HighQualiCerf2 = reader.GetValue(73).ToString();
                                    if (reader.GetValue(74) != null)
                                        e.HscSchoolName = reader.GetValue(74).ToString();
                                    if (reader.GetValue(75) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(75).ToString(), out float result6))
                                        {
                                            e.HscMark = result6;
                                        }
                                    }
                                    if (reader.GetValue(76) != null)
                                        e.HscPassYear = reader.GetValue(76).ToString();
                                    if (reader.GetValue(77) != null)
                                        e.HscCerf = reader.GetValue(77).ToString();
                                    if (reader.GetValue(78) != null)
                                        e.SslcSchoolName = reader.GetValue(78).ToString();
                                    if (reader.GetValue(79) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(79).ToString(), out float result7))
                                        {
                                            e.SslcMark = result7;
                                        }
                                    }
                                    if (reader.GetValue(80) != null)
                                        e.SslcPassYear = reader.GetValue(80).ToString();
                                    if (reader.GetValue(81) != null)
                                        e.SslcCerf = reader.GetValue(81).ToString();
                                    if (reader.GetValue(82) != null)
                                        e.OtherCerfName1 = reader.GetValue(82).ToString();
                                    if (reader.GetValue(83) != null)
                                        e.OtherCerfInstitute1 = reader.GetValue(83).ToString();
                                    if (reader.GetValue(84) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(84).ToString(), out float result8))
                                        {
                                            e.OtherCerfMark1 = result8;
                                        }
                                    }
                                    if (reader.GetValue(85) != null)
                                        e.OtherCerfDuration1 = reader.GetValue(85).ToString();
                                    if (reader.GetValue(86) != null)
                                        e.OtherCerfPassYear1 = reader.GetValue(86).ToString();
                                    if (reader.GetValue(87) != null)
                                        e.OtherCerf1 = reader.GetValue(87).ToString();
                                    if (reader.GetValue(88) != null)
                                        e.OtherCerfName2 = reader.GetValue(88).ToString();
                                    if (reader.GetValue(89) != null)
                                        e.OtherCerfInstitute2 = reader.GetValue(89).ToString();
                                    if (reader.GetValue(90) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(90).ToString(), out float result9))
                                        {
                                            e.OtherCerfMark2 = result9;
                                        }
                                    }
                                    if (reader.GetValue(91) != null)
                                        e.OtherCerfDuration2 = reader.GetValue(91).ToString();
                                    if (reader.GetValue(92) != null)
                                        e.OtherCerfPassYear2 = reader.GetValue(92).ToString();
                                    if (reader.GetValue(93) != null)
                                        e.OtherCerf2 = reader.GetValue(93).ToString();
                                    if (reader.GetValue(94) != null)
                                        e.OtherCerfName3 = reader.GetValue(94).ToString();
                                    if (reader.GetValue(95) != null)
                                        e.OtherCerfInstitute3 = reader.GetValue(95).ToString();
                                    if (reader.GetValue(96) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(96).ToString(), out float result10))
                                        {
                                            e.OtherCerfMark3 = result10;
                                        }
                                    }
                                    if (reader.GetValue(97) != null)
                                        e.OtherCerfDuration3 = reader.GetValue(97).ToString();
                                    if (reader.GetValue(98) != null)
                                        e.OtherCerfPassYear3 = reader.GetValue(98).ToString();
                                    if (reader.GetValue(99) != null)
                                        e.OtherCerf3 = reader.GetValue(99).ToString();
                                    if (reader.GetValue(100) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(100).ToString(), out float result11))
                                        {
                                            e.ExpYears = result11;
                                        }
                                    }
                                    if (reader.GetValue(101) != null)
                                        e.PreWorkCmp1 = reader.GetValue(101).ToString();
                                    if (reader.GetValue(102) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(102).ToString(), out DateTime dt3))
                                        {

                                            e.PreWorkCmpSdt1 = dt3.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(103) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(103).ToString(), out DateTime dt4))
                                        {

                                            e.PreWorkCmpEdt1 = dt4.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(104) != null)
                                        e.PreWorkCmpDoc1 = reader.GetValue(104).ToString();
                                    if (reader.GetValue(105) != null)
                                        e.PreWorkCmp2 = reader.GetValue(105).ToString();
                                    if (reader.GetValue(106) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(106).ToString(), out DateTime dt5))
                                        {

                                            e.PreWorkCmpSdt2 = dt5.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(107) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(107).ToString(), out DateTime dt6))
                                        {

                                            e.PreWorkCmpEdt2 = dt6.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(108) != null)
                                        e.PreWorkCmpDoc2 = reader.GetValue(108).ToString();
                                    if (reader.GetValue(109) != null)
                                        e.PreWorkCmp3 = reader.GetValue(109).ToString();
                                    if (reader.GetValue(110) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(110).ToString(), out DateTime dt7))
                                        {

                                            e.PreWorkCmpSdt3 = dt7.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(111) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(111).ToString(), out DateTime dt8))
                                        {

                                            e.PreWorkCmpEdt3 = dt8.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(112) != null)
                                        e.PreWorkCmpDoc3 = reader.GetValue(112).ToString();
                                    if (reader.GetValue(113) != null)
                                        e.PreWorkCmp4 = reader.GetValue(113).ToString();
                                    if (reader.GetValue(114) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(114).ToString(), out DateTime dt9))
                                        {

                                            e.PreWorkCmpSdt4 = dt9.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(115) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(115).ToString(), out DateTime dt10))
                                        {

                                            e.PreWorkCmpEdt4 = dt10.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(116) != null)
                                        e.PreWorkCmpDoc4 = reader.GetValue(116).ToString();
                                    if (reader.GetValue(117) != null)
                                        e.PreWorkCmp5 = reader.GetValue(117).ToString();
                                    if (reader.GetValue(118) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(118).ToString(), out DateTime dt11))
                                        {

                                            e.PreWorkCmpSdt5 = dt11.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(119) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(119).ToString(), out DateTime dt12))
                                        {

                                            e.PreWorkCmpEdt5 = dt12.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(120) != null)
                                        e.PreWorkCmpDoc5 = reader.GetValue(120).ToString();
                                    if (reader.GetValue(121) != null)
                                        e.WorkExBreak1 = reader.GetValue(121).ToString();
                                    if (reader.GetValue(122) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(122).ToString(), out DateTime dt13))
                                        {

                                            e.WorkExBreakSdt1 = dt13.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(123) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(123).ToString(), out DateTime dt14))
                                        {

                                            e.WorkExBreakEdt1 = dt14.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(124) != null)
                                        e.WorkExBreak2 = reader.GetValue(124).ToString();
                                    if (reader.GetValue(125) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(125).ToString(), out DateTime dt15))
                                        {

                                            e.WorkExBreakSdt2 = dt15.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(126) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(126).ToString(), out DateTime dt16))
                                        {

                                            e.WorkExBreakEdt2 = dt16.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(127) != null)
                                        e.WorkExBreak3 = reader.GetValue(127).ToString();
                                    if (reader.GetValue(128) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(128).ToString(), out DateTime dt17))
                                        {

                                            e.WorkExBreakSdt3 = dt17.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(129) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(129).ToString(), out DateTime dt18))
                                        {

                                            e.WorkExBreakEdt3 = dt18.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(130) != null)
                                        e.WorkExBreak4 = reader.GetValue(130).ToString();
                                    if (reader.GetValue(131) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(131).ToString(), out DateTime dt19))
                                        {

                                            e.WorkExBreakSdt4 = dt19.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(132) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(132).ToString(), out DateTime dt20))
                                        {

                                            e.WorkExBreakEdt4 = dt20.ToString("dd-MM-yyyy");

                                        }

                                    }
                                    if (reader.GetValue(133) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(133).ToString(), out float result12))
                                        {
                                            e.PreWorkCmpCtc = result12;
                                        }
                                    }
                                    if (reader.GetValue(134) != null)
                                        e.PreWorkCmpEpfStatus = reader.GetValue(134).ToString();
                                    if (reader.GetValue(135) != null)
                                        e.PreWorkCmpEsiStatus = reader.GetValue(135).ToString();
                                    if (reader.GetValue(136) != null)
                                        e.EmpDept = reader.GetValue(136).ToString();
                                    if (reader.GetValue(137) != null)
                                        e.EmpDesignation = reader.GetValue(137).ToString();
                                    if (reader.GetValue(138) != null)
                                        e.EmpRole = reader.GetValue(138).ToString();
                                    if (reader.GetValue(139) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(139).ToString(), out DateTime dt21))
                                        {
                                            e.EmpDoj = dt21.ToString("dd-MM-yyyy");
                                        }
                                    }
                                    if (reader.GetValue(140) != null)
                                        e.EmpOnboardCtg = reader.GetValue(140).ToString();
                                    if (reader.GetValue(141) != null)
                                    {
                                        if (int.TryParse(reader.GetValue(141).ToString(), out int result13))
                                        {
                                            e.ContractorId = result13;
                                        }
                                    }
                                    if (reader.GetValue(142) != null)
                                        e.SalaryPaidBy = reader.GetValue(142).ToString();
                                    if (reader.GetValue(143) != null)
                                        e.PaymentMode = reader.GetValue(143).ToString();
                                    if (reader.GetValue(144) != null)
                                        e.SalAccEligibility = reader.GetValue(144).ToString();
                                    if (reader.GetValue(145) != null)
                                        e.SalAccNo = reader.GetValue(145).ToString();
                                    if (reader.GetValue(146) != null)
                                        e.SalBankName = reader.GetValue(146).ToString();
                                    if (reader.GetValue(147) != null)
                                        e.SalBankIfsc = reader.GetValue(147).ToString();
                                    if (reader.GetValue(148) != null)
                                        e.SalBankBranch = reader.GetValue(148).ToString();
                                    if (reader.GetValue(149) != null)
                                        e.SalBenfCode = reader.GetValue(149).ToString();
                                    if (reader.GetValue(150) != null)
                                        e.EsiEpfEligibility = reader.GetValue(150).ToString();
                                    if (reader.GetValue(151) != null)
                                        e.Form11Willingness = reader.GetValue(151).ToString();
                                    if (reader.GetValue(152) != null)
                                        e.Form11Eligibility = reader.GetValue(152).ToString();
                                    if (reader.GetValue(153) != null)
                                        e.Form11No = reader.GetValue(153).ToString();
                                    if (reader.GetValue(154) != null)
                                        e.Form11Doc1 = reader.GetValue(154).ToString();
                                    if (reader.GetValue(155) != null)
                                        e.Form11Doc2 = reader.GetValue(155).ToString();
                                    if (reader.GetValue(156) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(156).ToString(), out DateTime dt22))
                                        {
                                            e.EsiJdt = dt22.ToString("dd-MM-yyyy");
                                        }
                                    }
                                    if (reader.GetValue(157) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(157).ToString(), out DateTime dt23))
                                        {
                                            e.EpfJdt = dt23.ToString("dd-MM-yyyy");
                                        }
                                    }
                                   
                                    if (reader.GetValue(158) != null)
                                        e.EsiNo = reader.GetValue(158).ToString();
                                    if (reader.GetValue(159) != null)
                                        e.EsiNomineeName = reader.GetValue(159).ToString();
                                    if (reader.GetValue(160) != null)
                                        e.EpfNo = reader.GetValue(160).ToString();
                                    if (reader.GetValue(161) != null)
                                        e.EpfNomineeName = reader.GetValue(161).ToString();
                                    if (reader.GetValue(162) != null)
                                        e.SalCriteria = reader.GetValue(162).ToString();
                                    if (reader.GetValue(163) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(163).ToString(), out float result14))
                                        {
                                            e.EmpSal = result14;
                                        }
                                    }
                                    if (reader.GetValue(164) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(164).ToString(), out float result15))
                                        {
                                            e.EmpWageShift = result15;
                                        }
                                    }
                                    if (reader.GetValue(165) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(165).ToString(), out float result16))
                                        {
                                            e.EmpWageHr = result16;
                                        }
                                    }
                                    if (reader.GetValue(166) != null)
                                    {
                                        if (float.TryParse(reader.GetValue(166).ToString(), out float result17))
                                        {
                                            e.EmpWageDay = result17;
                                        }
                                    }
                                    if (reader.GetValue(167) != null)
                                        e.RoomRent = reader.GetValue(167).ToString();
                                    if (reader.GetValue(168) != null)
                                        e.MessDeduction = reader.GetValue(168).ToString();
                                    if (reader.GetValue(169) != null)
                                        e.OffEmail = reader.GetValue(169).ToString();
                                    if (reader.GetValue(170) != null)
                                        e.OffMobile = reader.GetValue(170).ToString();
                                    if (reader.GetValue(171) != null)
                                        e.AssetEligibility = reader.GetValue(171).ToString();
                                    if (reader.GetValue(172) != null)
                                        e.Assets = reader.GetValue(172).ToString();
                                    if (reader.GetValue(173) != null)
                                        e.OnboardVia = reader.GetValue(173).ToString();
                                    if (reader.GetValue(174) != null)
                                        e.OnboardRefNo = reader.GetValue(174).ToString();
                                    if (reader.GetValue(175) != null)
                                        e.OnboardRefName1 = reader.GetValue(175).ToString();
                                    if (reader.GetValue(176) != null)
                                        e.OnboardRefName2 = reader.GetValue(176).ToString();
                                    if (reader.GetValue(177) != null)
                                        e.Attachment1 = reader.GetValue(177).ToString();
                                    if (reader.GetValue(178) != null)
                                        e.Attachment2 = reader.GetValue(178).ToString();
                                    if (reader.GetValue(179) != null)
                                        e.Attachment3 = reader.GetValue(179).ToString();
                                    if (reader.GetValue(180) != null)
                                        e.Attachment4 = reader.GetValue(180).ToString();
                                    if (reader.GetValue(181) != null)
                                        e.Attachment5 = reader.GetValue(181).ToString();
                                    if (reader.GetValue(182) != null)
                                        e.AadharVerf = reader.GetValue(182).ToString();
                                    if (reader.GetValue(183) != null)
                                        e.AadharVerfProof = reader.GetValue(183).ToString();
                                    if (reader.GetValue(184) != null)
                                        e.CerfVerf = reader.GetValue(184).ToString();
                                    if (reader.GetValue(185) != null)
                                        e.OriginalDocSubmission = reader.GetValue(185).ToString();
                                    if (reader.GetValue(186) != null)
                                        e.OriginalDocList = reader.GetValue(186).ToString();
                                    if (reader.GetValue(187) != null)
                                        e.OriginalDocAck = reader.GetValue(187).ToString();
                                    if (reader.GetValue(188) != null)
                                        e.OriginalDocAckNo = reader.GetValue(188).ToString();
                                    if (reader.GetValue(189) != null)
                                        e.OriginalDocAckProof = reader.GetValue(189).ToString();
                                    if (reader.GetValue(190) != null)
                                        e.EmpCurrentStatus = reader.GetValue(190).ToString();
                                    if (reader.GetValue(191) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(191).ToString(), out DateTime dt24))
                                        {
                                            e.EmpDoe = dt24.ToString("dd-MM-yyyy");
                                        }
                                    }
                                    if (reader.GetValue(192) != null)
                                        e.OriginalDocHandover = reader.GetValue(192).ToString();
                                    if (reader.GetValue(193) != null)
                                        e.OriginalDocAckBack = reader.GetValue(193).ToString();
                                    if (reader.GetValue(194) != null)
                                        e.ReleavedReason = reader.GetValue(194).ToString();
                                    if (reader.GetValue(195) != null)
                                    {
                                        if (DateTime.TryParse(reader.GetValue(195).ToString(), out DateTime dt25))
                                        {
                                            e.EmpRejoinDate = dt25.ToString("dd-MM-yyyy");
                                        }
                                    }
                                    
                                    if (reader.GetValue(196) != null)
                                    {
                                        if (int.TryParse(reader.GetValue(196).ToString(), out int result18))
                                        {
                                            e.ReportingTo = result18;
                                        }
                                    }
                                    if (reader.GetValue(197) != null)
                                    {
                                        if (int.TryParse(reader.GetValue(197).ToString(), out int result19))
                                        {
                                            e.EmpOldCode = result19;
                                        }
                                    }
                                    if (reader.GetValue(198) != null)
                                    {
                                        if (reader.GetValue(198).ToString() == null)
                                        {
                                            e.EmpCreateDate = DateOnly.FromDateTime(DateTime.Now);
                                        }
                                        else
                                        {
                                            //DateOnly.TryParse(reader.GetValue(198).ToString(), out DateOnly date);
                                            //e.EmpCreateDate = date;
                                            if (DateTime.TryParse(reader.GetValue(198).ToString(), out DateTime date))
                                            {
                                                e.EmpCreateDate = DateOnly.FromDateTime(date);
                                            }


                                        }
                                    }
                                    if (reader.GetValue(199) != null)
                                        e.TcCard = reader.GetValue(199).ToString();


                                    _context.Add(e);
                                    await _context.SaveChangesAsync();

                                }
                            } while (reader.NextResult());

                        }
                    }
                }
                ViewBag.Success = "Bulk uploaded Data Successfully.";
                return View();
            }
            catch
            {
                ViewBag.Success = "Unexpected Error occured, try uploading again with correct inputs.";
                return View();
            }
        }

        public IActionResult DownloadSalaryProcessMasterSampleExcel()
        {

            try
            {

                var builder = new StringBuilder();
                builder.AppendLine("Sl.No,Emp Code,Employee Name,Department,Designation,Company Name,Gender,Grade,Date of Birth,Date Of Joining,Marital Status,Payment Mode,Bank Name,A/c No.,Category,ESI No.,EPF No/UAN No");


                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "SalaryProcessMasterSample.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> SalaryProcessMaster(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file != null && file.Length > 0)
                {
                    var uploadFolders = $"{Directory.GetCurrentDirectory()}\\wwwroot\\SalaryProcessMasterUploads\\";

                    if (!Directory.Exists(uploadFolders))
                    {
                        Directory.CreateDirectory(uploadFolders);
                    }
                    var filePath = Path.Combine(uploadFolders, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            do
                            {
                                bool isHeaderSkipped = false;
                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    EmployeeMasterData1 spm = new EmployeeMasterData1();

                                    if (int.TryParse(reader.GetValue(1).ToString(), out int result1))
                                    {
                                        spm.EmpCode = result1;
                                    }

                                    spm.EmpName = reader.GetValue(2).ToString();
                                    spm.EmpDept = reader.GetValue(3).ToString();
                                    spm.EmpDesignation = reader.GetValue(4).ToString();
                                    spm.SalaryPaidBy = reader.GetValue(5).ToString();
                                    spm.Gender = reader.GetValue(6).ToString();
                                    spm.EmpCtg = reader.GetValue(7).ToString();
                                    if (reader.GetValue(8) == null)
                                    {
                                        spm.Dob = null;
                                    }
                                    else
                                    {
                                        spm.Dob = reader.GetValue(8).ToString();
                                    }
                                    if (reader.GetValue(9) == null)
                                    {
                                        spm.EmpDoj = null;
                                    }
                                    else
                                    {
                                        spm.EmpDoj = reader.GetValue(9).ToString();
                                    }

                                    //if (reader.GetValue(10) == null)
                                    //{
                                    //    spm.HighQuali = null;
                                    //}
                                    //else
                                    //{
                                    //    spm.HighQuali = reader.GetValue(10).ToString();
                                    //}
                                    if (reader.GetValue(10) == null)
                                    {
                                        spm.MaritalStatus = null;
                                    }
                                    else
                                    {
                                        spm.MaritalStatus = reader.GetValue(10).ToString();
                                    }

                                    spm.PaymentMode = reader.GetValue(11).ToString();
                                    if (reader.GetValue(12) == null)
                                    {
                                        spm.SalBankName = null;
                                    }
                                    else
                                    {
                                        spm.SalBankName = reader.GetValue(12).ToString();
                                    }
                                    if (reader.GetValue(13) == null)
                                    {
                                        spm.SalAccNo = null;
                                    }
                                    else
                                    {
                                        spm.SalAccNo = reader.GetValue(13).ToString();
                                    }

                                    spm.EsiEpfEligibility = reader.GetValue(14).ToString();
                                    if (reader.GetValue(15) == null)
                                    {
                                        spm.EsiNo = null;
                                    }
                                    else
                                    {
                                        spm.EsiNo = reader.GetValue(15).ToString();
                                    }
                                    if (reader.GetValue(16) == null)
                                    {
                                        spm.EpfNo = null;
                                    }
                                    else
                                    {
                                        spm.EpfNo = reader.GetValue(16).ToString();
                                    }



                                    _context.Add(spm);
                                    await _context.SaveChangesAsync();
                                }
                            } while (reader.NextResult());

                        }
                    }
                }
                ViewBag.Success = "Salary Process Master Successfully Uploaded.";

                return View();
            }
            catch
            {
                ViewBag.Error = "Unexpected Error occured, try uploading again with correct inputs.";
                return View();
            }
        }

        public IActionResult DownloadCompOffAdvancesSampleExcel()
        {

            try
            {

                var builder = new StringBuilder();
                builder.AppendLine("SL No,Date Of Import,Month(mmm-yy),Head Opf Account,Emp. Code,EmployeeName,Unit,Values");


                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "CompOffAdvancesSample.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CompOffAdvances(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file != null && file.Length > 0)
                {
                    var uploadFolders = $"{Directory.GetCurrentDirectory()}\\wwwroot\\CompOffAdvancesUploads\\";

                    if (!Directory.Exists(uploadFolders))
                    {
                        Directory.CreateDirectory(uploadFolders);
                    }
                    var filePath = Path.Combine(uploadFolders, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            do
                            {
                                bool isHeaderSkipped = false;
                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    CompOffAdvancesOneTime c = new CompOffAdvancesOneTime();
                                    if (DateTime.TryParse(reader.GetValue(1).ToString(), out DateTime dt2))
                                    {
                                        c.DateOfImport = dt2.ToString("dd-MM-yyyy");
                                    }

                                    if (DateTime.TryParse(reader.GetValue(2).ToString(), out DateTime dt1))
                                    {
                                        c.Month = dt1.ToString("dd-MM-yyyy");
                                    }
                                    
                                    c.HeadOpfAccount = reader.GetValue(3).ToString();
                                    if (int.TryParse(reader.GetValue(4).ToString(), out int result1))
                                    {
                                        c.EmpCode = result1;
                                    }

                                    c.EmpName = reader.GetValue(5).ToString();
                                    c.Unit = reader.GetValue(6).ToString();
                                    c.Value = reader.GetValue(7).ToString();

                                    int emp = _context.EmployeeMasterData1s.Count(x => x.EmpCode == c.EmpCode);
                                    if (emp == 0)
                                    {
                                        ViewBag.Error = "Employee Code " + c.EmpCode + " does not exists in Employee Master.Values after this error will not be entered.";
                                        return View();
                                    }
                                    int count = _context.CompOffAdvancesOnes.Count(x => x.Month == c.Month && x.EmpCode == c.EmpCode && x.HeadOpfAccount == c.HeadOpfAccount);
                                    if (count > 0)
                                    {
                                        ViewBag.Error = "Entry for the month already exists.";
                                        return View();
                                    }

                                    _context.Add(c);
                                    
                                    await _context.SaveChangesAsync();
                                }
                            } while (reader.NextResult());

                        }
                    }
                }
                ViewBag.Success = "CompOff/Advances (One time) Successfully Uploaded.";

                return View();
            }
            catch
            {
                ViewBag.Error = "Unexpected Error occured, try uploading again with correct inputs.";
                return View();
            }
        }


        public IActionResult DownloadYearlyLeaveSampleExcel()
        {

            try
            {

                var builder = new StringBuilder();
                builder.AppendLine("SL No,Date Of Import,Emp. Code,EmployeeName,CL,SL");


                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "YearlyLeaveSample.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> YearlyLeave(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file != null && file.Length > 0)
                {
                    var uploadFolders = $"{Directory.GetCurrentDirectory()}\\wwwroot\\YearlyLeaveUploads\\";

                    if (!Directory.Exists(uploadFolders))
                    {
                        Directory.CreateDirectory(uploadFolders);
                    }
                    var filePath = Path.Combine(uploadFolders, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            do
                            {
                                bool isHeaderSkipped = false;
                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    YearlyLeave y = new YearlyLeave();


                                    y.DateOfImport = reader.GetValue(1).ToString();

                                    if (int.TryParse(reader.GetValue(2).ToString(), out int result1))
                                    {
                                        y.EmpCode = result1;
                                    }

                                    y.EmpName = reader.GetValue(3).ToString();
                                    if (int.TryParse(reader.GetValue(4).ToString(), out int result2))
                                    {
                                        y.CL = result2;
                                    }
                                    if (int.TryParse(reader.GetValue(5).ToString(), out int result3))
                                    {
                                        y.SL = result3;
                                    }
                                    int emp = _context.EmployeeMasterData1s.Count(x => x.EmpCode == y.EmpCode);
                                    if (emp == 0)
                                    {
                                        ViewBag.Error = "Employee Code " + y.EmpCode + " does not exists in Employee Master.Values after this error will not be entered.";
                                        return View();
                                    }
                                   
                                    _context.Add(y);
                                    await _context.SaveChangesAsync();
                                }
                            } while (reader.NextResult());

                        }
                    }
                }
                ViewBag.Success = "Yearly Leave Successfully Uploaded.";

                return View();
            }
            catch
            {
                ViewBag.Error = "Unexpected Error occured, try uploading again with correct inputs.";
                return View();
            }
        }


        public IActionResult DownloadSalaryDetailsUploadSampleExcel()
        {

            try
            {

                var builder = new StringBuilder();
                builder.AppendLine("SL No.,Employee Code,Employee Name,Basic Pay,Other Allowances, GrossPay,NetPay,CTC,Last Edited Date");


                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "SalaryDetailsUploadSample.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> SalaryDetailsUpload(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file != null && file.Length > 0)
                {
                    var uploadFolders = $"{Directory.GetCurrentDirectory()}\\wwwroot\\SalaryDeatilsUploads\\";

                    if (!Directory.Exists(uploadFolders))
                    {
                        Directory.CreateDirectory(uploadFolders);
                    }
                    var filePath = Path.Combine(uploadFolders, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            do
                            {
                                bool isHeaderSkipped = false;
                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    SalaryCalculation s = new SalaryCalculation();

                                    if (int.TryParse(reader.GetValue(1).ToString(), out int result0))
                                    {
                                        s.EmpCode = result0;
                                    }
                                    s.EmpName = reader.GetValue(2).ToString();
                                    if (reader.GetValue(3) == null)
                                    {
                                        s.BasicPay = 0;
                                    }
                                    
                                    else if (float.TryParse(reader.GetValue(3).ToString(), out float result1))
                                    {
                                        s.BasicPay = result1;
                                    }
                                    if(reader.GetValue(4) == null)
                                    { s.OtherAllowances = 0; }

                                    else if (float.TryParse(reader.GetValue(4).ToString(), out float result2))
                                    {
                                        s.OtherAllowances = result2;
                                    }

                                    if ((reader.GetValue(5).ToString()) == "NULL" || (reader.GetValue(5).ToString()) == null)
                                    {
                                        s.GrossPay = 0;
                                    }
                                    else if (float.TryParse(reader.GetValue(5).ToString(), out float result3))
                                    {
                                        s.GrossPay = result3;
                                    }
                                    if (reader.GetValue(6) == null)
                                    {
                                        s.NetPay = 0;
                                    }
                                    else if (int.TryParse(reader.GetValue(6).ToString(), out int result4))
                                    {
                                        s.NetPay = result4;
                                    }
                                    
                                    if (reader.GetValue(7) == null)
                                    {
                                        s.CTC = 0;
                                    }
                                    else if (int.TryParse(reader.GetValue(7).ToString(), out int result5))
                                    {
                                        s.CTC = result5;
                                    }
                                    if (reader.GetValue(8) == null)
                                    {
                                        s.LastEditedDate = (DateTime.Now).ToString();
                                    }
                                    else
                                    {
                                        s.LastEditedDate = reader.GetValue(8).ToString();
                                    }

                                    SalaryCalculation emp = _context.SalaryCalculations.Where(x => x.EmpCode == s.EmpCode).FirstOrDefault();
                                    if(emp != null)
                                    {
                                        _context.Remove(emp);
                                        _context.Update(s);
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        _context.Add(s);
                                        await _context.SaveChangesAsync();
                                    }
                                   
                                }
                            } while (reader.NextResult());

                        }
                    }
                }
                ViewBag.Success = "Salary Details Successfully Uploaded.";

                return View();
            }
            catch
            {
                ViewBag.Error = "Unexpected Error occured, try uploading again with correct inputs.";
                return View();
            }
        }


        public IActionResult DownloadAttendanceSampleExcel()
        {

            try
            {

                var builder = new StringBuilder();
                builder.AppendLine("SL No,Month(mmm-yy),Employee Code,Employee Name,Total Shift,Total OT Hours,NH,No Of Days Addition in Comp,No Of Days Addition in CL,No Of Days Addition in SL, LOP,Advance 1," +
                    "Advance 2,Mess,Penalty,Incentive_2(Sales & Others),No Of Calender Days for Current Month,Monthly WeekOff Days as per Calender,Other Earnings,Other Deduction,TDS");


                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "AttendanceSample.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Attendance(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file != null && file.Length > 0)
                {
                    var uploadFolders = $"{Directory.GetCurrentDirectory()}\\wwwroot\\AttendanceUploads\\";

                    if (!Directory.Exists(uploadFolders))
                    {
                        Directory.CreateDirectory(uploadFolders);
                    }
                    var filePath = Path.Combine(uploadFolders, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            do
                            {
                                bool isHeaderSkipped = false;
                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    Attendance a = new Attendance();
                                    string dateString = reader.GetValue(1).ToString();
                                    DateTime dateTime = DateTime.ParseExact(dateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                    DateTime dateOnly = dateTime.Date;
                                    string dateOnlyString = dateOnly.ToString("dd-MM-yyyy");

                                    a.Month = dateOnlyString;

                                    if (int.TryParse(reader.GetValue(2).ToString(), out int result0))
                                    {
                                        a.EmpCode = result0;
                                    }
                                    a.EmpName = reader.GetValue(3).ToString();
                                    if (float.TryParse(reader.GetValue(4).ToString(), out float result1))
                                    {
                                        a.TotalShift = result1;
                                    }
                                    if (reader.GetValue(5) != null)
                                    {
                                        string timeString = reader.GetValue(5).ToString();
                                        string[] parts = timeString.Split('.');
                                        if (parts.Length >= 2 && int.TryParse(parts[0], out int days))
                                        {
                                            TimeSpan timeSpan;
                                            if (TimeSpan.TryParse(parts[1], out timeSpan))
                                            {
                                                // Add days to the time span
                                                timeSpan = timeSpan.Add(new TimeSpan(days, 0, 0, 0));
                                                a.OTDay = timeSpan.Days;
                                                a.TotalOTHrs = new TimeOnly(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                                            }

                                        }
                                        else
                                        {
                                            TimeOnly.TryParse(reader.GetValue(5).ToString(), out TimeOnly time);
                                            a.TotalOTHrs = time;
                                        }

                                    }
                                    else
                                    {
                                        a.OTDay = 0;
                                        a.TotalOTHrs = new TimeOnly(0, 0, 0);
                                    }
                                    if (int.TryParse(reader.GetValue(6).ToString(), out int result4))
                                    {
                                        a.NationalHolidays = result4;
                                    }
                                    if (float.TryParse(reader.GetValue(7).ToString(), out float result5))
                                    {
                                        a.CompOffDays = result5;
                                    }
                                    if (float.TryParse(reader.GetValue(8).ToString(), out float result2))
                                    {
                                        a.ClDays = result2;
                                    }
                                    if (float.TryParse(reader.GetValue(9).ToString(), out float result3))
                                    {
                                        a.SlDays = result3;
                                    }
                                    if (float.TryParse(reader.GetValue(10).ToString(), out float result6))
                                    {
                                        a.LOPDays = result6;
                                    }
                                    if (float.TryParse(reader.GetValue(11).ToString(), out float result9))
                                    {
                                        a.Advance1Amount = result9;
                                    }
                                    if (float.TryParse(reader.GetValue(12).ToString(), out float result10))
                                    {
                                        a.Advance2Amount = result10;
                                    }
                                    if (float.TryParse(reader.GetValue(13).ToString(), out float result11))
                                    {
                                        a.MessDeductionAmount = result11;
                                    }
                                    if (float.TryParse(reader.GetValue(14).ToString(), out float result12))
                                    {
                                        a.PenaltyDeductionAmount = result12;
                                    }
                                    if (float.TryParse(reader.GetValue(15).ToString(), out float result8))
                                    {
                                        a.IncentiveAmountSalesOthers = result8;
                                    }
                                    if (int.TryParse(reader.GetValue(16).ToString(), out int result16))
                                    {
                                        a.NoOfCalenderDaysInCurrentMonth = result16;
                                    }

                                    if (int.TryParse(reader.GetValue(17).ToString(), out int result7))
                                    {
                                        a.MonthlyWeekOffDays = result7;
                                    }
                                    if (float.TryParse(reader.GetValue(18).ToString(), out float result13))
                                    {
                                        a.OtherEarnings = result13;
                                    }
                                    if (reader.GetValue(19) == null)
                                    {
                                        a.OtherDeductions = 0;
                                    }
                                    if (float.TryParse(reader.GetValue(19).ToString(), out float result14))
                                    {
                                        a.OtherDeductions = result14;
                                    }
                                    if (float.TryParse(reader.GetValue(20).ToString(), out float result15))
                                    {
                                        a.TDS = result15;
                                    }


                                    int count = _context.Attendances.Count(x => x.Month == a.Month && x.EmpCode == a.EmpCode);

                                    if (count > 0)
                                    {
                                        ViewBag.Error = "Entry for the month already exists.";
                                        return View();
                                    }
                                    int emp = _context.EmployeeMasterData1s.Count(x => x.EmpCode == a.EmpCode);
                                    if (emp == 0)
                                    {
                                        ViewBag.Error = "Employee Code " + a.EmpCode + " does not exists in Employee Master.Values after this error will not be entered.";
                                        return View();
                                    }
                                    _context.Add(a);
                                    await _context.SaveChangesAsync();

                                }
                            } while (reader.NextResult());

                        }
                    }
                }
                ViewBag.Success = "Attendance Data Successfully Uploaded.";

                return View();
            }
            catch
            {
                ViewBag.Error = "Unexpected Error occured, try uploading again with correct inputs.";
                return View();
            }
        }
      
        public IActionResult ExportEsi(string MonthYear)
        {
            try
            {
                List<ExportEsiReport> esiReports = new List<ExportEsiReport>();
                List<ConsolidatedModel> e = _context.ConsolidatedModel.Where(x => x.MonthYear == MonthYear).ToList();
                foreach (var i in e)
                {
                    ExportEsiReport e2 = new ExportEsiReport();

                    e2.IPNumber = i.EsiNo;
                    e2.IPName = i.EmpName;
                    e2.Days = (int)Math.Round((decimal)i.NoOfPresentDays);

                    e2.Wages = i.GrossPay;
                    esiReports.Add(e2);
                }
                
                var builder = new StringBuilder();
                builder.AppendLine("IP Number,IP Name,No of Days for which wages paid/payable during the month,Total Monthly Wages, Reason Code for Zero workings days(numeric only; provide 0 for all other reasons- Click on the link for reference), Last Working Day");

                foreach( var emp in esiReports)
                {
                    if(emp.IPNumber != null)
                    builder.AppendLine($"{emp.IPNumber}, {emp.IPName},{emp.Days}, {Math.Round((decimal)emp.Wages)}, {emp.ReasonCode}, {emp.LastWorkingDate}");
                    
                }
                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "ExportEsiReport.csv");
            }
            catch(Exception ex) 
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult ExportConsolidatedReport(string MonthYear)
        {
            try
            {
                List<ConsolidatedModel> consolidatedReports = _context.ConsolidatedModel.Where(x => x.MonthYear == MonthYear).ToList();

                var builder = new StringBuilder();
                builder.AppendLine("S.NO,Emp Code,Emp Name,Department,Designation,Company Name, Gender, Grade, Date Of Birth, Date of joining, Qualification, Marital Status, Payment mode, Bank name, Account Number, Category, ESI No.,EPF No., Total Working Days, No. of Present Days, LOP Days, OT Hrs, Basic Pay, HRA, Incentive-1, Incentive-2, Other Earnings, PF Employer Contribution, PF Employee Contribution, ESI Employer Contribution, ESI Employee Contribution, Advance-1 , Advance-2, Mess, TDS, Other deduction, Penalty, Deduction, Gross Pay,Net Pay Before Ceiling, Net Pay After Ceiling, CTC, Contactor");

                foreach (var emp in consolidatedReports)
                {
                    builder.AppendLine($"{emp.Id}, {emp.EmpCode},{emp.EmpName}, {emp.Department}, {emp.Designation}, {emp.CompanyName}, {emp.Gender}, {emp.Grade}, {emp.DOB}, {emp.DOJ}, {emp.Qualification},{emp.MaritalStatus}, {emp.PaymentMode}, {emp.BankName}, {emp.AccountNumber}, {emp.EsiEpfEligibility}, {emp.EsiNo} , {emp.EpfNo}, {emp.TotalWorkingDays}, {emp.NoOfPresentDays}, {emp.LOPDays}, {emp.OTHrs}, {emp.BasicPay}, {emp.HRA}, {emp.Incentive1}, {emp.Incentive2}, {emp.OtherEarnings}, {emp.PfEmployerContribution+emp.EPSEmployerContribution}, {emp.PfEmployeeContribution}, {emp.EsiEmployerContribution}, {emp.EsiEmployeeContribution}, {emp.Advance1}, {emp.Advance2}, {emp.Mess}, {emp.TDS}, {emp.OtherDeductions},{emp.Penalty}, {emp.Deductions},{emp.GrossPay}, {emp.NetPayBeforeCeiling}, {emp.NetPayAfterCeiling}, {emp.CTC}, {emp.Contractor}");

                }
                return File(Encoding.UTF8.GetBytes(builder.ToString()), "txt/csv", "ExportConsolidatedReport.csv");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult ExportEPFToFile(string MonthYear)
        {
            
            List<ConsolidatedModel> memberDataList = _context.ConsolidatedModel.Where(x => x.MonthYear == MonthYear).ToList();
          
            StringBuilder content = new StringBuilder();
            //content.AppendLine("UAN#~#Member Name#~#Gross Wages#~#EPF Wages#~#EPS Wages#~#EDLI Wages#~#EPF Contribution remitted#~#EPS Contribution remitted#~#EPF and EPS Diff remitted#~#NCP Days#~#Refund of Advances");

            foreach (var memberData in memberDataList)
            {
                if (memberData.EpfNo != null)
                {
                    double? EPFWages = 0;
                    if (memberData.BasicPay > 15000)
                    {
                        EPFWages = 15000;
                    }
                    else
                    {
                        EPFWages = memberData.BasicPay;
                    }
                    content.AppendLine($"{memberData.EpfNo}#~#{memberData.EmpName}#~#{memberData.GrossPay}#~#{EPFWages}#~#{EPFWages}#~#{EPFWages}#~#{memberData.PfEmployeeContribution}#~#{memberData.EPSEmployerContribution}#~#{memberData.PfEmployerContribution}#~#{Math.Round((decimal)memberData.LOPDays)}#~#{0}");
                }
            }

            string fileName = "EPF.txt";

            return File(Encoding.UTF8.GetBytes(content.ToString()), "text/plain", fileName);
        }

        public ActionResult ExportHDFCToFile()
        {

            List<EmployeeMasterData1> memberDataList = _context.EmployeeMasterData1s.Where(x => x.SalBankName == "HDFC Bank" && x.PaymentMode == "Bank").ToList();
            
            StringBuilder content = new StringBuilder();
            content.AppendLine("Account No,C,Txn Amount,Txn Narration");

            foreach (var memberData in memberDataList)
            {
                if (memberData != null)
                {
                    string EmployeeName = memberData.EmpName;
                    string[] parts = EmployeeName.Split('.');
                    string EmpName;
                    if (parts.Length == 2)
                    {
                        EmpName = parts[0] + " " + parts[1];
                    }
                    else if (parts.Length == 3)
                    {
                        EmpName = parts[0] + " " + parts[1] + " " + parts[2];
                    }
                    else
                    {
                        EmpName = memberData.EmpName;
                    }

                    string monthYearString = _context.ConsolidatedModel.Where(x => x.EmpCode == memberData.EmpCode).Select(x => x.MonthYear).FirstOrDefault();
                    if (monthYearString != null)
                    {
                        DateTime monthYearDateTime = DateTime.ParseExact(monthYearString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string formattedMonthYear = monthYearDateTime.ToString("MMM yy", CultureInfo.InvariantCulture);

                        double? NetPay = _context.ConsolidatedModel.Where(x => x.EmpCode == memberData.EmpCode).Select(x => x.NetPayAfterCeiling).FirstOrDefault();
                        content.AppendLine($"{memberData.SalAccNo},C,{NetPay},{EmpName} {formattedMonthYear} Salary");
                    }
                }
            }

            string fileName = "Hdfc.txt";

            return File(Encoding.UTF8.GetBytes(content.ToString()), "text/plain", fileName);
        }

        public ActionResult ExportOtherBanksToFile()
        {

            List<EmployeeMasterData1> memberDataList = _context.EmployeeMasterData1s.Where(x => x.SalBankName != "HDFC Bank" && x.PaymentMode == "Bank").ToList();

            StringBuilder content = new StringBuilder();
           // content.AppendLine("Customer Ref No,Bene Name,Bene A/c No,IFSC Code,Account Type,Amount,Value Date");

            foreach (var memberData in memberDataList)
            {
                if (memberData != null)
                {
                    string EmployeeName = memberData.EmpName;
                    string[] parts = EmployeeName.Split('.');
                    string EmpName;
                    if (parts.Length == 2)
                    {
                        EmpName = parts[0] + " " + parts[1];
                    }
                    else if (parts.Length == 3)
                    {
                        EmpName = parts[0] + " " + parts[1] + " " + parts[2];
                    }
                    else
                    {
                        EmpName = memberData.EmpName;
                    }

                    string monthYearString = _context.ConsolidatedModel.Where(x => x.EmpCode == memberData.EmpCode).Select(x => x.MonthYear).FirstOrDefault();
                    if (monthYearString != null)
                    {
                        DateTime monthYearDateTime = DateTime.ParseExact(monthYearString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string formattedMonthYear = monthYearDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

                        double? NetPay = _context.ConsolidatedModel.Where(x => x.EmpCode == memberData.EmpCode).Select(x => x.NetPayAfterCeiling).FirstOrDefault();

                        content.AppendLine($"{EmpName},{EmpName},{memberData.SalAccNo},{memberData.SalBankIfsc},02,{NetPay},{formattedMonthYear}");
                    }
                }
            }

            string fileName = "OtherBanks.txt";

            return File(Encoding.UTF8.GetBytes(content.ToString()), "text/plain", fileName);
        }

        public ActionResult GeneratePdf(string MonthYear)
        {
            try
            {
                var cm = _context.ConsolidatedModel.Where(x => x.MonthYear == MonthYear).ToList();
                
                string folderPath = @"C:\Pdfs\"; 


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (cm.Count == 0)
                {
                    ViewBag.Error = "There is Nothing To Generate Pdf";
                    return View("ExportPDFReport");
                }

                foreach (var emp in cm)
                {

                    var document = new PdfDocument();
                 
                    string monthYearString = emp.MonthYear; 
                    DateTime monthYearDateTime = DateTime.ParseExact(monthYearString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string formattedMonthYear = monthYearDateTime.ToString("MMM-yy", CultureInfo.InvariantCulture);

                   

                    int? actualPay = (int?)emp.NetPayBeforeCeiling;
                    string netPayInWords = actualPay.HasValue ? actualPay.Value.ToWords(new CultureInfo("en-IN")).ToUpper() : "N/A";


                    string htmlContent = "<h1 style='text-align:center;'>PaySlip</h1>";
                    if (emp != null)
                    {

                        if (emp.CompanyName == "Sunbond Weldrods")
                        {
                            htmlContent += "<div class='header' style='padding: 20px;text-align: center;border:#000 solid 1px;'>";
                            htmlContent += "<h3 style='display:inline-block'>SUNBOND</h3>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='text-align:center;padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<h5> Payslip for the Month of " + "" + formattedMonthYear + "</h5>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;' >";
                            htmlContent += "<tbody>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Employee Code &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.EmpCode + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Bank Name &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.BankName + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;display:ijnline-block;font-weight: 600;'>&nbsp;Salary Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + formattedMonthYear + "&nbsp;&nbsp;" + " </td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Employee Name &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.EmpName + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;A/C No &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.AccountNumber + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Total Working Days &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.TotalWorkingDays + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Department &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.Department + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;ESI No &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;'>" + emp.EsiNo + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Total Salary Days &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.NoOfPresentDays + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Designation &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.Designation + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;PF No &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.EpfNo + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Wages Per Shift &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.WagesPerShift + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</tbody>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='text-align:center;padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;'>";
                            htmlContent += "<tbody>";
                            htmlContent += " <tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Advance 1 Amount </td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.OpeningAdvance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:30px;'></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:left;font-weight: 600;>&nbsp;Advance 2 Amount </td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.OpeningAdvance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            if (emp.DeductedAdvance1 == null)
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>0&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.DeductedAdvance1 + "&nbsp;&nbsp;" + "</td>";
                            }
                            htmlContent += "<td style='width:30px;'></td>";
                            if (emp.DeductedAdvance2 == null)
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>0&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.DeductedAdvance2 + "&nbsp;&nbsp;" + "</td>";
                            }

                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;text-align:left;>&nbsp;Deduction for the Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.Advance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:30px;'></td>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;text-align:left;>&nbsp;Deduction for the Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.Advance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Closing Balance &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.ClosingBalanceAdvance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:30px;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Closing Balance &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13px;text-align:right;>" + emp.ClosingBalanceAdvance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</tbody>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;'>";
                            htmlContent += " <tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<th colspan='2'>Gross Earnings(rs.)</th>";
                            htmlContent += "<td style='width:50px;'></td>";
                            htmlContent += "<th colspan='2'>Deduction(rs.)</th>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Basic</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.BasicPay + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            if (emp.EsiEmployeeContribution == null)
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Employee-ESI</td>";
                                htmlContent += "<td style='text-align:right;'>0&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Employee-ESI</td>";
                                htmlContent += "<td style='text-align:right;'>" + emp.EsiEmployeeContribution + "&nbsp;&nbsp;" + "</td>";
                            }
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;HRA</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.HRA + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Employee-EPF</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.PfEmployeeContribution + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Incentive_1</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Incentive1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Employee-Mess</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Mess + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += " </tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Incentive_2</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Incentive2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;TDS</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.TDS + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='text-align:right;'>0&nbsp;&nbsp;</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Penalty</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Penalty + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='text-align:right;'>0&nbsp;&nbsp;</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Advance1 Deducted</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Advance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='text-align:right;'>0&nbsp;&nbsp;</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Advance2 Deducted</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Advance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += " <tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Other Earnings</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.OtherEarnings + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Other Deduction</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.OtherDeductions + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += " <tr>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Total</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.GrossPay + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.Deductions + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600; colspan='2'>&nbsp;Net Pay: &nbsp;&nbsp;</td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.NetPayBeforeCeiling + "</td>";
                            htmlContent += "<td >&nbsp;</td>";
                            htmlContent += "<td style='width:50px;text-align:center;'></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;Rupees: &nbsp;&nbsp;</td>";
                            htmlContent += "<td>&nbsp;</td>";
                            htmlContent += "<td style='text-align:right;'>" + netPayInWords + " </td>";
                            htmlContent += "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";
                        }

                        else
                        {
                            htmlContent += "<div class='header' style='padding: 20px;text-align: center;border:#000 solid 1px;'>";
                            htmlContent += "<table>";
                            htmlContent += "<tr>";
                            htmlContent += "<td>";
                            string imageurl = "https://" + HttpContext.Request.Host.Value + "/Image/logo.png";
                            htmlContent += "<img style='width:150px;height:130%;background-color:lightblue;' src='" + imageurl + "' />";
                            htmlContent += "</td>";
                            htmlContent += "<td>";
                            htmlContent += "<h3 style='display:inline-block'>";
                            htmlContent += "ACCURA WELDRODS KOVAI PVT. LTD.<br /></h3>";
                            htmlContent += "<h5 style='display:inline-block'>NO.1/155-2,PONNANDANPALAYAM,S.F NO-454,PART 475/2,KANIYUR VILLAGE</h5>";
                            htmlContent += "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='text-align:center;padding:1px;border:#000 solid 1px;'>";
                            htmlContent += "<h5> Payslip for the Month of " + "  " + formattedMonthYear + "</h5>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;' >";
                            htmlContent += "<tbody>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Employee Code &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.EmpCode + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Bank Name &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.BankName + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;display:ijnline-block;font-weight: 600;'>&nbsp;Salary Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + formattedMonthYear + "&nbsp;&nbsp;" + " </td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Employee Name &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.EmpName + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;A/C No &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.AccountNumber + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Total Working Days &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.TotalWorkingDays + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Department &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.Department + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;ESI No &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;'>" + emp.EsiNo + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Total WeekOff Days &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.TotalWeekOffDays + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Designation &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.Designation + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;PF No &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.EpfNo + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td ></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;DOJ &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;'>" + emp.DOJ + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "</tbody>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;'>";
                            htmlContent += "<tr>";
                            htmlContent += "<th>Leave</th>";
                            htmlContent += "<th style='text-align:center;'>CL&nbsp;</th>";
                            htmlContent += "<th style='text-align:center;'>SL &nbsp;</th>";
                            htmlContent += "<td></td>";
                            htmlContent += "<th colspan='2' style='text-align:center;'>Compensation Leave Details</th>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Total Present Days &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + (emp.NoOfPresentDays - emp.NH - emp.CurrentCL - emp.CurrentSL - emp.CurrentMonthCompOffConsumed) + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>Total Allowed &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.TotalAllowedCL + "&nbsp;" + "</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.TotalAllowedSL + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;Opening Balance</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.OpeningBalanceCompOff + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;NH Days </td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.NH + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;>Consumed in Pre.Months &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.PreMonthsConsumedCL + "&nbsp;" + "</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.PreMonthsConsumedSL + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            if (emp.CurrentMonthCompOffToBeAdded == null)
                            {
                                htmlContent += "<td style='font-size: 12px;font-weight: 600;>&nbsp;&nbsp;Current month Addition </td>";
                                htmlContent += "<td style='font-size: 13px;text-align:center;>0&nbsp; </td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 12px;font-weight: 600;>&nbsp;&nbsp;Current month Addition </td>";
                                htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.CurrentMonthCompOffToBeAdded + "&nbsp;" + "</td>";
                            }
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;LOP Days</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.LOPDays + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += " <tr>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;>Consumed for the Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.CurrentCL + "&nbsp;" + "</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.CurrentSL + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;>&nbsp;&nbsp;Consumed for the Month </td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.CurrentMonthCompOffConsumed + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;Total Salary Days</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.NoOfPresentDays + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += " <td style='font-size: 13px;font-weight: 600;>Balance Available &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.BalanceCL + "&nbsp;" + "</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.BalanceSL + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;Balance Available &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:center;>" + emp.BalanceCompOff + "&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 11px;font-weight: 600;>&nbsp;&nbsp;Gross Salary=Basic+HRA &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.ActualGross + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='text-align:center;padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;'>";
                            htmlContent += "<tbody>";
                            htmlContent += " <tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Advance 1 Amount </td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.OpeningAdvance1+ "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:30px;'></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:left;font-weight: 600;>&nbsp;Advance 2 Amount </td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.OpeningAdvance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            if (emp.DeductedAdvance1 == null)
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>0&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.DeductedAdvance1 + "&nbsp;&nbsp;" + "</td>";
                            }
                            htmlContent += "<td style='width:30px;'></td>";
                            if (emp.DeductedAdvance2 == null)
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>0&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Deducted Amount &nbsp;</td>";
                                htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.DeductedAdvance2 + "&nbsp;&nbsp;" + "</td>";
                            }

                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;text-align:left;>&nbsp;Deduction for the Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.Advance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:30px;'></td>";
                            htmlContent += "<td style='font-size: 12px;font-weight: 600;text-align:left;>&nbsp;Deduction for the Month &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.Advance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;width:100px;></td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Closing Balance &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13px;text-align:right;>" + emp.ClosingBalanceAdvance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td style='width:30px;'></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;text-align:left;>&nbsp;Closing Balance &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13px;text-align:right;>" + emp.ClosingBalanceAdvance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</tbody>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table style='width:100%;' >";
                            htmlContent += "<tbody>";
                            htmlContent += "<tr>";
                            // htmlContent += "<th></th>";
                            htmlContent += "<th colspan='2'>SALARY PARTICULARS</th>";
                            htmlContent += "<th></th>";
                            //htmlContent += "<th></th>";
                            htmlContent += "<th colspan='2'>GROSS EARNINGS</th>";
                            htmlContent += "<th></th>";
                            //htmlContent += "<th></th>";
                            htmlContent += "<th colspan='2'>DEDUCTIONS</th>";
                            htmlContent += "<th></th>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Basic &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.ActualBasic + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Basic &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.BasicPay + "</td>";
                            htmlContent += "<td></td>";
                            if (emp.EsiEmployeeContribution == null)
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Employee-ESI</td>";
                                htmlContent += "<td style='text-align:right;'>0&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;Employee-ESI</td>";
                                htmlContent += "<td style='text-align:right;'>" + emp.EsiEmployeeContribution + "&nbsp;&nbsp;" + "</td>";
                            }
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>HRA &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.ActualHRA + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;HRA &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.HRA + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Employee-EPF &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.PfEmployeeContribution + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Incentive_1 &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.ActualIncentive1 + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Incentive_1 &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>" + emp.Incentive1 + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Employee-Mess &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.Mess + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Incentive_2 &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.ActualIncentive2 + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Incentive_2 &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>" + emp.Incentive2 + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;TDS &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.TDS + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'> &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>0</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp; &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>0</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Penalty &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.Penalty + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'> &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>0</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp; &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>0</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Advance1 Deducted&nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.Advance1 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'> &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>0</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp; &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>0</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Advance2 Deducted&nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.Advance2 + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Other Earnings &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.ActualOtherEarnings + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Other Earnings &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>" + emp.OtherEarnings + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp;Other Deductions &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.OtherDeductions + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>Total &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.ActualGross + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp; &nbsp;</td>";
                            htmlContent += " <td style='font-size: 13;text-align:right'>" + emp.GrossPay + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;'>&nbsp; &nbsp;</td>";
                            htmlContent += "<td style='font-size: 13;text-align:right'>" + emp.Deductions + "&nbsp;&nbsp;" + "</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "</tr>";
                            htmlContent += "</tbody>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";


                            htmlContent += "<div class='header' style='padding-top:5px;border:#000 solid 1px;'>";
                            htmlContent += "<table>";
                            htmlContent += "<tr>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600; colspan='2'>&nbsp;Net Pay: &nbsp;&nbsp;</td>";
                            htmlContent += "<td style='width:50px;'></td>";
                            htmlContent += "<td style='text-align:right;'>" + emp.NetPayBeforeCeiling + "</td>";
                            htmlContent += "<td>&nbsp;</td>";
                            htmlContent += "<td>&nbsp;</td>";
                            htmlContent += "<td></td>";
                            htmlContent += "<td style='font-size: 13px;font-weight: 600;>&nbsp;&nbsp;Rupees: &nbsp;&nbsp;</td>";
                            htmlContent += "<td>&nbsp;</td>";
                            htmlContent += "<td style='text-align:right;'>" + netPayInWords + " </td>";
                            htmlContent += "</td>";
                            htmlContent += "</tr>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";

                            htmlContent += "<div class='Footer' style='text-align:center;padding-top:1px;border:#000 solid 1px;'>";
                            htmlContent += "<table>";
                            htmlContent += "<tr>";
                            htmlContent += "<td><p>This communication is computer‐generated and please do not reply. For any queries, please contact hr@sunbond.in &  hr1@sunbond.in & hr2@sunbond.in</p></td>";
                            htmlContent += "</tr>";
                            htmlContent += "</table>";
                            htmlContent += "</div>";
                        }


                    }

                    PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);



                    byte[]? response = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        document.Save(ms);
                        response = ms.ToArray();
                    }
                    string fileName = emp.EmpCode + "_" + formattedMonthYear + "_Payslip" + ".pdf";
                    string filePath = Path.Combine(folderPath, fileName);
                    System.IO.File.WriteAllBytes(filePath, response);


                }

                ViewBag.Success = "PDFs generated successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error occurred while generating PDFs: " + ex.Message;
            }
            return View("ExportPDFReport");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    internal class ErrorViewModel
    {
        public string RequestId { get; set; }
    }



    
}
