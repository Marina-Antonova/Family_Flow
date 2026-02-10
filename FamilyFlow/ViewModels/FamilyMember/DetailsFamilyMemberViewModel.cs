namespace FamilyFlow.ViewModels.FamilyMember
{
    public class DetailsFamilyMemberViewModel : AllFamilyMembersViewModel
    {
        public ICollection<DetailsHouseTaskViewModel>? Tasks { get; set; }
            = new List<DetailsHouseTaskViewModel>();

        public ICollection<DetailsScheduleEventsViewModel>? Events { get; set; }
            = new List<DetailsScheduleEventsViewModel>();
    }
}
