using System;
using Xunit;
using testing_mandatory_backend.Services;
using testing_mandatory_backend.Tests.Fixtures;

namespace testing_mandatory_backend.Tests {
    [Trait("Category", "Unit")]
    public class CprGeneratorTests : IClassFixture<CprGeneratorFixture> {
        private readonly CprGeneratorFixture _fixture;

        public CprGeneratorTests(CprGeneratorFixture fixture) {
            _fixture = fixture;
        }

        // Testing GenerateRandomCpr method

        [Fact]
        public void GenerateRandomCpr_ShouldReturnValidCpr() {
            foreach (var cpr in _fixture.RandomCprs) {
                // Assert
                Assert.Matches(@"^\d{6}\d{4}$", cpr); // Check format

                string datePart = cpr.Substring(0, 6);
                DateTime date = DateTime.ParseExact(datePart, "ddMMyy", null);
                DateTime startDate = DateTime.Today.AddYears(-100);
                DateTime endDate = DateTime.Today.AddYears(-18);

                Assert.InRange(date, startDate, endDate); // Check date range
            }
        }

        // Testing GenerateCprWithBirthdayAndGender method

        [Fact]
        public void GenerateCprWithBirthdayAndGender_ShouldReturnCprInCorrectFormat() {
            foreach (var data in _fixture.RandomCprsWithGenderAndBirthday) {
                // Assert
                Assert.Matches(@"^\d{6}\d{4}$", data.Cpr); // Check format
            }
        }

        [Fact]
        public void GenerateCprWithBirthdayAndGender_ShouldMatchBirthday() {
            foreach (var data in _fixture.RandomCprsWithGenderAndBirthday) {
                // Assert
                string datePart = data.Cpr.Substring(0, 6);
                DateTime date = DateTime.ParseExact(datePart, "ddMMyy", null);

                Assert.Equal(data.Birthday.Date, date.Date); // Check if date part matches birthday
            }
        }

        [Fact]
        public void GenerateCprWithBirthdayAndGender_ShouldHaveCorrectLastDigitForGender() {
            foreach (var data in _fixture.RandomCprsWithGenderAndBirthday) {
                // Assert
                int lastDigit = int.Parse(data.Cpr[^1].ToString());
                if (data.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)) {
                    Assert.True(lastDigit % 2 != 0); // Check if last digit is odd for males
                }
                else if (data.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)) {
                    Assert.True(lastDigit % 2 == 0); // Check if last digit is even for females
                }
            }
        }
    }
}