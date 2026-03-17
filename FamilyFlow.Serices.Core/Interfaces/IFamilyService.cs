using FamilyFlow.Web.ViewModels.Family;
namespace FamilyFlow.Services.Core.Interfaces
{
    public interface IFamilyService
    {
        Task<FamilyViewModel?> GetFamilyForUserAsync(string userId);

        Task CreateFamilyAsync(FamilyViewModel model);

        Task<FamilyViewModel?> GetFamilyAsync(int id);

        Task EditFamilyAsync(int id, FamilyViewModel inputModel);

        Task DeleteFamilyAsync(int id, FamilyViewModel? viewModel);
    }
}
