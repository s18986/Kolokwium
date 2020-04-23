using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium.Requests;
using Kolokwium.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        public const string ConString = "Data Source=db-mssql;Initial Catalog=s18986;Integrated Security=True";
        [HttpGet("{id}")]
        public IActionResult GetPrescriptions(int id)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from dbo.Medicament, dbo.Prescription_Medicament, dbo.Prescription where dbo.Medicament.IdMedicament = dbo.Prescription_Medicament.IdMedicament and dbo.Prescription_Medicament.IdPrescription = dbo.Prescription.IdPrescription and dbo.Prescription.IdPrescription = @id";
                com.Parameters.AddWithValue("id", id);
                var request = new PrescriptionRequest();
                con.Open();
                var response = new PrescriptionResponse();
                SqlDataReader dr = com.ExecuteReader();
                List<String> lista = new List<string>();
                while (dr.Read())
                {
                    response.IdDoctor = (int)dr["IdDoctor"];
                    response.Date = (DateTime)dr["Date"];
                    response.DueDate = (DateTime)dr["DueDate"];
                    response.IdPatient = (int)dr["IdPatient"];
                    response.IdPrescription = (int)dr["IdPrescription"];
                    lista.Add((string)dr["Name"]);
                }
                response.Leki = lista;
                return Ok(response);
            }
            return BadRequest("XD");
        }
        [HttpPost]
        public IActionResult PostNewPrescription(NewPrescriptionRequest request)
        {
            int id = new Random().Next(10, 20000); // zabraklo czasu na dodanie id zgadzajacego sie z baza to dalem takie zeby dodac kilka razy nowa recepta i zobaczyc czy dziala
            


              using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                 
                com.Connection = con;
                com.CommandText = "insert into Prescription(IdPrescription, Date, DueDate, IdPatient, IdDoctor) "
                                      + "values (@IdPrescription, @Date, @DueDate, @IdPatient, @IdDoctor);";
                com.Parameters.AddWithValue("IdPrescription", id);
                com.Parameters.AddWithValue("Date", request.Date);
                com.Parameters.AddWithValue("DueDate", request.DueDate);
                com.Parameters.AddWithValue("IdPatient", request.IdPatient);
                com.Parameters.AddWithValue("IdDoctor", request.IdDoctor);
                con.Open();
                // zabraklo czasu
                }
                
            }
           
           
        }
      
    }
}