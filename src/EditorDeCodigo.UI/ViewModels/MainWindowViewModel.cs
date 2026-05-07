using System.ComponentModel;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using EditorDeCodigo.Native;
using EditorDeCodigo.Core;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace EditorDeCodigo.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _editorText = "";
    private readonly Dictionary<string, TextDocument> _documentCache = new ();
    public ObservableCollection<TextDocument> OpenDocuments {get; } = new();

    [ObservableProperty]
    private TextDocument? _activeDocument;

    [ObservableProperty]
    private string _statusBarText = "Aguardando digitação... (0 bytes)";

    partial void OnEditorTextChanged(string value)
    {
        if (value == null) return;

        int byteCount = SyntaxEngine.AnalyzeSyntax(value);

        StatusBarText = $"Motor nativo em C Ativo | {byteCount} bytes UTF-8 processados";
    }

    public void AddDocument(string uri, string content)
    {
        if (!_documentCache.ContainsKey(uri))
        {
            var doc = new TextDocument(uri, content);
            _documentCache.Add(uri, doc);
            OpenDocuments.Add(doc);
        }
    }
    public void CloseDocument(string uri)
    {
        if (_documentCache.Remove(uri, out var doc))
        {
            OpenDocuments.Remove(doc);
            doc.Dispose();            
        }
    }
}
