using System.Text;

namespace osuToolsV2.Writer.DefaultWriters;

public class StringObjectStringBuilderWriter : IObjectWriter<string, StringBuilder>
{
    public StringObjectStringBuilderWriter(StringBuilder objectWriter)
    {
        ObjectWriter = objectWriter;
    }
    
    public StringBuilder ObjectWriter { get; }
    public void Write(string obj)
    {
        ObjectWriter.Append(obj);
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