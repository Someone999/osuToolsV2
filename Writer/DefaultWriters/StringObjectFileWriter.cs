﻿using System.Text;

namespace osuToolsV2.Writer.DefaultWriters;

public class StringObjectFileWriter : IObjectWriter<string, FileStream>
{
    public StringObjectFileWriter(string path)
    {
        ObjectWriter = File.Open(path, FileMode.OpenOrCreate);
    }

    public StringObjectFileWriter(FileStream fileStream)
    {
        ObjectWriter = fileStream;
    }
    
    public FileStream ObjectWriter { get; }
    public void Write(object obj)
    {
        Write(obj.ToString() ?? "null");
    }
    public void Write(string obj)
    {
        byte[] bts = Encoding.Default.GetBytes(obj);
        ObjectWriter.Write(bts, 0, bts.Length);
    }

    public bool NeedClose { get; private set; } = true;
    public void Close()
    {
        if (!NeedClose)
        {
            return;
        }
        ObjectWriter.Dispose();
        NeedClose = false;
    }
}