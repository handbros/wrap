using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrap.IO.Types
{
    // Signature(4 Bytes) + Metadata Size(4 Bytes) + Profile Size(4 Bytes)

    /// <summary>
    /// The header of a WRAP package.
    /// </summary>
    public class WrapHeader
    {
        /// <summary>
        /// The signature bytes of the WRAP Executable File.
        /// </summary>
        public uint Signature { get; set; } = 0x57410102;

        /// <summary>
        /// The size of the metadata(Unit: Bytes).
        /// </summary>
        public uint MetadataSize { get; set; } = 0;

        /// <summary>
        /// The size of the profile(Unit: Bytes).
        /// </summary>
        public uint ProfileSize { get; set; } = 0;
    }
}
