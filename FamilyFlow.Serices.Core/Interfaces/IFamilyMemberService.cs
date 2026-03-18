using FamilyFlow.Web.ViewModels.FamilyMember;

namespace FamilyFlow.Services.Core.Interfaces
{
    public interface IFamilyMemberService
    {
        Task<IEnumerable<AllFamilyMembersViewModel>> GetAllFamilyMembersAsync(string userId);

        Task<DetailsFamilyMemberViewModel?> GetDetailsForFamilyMemberAsync(int id, string userId);

        Task CreateFamilyMemberAsync(CreateFamilyMemberViewModel inputModel, string userId, int familyId);

        Task<CreateFamilyMemberViewModel?> GetForEditFamilyMemberAsync(int id);

        Task EditFamilyMemberAsync(int id, CreateFamilyMemberViewModel inputModel);

        Task<DeleteFamilyMemberViewModel?> GetForDeleteFamilyMemberAsync(int id);

        Task DeleteFamilyMemberAsync(int id, DeleteFamilyMemberViewModel? viewModel);

    }
}
