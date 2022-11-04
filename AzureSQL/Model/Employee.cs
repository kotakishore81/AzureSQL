using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureSQL.Model
{
    public class Employee
    {
        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       // public string Email { get; set; }
       // public string Email { get; set; }
       // services.AddHttpsRedirection(options =>
			//{
			//	options.HttpsPort = 443;
		//	});
        public string PhoneNumber { get; set; }
    }
}
