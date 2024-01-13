namespace grupp_arbete.Tests
{
    public class SwedishPersonalNumberValidatorTests
    {
        [Theory]
        [InlineData("011114-0414", true)]   // Korrekt format och kontrollsiffra
        [InlineData("0111140414", true)]    // Korrekt format
        [InlineData("850213456", false)]    // För kort
        [InlineData("850213456789", false)] // För lång
        [InlineData("2504251234", false)]   // Ogiltig ålder
        [InlineData("200111140414", true)]  // Korrekt ålder
        public void IsValid_ShouldReturnExpectedResult(string input, bool expectedResult)
        {
            bool result = SwedishPersonalNumberValidator.IsValid(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("850213-4587", "Female")] // Jämnt antal = kvinna
        [InlineData("8502134578", "Male")]    // Udda antal = man
        public void GetGender_ShouldReturnExpectedGender(string input, string expectedGender)
        {
            string result = SwedishPersonalNumberValidator.GetGender(input);
            Assert.Equal(expectedGender, result);
        }

        [Fact]
        public void IsValid_WithInvalidControlNumber_ShouldReturnFalse()
        {
            bool result = SwedishPersonalNumberValidator.IsValid("8502134566"); // Ogiltig kontrollsiffra
            Assert.False(result);
        }

        [Fact]
        public void IsValid_WithInvalidAge_ShouldReturnFalse()
        {
            bool result = SwedishPersonalNumberValidator.IsValid("000213456700"); // Ogiltig ålder
            Assert.False(result);
        }

        [Fact]
        public void IsValid_WithInvalidFormat_ShouldReturnFalse()
        {
            bool result = SwedishPersonalNumberValidator.IsValid("85-0213-4567"); // Ogiltigt format med bindestreck
            Assert.False(result);
        }

        [Fact]
        public void GetGender_WithInvalidInput_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => SwedishPersonalNumberValidator.GetGender("850213456")); // Felaktigt format
        }

        [Fact]
        public void IsValid_WithNullInput_ShouldReturnFalse()
        {
            bool result = SwedishPersonalNumberValidator.IsValid(null); // Null input
            Assert.False(result);
        }

        [Fact]
        public void IsValid_WithEmptyStringInput_ShouldReturnFalse()
        {
            bool result = SwedishPersonalNumberValidator.IsValid(""); // Tom sträng
            Assert.False(result);
        }
    }
}
