using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlLiteWrapper
{
    internal static class HResult
    {
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int E_INVALIDARG = unchecked((int)0x80070057);
        public const int E_NOTIMPL = unchecked((int)0x80004001);
    }
}
