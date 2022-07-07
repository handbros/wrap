using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrap
{
    /// <summary>
    /// Provides common constant values. 
    /// </summary>
    public static class WrapConstants
    {
        /// <summary>
        /// The signature bytes of WRAP. The length is 4 bytes.
        /// </summary>
        public static readonly uint WrapSignature = 0x57410102;
    }
}
