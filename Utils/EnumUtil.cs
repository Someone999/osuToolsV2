namespace osuToolsV2.Utils;

public static class EnumUtil
{
    public static TEnum[] IntToEnum<TEnum>(int i) where TEnum: Enum
    {
        List<TEnum> enumMembers = new List<TEnum>();
        const int totalBits = 32;
        int shiftedBits = 0;
        while (i > 0)
        {
            var currentBit = i & 1;
            if (currentBit == 0)
            {
                i >>= 1;
                shiftedBits++;
                continue;
            }

            if (shiftedBits > totalBits)
            {
                throw new OverflowException("Mods shift overflowed");
            }
            
            enumMembers.Add((TEnum)(object)(currentBit << shiftedBits));
            i >>= 1;
            shiftedBits++;
        }

        return enumMembers.ToArray();
    }
    
}