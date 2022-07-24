using System.Reflection;

namespace osuToolsV2.Writer.DefaultWriters;

public class ObjectBinaryWriter : IObjectWriter<BinaryWriter>
{
    public ObjectBinaryWriter(Stream stream)
    {
        Type t = typeof(BinaryWriter);
        MethodInfo[] methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        _writeMethods = (from m in methods where m.Name == "Write" && m.GetParameters().Length == 1 select m)
            .ToDictionary(m => m.GetParameters()[0].ParameterType);
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

    private void Deprecated(object obj)
    {
        switch (obj)
        {
            case bool b:
                ObjectWriter.Write(b);
                break;
            case char c:
                ObjectWriter.Write(c);
                break;
            case char[] cArr:
                ObjectWriter.Write(cArr);
                break;
            case string str:
                ObjectWriter.Write(str);
                break;
            case short s:
                ObjectWriter.Write(s);
                break;
            case ushort us:
                ObjectWriter.Write(us);
                break;
            case int i:
                ObjectWriter.Write(i);
                break;
            case uint ui:
                ObjectWriter.Write(ui);
                break;
            case long l:
                ObjectWriter.Write(l);
                break;
            case ulong ul:
                ObjectWriter.Write(ul);
                break;
            case float f:
                ObjectWriter.Write(f);
                break;
            case double d:
                ObjectWriter.Write(d);
                break;
            case byte b:
                ObjectWriter.Write(b);
                break;
            case byte[] bArr:
                ObjectWriter.Write(bArr);
                break;
            case sbyte sb:
                ObjectWriter.Write(sb);
                break;
            case decimal d:
                ObjectWriter.Write(d);
                break;
        }
    }
    public bool NeedClose => true;
    public void Close()
    {
        ObjectWriter.Close();
    }
}