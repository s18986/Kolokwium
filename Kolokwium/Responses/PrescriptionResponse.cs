using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Responses
{
    public class PrescriptionResponse
    {
        public int IdPrescription { get; set; }
        public List<String> Leki { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public string temp { get; set; }
    }
}
