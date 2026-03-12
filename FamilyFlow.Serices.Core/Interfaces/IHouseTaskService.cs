using FamilyFlow.ViewModels.FamilyMember;
using FamilyFlow.ViewModels.HouseTasks;

namespace FamilyFlow.Services.Core.Interfaces
{
    public interface IHouseTaskService
    {
    
        Task<CreateEditTaskViewModel?> GetForCreateHouseTaskViewModelAsync (int familyMemberId);

        Task CreateHouseTaskAsync(int familyMemberId, CreateEditTaskViewModel inputModel);

        Task<CreateEditTaskViewModel?> GetForEditHouseTaskViewModelAsync (int id);

        Task EditHouseTaskAsync(int id, CreateEditTaskViewModel inputModel);

        Task<DeleteTaskViewModel?> GetForDeleteHouseTaskViewModelAsync(int id);

        Task DeleteHouseTaskAsync(int id, DeleteTaskViewModel viewModel);

    }
}