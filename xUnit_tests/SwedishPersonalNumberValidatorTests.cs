
public class SwedishPersonalNumberValidatorTests
{
    [Fact]
    public void IsValidPersonalNumber_ValidFormat_ShouldReturnTrue()
    {
        // Arrange
        string validNumber1 = "011114-0414";
        string validNumber2 = "20011114-0414";

        // Act
        bool result1 = SwedishPersonalNumberValidator.IsValidPersonalNumber(validNumber1);
        bool result2 = SwedishPersonalNumberValidator.IsValidPersonalNumber(validNumber2);

        // Assert
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void IsValidPersonalNumber_InvalidFormat_ShouldReturnFalse()
    {
        // Arrange
        string invalidNumber1 = "1234567890";
        string invalidNumber2 = "200101-56789";
        string invalidNumber3 = "2001010d12345678";

        // Act
        bool result1 = SwedishPersonalNumberValidator.IsValidPersonalNumber(invalidNumber1);
        bool result2 = SwedishPersonalNumberValidator.IsValidPersonalNumber(invalidNumber2);
        bool result3 = SwedishPersonalNumberValidator.IsValidPersonalNumber(invalidNumber3);

        // Assert
        Assert.False(result1);
        Assert.False(result2);
        Assert.False(result3);
    }

[Fact]
public void GetGender_MaleNumber_ShouldReturnMale()
{
    // Arrange
    string maleNumber = "20011114-0414";

    // Act
    string gender = SwedishPersonalNumberValidator.GetGender(maleNumber);

    // Assert
    Assert.Equal("Male", gender);
}

[Fact]
public void GetGender_FemaleNumber_ShouldReturnFemale()
{
    // Arrange
    string femaleNumber = "19790624-9045";

    // Act
    string gender = SwedishPersonalNumberValidator.GetGender(femaleNumber);

    // Assert
    Assert.Equal("Female", gender);
}


    [Fact]
    public void GetGender_InvalidNumber_ShouldThrowArgumentException()
    {
        // Arrange
        string invalidNumber = "1234567890";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => SwedishPersonalNumberValidator.GetGender(invalidNumber));
    }
}
