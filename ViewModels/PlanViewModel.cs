using PatientReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace PatientReport.ViewModels
{
    public class PlanViewModel
    {
        public PlanModel PlanInfo { get; set; }
        public PlanViewModel(PlanSetup planSetup)
        {
            PlanInfo = new PlanModel
            {
                Id = planSetup.Id,
                Name = planSetup.Name,
                CourseId = planSetup.Course.Id,
                ApprovalStatus = planSetup.ApprovalStatus.ToString(),
                Approver = planSetup.ApprovalHistory.OrderBy(x => x.ApprovalDateTime).Last().UserDisplayName,
                ApprovalHistory = String.Join("\n", planSetup.ApprovalHistory
                .Select(x => new { s = $"{x.ApprovalStatus} -  {x.ApprovalDateTime} by {x.UserId}" }).Select(x => x.s))
            };
            //select statement has an anonymous type inside that is the approval status, dat and user. 
        }
    }
}
