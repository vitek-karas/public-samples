using System;
using System.Runtime.InteropServices;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Calling to native");
        InjectManaged();
    }

    [DllImport("nativehost.dll", EntryPoint = "?InjectManaged@@YAHXZ")]
    static extern void InjectManaged();
}