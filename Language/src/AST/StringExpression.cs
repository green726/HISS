namespace AST;

public class StringExpression : Expression
{
    // public new string value;
    public bool builtInString;

    public StringExpression(Util.Token token, AST.Node? parent = null, bool dontAdd = false, bool builtInString = false) : base(token)
    {
        this.nodeType = NodeType.StringExpression;
        this.generator = new Generator.StringExpression(this);

        Parser.checkToken(token, expectedType: Util.TokenType.String);

        this.value = token.value;
        this.builtInString = builtInString;

        if (dontAdd == true)
        {
            return;
        }

        Spectre.Console.AnsiConsole.MarkupLine($"[red]string expr with value of: {this.value}[/]");
        if (parent != null)
        {
            parent.addChild(this);
        }
        else
        {
            throw new ParserException("Illegal string expression", token);
        }

    }

}
