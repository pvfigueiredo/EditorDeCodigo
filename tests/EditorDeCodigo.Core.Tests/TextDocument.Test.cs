using System.Reflection;
using EditorDeCodigo.Native;
using EditorDeCodigo.Core;
using System.Text;

namespace EditorDeCodigo.Core.Tests;

public class TextDocument__Test
{
    [Fact]
    public void AnalyzeSyntax_Should_Show_The_ExactLength()
    {
        //Arrange
        string codeSnippet = "int main() { return 0; }";
        int expectedLength = 24;

        //Act
        int actualLength = SyntaxEngine.AnalyzeSyntax(codeSnippet);

        //Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Theory]
    [InlineData("Console.WriteLine();")]
    [InlineData("")]
    [InlineData("Uma string com caracteres especiais: ç ã")]
    [InlineData("Emojis contam como 4 bytes 🚀")]
    public void AnalyzeSyntax_Should_Handle_Multiple_Cases(string code)
    {
        //Arrange
        int expectedByteLength = Encoding.UTF8.GetByteCount(code);
        // Act
        int actualByteLength = SyntaxEngine.AnalyzeSyntax(code);

        // Assert
        Assert.Equal(expectedByteLength, actualByteLength);
    }
}