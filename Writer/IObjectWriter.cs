namespace osuToolsV2.Writer;


public interface IObjectWriter
{
    void Write(object obj);
    bool NeedClose { get; }
    void Close();
}

public interface IObjectWriter<out TWriter> : IDisposable, IObjectWriter
{
    TWriter ObjectWriter { get; }
}

public interface IObjectWriter<in TWriteObject, out TWriter> : IObjectWriter<TWriter>
{
    void Write(TWriteObject obj);
}




