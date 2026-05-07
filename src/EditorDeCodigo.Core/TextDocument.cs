using EditorDeCodigo.Native;


namespace EditorDeCodigo.Core;

public class TextDocument : IDisposable
{
    public string Uri { get; private set; }
    public int Version { get; private set; }
    private List<string> _lines;

    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _lines.Clear();
            }

            Console.WriteLine($"[Core] Recursos do documento {Uri} liberados.");
            _disposed = true;
        }
    }

    ~TextDocument()
    {
        Dispose(false);
    }

    public TextDocument(string uri, string initialText = "")
    {
        Uri = uri;
        Version = 1;
        _lines = [.. initialText.Split(Environment.NewLine)];
    }

    public string GetText() => string.Join(Environment.NewLine, _lines);

    public void InsertText(int row, int col, string text)
    {
        if (row < 0 || row >= _lines.Count) return;

        var line = _lines[row];
        _lines[row] = line.Insert(col, text);
        
        Version++;

        AnalyzeSyntaxInBackground();
    }

    public void DeleteText(int row, int col, int length)
    {
        if (row < 0 || row >= _lines.Count) return;

        var line = _lines[row];
        _lines[row] = line.Remove(col, length);

        Version++;
    }

    private void AnalyzeSyntaxInBackground()
    {
        string fullText = GetText();
        int processdLength = SyntaxEngine.AnalyzeSyntax(fullText);

        Console.WriteLine($"[Core] Código analisado via C. Tamanho: {processdLength}");
    }
}