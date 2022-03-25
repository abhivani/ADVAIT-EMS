using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.helper
{
    public class AppData : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private static readonly AppData instance = new AppData();

        public static AppData Instance
        {
            get { return instance; }
        }

        static AppData() { }

        private AppData() { }

        internal IConfiguration Configuration { get; set; }
    }
}
