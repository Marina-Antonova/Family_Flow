using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.GCommon.Enums;
using FamilyFlow.Web.ViewModels.Schedule;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FamilyFlow.Services.Tests;

[TestFixture]
public class ScheduleServiceTests
{
    [Test]
    public async Task GetFullScheduleAsync_ReturnsEventsAndTasksOrderedByEndTime()
    {
        // Arrange
        using FamilyFlowDbContext dbContext = CreateDbContext();
        Guid ownerUserId = Guid.NewGuid();

        Family family = new()
        {
            Id = 1,
            Name = "Test Family",
            UserId = ownerUserId
        };

        FamilyMember child = CreateFamilyMember(1, "Mila", FamilyRole.Daughter, ownerUserId);
        FamilyMember adult = CreateFamilyMember(2, "Maria", FamilyRole.Mother, ownerUserId);

        ScheduleEvent scheduleEvent = new()
        {
            Id = 1,
            Title = "Doctor Visit",
            StartTime = new DateTime(2026, 4, 7, 9, 0, 0),
            EndTime = new DateTime(2026, 4, 7, 10, 0, 0),
            CreatorId = child.Id,
            AccompanyingAdultId = adult.Id,
            AccompanyingAdult = adult,
            Participants =
            [
                new ScheduleEventParticipant
                {
                    ScheduleEventId = 1,
                    FamilyMemberId = child.Id,
                    FamilyMember = child
                }
            ]
        };

        HouseTask houseTask = new()
        {
            Id = 1,
            Title = "Clean Room",
            DueDate = new DateTime(2026, 4, 7, 8, 0, 0),
            FamilyMemberId = child.Id,
            FamilyMemberHouseTasks = child
        };

        await dbContext.Families.AddAsync(family);
        await dbContext.FamilyMembers.AddRangeAsync(child, adult);
        await dbContext.ScheduleEvents.AddAsync(scheduleEvent);
        await dbContext.HouseTasks.AddAsync(houseTask);
        await dbContext.SaveChangesAsync();

        ScheduleService service = new(dbContext);

        //        // Act
        List<ScheduleItemViewModel> result = await service.GetFullScheduleAsync(ownerUserId.ToString());

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));

        Assert.That(result[0].Title, Is.EqualTo("Clean Room"));
        Assert.That(result[0].Type, Is.EqualTo("Task"));
        Assert.That(result[0].FamilyMemberName, Is.EqualTo("Mila"));
        Assert.That(result[0].StartTime, Is.Null);
        Assert.That(result[0].EndTime, Is.EqualTo(new DateTime(2026, 4, 7, 8, 0, 0)));
        Assert.That(result[0].AccompanyingAdultName, Is.Null);

        Assert.That(result[1].Title, Is.EqualTo("Doctor Visit"));
        Assert.That(result[1].Type, Is.EqualTo("Event"));
        Assert.That(result[1].FamilyMemberName, Is.EqualTo("Mila"));
        Assert.That(result[1].StartTime, Is.EqualTo(new DateTime(2026, 4, 7, 9, 0, 0)));
        Assert.That(result[1].EndTime, Is.EqualTo(new DateTime(2026, 4, 7, 10, 0, 0)));
        Assert.That(result[1].AccompanyingAdultName, Is.EqualTo("Maria"));
    }

    [Test]
    public async Task GetFullScheduleAsync_ReturnsEmptyList_WhenNoEventsOrTasksExist()
    {
        // Arrange
        using FamilyFlowDbContext dbContext = CreateDbContext();
        Guid ownerUserId = Guid.NewGuid();

        Family family = new()
        {
            Id = 1,
            Name = "Test Family",
            UserId = ownerUserId
        };

        await dbContext.Families.AddAsync(family);
        await dbContext.SaveChangesAsync();

        ScheduleService service = new(dbContext);

        // Act
        List<ScheduleItemViewModel> result = await service.GetFullScheduleAsync(ownerUserId.ToString());

        // Assert
        Assert.That(result, Is.Empty);
    }

    private static FamilyFlowDbContext CreateDbContext()
    {
        DbContextOptions<FamilyFlowDbContext> options = new DbContextOptionsBuilder<FamilyFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new FamilyFlowDbContext(options);
    }

    private static FamilyMember CreateFamilyMember(int id, string name, FamilyRole role, Guid ownerUserId)
    {
        return new FamilyMember
        {
            Id = id,
            Name = name,
            Role = role,
            Age = role is FamilyRole.Mother or FamilyRole.Father ? 34 : 10,
            FamilyId = 1,
            UserId = ownerUserId
        };
    }
}
