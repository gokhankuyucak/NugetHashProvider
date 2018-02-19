using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NugetHashProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            string packageId = "HelloNuget";
            string version = "1.0.2";
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            VersionFolderPathResolver versionFolderPathResolver = new VersionFolderPathResolver(path);
            var targetPath = versionFolderPathResolver.GetInstallPath(packageId, version);
            var targetTempNupkg = Path.Combine(targetPath, Path.GetRandomFileName());
            using (var nupkgStream = new FileStream(
                                      targetTempNupkg,
                                      FileMode.Create,
                                      FileAccess.ReadWrite,
                                      FileShare.ReadWrite | FileShare.Delete,
                                      bufferSize: 4096,
                                      useAsync: true))
            {
                string packageHash;
                nupkgStream.Position = 0;
                packageHash = Convert.ToBase64String(new CryptoHashProvider("SHA512").CalculateHash(nupkgStream));
                var hashPath = versionFolderPathResolver.GetHashPath(packageId, version);

                File.WriteAllText(hashPath, packageHash);
            }
        }
    }
}
