using Xunit;
public class SwedishPersonalNumberValidatorTests
{
    [Fact]
    public void IsValidPersonalNumber_ValidFormat_ShouldReturnTrue()
    {
        // Arrange
        string validNumber1 = "211018-7081";
        string validNumber2 = "19211018-7081";

        // Act
        bool result1 = SwedishPersonalNumberValidator.IsValid(validNumber1);
        bool result2 = SwedishPersonalNumberValidator.IsValid(validNumber2);

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
        bool result1 = SwedishPersonalNumberValidator.IsValid(invalidNumber1);
        bool result2 = SwedishPersonalNumberValidator.IsValid(invalidNumber2);
        bool result3 = SwedishPersonalNumberValidator.IsValid(invalidNumber3);

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
public void GetGender_WithInvalidInput_ShouldThrowArgumentException()
{
    // Arrange
    string invalidNumber = "3434442342";

    // Act & Assert
    Assert.Throws<ArgumentException>(() => SwedishPersonalNumberValidator.GetGender(invalidNumber));
}

}
