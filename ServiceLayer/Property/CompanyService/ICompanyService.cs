using DomainLayer.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.CompanyService
{
    public interface ICompanyService
    {
        IEnumerable<CompanyModel> GetCompanys();
        CompanyModel GetCompany(int id);
        void CreateCompany(CompanyModel company);
        void UpdateCompany(CompanyModel company);
        void DeleteCompany(int id);
    }
}
