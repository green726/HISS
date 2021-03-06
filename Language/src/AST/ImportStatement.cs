namespace AST;
using System.Linq;

public class ImportStatement : Node
{
    public HISSFile file;
    public Library library;

    public ImportStatement(Util.Token token) : base(token)
    {
        this.nodeType = NodeType.ImportStatement;
        this.newLineReset = true;
    }

    public override void addChild(Node child)
    {
        base.addChild(child);
    }

    public override void addChild(Util.Token child)
    {
        List<Library> matchingLib = HISS.projectInfo.libraries.Where(library => library.name == child.value).ToList();
        if (matchingLib.Count() == 1)
        {
            this.library = matchingLib[0];
        }
        else if (matchingLib.Count() > 1)
        {
            throw new ParserException("Multiple libraries found matching the same name - possible HIP bug or incorrect using statement", this);
        }
        else
        {
            string[] filePaths = Directory.GetFiles(HISS.projectInfo.path, child.value + ".hiss");

            if (filePaths.Length > 1)
            {
                throw new ParserException("Multiple files found matching the same name in using statement", this);
            }
            else if (filePaths.Length == 0)
            {
                throw ParserException.FactoryMethod("No file or library found that matches using statement", "Remove incorrect using statement - fix a possible typo", this, true);
            }

            this.file = new HISSFile(child.value, filePaths[0]);
        }


        string importFileValue = this.file.value;
        List<Util.Token> tokens = Lexer.lex(importFileValue);

        List<Util.Token> previousTokenList = Parser.tokenList;
        Parser.tokenList.InsertRange(Parser.currentTokenNum, tokens);
        // Parser.tokenList = tokens;
        // Parser.parseTokenRecursive(tokens[0], 0);

        Parser.tokenList = previousTokenList;

        base.addChild(child);

    }
}
