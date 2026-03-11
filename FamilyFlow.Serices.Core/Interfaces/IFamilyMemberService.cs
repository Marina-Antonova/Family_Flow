using FamilyFlow.ViewModels.FamilyMember;

namespace FamilyFlow.Services.Core.Interfaces
{
    public interface IFamilyMemberService
    {
        Task<IEnumerable<AllFamilyMembersViewModel>> GetAllFamilyMembersAsync();

        Task<DetailsFamilyMemberViewModel?> GetDetailsForFamilyMemberAsync(int id);

        Task CreateFamilyMemberAsync(CreateFamilyMemberViewModel inputModel);

        Task<CreateFamilyMemberViewModel?> GetForEditFamilyMemberAsync(int id);

        Task EditFamilyMemberAsync(int id, CreateFamilyMemberViewModel inputModel);

        Task<DeleteFamilyMemberViewModel?> GetForDeleteFamilyMemberAsync(int id);

        Task DeleteFamilyMemberAsync(int id, DeleteFamilyMemberViewModel? viewModel);

    }
}
