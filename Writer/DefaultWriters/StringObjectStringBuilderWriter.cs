using System.Text;

namespace osuToolsV2.Writer.DefaultWriters;

public class StringObjectStringBuilderWriter : IObjectWriter<string, StringBuilder>
{
    private StringBuilder _writer;

    public StringObjectStringBuilderWriter(StringBuilder writer)
    {
        _writer = writer;
    }

    public StringBuilder Writer
    {
        get => _writer;
        set
        {
            if (IsWriting)
            {
                return;
            }

            _writer = value;
        }
    }
    
    public bool IsWriting { get; private set; }

    public void Write(string obj)
    {
        IsWriting = true;
        Writer.Append(obj);
        IsWriting = false;
    }
    public void Write(object obj)
    {
        Write(obj.ToString() ?? "");
    }
    public bool NeedClose => false;
    public void Close()
    {
    }

    

    public void Dispose()
    {
    }
}