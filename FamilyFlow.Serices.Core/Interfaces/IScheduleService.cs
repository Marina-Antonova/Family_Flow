using FamilyFlow.Web.ViewModels.Schedule;

namespace FamilyFlow.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ScheduleItemViewModel>> GetFullScheduleAsync(string userId);
    }
}