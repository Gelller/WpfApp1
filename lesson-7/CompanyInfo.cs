using System;
using System.Collections.Generic;

namespace lesson_7
{
    public sealed class CompanyInfo
    {
        public string? CompanyName { get; set; }
        
        public Guid? CompanyCode { get; set; }

        public string? CompanyPhone { get; set; }

        public string? Сountry { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? House { get; set; }

        public int? CodeСountry { get; set; }

        public List<string> ListEmployees { get; set; }
       
    }

}
