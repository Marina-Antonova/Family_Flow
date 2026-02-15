using FamilyFlow.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static FamilyFlow.Common.ValidationConstants.HouseTask;

namespace FamilyFlow.ViewModels.HouseTasks
{
    public class CreateEditTaskViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MinLength(DescriptionMinLenght)]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [Required]
        public int FamilyMemberId { get; set; }
    }
}

