// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;

Console.WriteLine("Calling to native");
InjectManaged();

[DllImport("nativehost.dll", EntryPoint = "?InjectManaged@@YAHXZ")]
static extern void InjectManaged();