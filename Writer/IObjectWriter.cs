namespace osuToolsV2.Writer;


public interface IObjectWriter
{
    void Write(object obj);
    bool NeedClose { get; }
    void Close();
    bool IsWriting { get; }
}

public interface IObjectWriter<TWriter> : IDisposable, IObjectWriter
{
    TWriter Writer { get; set; }
}

public interface IObjectWriter<in TWriteObject, TWriter> : IObjectWriter<TWriter>
{
    void Write(TWriteObject obj);
}




