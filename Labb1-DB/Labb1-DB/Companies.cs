using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1_DB
{
    public class Companies
    {
        public int CompanyId { get; set; }
        public string Established { get; set; }
        public string CompanyName { get; set; }

        public Companies(int companyid, string established, string companyname)
        {
            this.CompanyName = companyname;
            this.CompanyId = companyid;
            this.Established = established;
        }
    }
}
