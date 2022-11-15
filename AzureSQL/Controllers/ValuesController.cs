using Azure.Storage.Blobs;
using AzureSQL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connectionString = "DefaultEndpointsProtocol=https;AccountName=kmkstorageaccount;AccountKey=MMVUhsL4em71Wcr9iRCHF33jzNs1RLNObSQWGoL591q09j/EEB1P6ab5SIR7PKDXazKjojP86L4t+AStopa/Zg==;EndpointSuffix=core.windows.net";
        private string _containerName = "readblobcontainer";
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {

            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeDatabase")))
            {
                var sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber FROM Employee";
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var employee = new Employee()
                    {
                        Id = (long)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                    };
                    employees.Add(employee);
                }
            }
            return employees;
            //return new string[] { "value1", "value2" ,"value3"};
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public void Get(int id)
        {
           
            
            string folderPath = @"C:\Users\003PT8744\Desktop\bills";
            var files = Directory.GetFiles(folderPath, "*", System.IO.SearchOption.AllDirectories);
            //BlobServiceClient Serviceclient = new BlobServiceClient(_connectionString);
            //BlobContainerClient containerClient = Serviceclient.GetBlobContainerClient(_containerName);

            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, _containerName);
            foreach (var file in files) {
                var filePathOverCloud = file.Replace(folderPath, string.Empty);
                using (MemoryStream strem = new MemoryStream(System.IO.File.ReadAllBytes(file))) {
                    containerClient.UploadBlob(filePathOverCloud, strem);
                }
            }

            //BlobClient blobclient = containerClient.GetBlobClient("Tax_App_Unit_Testing_1.xlsx");
            //blobclient.DownloadTo(@"C:\Users\003PT8744\Downloads\abc1.xlsx");

            //return "value";
        }

        [HttpGet("{id}")]
        public string GetFiles(int id)
        {


            string folderPath = @"C:\Users\003PT8744\Desktop\bills";
            var files = Directory.GetFiles(folderPath, "*", System.IO.SearchOption.AllDirectories);
            BlobServiceClient Serviceclient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = Serviceclient.GetBlobContainerClient(_containerName);
            BlobClient blobclient = containerClient.GetBlobClient("Tax_App_Unit_Testing_1.xlsx");
            blobclient.DownloadTo(@"C:\Users\003PT8744\Downloads\abc1.xlsx");

            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
