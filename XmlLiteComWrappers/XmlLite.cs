using System.Runtime.InteropServices;

namespace XmlLiteWrapper
{
    public enum XmlNodeType
    {
        XmlNodeType_None = 0,
        XmlNodeType_Element = 1,
        XmlNodeType_Attribute = 2,
        XmlNodeType_Text = 3,
        XmlNodeType_CDATA = 4,
        XmlNodeType_ProcessingInstruction = 7,
        XmlNodeType_Comment = 8,
        XmlNodeType_DocumentType = 10,
        XmlNodeType_Whitespace = 13,
        XmlNodeType_EndElement = 15,
        XmlNodeType_XmlDeclaration = 17
    }

    public enum XmlConformanceLevel
    {
        XmlConformanceLevel_Auto = 0,
        XmlConformanceLevel_Fragment = 1,
        XmlConformanceLevel_Document = 2
    }

    public enum DtdProcessing
    {
        DtdProcessing_Prohibit = 0,
        DtdProcessing_Parse = (DtdProcessing_Prohibit + 1)
    }

    public enum XmlReadState
    {
        XmlReadState_Initial = 0,
        XmlReadState_Interactive = 1,
        XmlReadState_Error = 2,
        XmlReadState_EndOfFile = 3,
        XmlReadState_Closed = 4
    }

    public enum XmlReaderProperty
    {
        XmlReaderProperty_MultiLanguage = 0,
        XmlReaderProperty_ConformanceLevel = (XmlReaderProperty_MultiLanguage + 1),
        XmlReaderProperty_RandomAccess = (XmlReaderProperty_ConformanceLevel + 1),
        XmlReaderProperty_XmlResolver = (XmlReaderProperty_RandomAccess + 1),
        XmlReaderProperty_DtdProcessing = (XmlReaderProperty_XmlResolver + 1),
        XmlReaderProperty_ReadState = (XmlReaderProperty_DtdProcessing + 1),
        XmlReaderProperty_MaxElementDepth = (XmlReaderProperty_ReadState + 1),
        XmlReaderProperty_MaxEntityExpansion = (XmlReaderProperty_MaxElementDepth + 1)
    }

    public enum XmlStandalone
    {
        XmlStandalone_Omit = 0,
        XmlStandalone_Yes = 1,
        XmlStandalone_No = 2
    }

    public enum XmlWriterProperty
    {
        XmlWriterProperty_MultiLanguage = 0,
        XmlWriterProperty_Indent = (XmlWriterProperty_MultiLanguage + 1),
        XmlWriterProperty_ByteOrderMark = (XmlWriterProperty_Indent + 1),
        XmlWriterProperty_OmitXmlDeclaration = (XmlWriterProperty_ByteOrderMark + 1),
        XmlWriterProperty_ConformanceLevel = (XmlWriterProperty_OmitXmlDeclaration + 1),
        XmlWriterProperty_CompactEmptyElement = (XmlWriterProperty_ConformanceLevel + 1)
    }

    public interface IXmlReader
    {
        internal static Guid IID = new("7279FC81-709D-4095-B63D-69FE4B0D9030");

        void SetInput(Stream stream);

        XmlNodeType Read();

        XmlNodeType NodeType { get; }
        
        string LocalName { get; }
    }

    public static class XmlLite
    {
        static XmlLiteComWrappers _comWrappers = new XmlLiteComWrappers();

        public static IXmlReader CreateXmlReader()
        {
            int hr = CreateXmlReader(ref IXmlReader.IID, out IntPtr obj, IntPtr.Zero);
            if (hr != HResult.S_OK)
                throw Marshal.GetExceptionForHR(hr)!;

            IXmlReader reader = (IXmlReader)_comWrappers.GetOrCreateObjectForComInstance(obj, CreateObjectFlags.UniqueInstance);
            Marshal.Release(obj);

            return reader;
        }

        [DllImport("xmllite")]
        private static extern int CreateXmlReader(ref Guid iid, out IntPtr obj, IntPtr malloc);
    }
}
