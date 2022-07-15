public static class Util
{
    public static string[] delimeters = { "(", ")", "{", "}", "[", "]" };
    public static string[] builtinFuncs = { "print", "println" };
    // public static TokenType[] delimTypes = { TokenType.ParenDelimiterOpen, TokenType.ParenDelimiterClose, TokenType.BrackDelimiterOpen, TokenType.BrackDelimiterClose, TokenType.SquareDelimiterOpen, TokenType.SquareDelimiterClose };

    public enum TokenType
    {
        Operator,
        Int,
        Double,
        Keyword,
        DelimiterClose,
        DelimiterOpen,
        AssignmentOp,
        String,
        Text,
        EOL, //end of line,
        EOF //end of file
    }

    public static List<TokenType> allTokenTypesExcept(List<TokenType> exceptedTypes)
    {
        List<TokenType> ret = new List<TokenType>();
        foreach (TokenType type in Enum.GetValues(typeof(TokenType)))
        {
            if (exceptedTypes.Contains(type))
            {
                continue;
            }
            ret.Add(type);
        }

        return ret;
    }

    public enum ClassType
    {
        Double,
        Int,
        String
    }


    public class Token
    {
        public string value;
        public TokenType type;
        public int line;
        public int column;
        public bool isDelim = false;
        public int charNum = 0;

        public Token(TokenType type, string value, int line, int column, bool isDelim = false)
        {
            this.isDelim = isDelim;
            this.value = value;
            this.type = type;
            this.line = line;
            this.column = column;

            if (this.type == TokenType.String)
            {
                this.value = value.Substring(1);
                this.value = this.value.Substring(0, (this.value.Length - 1));
            }
        }

        public Token(TokenType type, char value, int line, int column, bool isDelim = false)
        {
            this.isDelim = isDelim;
            this.value = value.ToString();
            this.type = type;
            this.line = line;
            this.column = column;
        }

        public Token(TokenType type, char value, int line, int column, int charNum, bool isDelim = false)
        {
            this.isDelim = isDelim;
            this.value = value.ToString();
            this.type = type;
            this.line = line;
            this.column = column;
            this.charNum = charNum;
        }

        public Token(TokenType type, string value, int line, int column, int charNum, bool isDelim = false)
        {
            this.isDelim = isDelim;
            this.value = value.ToString();
            this.type = type;
            this.line = line;
            this.column = column;
            this.charNum = charNum;
        }

    }


}
