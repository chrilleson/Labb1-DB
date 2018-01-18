using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1_DB
{
    public class Games
    {
        public int GameID { get; set; }
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Genre { get; set; }

        public Games(int gameid, int id, string gamename, string genre)
        {
            this.GameID = gameid;
            this.Id = id;
            this.GameName = gamename;
            this.Genre = genre;
        }
    }
}
