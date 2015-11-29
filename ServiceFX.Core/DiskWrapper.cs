using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFX.Core
{
    public class DiskWrapper
    {
        public static string GetDriveOfApplication()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fullPath = Path.GetDirectoryName(path);
            string drive = Path.GetPathRoot(fullPath);
            return drive;
        }

        public static bool IsDriveReady(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
                if (drive.IsReady && drive.Name == driveName) {
                    return drive.IsReady;
                }
            }

            return false;
        }

        public static long GetTotalFreeDiskSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
                if (drive.IsReady && drive.Name == driveName) {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        public static long GetTotalDiskSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
                if (drive.IsReady && drive.Name == driveName) {
                    return drive.TotalSize;
                }
            }
            return -1;
        }

        public static float GetFreeDiskSpacePercent(string driveName)
        {
            long freeSpace = GetTotalFreeDiskSpace(driveName);
            long totalSpace = GetTotalDiskSpace(driveName);

            float percent = (float)freeSpace / (float)totalSpace;

            return percent;
        }
    }
}
