using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using PatientReport.Views;
using PatientReport.ViewModels;
using Prism.Events;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext context, System.Windows.Window window, ScriptEnvironment environment)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            // Eclipse cannot see oxyplot unless a dummy reference exists in this class.
            OxyPlot.Wpf.AngleAxis dummy = new OxyPlot.Wpf.AngleAxis(); // for binary plugin it has to be here.
            // DVH vms require EventAggregator.
            IEventAggregator eventAggregator = new EventAggregator();
            window.Content = new MainView()
            {
                DataContext = new MainViewModel
                (
                    new PatientViewModel(context.Patient),
                    new PlanViewModel(context.PlanSetup),
                    new FieldViewModel(context.PlanSetup),
                    new DVHPlot.ViewModels.DVHViewModel(context.PlanSetup, eventAggregator),
                    new DVHPlot.ViewModels.DVHSelectionViewModel(context.PlanSetup, eventAggregator),
                    context.Patient,
                    context.CurrentUser
                )
            };
        }
    }
}
