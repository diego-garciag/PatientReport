using PatientReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace PatientReport.ViewModels
{
    public class PatientViewModel
    {
        public PatientModel PatientInfo { get; set; }
        public PatientViewModel(Patient patient)
        {
            PatientInfo = new PatientModel
            {
                Id = patient.Id,
                Name = $"{patient.LastName}, {patient.FirstName}",
                DOB = patient.DateOfBirth == null ? "N/A" : patient.DateOfBirth.ToString(),
                Hospital = patient.Hospital.Id
            };
        }
    }
}
