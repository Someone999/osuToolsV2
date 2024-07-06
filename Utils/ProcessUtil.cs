using System.Diagnostics;
using osuToolsV2.Native;

namespace osuToolsV2.Utils;

public static class ProcessUtil
{
    public enum ImageArchitectures : ushort
    {
        Unknown = 0,
        TargetHost = 1,
        I386 = 0x014C,
        R3000BigEndian = 0x160,
        R3000 = 0x162,
        R4000 = 0x166,
        R10000 = 0x168,
        WceMipsV2 = 0x169,
        Alpha = 0x184,
        Sh3 = 0x1A2,
        Sh3Dsp = 0x1A3,
        Sh3E = 0x1A4,
        Sh4 = 0x1A6,
        Sh5 = 0x1A8,
        Arm = 0x1C0,
        Thumb = 0x1C2,
        ArmNt = 0x1C4,
        Am33 = 0x1D3,
        PowerPc = 0x1F0,
        PowerPcFp = 0x1F1,
        Ia64 = 0x200,
        Mips16 = 0x266,
        Alpha64 = 0x284,
        MipsFpu = 0x366,
        MipsFpu16 = 0x466,
        Axp64 = Alpha64,
        TriCore = 0x520,
        Cef = 0xCEF,
        Ebc = 0xEBC,
        Amd64 = 0x8664,
        M32R = 0x9041,
        Arm64 = 0xAA64,
        Cee = 0xC0EE
    }
    
    public static bool Is64Image(string imageFile)
    {
        const int dosMagicLength = 2;
        const int peMagicLength = 4;
        var f = File.OpenRead(imageFile);
        BinaryReader binaryReader = new BinaryReader(f);
        var dosMagic = binaryReader.ReadBytes(dosMagicLength);
        if (dosMagic[0] != 0x4D || dosMagic[1] != 0x5A)
        {
            throw new BadImageFormatException("Not a PE image.");
        }
        
        const int peHeaderPosition = 0x40 - 4 - dosMagicLength;
        f.Position += peHeaderPosition;
        var peHeaderOffset = binaryReader.ReadInt32();
        f.Position = peHeaderOffset;
        var peMagic = binaryReader.ReadBytes(peMagicLength);
        if (peMagic[0] != 0x50 || peMagic[1] != 0x45)
        {
            throw new BadImageFormatException("Not a PE image.");
        }
        
        ImageArchitectures imageArch = (ImageArchitectures)binaryReader.ReadInt16();
        binaryReader.Dispose();
        f.Dispose();
        return Is64BitArch(imageArch);
    }

    private static bool Is64BitArch(ImageArchitectures imageArch)
    {
        var isAlpha64 = imageArch == ImageArchitectures.Alpha64;
        var isAmd64 = imageArch == ImageArchitectures.Amd64;
        var isIa64 = imageArch == ImageArchitectures.Ia64;
        var isArm64 = imageArch == ImageArchitectures.Arm64;
        return isAlpha64 || isAmd64 || isIa64 || isArm64;
    }
    private delegate bool Is64BitFunc2(IntPtr handle, out ImageArchitectures processMachine,
        out ImageArchitectures nativeMachine);
    
    private delegate bool Is64BitFunc(IntPtr handle, out bool isWow64);
    
    public static bool IsWow64Process(Process process)
    {
        var isWow642 = NativeDll.Kernel32.GetFunctionAs<Is64BitFunc2>("IsWow64Process2");
        if (isWow642 != null)
        {
            var isWow64 = isWow642(process.Handle, out var processMachine, out var nativeMachine);
            if (isWow64)
            {
                return true;
            }

            return !Is64BitArch(processMachine) && Is64BitArch(nativeMachine);
        }

        var isWow64Proc = NativeDll.Kernel32.GetFunctionAs<Is64BitFunc>("IsWow64Process");
        if (isWow64Proc != null)
        {
            return isWow64Proc(process.Handle, out var isWow64Process1)
                ? isWow64Process1
                : Is64Image(process);
        }

        return Is64Image(process);
    }

    private static bool Is64Image(Process process)
    {
        var path = process.MainModule?.FileName;
        if (path == null)
        {
            throw new Exception("Unable to detect process architecture.");
        }
        
        return Is64Image(path);
    }
}