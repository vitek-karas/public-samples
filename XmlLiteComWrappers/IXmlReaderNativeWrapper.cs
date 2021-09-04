using System.Runtime.InteropServices;

namespace XmlLiteWrapper
{
    internal unsafe class IXmlReaderNativeWrapper : IXmlReader
    {
        IntPtr _inst;
        XmlLiteComWrappers _comWrappers;

        private IXmlReaderNativeWrapper(IntPtr inst, XmlLiteComWrappers comWrappers)
        {
            _inst = inst;
            _comWrappers = comWrappers;
        }

        ~IXmlReaderNativeWrapper()
        {
            Marshal.Release(_inst);
        }

        internal static IXmlReaderNativeWrapper? CreateIfSupported(IntPtr ptr, XmlLiteComWrappers comWrappers)
        {
            if (Marshal.QueryInterface(ptr, ref IXmlReader.IID, out IntPtr inst) != HResult.S_OK)
                return default;

            return new IXmlReaderNativeWrapper(inst, comWrappers);
        }

        public void SetInput(Stream stream)
        {
            IntPtr pObj = _comWrappers.GetOrCreateComInterfaceForObject(stream, CreateComInterfaceFlags.None);
            Marshal.QueryInterface(pObj, ref IStreamManagedWrapper.IID_IStream, out IntPtr pStream);
            int hr = ((delegate* unmanaged<IntPtr, IntPtr, int>)(*(*(void***)_inst + 3)))(_inst, pStream);
            Marshal.Release(pStream);
            Marshal.Release(pObj);
            if (hr != HResult.S_OK)
                Marshal.ThrowExceptionForHR(hr);
        }

        public XmlNodeType Read()
        {
            int nodeType;
            int hr = ((delegate* unmanaged<IntPtr, int*, int>)(*(*(void***)_inst + 6)))(_inst, &nodeType);
            if (hr == HResult.S_FALSE)
                return XmlNodeType.XmlNodeType_None;
            else if (hr != HResult.S_OK)
                Marshal.ThrowExceptionForHR(hr);

            return (XmlNodeType)nodeType;
        }

        public XmlNodeType NodeType
        {
            get
            {
                int nodeType;
                int hr = ((delegate* unmanaged<IntPtr, int*, int>)(*(*(void***)_inst + 7)))(_inst, &nodeType);
                if (hr != HResult.S_OK)
                    Marshal.ThrowExceptionForHR(hr);

                return (XmlNodeType)nodeType;
            }
        }

        public string LocalName
        {
            get
            {
                IntPtr str;
                int size;
                int hr = ((delegate* unmanaged<IntPtr, IntPtr*, int*, int>)(*(*(void***)_inst + 14)))(_inst, &str, &size);
                if (hr != HResult.S_OK)
                    Marshal.ThrowExceptionForHR(hr);

                string? strLocal = Marshal.PtrToStringUni(str, size);
                return strLocal;
            }
        }
    }
}
