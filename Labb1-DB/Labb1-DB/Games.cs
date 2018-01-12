using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1_DB
{
    public class Games
    {
        public int Id { get; set; }
        public int CompanyID { get; set; }
        public string GameName { get; set; }
        public string Genre { get; set; }

        public Games(int id, int companyid, string gamename, string genre)
        {
            this.Id = id;
            this.CompanyID = companyid;
            this.GameName = gamename;
            this.Genre = genre;
        }
    }
}
