using System.ComponentModel.DataAnnotations;

namespace FamilyFlow.ViewModels.FamilyMember
{
    public class DetailsHouseTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime DueDate { get; set; }
    }
}
