using FamilyFlow.ViewModels.Schedule;

namespace FamilyFlow.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ScheduleItemViewModel>> GetFullScheduleAsync();
    }
}