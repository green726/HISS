public static class Util
{
    public static string[] delimeters = { "(", ")", "{", "}", "[", "]" };
    public static string[] builtinFuncs = {"print"};


    public enum TokenType
    {
        Operator,
        Number,
        Keyword,
        ParenDelimiterOpen,
        ParenDelimiterClose,
        BrackDelimiterOpen,
        BrackDelimiterClose,
        SquareDelimiterOpen,
        SquareDelimiterClose,
        Text,
        EOL, //end of line,
        EOF //end of file
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

        public Token(TokenType type, string value, int line, int column)
        {
            this.value = value;
            this.type = type;
            this.line = line;
            this.column = column;
        }

        public Token(TokenType type, char value, int line, int column)
        {
            this.value = value.ToString();
            this.type = type;
            this.line = line;
            this.column = column;
        }

    }


}

