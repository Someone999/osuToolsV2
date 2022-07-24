namespace osuToolsV2.Writer.DefaultWriters;

public class ObjectBinaryFileWriter : IObjectWriter<ObjectBinaryWriter>
{

    public ObjectBinaryFileWriter(string filePath)
    {
        ObjectWriter = new ObjectBinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read));
    }
    public ObjectBinaryWriter ObjectWriter { get; }
    public void Write(object obj)
    {
        ObjectWriter.Write(obj);
    }
    public bool NeedClose => true;
    public void Close()
    {
        ObjectWriter.Close();
    }
}