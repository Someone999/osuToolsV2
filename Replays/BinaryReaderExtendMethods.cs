namespace osuToolsV2.Replays;

public static class BinaryReaderExtendMethods
{
    public static string? ReadOsuString(this BinaryReader reader)
    {
        if (reader.ReadByte() == 0x0B)
        {
            return reader.ReadString();
        }
        
        reader.BaseStream.Position--;
        return null;
    }
}