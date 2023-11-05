namespace CompanyDomain.Models.Company
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyDescription { get; set; } = null!;
        public string CompanyPhone { get; set;} = null!;
        public string CompanyEmail { get; set; } = null!;
        public int CompanyStatus { get; set; }
        //public List<VanancyModel> Vanancy { get; set; }
        //public CompanyModel()
        //{
        //    Vanancy = new List<VanancyModel>();
        //}

    }
}
