using FamilyFlow.Web.ViewModels.ScheduleEvent;
using FamilyFlow.Data.Models;

namespace FamilyFlow.Services.Core.Interfaces
{
    public interface IScheduleEventService
    {
        Task<CreateEditEventViewModel?> GetForCreateScheduleEventViewModelAsync(int familyMemberId);

        Task CreateScheduleEventAsync(CreateEditEventViewModel inputModel, int familyMemberId);
        Task<CreateEditEventViewModel?> GetForEditScheduleEventViewModelAsync(int id);

        Task EditScheduleEventAsync(int id, CreateEditEventViewModel inputModel);

        Task<DeleteEventViewModel?> GetForDeleteScheduleEventViewModelAsync(int id);

        Task DeleteScheduleEventAsync(int id, DeleteEventViewModel viewModel);

        IEnumerable<CreateEditAdultViewModel> GetAllAdults(string userId);
        IEnumerable<CreateEditAdultViewModel> GetAllMembers(string userId);

        Task<FamilyMember?> FindSelectedFamilyMemberAsync(int familyMemberId);

    }
}
