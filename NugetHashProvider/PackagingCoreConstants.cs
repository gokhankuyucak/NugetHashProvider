using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetHashProvider
{
   public class PackagingCoreConstants
    {
        public static readonly string HashFileExtension = ".nupkg.sha512";
        public static readonly string NupkgExtension = ".nupkg";
        public static readonly string NuspecExtension = ".nuspec";
        public static readonly string PackageDownloadMarkerFileExtension = ".packagedownload.marker";

        /// <summary>
        /// _._ denotes an empty folder since OPC does not allow an
        /// actual empty folder.
        /// </summary>
        public static readonly string EmptyFolder = "_._";

        /// <summary>
        /// /_._ can be used to check empty folders from package readers where the / is normalized.
        /// </summary>
        public static readonly string ForwardSlashEmptyFolder = "/" + EmptyFolder;
    }
}
