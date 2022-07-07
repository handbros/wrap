using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrap.IO.Types
{
    /// <summary>
    /// The matadata of a WRAP package.
    /// </summary>
    public class WrapMetadata
    {
        /// <summary>
        /// Gets or sets the container type of a WRAP.
        /// </summary>
        public WrapContainerType ContainerType { get; set; } = WrapContainerType.Electron;

        /// <summary>
        /// The uncompressed size of the archive(Unit: Bytes).
        /// </summary>
        public ulong UncompressedSize { get; set; } = 0;

        /// <summary>
        /// The compressed size of the archive(Unit: Bytes).
        /// </summary>
        public ulong CompressedSize { get; set; } = 0;

        /// <summary>
        /// Gets or sets whether the archive is encrypted.
        /// </summary>
        public bool IsEncrypted { get; set; } = false;

        /// <summary>
        /// Gets or sets SHA-3 256 hash of the password.
        /// </summary>
        public string? PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets optional datas.
        /// </summary>
        public Dictionary<string, string> OptionalDatas { get; set; } = new Dictionary<string, string>();
    }
}
