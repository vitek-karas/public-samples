using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace DotNetLib
{
    public static class Lib
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct LibArgs
        {
            public IntPtr Message;
            public int Number;
        }

        [UnmanagedCallersOnly]
        public static void CustomEntryPointUnmanaged(LibArgs libArgs)
        {
            Console.WriteLine($"Hello, world! from {nameof(CustomEntryPointUnmanaged)} in {nameof(Lib)}");
            PrintLibArgs(libArgs);

            Assembly asm = Assembly.GetExecutingAssembly();
            Console.WriteLine($"Running in {asm.GetName().Name} in ALC = {AssemblyLoadContext.GetLoadContext(asm)}");

            // Load this assembly again, but this time to default ALC
            Assembly asmInDefault = AssemblyLoadContext.Default.LoadFromAssemblyPath(asm.Location);
            // Find the type again
            Type typeInDefault = asmInDefault.GetType(typeof(Lib).FullName);
            // Find the EntryPointToDefaultALC method
            MethodInfo methodInDefault = typeInDefault.GetMethod(nameof(EntryPointToDefaultALC));
            methodInDefault.Invoke(null, new object[] { });
        }

        public static void EntryPointToDefaultALC()
        {
            Console.WriteLine($"Hello from default ALC!");

            Assembly asm = Assembly.GetExecutingAssembly();
            Console.WriteLine($"Running in {asm.GetName().Name} in ALC = {AssemblyLoadContext.GetLoadContext(asm)}");
        }

        private static void PrintLibArgs(LibArgs libArgs)
        {
            string message = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Marshal.PtrToStringUni(libArgs.Message)
                : Marshal.PtrToStringUTF8(libArgs.Message);

            Console.WriteLine($"-- message: {message}");
            Console.WriteLine($"-- number: {libArgs.Number}");
        }
    }
}
