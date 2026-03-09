namespace FamilyFlow.ViewModels.HouseTasks
{
    public class DeleteTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string DueDate { get; set; } = null!;
        public int FamilyMemberId { get; set; }
    }
}
