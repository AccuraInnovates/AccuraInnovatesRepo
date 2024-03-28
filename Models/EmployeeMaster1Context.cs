using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Accura_Innovatives.Models;

public partial class EmployeeMaster1Context : DbContext
{
    public EmployeeMaster1Context()
    {
    }

    public EmployeeMaster1Context(DbContextOptions<EmployeeMaster1Context> options)
        : base(options)
    {
    }
    public virtual DbSet<ConsolidatedModel> ConsolidatedModel { get; set; }
    public virtual DbSet<EsiPf> EsiPfs { get; set; }
    public virtual DbSet<SalaryCalculation> SalaryCalculations { get; set; }
    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<SalaryProcessMaster> SalaryProcessMasters { get; set; }
    public virtual DbSet<YearlyLeave> YearlyLeaves { get; set; }
    public virtual DbSet<CompOffAdvancesOneTime> CompOffAdvancesOnes { get; set; }
    public virtual DbSet<AssetEmpM> AssetEmpMs { get; set; }

    public virtual DbSet<AssetsListM> AssetsListMs { get; set; }

    public virtual DbSet<BankM> BankMs { get; set; }

    public virtual DbSet<BloodGrpM> BloodGrpMs { get; set; }

    public virtual DbSet<CategoryM> CategoryMs { get; set; }

    public virtual DbSet<CompanyM> CompanyMs { get; set; }

    public virtual DbSet<ContractorM> ContractorMs { get; set; }

    public virtual DbSet<EmpRejoinDetail> EmpRejoinDetails { get; set; }

    public virtual DbSet<EmployeeMasterData1> EmployeeMasterData1s { get; set; }

    public virtual DbSet<LegalM> LegalMs { get; set; }

    public virtual DbSet<NationalityM> NationalityMs { get; set; }

    public virtual DbSet<QualificationM> QualificationMs { get; set; }

    public virtual DbSet<SalaryM> SalaryMs { get; set; }

    public virtual DbSet<UserLoginM> UserLoginMs { get; set; }

    public virtual DbSet<UserRightsM> UserRightsMs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetEmpM>(entity =>
        {
            entity.HasKey(e => e.AssetEmpId).HasName("PK__Asset_Em__C6A4CEEC3CE4DC67");

            entity.ToTable("Asset_Emp_M");

            entity.Property(e => e.AssetEmpId)
                .ValueGeneratedNever()
                .HasColumnName("ASSET_EMP_ID");
            entity.Property(e => e.AssetName)
                .HasMaxLength(200)
                .HasColumnName("ASSET_NAME");
            entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

            entity.HasOne(d => d.AssetNameNavigation).WithMany(p => p.AssetEmpMs)
                .HasForeignKey(d => d.AssetName)
                .HasConstraintName("FK__Asset_Emp__ASSET__2F9A1060");

            entity.HasOne(d => d.Emp).WithMany(p => p.AssetEmpMs)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__Asset_Emp__EMP_I__308E3499");
        });

        modelBuilder.Entity<SalaryProcessMaster>(entity =>
        {
            entity.HasKey(e => e.EmpCode).HasName("EmpCode");

            entity.ToTable("SalaryProcessMaster");
            entity.Property(e => e.EmpName)
                .HasMaxLength(100)
                .HasColumnName("EmployeeName");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("Department");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("Designation");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasColumnName("CompanyName");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("Gender");
            entity.Property(e => e.Grade)
                .HasMaxLength(10)
                .HasColumnName("Grade");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(20)
                .HasColumnName("DateOfBirth");
            entity.Property(e => e.DateOfJoining)
                .HasMaxLength(20)
                .HasColumnName("DateOfJoining");
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .HasColumnName("Qualification");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .HasColumnName("MaritalStatus");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(20)
                .HasColumnName("PaymentMode");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .HasColumnName("BankName");
            entity.Property(e => e.AccNo)
                .HasMaxLength(20)
                .HasColumnName("AccNo");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .HasColumnName("Category");
            entity.Property(e => e.EsiNo)
                .HasMaxLength(20)
                .HasColumnName("EsiNo");
            entity.Property(e => e.EpfNo)
                .HasMaxLength(20)
                .HasColumnName("EpfNo");
        });

        modelBuilder.Entity<CompOffAdvancesOneTime>(entity =>
        {
            entity.HasKey(e => e.CompOffAdvancesID).HasName("PK_CompOff__EB64010B16A235BF");

            entity.ToTable("CompOff_Advances_OneTime");

            entity.Property(e => e.CompOffAdvancesID)
            .ValueGeneratedOnAdd()
            .HasColumnName("COMPOFF_ADVANCES_ID");

            entity.Property(e => e.DateOfImport)
            .HasMaxLength(20)
            .HasColumnName("DATE_OF_IMPORT");

            entity.Property(e => e.Month)
             .HasMaxLength(20)
             .HasColumnName("MONTH");

            entity.Property(e => e.HeadOpfAccount)
            .HasMaxLength(50)
            .HasColumnName("HEAD_OPF_ACCOUNT");

            entity.Property(e => e.EmpCode)
            .HasColumnName("EMP_CODE");

            entity.Property(e => e.EmpName)
            .HasMaxLength(50)
            .HasColumnName("EMPLOYEE_NAME");

            entity.Property(e => e.Unit)
            .HasMaxLength(20)
            .HasColumnName("UNIT");

            entity.Property(e => e.Value)
            .HasMaxLength(20)
            .HasColumnName("VALUE");

            //entity.HasOne(d => d.EmpNavi).WithOne(p => p.Comp)
            //    .HasForeignKey<CompOffAdvancesOneTime>(d => d.EmpCode)
            //    .HasConstraintName("FK_COMPOFF_EMP_CODE");

        });

        modelBuilder.Entity<YearlyLeave>(entity =>
        {
            entity.HasKey(e => e.YearlyLeaveId).HasName("PK_Yearly_L_5FD028BC65FFF89E");

            entity.ToTable("Yearly_Leave");

            entity.Property(e => e.YearlyLeaveId)
           .ValueGeneratedOnAdd()
           .HasColumnName("YEARLY_LEAVE_ID");

            entity.Property(e => e.DateOfImport)
            .HasMaxLength(20)
            .HasColumnName("DATE_OF_IMPORT");

            entity.Property(e => e.EmpCode)
            .HasColumnName("EMP_CODE");

            entity.Property(e => e.EmpName)
            .HasMaxLength(50)
            .HasColumnName("EMPLOYEE_NAME");

            entity.Property(e => e.CL)
            .HasColumnName("CL");

            entity.Property(e => e.SL)
            .HasColumnName("SL");

            //entity.HasOne(d => d.Emp).WithOne(p => p.YearlyLeave)
            //    .HasForeignKey<YearlyLeave>(d => d.EmpCode)
            //    .HasConstraintName("FK_YL_EMP_CODE");
        });

        modelBuilder.Entity<AssetsListM>(entity =>
        {
            entity.HasKey(e => e.AssetName).HasName("PK__Assets_Name");

            entity.ToTable("Assets_List_M");

            entity.HasIndex(e => e.AssetId, "UQ__Asset_ID").IsUnique();

            entity.Property(e => e.AssetName)
                .HasMaxLength(200)
                .HasColumnName("ASSET_NAME");
            entity.Property(e => e.AssetDesc).HasColumnName("ASSET_DESC");
            entity.Property(e => e.AssetId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ASSET_ID");
        });

        modelBuilder.Entity<BankM>(entity =>
        {
            entity.HasKey(e => e.BankName).HasName("PK_Bank_M_1");

            entity.ToTable("Bank_M");

            entity.HasIndex(e => e.BankId, "UQ__Bnk_ID").IsUnique();

            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .HasColumnName("BANK_NAME");
            entity.Property(e => e.BankId)
                .ValueGeneratedOnAdd()
                .HasColumnName("BANK_ID");
            entity.Property(e => e.Desc).HasColumnName("DESC");
        });

        modelBuilder.Entity<BloodGrpM>(entity =>
        {
            entity.HasKey(e => e.BloodGrp).HasName("PK_Blood_Grp_");

            entity.ToTable("Blood_Grp_M");

            entity.HasIndex(e => e.BloodGrpId, "UQ__Bld_Grp_ID").IsUnique();

            entity.Property(e => e.BloodGrp)
                .HasMaxLength(20)
                .HasColumnName("BLOOD_GRP");
            entity.Property(e => e.BloodGrpDesc)
                .HasMaxLength(50)
                .HasColumnName("BLOOD_GRP_DESC");
            entity.Property(e => e.BloodGrpId)
                .ValueGeneratedOnAdd()
                .HasColumnName("BLOOD_GRP_ID");
        });

        modelBuilder.Entity<CategoryM>(entity =>
        {
            entity.HasKey(e => e.CtgCode);

            entity.ToTable("Category_M");

            entity.Property(e => e.CtgCode)
                .HasMaxLength(20)
                .HasColumnName("CTG_CODE");
            entity.Property(e => e.CtgDesc).HasColumnName("CTG_DESC");
            entity.Property(e => e.CtgDetails)
                .HasMaxLength(50)
                .HasColumnName("CTG_DETAILS");
        });

        modelBuilder.Entity<CompanyM>(entity =>
        {
            entity.HasKey(e => e.CompanyName).HasName("PK__Company_NAME");

            entity.ToTable("Company_M");

            entity.HasIndex(e => e.CompanyId, "UQ__Company__ID").IsUnique();

            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.CompanyDesc).HasColumnName("COMPANY_DESC");
            entity.Property(e => e.CompanyId)
                .ValueGeneratedOnAdd()
                .HasColumnName("COMPANY_ID");
        });

        modelBuilder.Entity<ContractorM>(entity =>
        {
            entity.HasKey(e => e.ContractorId).HasName("PK_Contractor__M");

            entity.ToTable("Contractor_M");

            entity.HasIndex(e => e.AadharNo, "UQ__Con_Aadhar_No").IsUnique();

            entity.HasIndex(e => e.ContactNo, "UQ__Con_Contact_No").IsUnique();

            entity.HasIndex(e => e.PanNo, "UQ__Con_Pan_No").IsUnique();

            entity.Property(e => e.ContractorId).HasColumnName("CONTRACTOR_ID");
            entity.Property(e => e.AadharNo)
                .HasMaxLength(20)
                .HasColumnName("AADHAR_NO");
            entity.Property(e => e.AccomEligibility)
                .HasMaxLength(20)
                .HasColumnName("ACCOM_ELIGIBILITY");
            entity.Property(e => e.CommAddressCity)
                .HasMaxLength(50)
                .HasColumnName("COMM_ADDRESS_CITY");
            entity.Property(e => e.CommAddressLine1).HasColumnName("COMM_ADDRESS_LINE_1");
            entity.Property(e => e.CommAddressLine2).HasColumnName("COMM_ADDRESS_LINE_2");
            entity.Property(e => e.CommAddressState)
                .HasMaxLength(50)
                .HasColumnName("COMM_ADDRESS_STATE");
            entity.Property(e => e.CommAddressZipcode)
                .HasMaxLength(50)
                .HasColumnName("COMM_ADDRESS_ZIPCODE");
            entity.Property(e => e.Commision).HasColumnName("COMMISION");
            entity.Property(e => e.CommisionPers).HasColumnName("COMMISION_PERS");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(20)
                .HasColumnName("CONTACT_NO");
            entity.Property(e => e.ContractorLicenceNo)
                .HasMaxLength(20)
                .HasColumnName("CONTRACTOR_LICENCE_NO");
            entity.Property(e => e.ContractorName)
                .HasMaxLength(50)
                .HasColumnName("CONTRACTOR_NAME");
            entity.Property(e => e.ContractorStatus)
                .HasMaxLength(20)
                .HasColumnName("CONTRACTOR_STATUS");
            entity.Property(e => e.CookEligibility)
                .HasMaxLength(20)
                .HasColumnName("COOK_ELIGIBILITY");
            entity.Property(e => e.EffectiveFrom).HasColumnName("EFFECTIVE_FROM");
            entity.Property(e => e.EffectiveTo).HasColumnName("EFFECTIVE_TO");
            entity.Property(e => e.EmpWage).HasColumnName("EMP_WAGE");
            entity.Property(e => e.FoodCast).HasColumnName("FOOD_CAST");
            entity.Property(e => e.GstNo)
                .HasMaxLength(20)
                .HasColumnName("GST_NO");
            entity.Property(e => e.PanNo)
                .HasMaxLength(20)
                .HasColumnName("PAN_NO");
            entity.Property(e => e.PermAddress).HasColumnName("PERM_ADDRESS");
            entity.Property(e => e.PermAddressCity)
                .HasMaxLength(50)
                .HasColumnName("PERM_ADDRESS_CITY");
            entity.Property(e => e.PermAddressLine2).HasColumnName("PERM_ADDRESS_LINE_2");
            entity.Property(e => e.PermAddressState)
                .HasMaxLength(50)
                .HasColumnName("PERM_ADDRESS_STATE");
            entity.Property(e => e.PermAddressZipcode)
                .HasMaxLength(50)
                .HasColumnName("PERM_ADDRESS_ZIPCODE");
            entity.Property(e => e.ShoesEligibility)
                .HasMaxLength(20)
                .HasColumnName("SHOES_ELIGIBILITY");
            entity.Property(e => e.TdsApplicable)
                .HasMaxLength(20)
                .HasColumnName("TDS_APPLICABLE");
            entity.Property(e => e.TdsPers).HasColumnName("TDS_PERS");
            entity.Property(e => e.UniformEligibility)
                .HasMaxLength(20)
                .HasColumnName("UNIFORM_ELIGIBILITY");
        });

        modelBuilder.Entity<EmpRejoinDetail>(entity =>
        {
            entity.HasKey(e => e.RejoinId).HasName("PK__Emp_Rejoin");

            entity.ToTable("Emp_Rejoin_Details");

            entity.HasIndex(e => e.EmpNewJoineeId, "UQ__Emp_Rejo__New_Joinee_Id").IsUnique();

            entity.HasIndex(e => e.NewEmpId, "UQ__Emp_Rejoin_New_Id").IsUnique();

            entity.HasIndex(e => e.OldEmpId, "UQ__Emp_Rejoin_Old_Emp_Id").IsUnique();

            entity.Property(e => e.RejoinId).HasColumnName("REJOIN_ID");
            entity.Property(e => e.EmpNewJoineeId).HasColumnName("EMP_NEW_JOINEE_ID");
            entity.Property(e => e.NewEmpId).HasColumnName("NEW_EMP_ID");
            entity.Property(e => e.OldEmpId).HasColumnName("OLD_EMP_ID");

            entity.HasOne(d => d.OldEmp).WithOne(p => p.EmpRejoinDetail)
                .HasForeignKey<EmpRejoinDetail>(d => d.OldEmpId)
                .HasConstraintName("FK_Old_Emp_Id___Emp_Master__Emp_Code");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttDedId).HasName("PK_ATT_DED_ID");
            entity.ToTable("Attendance");
            entity.Property(e => e.AttDedId)
            .ValueGeneratedOnAdd()
            .HasColumnName("ATT_DED_ID");
            entity.Property(e => e.Month)
            .HasColumnName("MONTH");
            entity.Property(e => e.EmpCode)
            .HasColumnName("EMP_CODE");
            entity.Property(e => e.EmpName)
            .HasColumnName("EMP_NAME");
            entity.Property(e => e.TotalShift)
            .HasMaxLength(2)
            .HasColumnName("TOTAL_SHIFT");
            entity.Property(e => e.OTDay)
            .HasColumnName("OT_DAY");
            entity.Property(e => e.TotalOTHrs)
            .HasMaxLength(3)
            .HasColumnName("TOTAL_OT_HRS");
            entity.Property(e => e.NationalHolidays)
            .HasMaxLength(2)
            .HasColumnName("NATIONAL_HOLIDAYS");
            entity.Property(e => e.CompOffDays)
           .HasMaxLength(2)
           .HasColumnName("COMP_OFF_DAYS");
            entity.Property(e => e.ClDays)
            .HasMaxLength(2)
            .HasColumnName("CL_DAYS");
            entity.Property(e => e.SlDays)
            .HasMaxLength(2)
            .HasColumnName("SL_DAYS");
            entity.Property(e => e.LOPDays)
            .HasMaxLength(2)
            .HasColumnName("LOP_DAYS");
            entity.Property(e => e.Advance1Amount)
            .HasColumnName("ADVANCE_1_AMOUNT");
            entity.Property(e => e.Advance2Amount)
            .HasColumnName("ADVANCE_2_AMOUNT");
            entity.Property(e => e.MessDeductionAmount)
            .HasColumnName("MESS_DEDUCTION_AMOUNT");
            entity.Property(e => e.PenaltyDeductionAmount)
            .HasColumnName("PENALTY_DEDUCTION_AMOUNT");
            entity.Property(e => e.IncentiveAmountSalesOthers)
            .HasColumnName("INCENTIVE_AMOUNT_SALES_OTHERS");
            entity.Property(e => e.NoOfCalenderDaysInCurrentMonth)
           .HasColumnName("NO_CALENDER_DAYS_CURRENT_MONTH");
            entity.Property(e => e.MonthlyWeekOffDays)
            .HasMaxLength(2)
            .HasColumnName("MONTHLY_WEEKOFF_DAYS");
            entity.Property(e => e.OtherEarnings)
            .HasColumnName("OTHER_EARNINGS");
            entity.Property(e => e.OtherDeductions)
            .HasColumnName("OTHER_DEDUCTION");
            entity.Property(e => e.TDS)
            .HasColumnName("TDS");
            
            entity.HasOne(d => d.Emp).WithMany(p => p.Attendance)
                .HasForeignKey(d => d.EmpCode)
                .HasConstraintName("FK_EMP_CODE");
           
        });

        modelBuilder.Entity<SalaryCalculation>(entity =>
        {
            entity.HasKey(e => e.SalCalId).HasName("[PK_SAL_CAL_ID]");
            entity.ToTable("Salary_Calculation");
            entity.Property(e => e.SalCalId)
            .ValueGeneratedOnAdd()
            .HasColumnName("SAL_CAL_ID");
            entity.Property(e => e.BasicPay)
            .HasColumnName("BASIC_PAY");
            entity.Property(e => e.EmpCode)
            .HasColumnName("EMP_CODE");
            entity.Property(e => e.EmpName)
            .HasColumnName("EMP_NAME");
            entity.Property(e => e.OtherAllowances)
            .HasColumnName("OTHER_ALLOWANCES");
            entity.Property(e => e.GrossPay)
            .HasColumnName("GROSS_PAY");
            entity.Property(e => e.Gross)
            .HasColumnName("Gross");
            entity.Property(e => e.NetPay)
            .HasColumnName("NET_PAY");
            entity.Property(e => e.CTC)
            .HasColumnName("CTC");          
            entity.Property(e => e.LastEditedDate)
            .HasColumnName("LAST_EDITED_DATE");
            entity.HasOne(d => d.EmpNav).WithOne(p => p.SalaryCalculation)
                .HasForeignKey<SalaryCalculation>(d => d.EmpCode)
                .HasConstraintName("FK_SAL_EMP_CODE");
        });

        modelBuilder.Entity<ConsolidatedModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Consolid__3214EC27415499B9");
            entity.Property(e =>e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
            entity.Property(e => e.MonthYear)
            .HasColumnName("MONTH_YEAR");
            entity.Property(e => e.EmpName)
            .HasColumnName("EMP_NAME");
            entity.Property(e => e.EmpCode)
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("DEPARTMENT");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("DESIGNATION");
            entity.Property(e => e.CompanyName)
            .HasMaxLength(50)
            .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.Gender)
            .HasMaxLength(20)
            .HasColumnName("GENDER");
            entity.Property(e => e.Grade)
            .HasMaxLength(20)
            .HasColumnName("GRADE");
            entity.Property(e => e.DOB)
            .HasColumnName("DOB");
            entity.Property(e => e.DOJ)
            .HasColumnName("DOJ");
            entity.Property(e => e.Qualification)
            .HasColumnName("QUALIFICATION");
            entity.Property(e => e.MaritalStatus)
            .HasColumnName("MARITAL_STATUS");
            entity.Property(e => e.PaymentMode)
            .HasColumnName("PAYMENT_MODE");
            entity.Property(e => e.BankName)
            .HasColumnName("BANK_NAME");
            entity.Property(e => e.AccountNumber)
            .HasColumnName("ACCOUNT_NUMBER");
            entity.Property(e => e.EsiEpfEligibility)
            .HasColumnName("ESI_EPF_ELIGIBILLITY");
            entity.Property(e => e.EsiNo)
            .HasColumnName("ESI_NO");
            entity.Property(e => e.EpfNo)
            .HasColumnName("EPF_NO");
            entity.Property(e => e.TotalWorkingDays)
            .HasColumnName("TOTAL_WORKING_DAYS");
            entity.Property(e => e.NoOfPresentDays)
            .HasColumnName("NO_OF_PRESENT_DAYS");
            entity.Property(e => e.LOPDays)
            .HasColumnName("LOP_DAYS");
            entity.Property(e => e.OTHrs)
            .HasColumnName("OT_HRS");
            entity.Property(e => e.BasicPay)
            .HasColumnName("BASIC_PAY");
            entity.Property(e => e.HRA)
            .HasColumnName("HRA");
            entity.Property(e => e.Incentive1)
            .HasColumnName("INCENTIVE_1");
            entity.Property(e => e.Incentive2)
            .HasColumnName("INCENTIVE_2");
            entity.Property(e => e.OtherEarnings)
            .HasColumnName("OTHER_EARNINGS");
            entity.Property(e => e.Deductions)
            .HasColumnName("DEDUCTIONS");
            entity.Property(e => e.EsiEmployeeContribution)
            .HasColumnName("ESI_EMPLOYEE_CONTRIBUTION");
            entity.Property(e => e.EsiEmployerContribution)
            .HasColumnName("ESI_EMPLOYER_CONTRIBUTION");
            entity.Property(e => e.PfEmployeeContribution)
            .HasColumnName("PF_EMPLOYEE_CONTRIBUTION");
            entity.Property(e => e.PfEmployerContribution)
            .HasColumnName("PF_EMPLOYER_CONTRIBUTION");
            entity.Property(e => e.EPSEmployerContribution)
            .HasColumnName("EPS_CONTRIBUTION");
            entity.Property(e => e.NetPayBeforeCeiling)
            .HasColumnName("NET_PAY_BEFORE_CEILING");
            entity.Property(e => e.NetPayAfterCeiling)
            .HasColumnName("NET_PAY_AFTER_CEILING");
            entity.Property(e => e.OpeningAdvance1)
           .HasColumnName("OPENING_ADVANCE_1");
            entity.Property(e => e.OpeningAdvance2)
           .HasColumnName("OPENING_ADVANCE_2");
            entity.Property(e => e.DeductedAdvance1)
           .HasColumnName("DEDUCTED_ADVANCE_1");
            entity.Property(e => e.DeductedAdvance2)
           .HasColumnName("DEDUCTED_ADVANCE_2");
            entity.Property(e => e.Advance1)
           .HasColumnName("ADVANCE_1");
            entity.Property(e => e.Advance2)
           .HasColumnName("ADVANCE_2");
            entity.Property(e => e.ClosingBalanceAdvance1)
           .HasColumnName("CLOSING_BALANCE_ADVANCE_1");
            entity.Property(e => e.ClosingBalanceAdvance2)
           .HasColumnName("CLOSING_BALANCE_ADVANCE_2");
            entity.Property(e => e.Mess)
           .HasColumnName("MESS");
            entity.Property(e => e.TDS)
           .HasColumnName("TDS");
            entity.Property(e => e.OtherDeductions)
           .HasColumnName("OTHER_DEDUCTIONS");
            entity.Property(e => e.GrossPay)
           .HasColumnName("GROSS_PAY");
            entity.Property(e => e.CTC)
           .HasColumnName("CTC");
            entity.Property(e => e.Contractor)
           .HasColumnName("CONTRACTOR");
            entity.Property(e => e.TotalWeekOffDays)
           .HasColumnName("TOTAL_WEEKOFF_DAYS");
            entity.Property(e => e.TotalAllowedCL)
           .HasColumnName("TOTAL_ALLOWED_CL");
            entity.Property(e => e.TotalAllowedSL)
           .HasColumnName("TOTAL_ALLOWED_SL");
            entity.Property(e => e.PreMonthsConsumedCL)
           .HasColumnName("PRE_MONTH_CONSUMED_CL");
            entity.Property(e => e.PreMonthsConsumedSL)
           .HasColumnName("PRE_MONTH_CONSUMED_SL");
            entity.Property(e => e.CurrentCL)
            .HasColumnName("CURRENT_CL");
            entity.Property(e => e.BalanceCL)
            .HasColumnName("BALANCE_CL");
            entity.Property(e => e.BalanceSL)
            .HasColumnName("BALANCE_SL");
            entity.Property(e => e.CurrentSL)
            .HasColumnName("CURRENT_SL");
            entity.Property(e => e.OpeningBalanceCompOff)
            .HasColumnName("OPENING_BALANCE_COMP_OFF");
            entity.Property(e => e.CurrentMonthCompOffToBeAdded)
            .HasColumnName("CURRENT_MONTH_COMP_OFF_TO_BE_ADDED");
            entity.Property(e => e.CurrentMonthCompOffConsumed)
            .HasColumnName("CURRENT_MONTH_COMP_OFF_CONSUMED");
            entity.Property(e => e.BalanceCompOff)
            .HasColumnName("BALANCE_COMP_OFF");
            entity.Property(e => e.NH)
            .HasColumnName("NH");
            entity.Property(e => e.Penalty)
            .HasColumnName("PENALTY");
            entity.Property(e => e.ActualGross)
            .HasColumnName("ACTUAL_GROSS");
            entity.Property(e => e.ActualBasic)
            .HasColumnName("ACTUAL_BASIC_PAY");
            entity.Property(e => e.ActualHRA)
            .HasColumnName("ACTUAL_HRA");
            entity.Property(e => e.ActualIncentive1)
            .HasColumnName("ACTUAL_INCENTIVE_1");
            entity.Property(e => e.ActualIncentive2)
            .HasColumnName("ACTUAL_INCENTIVE_2");
            entity.Property(e => e.ActualOtherEarnings)
            .HasColumnName("ACTUAL_OTHER_EARNINGS");
            entity.Property(e => e.ActualSalary)
            .HasColumnName("ACTUAL_SALARY");
            entity.Property(e => e.ActualNetPay)
            .HasColumnName("ACTUAL_NET_PAY");
            entity.Property(e => e.WagesPerShift)
            .HasColumnName("WAGES_PER_SHIFT");
        });

        modelBuilder.Entity<EsiPf>(entity =>
        {
            entity.HasKey(e => e.EsiPfID).HasName("PK_ESI_PF_ID");

            entity.ToTable("EsiPf");
            entity.Property(e => e.EsiPfID)
                .HasColumnName("EsiPfID");
            entity.Property(e => e.EsiEmployeeContribution)
                .HasColumnName("ESI_EMPLOYEE_CONTRIBUTION");
            entity.Property(e => e.EsiEmployerContribution)
                .HasColumnName("ESI_EMPLOYER_CONTRIBUTION");
            entity.Property(e => e.EpfEmployeeContribution)
                .HasColumnName("PF_EMPLOYEE_CONTRIBUTION");
            entity.Property(e => e.EpfEmployerContribution)
                .HasColumnName("PF_EMPLOYER_CONTRIBUTION");
            entity.Property(e => e.EpsEmployerContribution)
                .HasColumnName("EPS_EMPLOYER_CONTRIBUTION");
            entity.Property(e => e.EffectFrom)
                .HasMaxLength(20)
                .HasColumnName("EFFECT_FROM");
            entity.Property(e => e.EffectTo)
               .HasMaxLength(20)
               .HasColumnName("EFFECT_TO");
        });

        modelBuilder.Entity<EmployeeMasterData1>(entity =>
        {
            entity.HasKey(e => e.EmpCode).HasName("PK_EMPLOYEE_MASTER_DATA_1");

            entity.ToTable("Employee_Master_Data_1");

            

            entity.Property(e => e.EmpCode)
                .ValueGeneratedNever()
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.AadharCard)
                .HasColumnName("AADHAR_CARD");
            entity.Property(e => e.AadharDob).HasColumnName("AADHAR_DOB");
            entity.Property(e => e.AadharNo)
                .HasMaxLength(50)
                .HasColumnName("AADHAR_NO");
            entity.Property(e => e.AadharVerf)
                .HasMaxLength(20)
                .HasColumnName("AADHAR_VERF");
            entity.Property(e => e.AadharVerfProof)
                .HasColumnName("AADHAR_VERF_PROOF");
            entity.Property(e => e.AssetEligibility)
                .HasMaxLength(20)
                .HasColumnName("ASSET_ELIGIBILITY");
            entity.Property(e => e.Assets)
                .HasMaxLength(200)
                .HasColumnName("ASSETS");
            entity.Property(e => e.Attachment1)
                .HasColumnName("ATTACHMENT_1");
            entity.Property(e => e.Attachment2)
                .HasColumnName("ATTACHMENT_2");
            entity.Property(e => e.Attachment3)
                .HasColumnName("ATTACHMENT_3");
            entity.Property(e => e.Attachment4)
                .HasColumnName("ATTACHMENT_4");
            entity.Property(e => e.Attachment5)
                .HasColumnName("ATTACHMENT_5");
            entity.Property(e => e.BloodGrp)
                .HasMaxLength(20)
                .HasColumnName("BLOOD_GRP");
            entity.Property(e => e.CerfVerf)
                .HasMaxLength(20)
                .HasColumnName("CERF_VERF");
            entity.Property(e => e.CommAddressCity)
                .HasMaxLength(50)
                .HasColumnName("COMM_ADDRESS_CITY");
            entity.Property(e => e.CommAddressLine1).HasColumnName("COMM_ADDRESS_LINE_1");
            entity.Property(e => e.CommAddressLine2).HasColumnName("COMM_ADDRESS_LINE_2");
            entity.Property(e => e.CommAddressState)
                .HasMaxLength(50)
                .HasColumnName("COMM_ADDRESS_STATE");
            entity.Property(e => e.CommAddressZipcode).HasColumnName("COMM_ADDRESS_ZIPCODE");
            entity.Property(e => e.ContractorId).HasColumnName("CONTRACTOR_ID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.DrvLinCard)
                .HasColumnName("DRV_LIN_CARD");
            entity.Property(e => e.DrvLinNo)
                .HasMaxLength(20)
                .HasColumnName("DRV_LIN_NO");
            entity.Property(e => e.DrvLinVal).HasColumnName("DRV_LIN_VAL");
            entity.Property(e => e.EmerContactName)
                .HasMaxLength(50)
                .HasColumnName("EMER_CONTACT_NAME");
            entity.Property(e => e.EmerContactNo)
                .HasMaxLength(20)
                .HasColumnName("EMER_CONTACT_NO");
            entity.Property(e => e.EmerContactRel)
                .HasMaxLength(20)
                .HasColumnName("EMER_CONTACT_REL");
            entity.Property(e => e.EmpAadharName)
                .HasMaxLength(50)
                .HasColumnName("EMP_AADHAR_NAME");
            entity.Property(e => e.EmpBcardName)
                .HasMaxLength(50)
                .HasColumnName("EMP_BCARD_NAME");
            entity.Property(e => e.EmpCertifName)
                .HasMaxLength(50)
                .HasColumnName("EMP_CERTIF_NAME");
            entity.Property(e => e.EmpCreateDate).HasColumnName("EMP_CREATE_DATE");
            entity.Property(e => e.EmpCtg)
                .HasMaxLength(20)
                .HasColumnName("EMP_CTG");
            entity.Property(e => e.EmpCurrentStatus)
                .HasMaxLength(20)
                .HasColumnName("EMP_CURRENT_STATUS");
            entity.Property(e => e.EmpDept)
                .HasMaxLength(50)
                .HasColumnName("EMP_DEPT");
            entity.Property(e => e.EmpDesignation)
                .HasMaxLength(50)
                .HasColumnName("EMP_DESIGNATION");
            entity.Property(e => e.EmpDoe).HasColumnName("EMP_DOE");
            entity.Property(e => e.EmpDoj).HasColumnName("EMP_DOJ");
            entity.Property(e => e.EmpName)
                .HasMaxLength(50)
                .HasColumnName("EMP_NAME");
            entity.Property(e => e.EmpNameInBank)
                .HasMaxLength(50)
                .HasColumnName("EMP_NAME_IN_BANK");
            entity.Property(e => e.EmpOldCode).HasColumnName("EMP_OLD_CODE");
            entity.Property(e => e.EmpOnboardCtg)
                .HasMaxLength(20)
                .HasColumnName("EMP_ONBOARD_CTG");
            entity.Property(e => e.EmpPanName)
                .HasMaxLength(50)
                .HasColumnName("EMP_PAN_NAME");
            entity.Property(e => e.EmpPhoto)
                .HasColumnName("EMP_PHOTO");
            entity.Property(e => e.EmpRejoinDate).HasColumnName("EMP_REJOIN_DATE");
            entity.Property(e => e.EmpRole)
                .HasMaxLength(50)
                .HasColumnName("EMP_ROLE");
            entity.Property(e => e.EmpSal).HasColumnName("EMP_SAL");
            entity.Property(e => e.EmpWageDay).HasColumnName("EMP_WAGE_DAY");
            entity.Property(e => e.EmpWageHr).HasColumnName("EMP_WAGE_HR");
            entity.Property(e => e.EmpWageShift).HasColumnName("EMP_WAGE_SHIFT");
            entity.Property(e => e.EpfJdt).HasColumnName("EPF_JDT");
            entity.Property(e => e.EpfNo)
                .HasMaxLength(50)
                .HasColumnName("EPF_NO");
            entity.Property(e => e.EpfNomineeName)
                .HasMaxLength(50)
                .HasColumnName("EPF_NOMINEE_NAME");
            entity.Property(e => e.EsiEpfEligibility)
                .HasMaxLength(20)
                .HasColumnName("ESI_EPF_ELIGIBILITY");
            entity.Property(e => e.EsiJdt).HasColumnName("ESI_JDT");
            entity.Property(e => e.EsiNo)
                .HasMaxLength(50)
                .HasColumnName("ESI_NO");
            entity.Property(e => e.EsiNomineeName)
                .HasMaxLength(50)
                .HasColumnName("ESI_NOMINEE_NAME");
            entity.Property(e => e.ExpYears).HasColumnName("EXP_YEARS");
            entity.Property(e => e.FamMemContact1)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_CONTACT_1");
            entity.Property(e => e.FamMemContact2)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_CONTACT_2");
            entity.Property(e => e.FamMemContact3)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_CONTACT_3");
            entity.Property(e => e.FamMemContact4)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_CONTACT_4");
            entity.Property(e => e.FamMemContact5)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_CONTACT_5");
            entity.Property(e => e.FamMemName1)
                .HasMaxLength(50)
                .HasColumnName("FAM_MEM_NAME_1");
            entity.Property(e => e.FamMemName2)
                .HasMaxLength(50)
                .HasColumnName("FAM_MEM_NAME_2");
            entity.Property(e => e.FamMemName3)
                .HasMaxLength(50)
                .HasColumnName("FAM_MEM_NAME_3");
            entity.Property(e => e.FamMemName4)
                .HasMaxLength(50)
                .HasColumnName("FAM_MEM_NAME_4");
            entity.Property(e => e.FamMemName5)
                .HasMaxLength(50)
                .HasColumnName("FAM_MEM_NAME_5");
            entity.Property(e => e.FamMemRel1)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_REL_1");
            entity.Property(e => e.FamMemRel2)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_REL_2");
            entity.Property(e => e.FamMemRel3)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_REL_3");
            entity.Property(e => e.FamMemRel4)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_REL_4");
            entity.Property(e => e.FamMemRel5)
                .HasMaxLength(20)
                .HasColumnName("FAM_MEM_REL_5");
            entity.Property(e => e.Form11Doc1)
                .HasColumnName("FORM11_DOC_1");
            entity.Property(e => e.Form11Doc2)
                .HasColumnName("FORM11_DOC_2");
            entity.Property(e => e.Form11Eligibility)
                .HasMaxLength(20)
                .HasColumnName("FORM11_ELIGIBILITY");
            entity.Property(e => e.Form11No)
                .HasMaxLength(20)
                .HasColumnName("FORM11_NO");
            entity.Property(e => e.Form11Willingness)
                .HasMaxLength(20)
                .HasColumnName("FORM11_WILLINGNESS");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("GENDER");
            entity.Property(e => e.HighQuali)
                .HasMaxLength(50)
                .HasColumnName("HIGH_QUALI");
            entity.Property(e => e.HighQuali2)
                .HasMaxLength(50)
                .HasColumnName("HIGH_QUALI_2");
            entity.Property(e => e.HighQualiCerf1)
                .HasColumnName("HIGH_QUALI_CERF_1");
            entity.Property(e => e.HighQualiCerf2)
                .HasColumnName("HIGH_QUALI_CERF_2");
            entity.Property(e => e.HighQualiInstituteName).HasColumnName("HIGH_QUALI_INSTITUTE_NAME");
            entity.Property(e => e.HighQualiInstituteName2).HasColumnName("HIGH_QUALI_INSTITUTE_NAME_2");
            entity.Property(e => e.HighQualiMark).HasColumnName("HIGH_QUALI_MARK");
            entity.Property(e => e.HighQualiMark2).HasColumnName("HIGH_QUALI_MARK_2");
            entity.Property(e => e.HighQualiPassYear)
                .HasMaxLength(50)
                .HasColumnName("HIGH_QUALI_PASS_YEAR");
            entity.Property(e => e.HighQualiPassYear2)
                .HasMaxLength(50)
                .HasColumnName("HIGH_QUALI_PASS_YEAR_2");
            entity.Property(e => e.HscCerf)
                .HasColumnName("HSC_CERF");
            entity.Property(e => e.HscMark).HasColumnName("HSC_MARK");
            entity.Property(e => e.HscPassYear)
                .HasMaxLength(50)
                .HasColumnName("HSC_PASS_YEAR");
            entity.Property(e => e.HscSchoolName).HasColumnName("HSC_SCHOOL_NAME");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .HasColumnName("MARITAL_STATUS");
            entity.Property(e => e.MessDeduction)
                .HasMaxLength(20)
                .HasColumnName("MESS_DEDUCTION");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("NATIONALITY");
            entity.Property(e => e.OffEmail)
                .HasMaxLength(50)
                .HasColumnName("OFF_EMAIL");
            entity.Property(e => e.OffMobile)
                .HasMaxLength(20)
                .HasColumnName("OFF_MOBILE");
            entity.Property(e => e.OnboardRefName1)
                .HasMaxLength(50)
                .HasColumnName("ONBOARD_REF_NAME_1");
            entity.Property(e => e.OnboardRefName2)
                .HasMaxLength(50)
                .HasColumnName("ONBOARD_REF_NAME_2");
            entity.Property(e => e.OnboardRefNo)
                .HasMaxLength(20)
                .HasColumnName("ONBOARD_REF_NO");
            entity.Property(e => e.OnboardVia)
                .HasMaxLength(50)
                .HasColumnName("ONBOARD_VIA");
            entity.Property(e => e.OriginalDocAck)
                .HasMaxLength(20)
                .HasColumnName("ORIGINAL_DOC_ACK");
            entity.Property(e => e.OriginalDocAckBack)
                .HasMaxLength(20)
                .HasColumnName("ORIGINAL_DOC_ACK_BACK");
            entity.Property(e => e.OriginalDocAckNo)
                .HasMaxLength(20)
                .HasColumnName("ORIGINAL_DOC_ACK_NO");
            entity.Property(e => e.OriginalDocAckProof)
                .HasColumnName("ORIGINAL_DOC_ACK_PROOF");
            entity.Property(e => e.OriginalDocHandover)
                .HasMaxLength(20)
                .HasColumnName("ORIGINAL_DOC_HANDOVER");
            entity.Property(e => e.OriginalDocList).HasColumnName("ORIGINAL_DOC_LIST");
            entity.Property(e => e.OriginalDocSubmission)
                .HasMaxLength(20)
                .HasColumnName("ORIGINAL_DOC_SUBMISSION");
            entity.Property(e => e.OtherCerf1)
                .HasColumnName("OTHER_CERF_1");
            entity.Property(e => e.OtherCerf2)
                .HasColumnName("OTHER_CERF_2");
            entity.Property(e => e.OtherCerf3)
                .HasColumnName("OTHER_CERF_3");
            entity.Property(e => e.OtherCerfDuration1)
                .HasMaxLength(20)
                .HasColumnName("OTHER_CERF_DURATION_1");
            entity.Property(e => e.OtherCerfDuration2)
                .HasMaxLength(20)
                .HasColumnName("OTHER_CERF_DURATION_2");
            entity.Property(e => e.OtherCerfDuration3)
                .HasMaxLength(20)
                .HasColumnName("OTHER_CERF_DURATION_3");
            entity.Property(e => e.OtherCerfInstitute1).HasColumnName("OTHER_CERF_INSTITUTE_1");
            entity.Property(e => e.OtherCerfInstitute2).HasColumnName("OTHER_CERF_INSTITUTE_2");
            entity.Property(e => e.OtherCerfInstitute3).HasColumnName("OTHER_CERF_INSTITUTE_3");
            entity.Property(e => e.OtherCerfMark1).HasColumnName("OTHER_CERF_MARK_1");
            entity.Property(e => e.OtherCerfMark2).HasColumnName("OTHER_CERF_MARK_2");
            entity.Property(e => e.OtherCerfMark3).HasColumnName("OTHER_CERF_MARK_3");
            entity.Property(e => e.OtherCerfName1)
                .HasMaxLength(50)
                .HasColumnName("OTHER_CERF_NAME_1");
            entity.Property(e => e.OtherCerfName2)
                .HasMaxLength(50)
                .HasColumnName("OTHER_CERF_NAME_2");
            entity.Property(e => e.OtherCerfName3)
                .HasMaxLength(50)
                .HasColumnName("OTHER_CERF_NAME_3");
            entity.Property(e => e.OtherCerfPassYear1)
                .HasMaxLength(50)
                .HasColumnName("OTHER_CERF_PASS_YEAR_1");
            entity.Property(e => e.OtherCerfPassYear2)
                .HasMaxLength(50)
                .HasColumnName("OTHER_CERF_PASS_YEAR_2");
            entity.Property(e => e.OtherCerfPassYear3)
                .HasMaxLength(20)
                .HasColumnName("OTHER_CERF_PASS_YEAR_3");
            entity.Property(e => e.PanCard)
                .HasColumnName("PAN_CARD");
            entity.Property(e => e.PanNo)
                .HasMaxLength(50)
                .HasColumnName("PAN_NO");
            entity.Property(e => e.PassportCard)
                .HasColumnName("PASSPORT_CARD");
            entity.Property(e => e.PassportNo)
                .HasMaxLength(50)
                .HasColumnName("PASSPORT_NO");
            entity.Property(e => e.PassportVal).HasColumnName("PASSPORT_VAL");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(20)
                .HasColumnName("PAYMENT_MODE");
            entity.Property(e => e.PerAccNo)
                .HasMaxLength(50)
                .HasColumnName("PER_ACC_NO");
            entity.Property(e => e.PerBnkBranch)
                .HasMaxLength(50)
                .HasColumnName("PER_BNK_BRANCH");
            entity.Property(e => e.PerBnkIfsc)
                .HasMaxLength(50)
                .HasColumnName("PER_BNK_IFSC");
            entity.Property(e => e.PerBnkName)
                .HasMaxLength(50)
                .HasColumnName("PER_BNK_NAME");
            entity.Property(e => e.PerEmail)
                .HasMaxLength(50)
                .HasColumnName("PER_EMAIL");
            entity.Property(e => e.PerMobile)
                .HasMaxLength(20)
                .HasColumnName("PER_MOBILE");
            entity.Property(e => e.PermAddressCity)
                .HasMaxLength(50)
                .HasColumnName("PERM_ADDRESS_CITY");
            entity.Property(e => e.PermAddressLine1).HasColumnName("PERM_ADDRESS_LINE_1");
            entity.Property(e => e.PermAddressLine2).HasColumnName("PERM_ADDRESS_LINE_2");
            entity.Property(e => e.PermAddressState)
                .HasMaxLength(50)
                .HasColumnName("PERM_ADDRESS_STATE");
            entity.Property(e => e.PermAddressZipcode).HasColumnName("PERM_ADDRESS_ZIPCODE");
            entity.Property(e => e.PreWorkCmp1).HasColumnName("PRE_WORK_CMP_1");
            entity.Property(e => e.PreWorkCmp2).HasColumnName("PRE_WORK_CMP_2");
            entity.Property(e => e.PreWorkCmp3).HasColumnName("PRE_WORK_CMP_3");
            entity.Property(e => e.PreWorkCmp4).HasColumnName("PRE_WORK_CMP_4");
            entity.Property(e => e.PreWorkCmp5).HasColumnName("PRE_WORK_CMP_5");
            entity.Property(e => e.PreWorkCmpCtc).HasColumnName("PRE_WORK_CMP_CTC");
            entity.Property(e => e.PreWorkCmpDoc1)
                .HasColumnName("PRE_WORK_CMP_DOC_1");
            entity.Property(e => e.PreWorkCmpDoc2)
                .HasColumnName("PRE_WORK_CMP_DOC_2");
            entity.Property(e => e.PreWorkCmpDoc3)
                .HasColumnName("PRE_WORK_CMP_DOC_3");
            entity.Property(e => e.PreWorkCmpDoc4)
                .HasColumnName("PRE_WORK_CMP_DOC_4");
            entity.Property(e => e.PreWorkCmpDoc5)
                .HasColumnName("PRE_WORK_CMP_DOC_5");
            entity.Property(e => e.PreWorkCmpEdt1).HasColumnName("PRE_WORK_CMP_EDT_1");
            entity.Property(e => e.PreWorkCmpEdt2).HasColumnName("PRE_WORK_CMP_EDT_2");
            entity.Property(e => e.PreWorkCmpEdt3).HasColumnName("PRE_WORK_CMP_EDT_3");
            entity.Property(e => e.PreWorkCmpEdt4).HasColumnName("PRE_WORK_CMP_EDT_4");
            entity.Property(e => e.PreWorkCmpEdt5).HasColumnName("PRE_WORK_CMP_EDT_5");
            entity.Property(e => e.PreWorkCmpEpfStatus)
                .HasMaxLength(20)
                .HasColumnName("PRE_WORK_CMP_EPF_STATUS");
            entity.Property(e => e.PreWorkCmpEsiStatus)
                .HasMaxLength(20)
                .HasColumnName("PRE_WORK_CMP_ESI_STATUS");
            entity.Property(e => e.PreWorkCmpSdt1).HasColumnName("PRE_WORK_CMP_SDT_1");
            entity.Property(e => e.PreWorkCmpSdt2).HasColumnName("PRE_WORK_CMP_SDT_2");
            entity.Property(e => e.PreWorkCmpSdt3).HasColumnName("PRE_WORK_CMP_SDT_3");
            entity.Property(e => e.PreWorkCmpSdt4).HasColumnName("PRE_WORK_CMP_SDT_4");
            entity.Property(e => e.PreWorkCmpSdt5).HasColumnName("PRE_WORK_CMP_SDT_5");
            entity.Property(e => e.PresentAddressCity)
                .HasMaxLength(50)
                .HasColumnName("PRESENT_ADDRESS_CITY");
            entity.Property(e => e.PresentAddressLine1).HasColumnName("PRESENT_ADDRESS_LINE_1");
            entity.Property(e => e.PresentAddressLine2).HasColumnName("PRESENT_ADDRESS_LINE_2");
            entity.Property(e => e.PresentAddressState)
                .HasMaxLength(50)
                .HasColumnName("PRESENT_ADDRESS_STATE");
            entity.Property(e => e.PresentAddressZipcode).HasColumnName("PRESENT_ADDRESS_ZIPCODE");
            entity.Property(e => e.ReleavedReason).HasColumnName("RELEAVED_REASON");
            entity.Property(e => e.ReportingTo).HasColumnName("REPORTING_TO");
            entity.Property(e => e.RoomRent)
                .HasMaxLength(20)
                .HasColumnName("ROOM_RENT");
            entity.Property(e => e.SalAccEligibility)
                .HasMaxLength(20)
                .HasColumnName("SAL_ACC_ELIGIBILITY");
            entity.Property(e => e.SalAccNo)
                .HasMaxLength(50)
                .HasColumnName("SAL_ACC_NO");
            entity.Property(e => e.SalBankBranch)
                .HasMaxLength(50)
                .HasColumnName("SAL_BANK_BRANCH");
            entity.Property(e => e.SalBankIfsc)
                .HasMaxLength(50)
                .HasColumnName("SAL_BANK_IFSC");
            entity.Property(e => e.SalBankName)
                .HasMaxLength(50)
                .HasColumnName("SAL_BANK_NAME");
            entity.Property(e => e.SalBenfCode)
                .HasMaxLength(50)
                .HasColumnName("SAL_BENF_CODE");
            entity.Property(e => e.SalCriteria)
                .HasMaxLength(20)
                .HasColumnName("SAL_CRITERIA");
            entity.Property(e => e.SalaryPaidBy)
                .HasMaxLength(50)
                .HasColumnName("SALARY_PAID_BY");
            entity.Property(e => e.SslcCerf)
                .HasColumnName("SSLC_CERF");
            entity.Property(e => e.SslcMark).HasColumnName("SSLC_MARK");
            entity.Property(e => e.SslcPassYear)
                .HasMaxLength(50)
                .HasColumnName("SSLC_PASS_YEAR");
            entity.Property(e => e.SslcSchoolName).HasColumnName("SSLC_SCHOOL_NAME");
            entity.Property(e => e.TcCard)
                .HasColumnName("TC_CARD");
            entity.Property(e => e.WorkExBreak1).HasColumnName("WORK_EX_BREAK_1");
            entity.Property(e => e.WorkExBreak2).HasColumnName("WORK_EX_BREAK_2");
            entity.Property(e => e.WorkExBreak3).HasColumnName("WORK_EX_BREAK_3");
            entity.Property(e => e.WorkExBreak4).HasColumnName("WORK_EX_BREAK_4");
            entity.Property(e => e.WorkExBreakEdt1).HasColumnName("WORK_EX_BREAK_EDT_1");
            entity.Property(e => e.WorkExBreakEdt2).HasColumnName("WORK_EX_BREAK_EDT_2");
            entity.Property(e => e.WorkExBreakEdt3).HasColumnName("WORK_EX_BREAK_EDT_3");
            entity.Property(e => e.WorkExBreakEdt4).HasColumnName("WORK_EX_BREAK_EDT_4");
            entity.Property(e => e.WorkExBreakSdt1).HasColumnName("WORK_EX_BREAK_SDT_1");
            entity.Property(e => e.WorkExBreakSdt2).HasColumnName("WORK_EX_BREAK_SDT_2");
            entity.Property(e => e.WorkExBreakSdt3).HasColumnName("WORK_EX_BREAK_SDT_3");
            entity.Property(e => e.WorkExBreakSdt4).HasColumnName("WORK_EX_BREAK_SDT_4");

            entity.HasOne(d => d.BloodGrpNavigation).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.BloodGrp)
                .HasConstraintName("FK_BLD_GRP___BLD_GRP_M");

            entity.HasOne(d => d.Contractor).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.ContractorId)
                .HasConstraintName("FK_CON_ID___CON_M");

            entity.HasOne(d => d.EmpCtgNavigation).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.EmpCtg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ctg");

            entity.HasOne(d => d.EmpRoleNavigation).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.EmpRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emp_Master_Emp_Role_User_login");

            entity.HasOne(d => d.EsiEpfEligibilityNavigation).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.EsiEpfEligibility)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leg_name__Esi_Epf_Elig___Emp_Master");

            entity.HasOne(d => d.HighQualiNavigation).WithMany(p => p.EmployeeMasterData1HighQualiNavigations)
                .HasForeignKey(d => d.HighQuali)
                .HasConstraintName("FK_Quali_M___High_Quali");

            entity.HasOne(d => d.HighQuali2Navigation).WithMany(p => p.EmployeeMasterData1HighQuali2Navigations)
                .HasForeignKey(d => d.HighQuali2)
                .HasConstraintName("FK_Quali_M___High_Quali_2");

            entity.HasOne(d => d.NationalityNavigation).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.Nationality)
                .HasConstraintName("FK_Nationality_M__Emp_Master");

            entity.HasOne(d => d.PerBnkNameNavigation).WithMany(p => p.EmployeeMasterData1PerBnkNameNavigations)
                .HasForeignKey(d => d.PerBnkName)
                .HasConstraintName("FK_BANK_EMP_PER_BANK");

            entity.HasOne(d => d.SalBankNameNavigation).WithMany(p => p.EmployeeMasterData1SalBankNameNavigations)
                .HasForeignKey(d => d.SalBankName)
                .HasConstraintName("FK_BANK_EMP_SAL_BANK");

            entity.HasOne(d => d.SalaryPaidByNavigation).WithMany(p => p.EmployeeMasterData1s)
                .HasForeignKey(d => d.SalaryPaidBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SAL_PAID___COMPANY_M");
        });

        modelBuilder.Entity<LegalM>(entity =>
        {
            entity.HasKey(e => e.LegName).HasName("PK__Legel_Name");

            entity.ToTable("Legal_M");

            entity.HasIndex(e => e.LegId, "UQ__Legel_ID").IsUnique();

            entity.Property(e => e.LegName)
                .HasMaxLength(20)
                .HasColumnName("LEG_NAME");
            entity.Property(e => e.LegDesc).HasColumnName("LEG_DESC");
            entity.Property(e => e.LegId)
                .ValueGeneratedOnAdd()
                .HasColumnName("LEG_ID");
        });

        modelBuilder.Entity<NationalityM>(entity =>
        {
            entity.HasKey(e => e.Nationality);

            entity.ToTable("Nationality_M");

            entity.HasIndex(e => e.NationalityId, "UQ__National__ID").IsUnique();

            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("NATIONALITY");
            entity.Property(e => e.NationalityDesc).HasColumnName("NATIONALITY_DESC");
            entity.Property(e => e.NationalityId)
                .ValueGeneratedOnAdd()
                .HasColumnName("NATIONALITY_ID");
        });

        modelBuilder.Entity<QualificationM>(entity =>
        {
            entity.HasKey(e => e.QualiName).HasName("PK_Qualification_Name");

            entity.ToTable("Qualification_M");

            entity.HasIndex(e => e.QualiId, "UQ__Quali_ID").IsUnique();

            entity.Property(e => e.QualiName)
                .HasMaxLength(50)
                .HasColumnName("QUALI_NAME");
            entity.Property(e => e.QualiDesc).HasColumnName("QUALI_DESC");
            entity.Property(e => e.QualiId)
                .ValueGeneratedOnAdd()
                .HasColumnName("QUALI_ID");
        });

        modelBuilder.Entity<SalaryM>(entity =>
        {
            entity.HasKey(e => e.SalId).HasName("PK_SALARY_M");

            entity.ToTable("Salary_M");

            entity.Property(e => e.SalId).HasColumnName("SAL_ID");
            entity.Property(e => e.SalCategory)
                .HasMaxLength(20)
                .HasColumnName("SAL_CATEGORY");
            entity.Property(e => e.SalDesc).HasColumnName("SAL_DESC");
            entity.Property(e => e.SalType)
                .HasMaxLength(20)
                .HasColumnName("SAL_TYPE");

            entity.HasOne(d => d.SalCategoryNavigation).WithMany(p => p.SalaryMs)
                .HasForeignKey(d => d.SalCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sal_Ctg_Ctg_M");
        });

        modelBuilder.Entity<UserLoginM>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK_User_Login_id");

            entity.ToTable("User_Login_M");

            entity.Property(e => e.LoginId)
                .HasMaxLength(20)
                .HasColumnName("LOGIN_ID");
            entity.Property(e => e.EmpGroup)
                .HasMaxLength(50)
                .HasColumnName("EMP_GROUP");
            entity.Property(e => e.EmpRole)
                .HasMaxLength(50)
                .HasColumnName("EMP_ROLE");
            entity.Property(e => e.EmpState)
                .HasMaxLength(50)
                .HasColumnName("EMP_STATE");
            entity.Property(e => e.EmpSubgroup)
                .HasMaxLength(50)
                .HasColumnName("EMP_SUBGROUP");
            entity.Property(e => e.LastLogged).HasColumnName("Last_Logged");
            entity.Property(e => e.OldPassword1)
                .HasMaxLength(50)
                .HasColumnName("OLD_PASSWORD_1");
            entity.Property(e => e.OldPassword2)
                .HasMaxLength(50)
                .HasColumnName("OLD_PASSWORD_2");
            entity.Property(e => e.OldPassword3)
                .HasMaxLength(50)
                .HasColumnName("OLD_PASSWORD_3");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.EmpRoleNavigation).WithMany(p => p.UserLoginMs)
                .HasForeignKey(d => d.EmpRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Rights_Emp_Role_User_login");
        });

        modelBuilder.Entity<UserRightsM>(entity =>
        {
            entity.HasKey(e => e.EmpRole).HasName("PK_User_Rights_M_1");

            entity.ToTable("User_Rights_M");

            entity.HasIndex(e => e.EmpRoleId, "UQ__User_Rig__94C92AE215F20917").IsUnique();

            entity.Property(e => e.EmpRole)
                .HasMaxLength(50)
                .HasColumnName("EMP_ROLE");
            entity.Property(e => e.EmpRoleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("EMP_ROLE_ID");
            entity.Property(e => e.RoleCreate)
                .HasMaxLength(20)
                .HasColumnName("ROLE_CREATE");
            entity.Property(e => e.RoleDelete)
                .HasMaxLength(20)
                .HasColumnName("ROLE_DELETE");
            entity.Property(e => e.RoleEdit)
                .HasMaxLength(20)
                .HasColumnName("ROLE_EDIT");
            entity.Property(e => e.RoleView)
                .HasMaxLength(20)
                .HasColumnName("ROLE_VIEW");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
