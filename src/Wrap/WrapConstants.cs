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
        #region ::Header::

        /// <summary>
        /// The signature bytes of WRAP. The length is 4 bytes.
        /// </summary>
        public const uint HEADER_SIGNATURE = 0x57410102;

        #endregion

        #region ::Optional Data Keys for Metadata::

        /// <summary>
        /// The optional data key: resources path for electron container. 
        /// </summary>
        public const string OD_KEY_ELECTRON_RES_PATH = "ELEC_RES_PATH";

        /// <summary>
        /// The optional data key: 'package.json' path for nw.js container. 
        /// </summary>
        public const string OD_KEY_NWJS_PKG_PATH = "NWJS_PKG_PATH";

        /// <summary>
        /// The optional data key: resources path for nw.js container. 
        /// </summary>
        public const string OD_KEY_NWJS_RESOURCES_PATH = "NWJS_RES_PATH";

        #endregion
    }
}
