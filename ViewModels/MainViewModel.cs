using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientReport.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(PatientViewModel patientViewModel, 
            PlanViewModel planViewModel, 
            FieldViewModel fieldViewModel)
        {
            PatientViewModel = patientViewModel;
            PlanViewModel = planViewModel;
            FieldViewModel = fieldViewModel;
        }

        public PatientViewModel PatientViewModel { get; }
        public PlanViewModel PlanViewModel { get; }
        public FieldViewModel FieldViewModel { get; }
    }
}
