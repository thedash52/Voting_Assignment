using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace votingFrontend.Services
{
    internal class RestService
    {
        public RestService()
        {

        }

        internal async Task<bool> Login(string firstName, string lastName, DateTime dob, string electoralId)
        {
            string first = "Bob";
            string last = "Smith";
            DateTime birth = DateTime.Parse("1993-08-10");
            string id = "ABC123456";

            if (first != firstName || last != lastName || birth.Date != dob.Date || id != electoralId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
