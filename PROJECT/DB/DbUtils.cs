using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PROJECT.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.DB
{
    public class DbUtils
    {

        public static void ConfigureDBConnection(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Read db connection strings from appsettings.json
                string dbConnect = string.Empty;
                IConfiguration config = AppData.Instance.Configuration;
                if (config != null)
                {
                    dbConnect = config.GetConnectionString("LocalDebugMyProjectAppDB");
                }

                if (!string.IsNullOrEmpty(dbConnect))
                {
                    optionsBuilder.UseSqlServer(dbConnect);
                }
            }
        }

        public static void LogMessage(string msg)
        {
            // Log with Debug for now. Will write to real log later
            System.Diagnostics.Debug.WriteLine(msg);
        }

        public static string QuoteMe(string stringToQuote)
        {
            string quoted = "'" + stringToQuote + "'";
            return quoted;
        }
    }
}
