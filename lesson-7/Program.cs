using System;
using System.Collections.Generic;

namespace lesson_7
{
   

    class Program
    {
        static void Main(string[] args)
        {
            CompanyInfo companyInfo = new CompanyInfo()
            {
                CompanyName = "ООО Моя супер компания",
                CompanyPhone = "88005553535",
                CompanyCode = Guid.NewGuid(),
                Сountry = "Россия",
                City = "Москва",
                Street = "Луговая",
                House = "1",
                CodeСountry = 100,
                ListEmployees =new List<string> { "Сотрудник"}
            };

            ReportService reportService = new ReportService();

            reportService.GenerateReport(companyInfo);
        }
    }

}
