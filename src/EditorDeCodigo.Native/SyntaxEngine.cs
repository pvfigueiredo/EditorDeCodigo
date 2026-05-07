using System.Runtime.InteropServices;

namespace EditorDeCodigo.Native;

public static partial class SyntaxEngine
{
    private const string LibName = "editor_syntax";
    
    [LibraryImport(LibName, EntryPoint = "analyze_syntax", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int AnalyzeSyntax(string codeText);
}