using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrap.IO.Types
{
    /// <summary>
    /// The profile of a WRAP package.
    /// </summary>
    public class WrapProfile
    {
        /// <summary>
        /// Gets or sets a icon encoded as base64.
        /// </summary>
        public string? IconImage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a icon encoded as base64.
        /// </summary>
        public string? BackgroundImage { get; set; } = string.Empty;

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
    }
}
