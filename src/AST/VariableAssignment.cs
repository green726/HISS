public class VariableAssignment : ASTNode
{
    public string name = "";
    public TypeAST type;
    public string assignmentOp = "";
    public string strValue = "";
    public bool mutable = false;
    private int childLoop = 0;

    public bool reassignment = false;
    public BinaryExpression? bin = null;

    public VariableAssignment(Util.Token token, bool mutable, ASTNode? parent = null) : base(token)
    {
        this.mutable = mutable;
        this.nodeType = NodeType.VariableAssignment;
        Parser.nodes.Add(this);
        Parser.globalVarAss.Add(this);

        if (token.value != "const" && token.value != "var")
        {
            reassignment = true;
            if (parent != null)
            {
                ASTNode prevNode = parent.children.Last();
                if (prevNode.nodeType == NodeType.VariableExpression)
                {
                    VariableExpression prevVarExpr = (VariableExpression)prevNode;
                    this.name = prevVarExpr.varName;
                    prevVarExpr.addParent(this);
                    this.children.Add(prevVarExpr);
                }
            }
            else
            {
                ASTNode prevNode = Parser.nodes.Last();
                if (prevNode.nodeType == ASTNode.NodeType.VariableExpression)
                {
                    VariableExpression prevVarExpr = (VariableExpression)prevNode;
                    this.name = prevVarExpr.varName;
                    prevVarExpr.addParent(this);
                    this.children.Add(prevVarExpr);
                }
            }
        }
    }

    public override void addChild(Util.Token child)
    {
        if (!reassignment)
        {
            switch (childLoop)
            {
                case 0:
                    this.type = new TypeAST(child);
                    break;
                case 1:
                    this.name = child.value;
                    break;
                case 2:
                    if (child.type != Util.TokenType.AssignmentOp) throw new ParserException($"expected assignment op but got {child.type} in a variable assignment", child);
                    this.assignmentOp = child.value;
                    break;
                case 3:
                    this.strValue = child.value;
                    break;
                default:
                    throw new ParserException($"Illegal extra items after variable assignment", this);

            }
        }
        else
        {
            switch (childLoop)
            {
                case 0:
                    break;
            }
        }
        childLoop++;


    }

    public override void addChild(ASTNode node)
    {
        if (!reassignment)
        {
            switch (node.nodeType)
            {
                case NodeType.StringExpression:
                    if (childLoop == 3)
                    {
                        StringExpression strExp = (StringExpression)node;
                        this.strValue = strExp.value;
                    }
                    else
                    {
                        throw new ParserException($"Illegal value (type {node.nodeType}) of variable {this.name}", node);
                    }
                    break;
            }
        }
        else
        {
            switch (childLoop)
            {
                case 1:
                    if (node.nodeType == NodeType.BinaryExpression)
                    {
                        BinaryExpression binExpr = (BinaryExpression)node;
                        binExpr.leftHand = this.children.Last();
                        this.bin = binExpr;
                        this.children.Add(binExpr);
                    }
                    break;
            }
            childLoop++;
        }
    }


}
