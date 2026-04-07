using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;


[TestFixture]
public class FamilyServiceTests
{
    private FamilyFlowDbContext dbContext;
    private FamilyService familyService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new FamilyFlowDbContext(options);
        familyService = new FamilyService(dbContext);
    }

    [Test]
    public async Task GetFamilyForUserAsync_ReturnsFamily_WhenUserIsFamilyMember()
    {
        // Arrange
        var userId = Guid.NewGuid();

        dbContext.Families.Add(new Family
        {
            Id = 1,
            Name = "Test Family",
            UserId = Guid.NewGuid()
        });

        dbContext.FamilyMembers.Add(new FamilyMember
        {
            FamilyId = 1,
            LinkedUserId = userId
        });

        await dbContext.SaveChangesAsync();

        // Act
        var result = await familyService.GetFamilyForUserAsync(userId.ToString());

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Test Family"));
    }

    [Test]
    public async Task GetFamilyForUserAsync_ReturnsNull_WhenUserIdIsInvalid()
    {
        // Act
        var result = await familyService.GetFamilyForUserAsync("invalid-guid");

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetFamilyForUserAsync_ReturnsNull_WhenUserHasNoFamily()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var result = await familyService.GetFamilyForUserAsync(userId.ToString());

        // Assert
        Assert.That(result, Is.Null);
    }
}