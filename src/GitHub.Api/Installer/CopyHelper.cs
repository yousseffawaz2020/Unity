﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitHub.Logging;

namespace GitHub.Unity
{
    public static class CopyHelper
    {
        private static readonly ILogging Logger = LogHelper.GetLogger(typeof(CopyHelper));

        public static void Copy(NPath fromPath, NPath toPath)
        {
            Logger.Trace("Copying from " + fromPath + " to " + toPath + ".");
            
            try
            {
                CopyFolder(fromPath, toPath);
            }
            catch (Exception ex1)
            {
                Logger.Warning(ex1, "Error copying.");

                try
                {
                    CopyFolderContents(fromPath, toPath);
                }
                catch (Exception ex2)
                {
                    Logger.Error(ex1, "Error copying contents.");
                    throw;
                }
            }
            finally
            {
                fromPath.DeleteIfExists();
            }
        }
        public static void CopyFolder(NPath fromPath, NPath toPath)
        {
            Logger.Trace("CopyFolder fromPath: {0} toPath:{1}", fromPath.ToString(), toPath.ToString());
            toPath.DeleteIfExists();
            toPath.EnsureParentDirectoryExists();
            fromPath.Move(toPath);
        }

        public static void CopyFolderContents(NPath fromPath, NPath toPath)
        {
            Logger.Trace("CopyFolder Contents fromPath: {0} toPath:{1}", fromPath.ToString(), toPath.ToString());
            toPath.DeleteContents();
            fromPath.MoveFiles(toPath, true);
        }
    }
}
