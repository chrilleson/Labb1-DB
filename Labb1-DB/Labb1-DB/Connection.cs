using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1_DB
{
    public class Connection
    {
        private static string DBConnection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Labb1 - DB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

        public static string ConnectionString
        {
            get { return DBConnection; }
            set { DBConnection = value; }
        }
    }
}
