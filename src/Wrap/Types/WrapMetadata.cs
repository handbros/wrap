using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrap.Types
{
    /// <summary>
    /// The metadata of a WRAP Executable File
    /// </summary>
    public class WrapMetadata
    {
        /// <summary>
        /// Gets or sets a icon encoded as base64.
        /// </summary>
        public string? Icon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a product name.
        /// </summary>
        public string? ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a url linked to the product name.
        /// </summary>
        public string? ProductUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a creator.
        /// </summary>
        public string? Creator { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a url linked to the creator.
        /// </summary>
        public string? CreatorUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a description.
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a version string.
        /// </summary>
        public string? Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the container type of a WRAP.
        /// </summary>
        public WrapContainerType ContainerType { get; set; } = WrapContainerType.Electron;

        /// <summary>
        /// Gets or sets whether the archive is encrypted.
        /// </summary>
        public bool IsEncrypted { get; set; } = false;

        /// <summary>
        /// Gets or sets SHA-3 256 hash of the password.
        /// </summary>
        public string? PasswordHash { get; set; } = string.Empty;
    }
}
