using System.Collections;
using System.Runtime.InteropServices;

namespace XmlLiteWrapper
{
    internal sealed unsafe class XmlLiteComWrappers : ComWrappers
    {
        static readonly ComInterfaceEntry* IStreamManagedWrapperDefinition;

        static XmlLiteComWrappers()
        {
            GetIUnknownImpl(out IntPtr fpQI, out IntPtr fpAddRef, out IntPtr fpRelease);
            IStreamManagedWrapperDefinition = IStreamManagedWrapper.GetInterfaceEntries(fpQI, fpAddRef, fpRelease);
        }

        public XmlLiteComWrappers()
        {
        }

        protected override unsafe ComInterfaceEntry* ComputeVtables(object obj, CreateComInterfaceFlags flags, out int count)
        {
            if (obj is Stream)
            {
                count = 1;
                return IStreamManagedWrapperDefinition;
            }

            count = 0;
            return null;
        }

        protected override object? CreateObject(IntPtr externalComObject, CreateObjectFlags flags)
        {
            return IXmlReaderNativeWrapper.CreateIfSupported(externalComObject, this);
        }

        protected override void ReleaseObjects(IEnumerable objects)
        {
            throw new NotImplementedException();
        }
    }
}
