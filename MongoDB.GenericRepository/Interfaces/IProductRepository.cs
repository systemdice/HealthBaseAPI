using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
    }

    public interface IUserRegistrationRepository : IRepository<UserRegistration>
    {
    }

    public interface IDeptInvestigationRepository : IRepository<DeptInvestigation>
    {
    }

    public interface IUnitsCategoryRepository : IRepository<UnitsCategory>
    {
    }
    
    public interface ITestsCategoryRepository : IRepository<TestsCategory>
    {
    }
	
	public interface ICategoryMasterRepository : IRepository<CategoryMaster>
    {
    }
	
	public interface IPatientDetailsRepository : IRepository<PatientDetails>
    {
    }
	public interface IReferralMasterRepository : IRepository<ReferralMaster>
    {
    }

    public interface IPaymentHistoryRepository : IRepository<PaymentHistory>
    {
    }

    public interface ITestHistoryRepository : IRepository<TestHistory>
    {
    }

    public interface IAppointmentDetailRepository : IRepository<AppointmentDetail>
    {
    }

    public interface IAvailabilityRepository : IRepository<Availability>
    {
    }

    public interface INewModifyCaseRepository : IRepository<NewModifyCase>
    {
    }
    public interface IAddQuestionRepository : IRepository<AddQuestion>
    {
    }
    public interface IFarmacyDeliveryToPatientRepository : IRepository<FarmacyDeliveryToPatient>
    {
    }

    public interface IInventoryMasterRepository : IRepository<InventoryMaster>
    {
    }
    public interface IExpensesRepository : IRepository<Expenses>
    {
    }

    public interface IReportRepository : IRepository<Report>
    {
    }

    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public interface ILabTestMasterRepository : IRepository<LabTestMaster>
    {
    }
    public interface ILabTestIndividualRepository : IRepository<LabTestIndividual>
    {
    }

    public interface IUserDetailsRepository : IRepository<UserDetails>
    {
    }
    public interface ITimesheetRepository : IRepository<Timesheet>
    {
    }
    public interface IFollowupRepository : IRepository<Followup>
    {
    }
    public interface ICompanyMasterRepository : IRepository<CompanyMaster>
    {
    }
    public interface IBedManagementRepository : IRepository<BedManagement>
    {
    }
    public interface IVaccinationsRepository : IRepository<Vaccinations>
    {
    }
    public interface IPrintBillRepository : IRepository<PrintBill>
    {
    }
    public interface ILeaveMangementRepository : IRepository<LeaveMangement>
    {
    }
    public interface IGroupTestsRepository : IRepository<GroupTests>
    {
    }

}
