using FamilyFlow.Data.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Globalization;

namespace FamilyFlow.ViewModels.Schedule
{
    public class ScheduleItemViewModel
    {
        public string FamilyMemberName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string AccompanyingAdultName { get; set; }

    }
}
