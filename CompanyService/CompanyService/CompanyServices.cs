using CompanyDomain.Models.Company;
using CompanyRepository.Infrastructure.Company;

namespace CompanyService.Property.CompanyService
{
    public class CompanyServices : ICompanyService
    {
        private ICompanyLogic<CompanyModel> _company;
        public CompanyServices(ICompanyLogic<CompanyModel> company)
        {
            _company = company;
        }
        public IEnumerable<CompanyModel> GetCompanys()
        {
            return _company.GetAll();
        }

        public CompanyModel GetCompany(int id)
        {
            return _company.Get(id);
        }

        public void CreateCompany(CompanyModel company)
        {
            _company.Create(company);
        }
        public void UpdateCompany(CompanyModel company)
        {
            _company.Update(company);
        }

        public void DeleteCompany(int id)
        {
            CompanyModel company = GetCompany(id);
            _company.Delete(company);
            _company.SaveChanges();
        }
    }
}
