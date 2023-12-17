using SimpleCalculator.Calculator;
namespace SimpleCalculator.Tests;

public class TokenizerTests
{
    [Test]
    public void Tokenize_Addition()
    {
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[1].TokenType == TokenType.Addition);
        Assert.That(actual[7].TokenType == TokenType.Addition);
    }

    [Test]
    public void Tokenize_Subtraction()
    {
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[10].TokenType == TokenType.Subtraction);
    }

    [Test]
    public void Tokenize_Multiplication()
    {
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[3].TokenType == TokenType.Multiplication);
    }

    [Test]
    public void Tokenize_Division()
    {
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[5].TokenType == TokenType.Division);
    }

    [Test]
    public void Tokenize_OpenParenthesis()
    {
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[8].TokenType == TokenType.OpenParenthesis);
    }

    [Test]
    public void Tokenize_CloseParenthesis()
    {
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+2*3/4+(5-1)").ToList();

        Assert.That(actual[12].TokenType == TokenType.CloseParenthesis);
    }

    [Test]
    public void Tokenize_Positive_Numbers()
    {
        var tokenizer = new Tokenizer();
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
        var tokenizer = new Tokenizer();
        var actual = tokenizer.Tokenize("1+-2.12*3/4+(-5-1)").ToList();

        Assert.That(actual[2].TokenType == TokenType.Number);
        Assert.That(actual[9].TokenType == TokenType.Number);
    }
}