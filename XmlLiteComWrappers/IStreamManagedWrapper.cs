using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.ComWrappers;

namespace XmlLiteWrapper
{
    internal unsafe static class IStreamManagedWrapper
    {
        public static ComInterfaceEntry* GetInterfaceEntries(IntPtr fpQI, IntPtr fpAddRef, IntPtr fpRelease)
        {
            var vtable = (IntPtr*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(IStreamManagedWrapper), IntPtr.Size * 14);
            vtable[0] = fpQI;
            vtable[1] = fpAddRef;
            vtable[2] = fpRelease;
            vtable[3] = (IntPtr)(delegate* unmanaged<IntPtr, void*, uint, uint*, int>)&Read;
            vtable[4] = (IntPtr)(delegate* unmanaged<IntPtr, void*, uint, uint*, int>)&Write;
            vtable[5] = (IntPtr)(delegate* unmanaged<IntPtr, ulong, int, ulong*, int>)&Seek;
            vtable[6] = (IntPtr)(delegate* unmanaged<IntPtr, ulong, int>)&SetSize;
            vtable[7] = (IntPtr)(delegate* unmanaged<IntPtr, IntPtr, ulong, ulong*, ulong*, int>)&CopyTo;
            vtable[8] = (IntPtr)(delegate* unmanaged<IntPtr, int, int>)&Commit;
            vtable[9] = (IntPtr)(delegate* unmanaged<IntPtr, int>)&Revert;
            vtable[10] = (IntPtr)(delegate* unmanaged<IntPtr, ulong, ulong, int, int>)&LockRegion;
            vtable[11] = (IntPtr)(delegate* unmanaged<IntPtr, ulong, ulong, int, int>)&UnlockRegion;
            vtable[12] = (IntPtr)(delegate* unmanaged<IntPtr, IntPtr, int, int>)&Stat;
            vtable[13] = (IntPtr)(delegate* unmanaged<IntPtr, IntPtr*, int>)&Clone;
            IntPtr IStreamVTable = (IntPtr)vtable;

            var entries = (ComInterfaceEntry*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(IStreamManagedWrapper), sizeof(ComInterfaceEntry) * 1);
            entries[0].IID = IID_IStream;
            entries[0].Vtable = IStreamVTable;

            return entries;
        }

        static class STREAM_SEEK
        {
            public const int STREAM_SEEK_SET = 0;
            public const int STREAM_SEEK_CUR = 1;
            public const int STREAM_SEEK_END = 2;
        }

        internal static Guid IID_IStream = new("0000000c-0000-0000-C000-000000000046");

        [UnmanagedCallersOnly]
        static int Read(IntPtr _this, void* pv, uint cb, uint* pcbRead)
        {
            try
            {
                *pcbRead = (uint)ComInterfaceDispatch.GetInstance<Stream>((ComInterfaceDispatch*)_this)
                    .Read(new Span<byte>(pv, (int)cb));
            }
            catch (Exception e)
            {
                return e.HResult;
            }

            return HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        static int Write(IntPtr _this, void* pv, uint cb, uint* pcbWritten)
        {
            try
            {
                ComInterfaceDispatch.GetInstance<Stream>((ComInterfaceDispatch*)_this)
                    .Write(new ReadOnlySpan<byte>(pv, (int)cb));
                *pcbWritten = cb;
            }
            catch (Exception e)
            {
                return e.HResult;
            }

            return HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        static int Seek(IntPtr _this, ulong move, int origin, ulong* newPosition)
        {
            try
            {
                SeekOrigin mOrigin = origin switch
                {
                    STREAM_SEEK.STREAM_SEEK_SET => SeekOrigin.Begin,
                    STREAM_SEEK.STREAM_SEEK_CUR => SeekOrigin.Current,
                    STREAM_SEEK.STREAM_SEEK_END => SeekOrigin.End,
                    _ => throw new ArgumentOutOfRangeException(nameof(origin))
                };
                *newPosition = (ulong)ComInterfaceDispatch.GetInstance<Stream>((ComInterfaceDispatch*)_this)
                    .Seek((long)move, mOrigin);
            }
            catch (Exception e)
            {
                return e.HResult;
            }

            return HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        static int SetSize(IntPtr _this, ulong newSize)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int CopyTo(IntPtr _this, IntPtr stm, ulong cb, ulong* pcbRead, ulong* pcbWritten)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int Commit(IntPtr _this, int flags)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int Revert(IntPtr _this)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int LockRegion(IntPtr _this, ulong offset, ulong cb, int lockType)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int UnlockRegion(IntPtr _this, ulong offset, ulong cb, int lockType)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int Stat(IntPtr _this, IntPtr pstatstg, int flag)
        {
            return HResult.E_NOTIMPL;
        }

        [UnmanagedCallersOnly]
        static int Clone(IntPtr _this, IntPtr* ppstm)
        {
            return HResult.E_NOTIMPL;
        }
    }
}
