using FamilyFlow.ViewModels.ScheduleEvent;
using FamilyFlow.Data.Models;

namespace FamilyFlow.Services.Core.Interfaces
{
    public interface IScheduleEventService
    {
        Task<CreateEditEventViewModel?> GetForCreateScheduleEventViewModelAsync(int familyMemberId);

        Task CreateScheduleEventAsync(CreateEditEventViewModel inputModel);

        Task<CreateEditEventViewModel?> GetForEditScheduleEventViewModelAsync(int id);

        Task EditScheduleEventAsync(int id, CreateEditEventViewModel inputModel);

        Task<DeleteEventViewModel?> GetForDeleteScheduleEventViewModelAsync(int id);

        Task DeleteScheduleEventAsync(int id, DeleteEventViewModel viewModel);

        IEnumerable<CreateEditAdultViewModel> GetAllAdults();

        Task<FamilyMember?> FindSelectedFamilyMemberAsync(int familyMemberId);
    }
}
