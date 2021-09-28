using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientReport.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(PatientViewModel patientViewModel, PlanViewModel planViewModel)
        {
            PatientViewModel = patientViewModel;
            PlanViewModel = planViewModel;
        }

        public PatientViewModel PatientViewModel { get; }
        public PlanViewModel PlanViewModel { get; }
    }
}
