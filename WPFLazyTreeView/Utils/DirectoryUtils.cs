﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLazyTreeView.Utils
{
    public static class DirectoryUtils
    {
        private static bool IsValidDirectoriesOrFiles(FileSystemInfo fsi)
        {
            try
            {
                // 숨겨진 폴더를 확인합니다.
                bool isHidden = (fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                // 권한이 있는 폴더를 확인합니다.
                bool isSystem = (fsi.Attributes & FileAttributes.System) == FileAttributes.System;

                return !(isHidden || isSystem);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public static bool IsDirectoryOrFileExists(string path)
        {
            IEnumerable<DirectoryInfo> directories = GetDirectories(path);
            if (directories.Any()) return true;

            IEnumerable<FileInfo> files = GetFiles(path);
            if (files.Any()) return true;

            return false;
        }

        public static IEnumerable<DirectoryInfo> GetDirectories(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                return di.GetDirectories("*", SearchOption.TopDirectoryOnly).Where(IsValidDirectoriesOrFiles);
            }
            else
            {
                return Enumerable.Empty<DirectoryInfo>();
            }
        }

        public static IEnumerable<FileInfo> GetFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                return di.GetFiles("*", SearchOption.TopDirectoryOnly).Where(IsValidDirectoriesOrFiles);
            }
            else
            {
                return Enumerable.Empty<FileInfo>();
            }
        }
    }
}
