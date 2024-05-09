using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace osuToolsV2.Native;

public class NativeDll
{
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    static extern IntPtr GetProcAddress(IntPtr moduleHandle, string func);
    
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern IntPtr LoadLibraryW(string lib);

    private readonly IntPtr? _moduleHandle;

    public NativeDll(string dllPath)
    {
        var loadedModule = LoadLibraryW(dllPath);
        if (loadedModule == IntPtr.Zero)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        _moduleHandle = loadedModule;
    }

    public IntPtr GetFunction(string funcName)
    {
        if (_moduleHandle == null)
        {
            throw new InvalidOperationException("Module not loaded");
        }
        
        return GetProcAddress(_moduleHandle.Value, funcName);
    }

    public T? GetFunctionAs<T>(string funcName) where T : Delegate
    {
        var funcPtr = GetFunction(funcName);
        return funcPtr == IntPtr.Zero ? default : Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
    }

    public static NativeDll Kernel32 { get; } = new NativeDll("kernel32.dll");
    public static NativeDll User32 { get; } = new NativeDll("user32.dll");
}