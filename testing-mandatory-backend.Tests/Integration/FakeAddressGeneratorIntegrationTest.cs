﻿using testing_mandatory_backend.Models;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using Xunit;

[Trait("Category", "Integration")]
/* 
    Run the tests that are in the "Sequential" collection sequentially (and not in parallel)
    This is because they share the same test database (the TestDatabaseFixture)
    If they don't run sequentially different tests will fail at different times
*/
[Collection("Sequential")]
public class FakeAddressGeneratorIntegrationTest: IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private readonly PostalCodeRepository _postalCodeRepository;
    private readonly FakeAddressGenerator _generator;

    public FakeAddressGeneratorIntegrationTest(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
        _fixture.CreateNewConnection();
        _fixture.ResetDatabase();

        _postalCodeRepository = new(_fixture.Connection);
        _generator = new(_postalCodeRepository);
    }

    [Fact]
    public void GenerateFakeAddress_Returns_CorrectOutput()
    {
        // Arrange
        (string postalCode, string townName) seedData = ("1111", "TestTown");
        _fixture.SeedDatabase(seedData);

        // Act
        FakeAddress fakeAddress = _generator.GenerateFakeAddress();

        // Assert
        Assert.Equal(fakeAddress.PostalCode, seedData.postalCode);
        Assert.Equal(fakeAddress.TownName, seedData.townName);
    }

    [Fact]
    public void GenerateFakeAddress_ThrowsException_When_EmptyList()
    {
        // Act and Assert
        Assert.Throws<Exception>(()  => _generator.GenerateFakeAddress());
    }

    [Theory]
    [InlineData("1111", null)]
    [InlineData(null, "Glostrup")]
    [InlineData(null, null)]
    [InlineData("1111", "")]
    [InlineData("", "Glostrup")]
    [InlineData("", "")]
    public void GenerateFakeAddress_ThrowsException_When_PostalCodeOrTownName_IsMissing(string? postalCode, string? townName)
    {
        // Arrange
        var seedData = (postalCode, townName);
        _fixture.SeedDatabase(seedData);

        // Act and Assert
        Assert.Throws<Exception>(() => _generator.GenerateFakeAddress());
    }
}
