using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetHashProvider
{
    public class VersionFolderPathResolver
    {
        /// <summary>
        /// Gets the packages directory root folder.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets a flag indicating whether or not package ID's and versions are made lowercase.
        /// </summary>
        public bool IsLowerCase { get; }

        /// <summary>
        /// Initializes a new <see cref="VersionFolderPathResolver" /> class.
        /// </summary>
        /// <param name="rootPath">The packages directory root folder.</param>
        public VersionFolderPathResolver(string rootPath) : this(rootPath, isLowercase: true)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="VersionFolderPathResolver" /> class.
        /// </summary>
        /// <param name="rootPath">The packages directory root folder.</param>
        /// <param name="isLowercase"><c>true</c> if package ID's and versions are made lowercase;
        /// otherwise <c>false</c>.</param>
        public VersionFolderPathResolver(string rootPath, bool isLowercase)
        {
            RootPath = rootPath;
            IsLowerCase = isLowercase;
        }

        /// <summary>
        /// Gets the package install path.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The package install path.</returns>
        public virtual string GetInstallPath(string packageId, string version)
        {
            return Path.Combine(
                RootPath,
                GetPackageDirectory(packageId, version));
        }

        /// <summary>
        /// Gets the package version list path.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <returns>The package version list path.</returns>
        public string GetVersionListPath(string packageId)
        {
            return Path.Combine(
                RootPath,
                GetVersionListDirectory(packageId));
        }

        /// <summary>
        /// Gets the package file path.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The package file path.</returns>
        public string GetPackageFilePath(string packageId, string version)
        {
            return Path.Combine(
                GetInstallPath(packageId, version),
                GetPackageFileName(packageId, version));
        }

        /// <summary>
        /// Gets the manifest file path.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The manifest file path.</returns>
        public string GetManifestFilePath(string packageId, string version)
        {
            packageId = Normalize(packageId);
            return Path.Combine(
                GetInstallPath(packageId, version),
                GetManifestFileName(packageId, version));
        }

        /// <summary>
        /// Gets the hash file path.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The hash file path.</returns>
        public string GetHashPath(string packageId, string version)
        {
            return Path.Combine(
                GetInstallPath(packageId, version),
                GetHashFileName(packageId, version));
        }

        /// <summary>
        /// Gets the hash file name.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The hash file name.</returns>
        public string GetHashFileName(string packageId, string version)
        {
            return $"{Normalize(packageId)}.{NormalizeVersion(version)}{PackagingCoreConstants.HashFileExtension}";
        }

        /// <summary>
        /// Gets the version list directory.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <returns>The version list directory.</returns>
        public virtual string GetVersionListDirectory(string packageId)
        {
            return Normalize(packageId);
        }

        /// <summary>
        /// Gets the package directory.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The package directory.</returns>
        public virtual string GetPackageDirectory(string packageId, string version)
        {
            return Path.Combine(
                GetVersionListDirectory(packageId),
                NormalizeVersion(version));
        }

        /// <summary>
        /// Gets the package file name.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The package file name.</returns>
        public virtual string GetPackageFileName(string packageId, string version)
        {
            return $"{Normalize(packageId)}.{NormalizeVersion(version)}{PackagingCoreConstants.NupkgExtension}";
        }

        /// <summary>
        /// Gets the package download marker file name.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <returns>The package download marker file name.</returns>
        public string GetPackageDownloadMarkerFileName(string packageId)
        {
            return $"{Normalize(packageId)}{PackagingCoreConstants.PackageDownloadMarkerFileExtension}";
        }

        /// <summary>
        /// Gets the manifest file name.
        /// </summary>
        /// <param name="packageId">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns>The manifest file name.</returns>
        public virtual string GetManifestFileName(string packageId, string version)
        {
            return $"{Normalize(packageId)}{PackagingCoreConstants.NuspecExtension}";
        }

        private string NormalizeVersion(string version)
        {
            var versionString = version;

            if (IsLowerCase)
            {
                versionString = versionString.ToLowerInvariant();
            }

            return versionString;
        }

        private string Normalize(string packageId)
        {
            if (IsLowerCase)
            {
                packageId = packageId.ToLowerInvariant();
            }

            return packageId;
        }
    }
}
