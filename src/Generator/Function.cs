namespace Generator;

using LLVMSharp;
using static IRGen;


public class Function : Base
{
    AST.Function func;

    public Function(AST.Node node)
    {
        this.func = (AST.Function)node;
    }

    public override void generate()
    {

    }

    public void generateFunction(AST.Function funcNode)
    {
        //TODO: change this in the future once more variables are added
        namedValuesLLVM.Clear();

        funcNode.prototype.generator.generate();

        LLVMValueRef function = valueStack.Pop();
        LLVMBasicBlockRef entryBlock = LLVM.AppendBasicBlock(function, "entry");

        LLVM.PositionBuilderAtEnd(builder, entryBlock);

        // try
        // {

        if (funcNode.prototype.name == "main")
        {
            Console.WriteLine("main func identified");
            mainEntryBlock = entryBlock;
            mainBuilt = true;
            foreach (AST.Node node in nodesToBuild)
            {
                node.generator.generate();
            }
        }

        for (var i = 0; i < funcNode.body.Count(); i++)
        {
            funcNode.body[i].generator.generate();
        }
        // }
        // catch (Exception)
        // {
        //     LLVM.DeleteFunction(function);
        //     throw;
        // }

        LLVM.BuildRet(builder, valueStack.Pop());

        LLVM.VerifyFunction(function, LLVMVerifierFailureAction.LLVMPrintMessageAction);



        valueStack.Push(function);
    }

}
