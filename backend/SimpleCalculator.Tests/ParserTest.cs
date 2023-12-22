namespace SimpleCalculator.Tests;

using SimpleCalculator.Tokenizer;
using SimpleCalculator.Parser;
using Moq;
using Microsoft.Extensions.Logging;

public class ParserTests
{
    [Test]
    public void Parser_Test_AST()
    {
        var loggerMock = new Mock<ILogger>();
        var parser = new Parser(new Tokenizer(loggerMock.Object),loggerMock.Object,"1*2*3+5/2-1+23");
        var actual = (BinaryOperationNode) parser.Parse();

        Assert.That(actual.Token.TokenType, Is.EqualTo(TokenType.Addition));

        var left = (BinaryOperationNode) actual.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Multiplication));

        // down left side of tree
        var rightnum = (NumberNode) left.Right;
        left = (BinaryOperationNode) left.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Multiplication));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("3"));

        rightnum = (NumberNode) left.Right;
        var leftnum = (NumberNode) left.Left;
        Assert.That(leftnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(leftnum.Token.Value, Is.EqualTo("1"));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("2"));

        // down right side of tree
        var right = (BinaryOperationNode) actual.Right;
        Assert.That(right.Token.TokenType, Is.EqualTo(TokenType.Addition));
        
        rightnum = (NumberNode) right.Right;
        left = (BinaryOperationNode) right.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Subtraction));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("23"));

        rightnum = (NumberNode) left.Right;
        left = (BinaryOperationNode) left.Left;
        Assert.That(left.Token.TokenType, Is.EqualTo(TokenType.Division));
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("1"));

        rightnum = (NumberNode) left.Right;
        leftnum = (NumberNode) left.Left;
        Assert.That(rightnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(rightnum.Token.Value, Is.EqualTo("2"));
        Assert.That(leftnum.Token.TokenType, Is.EqualTo(TokenType.Number));
        Assert.That(leftnum.Token.Value, Is.EqualTo("5"));
    }
}