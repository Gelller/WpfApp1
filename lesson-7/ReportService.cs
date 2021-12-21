using System.IO;
using TemplateEngine.Docx;

namespace lesson_7
{
    public sealed class ReportService
    {
        public void GenerateReport(CompanyInfo companyInfo, string output = "")
        {
            if (companyInfo is null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(output))
            {
                output = Path.Combine(Directory.GetCurrentDirectory(), "CompanyReport.docx");
            }

            if (File.Exists(output))
            {
                File.Delete(output);
            }

            File.Copy("C:\\1\\Простой_договор.docx", output);

            var valuesToFill = new Content(
                new FieldContent("Company Name", companyInfo.CompanyName),
                new FieldContent("Company Phone", companyInfo.CompanyPhone),
                   new FieldContent("Company Code", companyInfo.CompanyCode.ToString()),
                      new FieldContent("Сountry", companyInfo.Сountry),
                         new FieldContent("City", companyInfo.City),
                            new FieldContent("Street", companyInfo.Street),
                               new FieldContent("House", companyInfo.House),
                                  new FieldContent("Code Сountry", companyInfo.CodeСountry.ToString()),
                                     new FieldContent("List Employees", companyInfo.ListEmployees[0])                                                               
            );

            using (var outputDocument =
                new TemplateProcessor(output)
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
            }
        }
    }

}
