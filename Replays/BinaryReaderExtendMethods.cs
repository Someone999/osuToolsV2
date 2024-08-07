namespace osuToolsV2.Replays;

public static class BinaryReaderExtendMethods
{
    public static string ReadOsuString(this BinaryReader reader)
    {
        var indicator = reader.ReadByte();
        switch (indicator)
        {
            case 0x00:
                return string.Empty;
            case 0x0B:
                return reader.ReadString();
            default: 
                throw new FormatException("Not a valid string format");
        }
    }
}