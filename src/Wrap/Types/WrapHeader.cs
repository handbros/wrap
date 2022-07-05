using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrap.Types
{
    // Signature(4 Bytes) + Uncompressed Size(8 Bytes) + Compressed Size(8 Bytes) + Metadata Size(4 Bytes)

    /// <summary>
    /// The header of a WRAP Executable File
    /// </summary>
    public class WrapHeader
    {
        /// <summary>
        /// The signature bytes of the WRAP Executable File
        /// </summary>
        public uint Signature { get; set; } = 0x57410102;

        /// <summary>
        /// The uncompressed size of the archive(Unit: Bytes)
        /// </summary>
        public ulong UncompressedSize { get; set; } = 0;

        /// <summary>
        /// The compressed size of the archive(Unit: Bytes)
        /// </summary>
        public ulong CompressedSize { get; set; } = 0;

        /// <summary>
        /// The size of the metadata
        /// </summary>
        public uint MetadataSize { get; set; } = 0;
    }
}
