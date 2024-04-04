using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accura_Innovatives.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection.Emit;
using NuGet.ContentModel;
using System.Net.Mail;
using System.Data;
using PagedList.Mvc;
using PagedList;
using cloudscribe.Pagination.Models;

namespace Accura_Innovatives.Controllers
{
    public class EmployeeMasterData1Controller : Controller
    {
        private readonly EmployeeMaster1Context _context;
        private readonly IWebHostEnvironment hostingenvironment;
        public EmployeeMasterData1Controller(EmployeeMaster1Context context, IWebHostEnvironment hc)
        {
            _context = context;
            hostingenvironment = hc;
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {

            ViewBag.CurrentFilter = searchString;


            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var employeeMaster1Context = from b in _context.EmployeeMasterData1s.Include(e => e.BloodGrpNavigation).Include(e => e.Contractor).Include(e => e.EmpCtgNavigation).Include(e => e.EmpRoleNavigation).Include(e => e.EsiEpfEligibilityNavigation).Include(e => e.HighQuali2Navigation).Include(e => e.HighQualiNavigation).Include(e => e.NationalityNavigation).Include(e => e.PerBnkNameNavigation).Include(e => e.SalBankNameNavigation).Include(e => e.SalaryPaidByNavigation)
                                         select b;

            var EmployeeMasterData1Count = employeeMaster1Context.Count();


            if (!String.IsNullOrEmpty(searchString))
            {

                int code = Convert.ToInt32(searchString);
                //_context.EmployeeMasterData1s.Where(x => x.EmpCode == code ).ToList();
                employeeMaster1Context = _context.EmployeeMasterData1s.Where(x => x.EmpCode == code || searchString == null);
                EmployeeMasterData1Count = employeeMaster1Context.Count();

            }


            employeeMaster1Context = employeeMaster1Context
            .Skip(ExcludeRecords)
            .Take(pageSize);

            var result = new PagedResult<EmployeeMasterData1>
            {
                Data = employeeMaster1Context.AsNoTracking().ToList(),
                TotalItems = EmployeeMasterData1Count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewBag.Success = TempData["SuccessMessage"];
             return View(result);
        }
        
        //public IActionResult SalaryDetailsUpload()
        //{
        //    var salaryEmpCodes = _context.SalaryCalculations.Select(s => s.EmpCode).ToList();
        //    var newSalaryCalculation = new SalaryCalculation();
        //    var employeesNotInSalary = _context.EmployeeMasterData1s
        //        .Where(e => !salaryEmpCodes.Contains(e.EmpCode))
        //        .ToList();
        //    if (employeesNotInSalary.Any())
        //    {
        //        foreach (var employee in employeesNotInSalary)
        //        {
        //            newSalaryCalculation.EmpCode = employee.EmpCode;
        //            newSalaryCalculation.EmpName = employee.EmpName;
        //            if (employee.SalCriteria == "Salary")
        //            {
        //                newSalaryCalculation.GrossPay = (double)employee.EmpSal;
        //            }
        //            else if (employee.SalCriteria == "Wages")
        //            {
        //                newSalaryCalculation.GrossPay = (double)employee.EmpWageDay;
        //            }
        //        }


        //        _context.SalaryCalculations.Add(newSalaryCalculation);
        //        _context.SaveChanges();

        //        TempData["SuccessMessage"] = "Salary Details Uploaded successfully.";
        //    }
        //    else
        //    {
        //        TempData["SuccessMessage"] = "No new details Added.";
        //    }
        //    return RedirectToAction("Index");
        //}

        public IActionResult SalaryDetailsUpload()
        {
            var salaryEmpCodes = _context.SalaryCalculations.Select(s => s.EmpCode).ToList();

            var employeesNotInSalary = _context.EmployeeMasterData1s
                .Where(e => !salaryEmpCodes.Contains(e.EmpCode))
                .ToList();
            if (employeesNotInSalary.Any())
            {
                foreach (var employee in employeesNotInSalary)
                {
                    var newSalaryCalculation = new SalaryCalculation();
                    newSalaryCalculation.EmpCode = employee.EmpCode;
                    newSalaryCalculation.EmpName = employee.EmpName;
                    if (employee.SalCriteria == "Salary")
                    {
                        newSalaryCalculation.GrossPay = (double)employee.EmpSal;
                    }
                    else if (employee.SalCriteria == "Wages")
                    {
                        newSalaryCalculation.GrossPay = (double)employee.EmpWageDay;
                    }
                    _context.SalaryCalculations.Add(newSalaryCalculation);
                    _context.SaveChanges();
                }

                TempData["SuccessMessage"] = "Salary Details Uploaded successfully.";
            }
            else
            {
                TempData["SuccessMessage"] = "No new details Added.";
            }
            return RedirectToAction("Index");
        }

        // GET: EmployeeMasterData1/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var employeeMasterData1 = await _context.EmployeeMasterData1s
                .Include(e => e.BloodGrpNavigation)
                .Include(e => e.Contractor)
                .Include(e => e.EmpCtgNavigation)
                .Include(e => e.EmpRoleNavigation)
                .Include(e => e.EsiEpfEligibilityNavigation)
                .Include(e => e.HighQuali2Navigation)
                .Include(e => e.HighQualiNavigation)
                .Include(e => e.NationalityNavigation)
                .Include(e => e.PerBnkNameNavigation)
                .Include(e => e.SalBankNameNavigation)
                .Include(e => e.SalaryPaidByNavigation)
                .FirstOrDefaultAsync(m => m.EmpCode == id);
            if (employeeMasterData1 == null)
            {
                return NotFound();
            }

            return View(employeeMasterData1);
        }

        // GET: EmployeeMasterData1/Create
        public IActionResult Create()
        {
            ViewData["BloodGrp"] = new SelectList(_context.BloodGrpMs, "BloodGrp", "BloodGrp");
            ViewData["ContractorId"] = new SelectList(_context.ContractorMs, "ContractorId", "ContractorId");
            ViewData["EmpCtg"] = new SelectList(_context.CategoryMs, "CtgCode", "CtgCode");
            ViewData["EmpRole"] = new SelectList(_context.UserRightsMs, "EmpRole", "EmpRole");
            ViewData["EsiEpfEligibility"] = new SelectList(_context.LegalMs, "LegName", "LegName");
            ViewData["HighQuali2"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName");
            ViewData["HighQuali"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName");
            ViewData["Nationality"] = new SelectList(_context.NationalityMs, "Nationality", "Nationality");
            ViewData["PerBnkName"] = new SelectList(_context.BankMs, "BankName", "BankName");
            ViewData["SalBankName"] = new SelectList(_context.BankMs, "BankName", "BankName");
            ViewData["SalaryPaidBy"] = new SelectList(_context.CompanyMs, "CompanyName", "CompanyName");
            return View();
        }

        // POST: EmployeeMasterData1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel emp)
        {
            try {
                //if (ModelState.IsValid)
                //{
                    emp.EmpCode = GenerateEmployeeID(emp.EmpCtg);
                    emp.EmpOnboardCtg = emp.EmpCtg;
                    emp.EmpCreateDate = DateOnly.FromDateTime(DateTime.Now);

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
                    emp.ProfilePhoto = null;
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
                else { emp.OtherCerf2 = null;}
                string OtCer3Filename = "";
                if (emp.OtherCerf3 != null)
                {
                    string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                    OtCer3Filename = Guid.NewGuid().ToString() + "_" + emp.OtherCerf3.FileName;
                    string filePath = Path.Combine(uploadFolder, OtCer3Filename);
                    emp.OtherCerf3.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                else { emp.OtherCerf3 = null;}
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
                EmployeeMasterData1 e = new EmployeeMasterData1
                {
                    EmpCtg = emp.EmpCtg,
                    EmpCode = emp.EmpCode,
                    EmpName = emp.EmpName,
                    EmpAadharName = emp.EmpAadharName,
                    EmpPanName= emp.EmpPanName,
                    EmpCertifName= emp.EmpCertifName,
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
                    CommAddressLine1= emp.CommAddressLine1,
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
                    FamMemContact4= emp.FamMemContact4,
                    FamMemName5 = emp.FamMemName5,
                    FamMemRel5 = emp.FamMemRel5,
                    FamMemContact5 = emp.FamMemContact5,
                    EmerContactName = emp.EmerContactName,
                    EmerContactRel = emp.EmerContactRel,
                    EmerContactNo = emp.EmerContactNo,
                    HighQuali = emp.HighQuali,
                    HighQualiInstituteName = emp.HighQualiInstituteName,
                    HighQualiMark = emp.HighQualiMark/100,
                    HighQualiPassYear = emp.HighQualiPassYear,
                    HighQualiCerf1 = PGFilename,
                    HighQuali2= emp.HighQuali2,
                    HighQualiInstituteName2 = emp.HighQualiInstituteName2,
                    HighQualiMark2 = emp.HighQualiMark2/100,
                    HighQualiPassYear2= emp.HighQualiPassYear2,
                    HighQualiCerf2= UGFilename,
                    HscSchoolName= emp.HscSchoolName,
                    HscMark = emp.HscMark/100,
                    HscPassYear = emp.HscPassYear,
                    HscCerf = HscFilename,
                    SslcSchoolName = emp.SslcSchoolName,
                    SslcMark = emp.SslcMark/100,
                    SslcPassYear = emp.SslcPassYear,
                    SslcCerf = SslcFilename,
                    OtherCerfName1 = emp.OtherCerfName1,
                    OtherCerfInstitute1 = emp.OtherCerfInstitute1,
                    OtherCerfMark1 = emp.OtherCerfMark1/100,
                    OtherCerfDuration1 = emp.OtherCerfDuration1,
                    OtherCerfPassYear1 = emp.OtherCerfPassYear1,
                    OtherCerf1 = OtCer1Filename,
                    OtherCerfName2 = emp.OtherCerfName2,
                    OtherCerfInstitute2 = emp.OtherCerfInstitute2,
                    OtherCerfMark2 = emp.OtherCerfMark2/100,
                    OtherCerfDuration2 = emp.OtherCerfDuration2,
                    OtherCerfPassYear2 = emp.OtherCerfPassYear2,
                    OtherCerf2 = OtCer2Filename,
                    OtherCerfName3 = emp.OtherCerfName3,
                    OtherCerfInstitute3 = emp.OtherCerfInstitute3,
                    OtherCerfMark3 = emp.OtherCerfMark3/100,
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
                    SalBankName =  emp.SalBankName,
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
                _context.Add(e);
                _context.SaveChanges();
                //ViewBag.success = "Record Added";




               // _context.Add(employeeMasterData1);
                //    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
               // }
               // ViewData["BloodGrp"] = new SelectList(_context.BloodGrpMs, "BloodGrp", "BloodGrp", employeeMasterData1.BloodGrp);
               // ViewData["ContractorId"] = new SelectList(_context.ContractorMs, "ContractorId", "ContractorId", employeeMasterData1.ContractorId);
               // ViewData["EmpCtg"] = new SelectList(_context.CategoryMs, "CtgCode", "CtgCode", employeeMasterData1.EmpCtg);
               // ViewData["EmpRole"] = new SelectList(_context.UserRightsMs, "EmpRole", "EmpRole", employeeMasterData1.EmpRole);
               // ViewData["EsiEpfEligibility"] = new SelectList(_context.LegalMs, "LegName", "LegName", employeeMasterData1.EsiEpfEligibility);
               // ViewData["HighQuali2"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName", employeeMasterData1.HighQuali2);
               // ViewData["HighQuali"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName", employeeMasterData1.HighQuali);
               // ViewData["Nationality"] = new SelectList(_context.NationalityMs, "Nationality", "Nationality", employeeMasterData1.Nationality);
               // ViewData["PerBnkName"] = new SelectList(_context.BankMs, "BankName", "BankName", employeeMasterData1.PerBnkName);
               // ViewData["SalBankName"] = new SelectList(_context.BankMs, "BankName", "BankName", employeeMasterData1.SalBankName);
               // ViewData["SalaryPaidBy"] = new SelectList(_context.CompanyMs, "CompanyName", "CompanyName", employeeMasterData1.SalaryPaidBy);
               //// return View(employeeMasterData1);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
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
                return View(emp);
                //return View();
            }
        }

        private int GenerateEmployeeID(string empCtg)
        {
            int maxEmpId, newEmpId = 0;
            
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-F6E9MJO\\SQLEXPRESS;Initial Catalog=Employee_Master_1;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT MAX(EMP_CODE) FROM Employee_Master_Data_1 WHERE EMP_CTG = @EmpCtg", connection))
                {
                    command.Parameters.AddWithValue("@EmpCtg", empCtg);
                    object result = command.ExecuteScalar();
                    
                    if (result != null && !string.IsNullOrEmpty(result.ToString()))
                    {
                        maxEmpId = (int)result;
                        newEmpId = maxEmpId + 1;
                    }

                    else
                    {
                        switch (empCtg)
                        {
                            case "A":
                                newEmpId = 10001;
                                break;
                            case "B":
                                newEmpId = 20001;
                                break;
                            case "C":
                                newEmpId = 30001;
                                break;
                            case "D":
                                newEmpId = 40001;
                                break;
                            case "E":
                                newEmpId = 50001;
                                break;
                            case "F":
                                newEmpId = 60001;
                                break;
                        }
                    }
                    return newEmpId;
                }
            }
        }
        static string ProfilePic, AadharPic, PanPic, DrvLinPic, PassportPic, PG,UG, Hsc,Sslc, OtCer1, OtCer2, OtCer3, PreWorkDoc1, PreWorkDoc2,
            PreWorkDoc3, PreWorkDoc4, PreWorkDoc5, Doc1, Doc2, Attachment1Pic,
                Attachment2Pic,
                Attachment3Pic ,
                Attachment4Pic,Dob,Doj,AadharDob,DrvLinVal,PassportVal,
            PreWorkCmpSdt1, PreWorkCmpEdt1, PreWorkCmpSdt2, PreWorkCmpEdt2, PreWorkCmpSdt3, PreWorkCmpEdt3, PreWorkCmpSdt4, PreWorkCmpEdt4, PreWorkCmpSdt5, PreWorkCmpEdt5,
            WorkExBreakSdt1, WorkExBreakSdt2, WorkExBreakSdt3, WorkExBreakSdt4,WorkExBreakEdt1,WorkExBreakEdt2, WorkExBreakEdt3,WorkExBreakEdt4,
            EsiJdt, EpfJdt,EmpCreateDate,
                Attachment5Pic , AadVer, OrgDoc,TC;
        static double Sal = 0, WagesPerDAy = 0;
        // GET: EmployeeMasterData1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var emp = await _context.EmployeeMasterData1s.FindAsync(id);
                if (emp == null)
                {
                    return NotFound();
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
                if (emp.EmpSal != null)
                {
                    Sal = (double)emp.EmpSal;
                }
                if (emp.EmpWageDay != null)
                {
                    WagesPerDAy = (double)emp.EmpWageDay;
                }
                AadharDob = emp.AadharDob;
                Dob = emp.Dob;
                Doj = emp.EmpDoj;
                DrvLinVal = emp.DrvLinVal;
                PassportVal = emp.PassportVal;
                PreWorkCmpSdt1 = emp.PreWorkCmpSdt1;
                PreWorkCmpSdt2 = emp.PreWorkCmpSdt2;
                PreWorkCmpSdt3 = emp.PreWorkCmpSdt3;
                PreWorkCmpSdt4 = emp.PreWorkCmpSdt4;
                PreWorkCmpSdt5 = emp.PreWorkCmpSdt5;
                PreWorkCmpEdt1 = emp.PreWorkCmpEdt1;
                PreWorkCmpEdt2 = emp.PreWorkCmpEdt2;
                PreWorkCmpEdt3 = emp.PreWorkCmpEdt3;
                PreWorkCmpEdt4 = emp.PreWorkCmpEdt4;
                PreWorkCmpEdt5 = emp.PreWorkCmpEdt5;
                WorkExBreakSdt1 = emp.WorkExBreakSdt1;
                WorkExBreakSdt2 = emp.WorkExBreakSdt2;
                WorkExBreakSdt3 = emp.WorkExBreakSdt3;
                WorkExBreakSdt4 = emp.WorkExBreakSdt4;
                WorkExBreakEdt1 = emp.WorkExBreakEdt1;
                WorkExBreakEdt2 = emp.WorkExBreakEdt2;
                WorkExBreakEdt3 = emp.WorkExBreakEdt3;
                WorkExBreakEdt4 = emp.WorkExBreakEdt4;
                EsiJdt = emp.EsiJdt;
                EpfJdt = emp.EpfJdt;


                e.EmpCtg = emp.EmpCtg;
                e.EmpCode = emp.EmpCode;
                e.EmpName = emp.EmpName;
                e.EmpAadharName = emp.EmpAadharName;
                e.EmpPanName = emp.EmpPanName;
                e.EmpCertifName = emp.EmpCertifName;
                e.EmpNameInBank = emp.EmpNameInBank;
                e.EmpBcardName = emp.EmpBcardName;
                if (emp.EmpPhoto != null && emp.EmpPhoto != "" && emp.EmpPhoto != "NULL")
                {
                    ProfilePic = emp.EmpPhoto;
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
                    AadharPic = emp.AadharCard;
                    string path1 = "./wwwroot/Images/" + emp.AadharCard;
                    using (var stream = System.IO.File.OpenRead(path1))
                    {
                        e.AadharCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.PanNo = emp.PanNo;
                if (emp.PanCard != null && emp.PanCard != "")
                {
                    PanPic = emp.PanCard;
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
                    DrvLinPic = emp.DrvLinCard;
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
                    PassportPic = emp.PassportCard;
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
                if(emp.HighQualiMark != null)
                {
                    e.HighQualiMark = emp.HighQualiMark;
                }
                else
                {
                    e.HighQualiMark = 0;
                }
                e.HighQualiPassYear = emp.HighQualiPassYear;
                if (emp.HighQualiCerf1 != null && emp.HighQualiCerf1 != "")
                {
                    PG = emp.HighQualiCerf1;
                    string path5 = "./wwwroot/Images/" + emp.HighQualiCerf1;
                    using (var stream = System.IO.File.OpenRead(path5))
                    {
                        e.HighQualiCerf1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.HighQuali2 = emp.HighQuali2;
                e.HighQualiInstituteName2 = emp.HighQualiInstituteName2;
                if (emp.HighQualiMark2 != null)
                {
                    e.HighQualiMark2 = emp.HighQualiMark2;
                }
                else
                {
                    e.HighQualiMark2 = 0;
                }
                e.HighQualiPassYear2 = emp.HighQualiPassYear2;
                if (emp.HighQualiCerf2 != null && emp.HighQualiCerf2 != "")
                {
                    UG = emp.HighQualiCerf2;
                    string path6 = "./wwwroot/Images/" + emp.HighQualiCerf2;
                    using (var stream = System.IO.File.OpenRead(path6))
                    {
                        e.HighQualiCerf2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.HscSchoolName = emp.HscSchoolName;
                if(emp.HscMark != null)
                {
                    e.HscMark = emp.HscMark;
                }
                else
                {
                    e.HscMark = 0;
                }
                e.HscPassYear = emp.HscPassYear;
                if (emp.HscCerf != null && emp.HscCerf != "")
                {
                    Hsc = emp.HscCerf;
                    string path7 = "./wwwroot/Images/" + emp.HscCerf;
                    using (var stream = System.IO.File.OpenRead(path7))
                    {
                        e.HscCerf = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.SslcSchoolName = emp.SslcSchoolName;
                if(emp.SslcMark != null)
                {
                    e.SslcMark = emp.SslcMark;
                }
                else
                {
                    e.SslcMark = 0;
                }
                e.SslcPassYear = emp.SslcPassYear;
                if (emp.SslcCerf != null && emp.SslcCerf != "")
                {
                    Sslc = emp.SslcCerf;
                    string path8 = "./wwwroot/Images/" + emp.SslcCerf;
                    using (var stream = System.IO.File.OpenRead(path8))
                    {
                        e.SslcCerf = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.OtherCerfName1 = emp.OtherCerfName1;
                e.OtherCerfInstitute1 = emp.OtherCerfInstitute1;
                if(emp.OtherCerfMark1 !=null)
                { 
                e.OtherCerfMark1 = emp.OtherCerfMark1;
                }
                else
                {
                    e.OtherCerfMark1 = 0;
                }
                e.OtherCerfDuration1 = emp.OtherCerfDuration1;
                e.OtherCerfPassYear1 = emp.OtherCerfPassYear1;
                if (emp.OtherCerf1 != null && emp.OtherCerf1 != "")
                {
                    OtCer1 = emp.OtherCerf1;
                    string path9 = "./wwwroot/Images/" + emp.OtherCerf1;
                    using (var stream = System.IO.File.OpenRead(path9))
                    {
                        e.OtherCerf1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.OtherCerfName2 = emp.OtherCerfName2;
                e.OtherCerfInstitute2 = emp.OtherCerfInstitute2;
                if(emp.OtherCerfMark2 != null)
                {
                    e.OtherCerfMark2 = emp.OtherCerfMark2;
                }
                else
                {
                    e.OtherCerfMark2 = 0;
                }
                
                e.OtherCerfDuration2 = emp.OtherCerfDuration2;
                e.OtherCerfPassYear2 = emp.OtherCerfPassYear2;
                if (emp.OtherCerf2 != null && emp.OtherCerf2 != "")
                {
                    OtCer2 = emp.OtherCerf2;
                    string path10 = "./wwwroot/Images/" + emp.OtherCerf2;
                    using (var stream = System.IO.File.OpenRead(path10))
                    {
                        e.OtherCerf2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.OtherCerfName3 = emp.OtherCerfName3;
                e.OtherCerfInstitute3 = emp.OtherCerfInstitute3;
                if(emp.OtherCerfMark3 != null)
                {
                    e.OtherCerfMark3 = emp.OtherCerfMark3;
                }
                else
                {
                    e.OtherCerfMark3 = 0;
                }
                e.OtherCerfDuration3 = emp.OtherCerfDuration3;
                e.OtherCerfPassYear3 = emp.OtherCerfPassYear3;
                if (emp.OtherCerf3 != null && emp.OtherCerf3 != "")
                {
                    OtCer3 = emp.OtherCerf3;
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
                    PreWorkDoc1 = emp.PreWorkCmpDoc1;
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
                    PreWorkDoc2 = emp.PreWorkCmpDoc2;
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
                    PreWorkDoc3 = emp.PreWorkCmpDoc3;
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
                    PreWorkDoc4 = emp.PreWorkCmpDoc4;
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
                    PreWorkDoc5 = emp.PreWorkCmpDoc5;
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
                    Doc1 = emp.Form11Doc1;
                    string path17 = "./wwwroot/Images/" + emp.Form11Doc1;
                    using (var stream = System.IO.File.OpenRead(path17))
                    {
                        e.Form11Doc1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                if (emp.Form11Doc2 != null && emp.Form11Doc2 != "")
                {
                    Doc2 = emp.Form11Doc2;
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
                    Attachment1Pic = emp.Attachment1;
                    string path19 = "./wwwroot/Images/" + emp.Attachment1;
                    using (var stream = System.IO.File.OpenRead(path19))
                    {
                        e.Attachment1 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                if (emp.Attachment2 != null && emp.Attachment2 != "")
                {
                    Attachment2Pic = emp.Attachment2;
                    string path20 = "./wwwroot/Images/" + emp.Attachment2;
                    using (var stream = System.IO.File.OpenRead(path20))
                    {
                        e.Attachment2 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                if (emp.Attachment3 != null && emp.Attachment3 != "")
                {
                    Attachment3Pic = emp.Attachment3;
                    string path21 = "./wwwroot/Images/" + emp.Attachment3;
                    using (var stream = System.IO.File.OpenRead(path21))
                    {
                        e.Attachment3 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                if (emp.Attachment4 != null && emp.Attachment4 != "")
                {
                    Attachment4Pic = emp.Attachment4;
                    string path22 = "./wwwroot/Images/" + emp.Attachment4;
                    using (var stream = System.IO.File.OpenRead(path22))
                    {
                        e.Attachment4 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                if (emp.Attachment5 != null && emp.Attachment5 != "")
                {
                    Attachment5Pic = emp.Attachment5;
                    string path23 = "./wwwroot/Images/" + emp.Attachment5;
                    using (var stream = System.IO.File.OpenRead(path23))
                    {
                        e.Attachment5 = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                e.AadharVerf = emp.AadharVerf;
                if (emp.AadharVerfProof != null && emp.AadharVerfProof != "")
                {
                    AadVer = emp.AadharVerfProof;
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
                    OrgDoc = emp.OriginalDocAckProof;
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
                    TC = emp.TcCard;
                    string path26 = "./wwwroot/Images/" + emp.TcCard;
                    using (var stream = System.IO.File.OpenRead(path26))
                    {
                        e.TcCard = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }

                return View(e);
            }
            catch(Exception ex) 
            {
                ViewBag.ErrorMessage = "An Error Occured: Please Try Again Later...";
                return View();
            }
        }

        // POST: EmployeeMasterData1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EmployeeViewModel emp)
        {
            try { 
                if(emp.EmpSal != Sal || emp.EmpWageDay != WagesPerDAy)
                {
                    SalaryCalculation s = _context.SalaryCalculations.Where(x => x.EmpCode == id).FirstOrDefault();
                    if (s != null)
                    {
                        if (emp.SalCriteria == "Salary")
                        {
                            s.GrossPay = (double)emp.EmpSal;
                        }
                        else
                        {
                            s.GrossPay = (double)emp.EmpWageDay;
                        }

                    }
                }

                if (emp.EmpDoj == "NULL" || emp.EmpDoj == null)
                {
                    emp.EmpDoj = Doj;
                }
                if(emp.AadharDob == "NULL" || emp.AadharDob == null)
                {
                    emp.AadharDob = AadharDob;
                }
                if (emp.Dob == "NULL" || emp.Dob == null)
                {
                    emp.Dob = Dob;
                }
                if (emp.DrvLinVal == "NULL" || emp.DrvLinVal == null)
                {
                    emp.DrvLinVal = DrvLinVal;
                }
                if (emp.PassportVal == "NULL" || emp.PassportVal == null)
                {
                    emp.PassportVal = PassportVal;
                }
                if (emp.PreWorkCmpSdt1 == "NULL" || emp.PreWorkCmpSdt1 == null)
                {
                    emp.PreWorkCmpSdt1 = PreWorkCmpSdt1;
                }
                if (emp.PreWorkCmpSdt2 == "NULL" || emp.PreWorkCmpSdt2 == null)
                {
                    emp.PreWorkCmpSdt2 = PreWorkCmpSdt2;
                }
                if (emp.PreWorkCmpSdt3 == "NULL" || emp.PreWorkCmpSdt3 == null)
                {
                    emp.PreWorkCmpSdt3 = PreWorkCmpSdt3;
                }
                if (emp.PreWorkCmpSdt4 == "NULL" || emp.PreWorkCmpSdt4 == null)
                {
                    emp.PreWorkCmpSdt4 = PreWorkCmpSdt4;
                }
                if (emp.PreWorkCmpSdt5 == "NULL" || emp.PreWorkCmpSdt5 == null)
                {
                    emp.PreWorkCmpSdt5 = PreWorkCmpSdt5;
                }
                if (emp.PreWorkCmpEdt1 == "NULL" || emp.PreWorkCmpEdt1 == null)
                {
                    emp.PreWorkCmpEdt1 = PreWorkCmpEdt1;
                }
                if (emp.PreWorkCmpEdt2 == "NULL" || emp.PreWorkCmpEdt2 == null)
                {
                    emp.PreWorkCmpEdt2 = PreWorkCmpEdt2;
                }
                if (emp.PreWorkCmpEdt3 == "NULL" || emp.PreWorkCmpEdt3 == null)
                {
                    emp.PreWorkCmpEdt3 = PreWorkCmpEdt3;
                }
                if (emp.PreWorkCmpEdt4 == "NULL" || emp.PreWorkCmpEdt4 == null)
                {
                    emp.PreWorkCmpEdt4 = PreWorkCmpEdt4;
                }
                if (emp.PreWorkCmpEdt5 == "NULL" || emp.PreWorkCmpEdt5 == null)
                {
                    emp.PreWorkCmpEdt5 = PreWorkCmpEdt5;
                }
                if (emp.WorkExBreakSdt1 == "NULL" || emp.WorkExBreakSdt1 == null)
                {
                    emp.WorkExBreakSdt1 = WorkExBreakSdt1;
                }
                if (emp.WorkExBreakSdt1 == "NULL" || emp.WorkExBreakSdt1 == null)
                {
                    emp.WorkExBreakSdt1 = WorkExBreakSdt1;
                }
                if (emp.WorkExBreakSdt2 == "NULL" || emp.WorkExBreakSdt2 == null)
                {
                    emp.WorkExBreakSdt2 = WorkExBreakSdt2;
                }
                if (emp.WorkExBreakSdt3 == "NULL" || emp.WorkExBreakSdt3 == null)
                {
                    emp.WorkExBreakSdt3 = WorkExBreakSdt3;
                }
                if (emp.WorkExBreakSdt4 == "NULL" || emp.WorkExBreakSdt4 == null)
                {
                    emp.WorkExBreakSdt4 = WorkExBreakSdt4;
                }
                if (emp.WorkExBreakEdt1 == "NULL" || emp.WorkExBreakEdt1 == null)
                {
                    emp.WorkExBreakEdt1 = WorkExBreakEdt1;
                }
                if (emp.WorkExBreakEdt2 == "NULL" || emp.WorkExBreakEdt2 == null)
                {
                    emp.WorkExBreakEdt2 = WorkExBreakEdt2;
                }
                if (emp.WorkExBreakEdt3 == "NULL" || emp.WorkExBreakEdt3 == null)
                {
                    emp.WorkExBreakEdt3 = WorkExBreakEdt3;
                }
                if (emp.WorkExBreakEdt4 == "NULL" || emp.WorkExBreakEdt4 == null)
                {
                    emp.WorkExBreakEdt4 = WorkExBreakEdt4;
                }
                if (emp.EsiJdt == "NULL" || emp.EsiJdt == null)
                {
                    emp.EsiJdt = EsiJdt;
                }
                if (emp.EpfJdt == "NULL" || emp.EpfJdt == null)
                {
                    emp.EpfJdt = EpfJdt;
                }
                

                string filename = "";


            if (emp.ProfilePhoto != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + emp.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadFolder, filename);
                emp.ProfilePhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else if (ProfilePic != null && ProfilePic  != "" && ProfilePic !="NULL")
            {
                filename = ProfilePic;
            }
            else
            {
                emp.ProfilePhoto = null;
            }
            string aadharFilename = "";
            if (emp.AadharCard != null)
            {
                string uploadFolder = Path.Combine(hostingenvironment.WebRootPath, "Images");
                aadharFilename = Guid.NewGuid().ToString() + "_" + emp.AadharCard.FileName;
                string filePath = Path.Combine(uploadFolder, aadharFilename);
                emp.AadharCard.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            else if (AadharPic != null && AadharPic != "")
            {
                aadharFilename = AadharPic;
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
            else if (PanPic != null && PanPic != "")
            {
                panFilename = PanPic;
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
            else if (DrvLinPic != null && DrvLinPic != "")
            {
                drvFilename = DrvLinPic;
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
            else if (PassportPic != null && PassportPic != "")
            {
                passFilename = PassportPic;
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
            else if (PG != null && PG != "")
            {
                PGFilename = PG;
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
            else if (UG != null && UG != "")
            {
                UGFilename = UG;
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
            else if (Hsc != null && Hsc != "")
            {
                HscFilename = Hsc;
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
            else if (Sslc != null && Sslc != "")
            {
                SslcFilename = Sslc;
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
            else if (Doc1 != null && Doc1 != "")
            {
                Doc1Filename = Doc1;
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
            else if (Doc2 != null && Doc2 != "")
            {
                Doc2Filename = Doc2;
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
            else if (OtCer1 != null && OtCer1 != "")
            {
                OtCer1Filename = OtCer1;
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
            else if (OtCer2 != null && OtCer2 != "")
            {
                OtCer2Filename = OtCer2;
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
            else if (OtCer3 != null && OtCer3 != "")
            {
                OtCer3Filename = OtCer3;
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
            else if (PreWorkDoc1 != null && PreWorkDoc1 != "")
            {
                PreWorkDoc1Filename = PreWorkDoc1;
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
            else if (PreWorkDoc2 != null && PreWorkDoc2 != "")
            {
                PreWorkDoc2Filename = PreWorkDoc2;
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
            else if (PreWorkDoc3 != null && PreWorkDoc3 != "")
            {
                PreWorkDoc3Filename = PreWorkDoc3;
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
            else if (PreWorkDoc4 != null && PreWorkDoc4 != "")
            {
                PreWorkDoc4Filename = PreWorkDoc4;
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
            else if (PreWorkDoc5 != null && PreWorkDoc5 != "")
            {
                PreWorkDoc5Filename = PreWorkDoc5;
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
            else if (Attachment1Pic != null && Attachment1Pic != "")
            {
                Att1Filename = Attachment1Pic;
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
            else if (Attachment2Pic != null && Attachment2Pic != "")
            {
                Att2Filename = Attachment2Pic;
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
            else if (Attachment3Pic != null && Attachment3Pic != "")
            {
                Att3Filename = Attachment3Pic;
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
            else if (Attachment4Pic != null && Attachment4Pic != "")
            {
                Att4Filename = Attachment4Pic;
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
            else if (Attachment5Pic != null && Attachment5Pic != "")
            {
                Att5Filename = Attachment5Pic;
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
            else if (AadVer != null && AadVer != "")
            {
                AadVerFilename = AadVer;
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
            else if (OrgDoc != null && OrgDoc != "")
            {
                OrgDocFilename = OrgDoc;
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
            else if (TC != null && TC != "")
            {
                TcFilename = TC;
            }
            else { emp.TcCard = null; }
            emp.EmpOnboardCtg = emp.EmpCtg;
            EmployeeMasterData1 em = new EmployeeMasterData1
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
                HighQualiMark = emp.HighQualiMark/100,
                HighQualiPassYear = emp.HighQualiPassYear,
                HighQualiCerf1 = PGFilename,
                HighQuali2 = emp.HighQuali2,
                HighQualiInstituteName2 = emp.HighQualiInstituteName2,
                HighQualiMark2 = emp.HighQualiMark2/100,
                HighQualiPassYear2 = emp.HighQualiPassYear2,
                HighQualiCerf2 = UGFilename,
                HscSchoolName = emp.HscSchoolName,
                HscMark = emp.HscMark/100,
                HscPassYear = emp.HscPassYear,
                HscCerf = HscFilename,
                SslcSchoolName = emp.SslcSchoolName,
                SslcMark = emp.SslcMark/100,
                SslcPassYear = emp.SslcPassYear,
                SslcCerf = SslcFilename,
                OtherCerfName1 = emp.OtherCerfName1,
                OtherCerfInstitute1 = emp.OtherCerfInstitute1,
                OtherCerfMark1 = emp.OtherCerfMark1/100,
                OtherCerfDuration1 = emp.OtherCerfDuration1,
                OtherCerfPassYear1 = emp.OtherCerfPassYear1,
                OtherCerf1 = OtCer1Filename,
                OtherCerfName2 = emp.OtherCerfName2,
                OtherCerfInstitute2 = emp.OtherCerfInstitute2,
                OtherCerfMark2 = emp.OtherCerfMark2/100,
                OtherCerfDuration2 = emp.OtherCerfDuration2,
                OtherCerfPassYear2 = emp.OtherCerfPassYear2,
                OtherCerf2 = OtCer2Filename,
                OtherCerfName3 = emp.OtherCerfName3,
                OtherCerfInstitute3 = emp.OtherCerfInstitute3,
                OtherCerfMark3 = emp.OtherCerfMark3/100,
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


          
            
           
                    _context.Update(em);
                    await _context.SaveChangesAsync();
                ProfilePic = null; AadharPic = null; PanPic = null; DrvLinPic = null; PassportPic = null; PG = null; UG = null;
                Hsc = null; Sslc = null; OtCer1 = null; OtCer2 = null; OtCer3 = null; PreWorkDoc1 = null; PreWorkDoc2 = null;
                PreWorkDoc3 = null; PreWorkDoc4 = null; PreWorkDoc5 = null; Doc1 = null; Doc2 = null; Attachment1Pic = null;
                Attachment2Pic = null;
                Attachment3Pic = null;
                Attachment4Pic = null; Dob = null; Doj = null;
                Attachment5Pic = null; AadVer = null; OrgDoc = null; TC = null;
            }
                catch (Exception ex)
                {
                
                    //if (!EmployeeMasterData1Exists(emp.EmpCode))
                    //{
                    //ViewBag.ErrorMessage = "An error occurred:Not Found";
                    //return View();
                    // }
                    //else
                    //{
                    ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                    //return View();
                    //}
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
                return View(emp);
            }

                return RedirectToAction(nameof(Index));
            //}
            //ViewData["BloodGrp"] = new SelectList(_context.BloodGrpMs, "BloodGrp", "BloodGrp", employeeMasterData1.BloodGrp);
            //ViewData["ContractorId"] = new SelectList(_context.ContractorMs, "ContractorId", "ContractorId", employeeMasterData1.ContractorId);
            //ViewData["EmpCtg"] = new SelectList(_context.CategoryMs, "CtgCode", "CtgCode", employeeMasterData1.EmpCtg);
            //ViewData["EmpRole"] = new SelectList(_context.UserRightsMs, "EmpRole", "EmpRole", employeeMasterData1.EmpRole);
            //ViewData["EsiEpfEligibility"] = new SelectList(_context.LegalMs, "LegName", "LegName", employeeMasterData1.EsiEpfEligibility);
            //ViewData["HighQuali2"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName", employeeMasterData1.HighQuali2);
            //ViewData["HighQuali"] = new SelectList(_context.QualificationMs, "QualiName", "QualiName", employeeMasterData1.HighQuali);
            //ViewData["Nationality"] = new SelectList(_context.NationalityMs, "Nationality", "Nationality", employeeMasterData1.Nationality);
            //ViewData["PerBnkName"] = new SelectList(_context.BankMs, "BankName", "BankName", employeeMasterData1.PerBnkName);
            //ViewData["SalBankName"] = new SelectList(_context.BankMs, "BankName", "BankName", employeeMasterData1.SalBankName);
            //ViewData["SalaryPaidBy"] = new SelectList(_context.CompanyMs, "CompanyName", "CompanyName", employeeMasterData1.SalaryPaidBy);
            //return View(employeeMasterData1);
           

        }

        // GET: EmployeeMasterData1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var employeeMasterData1 = await _context.EmployeeMasterData1s
                    .Include(e => e.BloodGrpNavigation)
                    .Include(e => e.Contractor)
                    .Include(e => e.EmpCtgNavigation)
                    .Include(e => e.EmpRoleNavigation)
                    .Include(e => e.EsiEpfEligibilityNavigation)
                    .Include(e => e.HighQuali2Navigation)
                    .Include(e => e.HighQualiNavigation)
                    .Include(e => e.NationalityNavigation)
                    .Include(e => e.PerBnkNameNavigation)
                    .Include(e => e.SalBankNameNavigation)
                    .Include(e => e.SalaryPaidByNavigation)
                    .FirstOrDefaultAsync(m => m.EmpCode == id);
                if (employeeMasterData1 == null)
                {
                    return NotFound();
                }
                return View(employeeMasterData1);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View();
            }
            
        }

        //// POST: EmployeeMasterData1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var employeeMasterData1 = await _context.EmployeeMasterData1s.FindAsync(id);
        //    if (employeeMasterData1 != null)
        //    {
        //        _context.EmployeeMasterData1s.Remove(employeeMasterData1);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // POST: EmployeeMasterData1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeMasterData1 = await _context.EmployeeMasterData1s.FindAsync(id);
            var SalCal = _context.SalaryCalculations.Where(x => x.EmpCode == id).FirstOrDefault();
            if (SalCal != null) { _context.SalaryCalculations.Remove(SalCal); }
            var Att = _context.Attendances.Where(x => x.EmpCode == id).FirstOrDefault();
            if (Att != null) { _context.Attendances.Remove(Att); }
            if (employeeMasterData1 != null)
            {
                _context.EmployeeMasterData1s.Remove(employeeMasterData1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMasterData1Exists(int id)
        {
            return _context.EmployeeMasterData1s.Any(e => e.EmpCode == id);
        }


    }
}
