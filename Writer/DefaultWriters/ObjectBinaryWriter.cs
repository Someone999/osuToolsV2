using System.Reflection;

namespace osuToolsV2.Writer.DefaultWriters;

public class ObjectBinaryWriter : IObjectWriter<BinaryWriter>
{
    public ObjectBinaryWriter(Stream stream)
    {
        if (_writeMethods.Count == 0)
        {
            Type t = typeof(BinaryWriter);
            MethodInfo[] methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            _writeMethods = (from m in methods where m.Name == "Write" && m.GetParameters().Length == 1 select m)
                .ToDictionary(m => m.GetParameters()[0].ParameterType);
        }
       
        ObjectWriter = new BinaryWriter(stream);
    }
    
    public BinaryWriter ObjectWriter { get; }
    private static Dictionary<Type, MethodInfo> _writeMethods = new Dictionary<Type, MethodInfo>();
    public void Write(object obj)
    {
        if (!_writeMethods.ContainsKey(obj.GetType()))
        {
            throw new NotSupportedException("Unsupported type.");
        }


        MethodInfo writeMethod = _writeMethods[obj.GetType()];
        writeMethod.Invoke(ObjectWriter, new[] {obj});
    }

   
    public bool NeedClose => true;
    public void Close()
    {
        ObjectWriter.Close();
    }

    private bool _disposed;
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        ObjectWriter.Dispose();
        _disposed = true;
    }
}