namespace SimpleCalculator.Tests;

using Microsoft.Extensions.Logging;
using Moq;
using SimpleCalculator.Tokenizer;

public class TokenizerTests
{
    [Test]
    public void Tokenize_Addition()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[1].TokenType == TokenType.Addition);
        Assert.That(actual[7].TokenType == TokenType.Addition);
    }

    [Test]
    public void Tokenize_Subtraction()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[10].TokenType == TokenType.Subtraction);
    }

    [Test]
    public void Tokenize_Multiplication()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[3].TokenType == TokenType.Multiplication);
    }

    [Test]
    public void Tokenize_Division()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[5].TokenType == TokenType.Division);
    }

    [Test]
    public void Tokenize_OpenParenthesis()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[8].TokenType == TokenType.OpenParenthesis);
    }

    [Test]
    public void Tokenize_CloseParenthesis()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[12].TokenType == TokenType.CloseParenthesis);
    }

    [Test]
    public void Tokenize_Positive_Numbers()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+2.12*3/4+(5-1)").ToList();

        Assert.That(actual[0].TokenType == TokenType.Number);
        Assert.That(actual[2].TokenType == TokenType.Number);
        Assert.That(actual[4].TokenType == TokenType.Number);
        Assert.That(actual[6].TokenType == TokenType.Number);
        Assert.That(actual[9].TokenType == TokenType.Number);
        Assert.That(actual[11].TokenType == TokenType.Number);
    }

    [Test]
    public void Tokenize_Negative_Numbers()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+-2.12*3/4+(-5-1)").ToList();

        Assert.That(actual[2].TokenType == TokenType.Number);
        Assert.That(actual[9].TokenType == TokenType.Number);
    }

    [Test]
    public void Tokenize_Invalid()
    {
        var loggerMock = new Mock<ILogger>();
        var tokenizer = new Tokenizer(loggerMock.Object);
        var actual = tokenizer.Tokenize("1+-2.12a*3/4+(-5abc-1)").ToList();

        Assert.That(actual[3].TokenType == TokenType.Invalid);
        Assert.That(actual[11].TokenType == TokenType.Invalid);
    }
}