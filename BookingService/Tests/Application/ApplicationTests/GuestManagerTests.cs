using Application;
using Application.Guest.DTO;
using Application.Guest.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Domain.ValueObjects;
using Moq;

namespace ApplicationTests;

public class Tests
{
    GuestManager _guestManager;
    [SetUp]
    public void Setup()
    {
        var fakeRepo = new Mock<IGuestRepository>();
        
        fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(1123));
        _guestManager = new GuestManager(fakeRepo.Object);
    }

    [Test]
    public async Task HappyPath()
    {
        var guestDTO = new GuestDTO
        {
            Name = "GuestName",
            Surname = "GuestSurname",
            Email = "GuestEmail@email.com",
            IdNumber = "12321321",
            IdTypeCode = 1
        };
        
        var res = await _guestManager.CreateGuest(new CreateGuestRequest
        {
            Data = guestDTO
        });
        
        Assert.That(res, Is.Not.Null);
        Assert.That(res.Sucess, Is.True);
    }

    [TestCase("")]
    [TestCase(null)]
    [TestCase("a")]
    [TestCase("ad")]
    [TestCase("1")]
    [TestCase("123")]
    public async Task ShouldReturnInvalidPersonDocumentIdExceptionWhenDocsAreInvalid(string? docNumber)
    {
        var guestDTO = new GuestDTO
        {
            Name = "GuestName",
            Surname = "GuestSurname",
            Email = "GuestEmail@email.com",
            IdNumber = docNumber,
            IdTypeCode = 1
        };
        
        var res = await _guestManager.CreateGuest(new CreateGuestRequest
        {
            Data = guestDTO
        });
        
        Assert.That(res, Is.Not.Null);
        Assert.That(res.Sucess, Is.False);
        Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodesEnum.INVALID_PERSON_ID));
        Assert.That(res.Message, Is.EqualTo("The ID passed is not valid"));
    }
    
    [TestCase("","surnametest", "email@email.com")]
    [TestCase(null,"surnametest", "email@email.com")]
    [TestCase("name","", "email@email.com")]
    [TestCase("name",null, "email@email.com")]
    [TestCase("name","surnametest", "")]
    [TestCase("name","surnametest", null)]
    public async Task ShouldReturnMissingRequiredInformationExceptionWhenDataAreInvalid(
        string? name,
        string? surname,
        string? email)
    {
        var guestDTO = new GuestDTO
        {
            Name = name,
            Surname = surname,
            Email = email,
            IdNumber = "1312312313",
            IdTypeCode = 1
        };
        
        var res = await _guestManager.CreateGuest(new CreateGuestRequest
        {
            Data = guestDTO
        });
        
        Assert.That(res, Is.Not.Null);
        Assert.That(res.Sucess, Is.False);
        Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodesEnum.MISSING_REQUIRED_DATA));
        Assert.That(res.Message, Is.EqualTo("Missing required fields"));
    }
    
    
    [TestCase("email")]
    public async Task ShouldReturnInvalidEmailExceptionWhenEmailAreInvalid(
        string email)
    {
        var guestDTO = new GuestDTO
        {
            Name = "cleber",
            Surname = "silva",
            Email = email,
            IdNumber = "1312312313",
            IdTypeCode = 1
        };
        
        var res = await _guestManager.CreateGuest(new CreateGuestRequest
        {
            Data = guestDTO
        });
        
        Assert.That(res, Is.Not.Null);
        Assert.That(res.Sucess, Is.False);
        Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodesEnum.INVALID_EMAIL));
        Assert.That(res.Message, Is.EqualTo("The given email is not valid"));
    }
}