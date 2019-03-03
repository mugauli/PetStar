using Grapesoft.Petstar.Events.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Grapesoft.Petstar.Events.Util
{
    public class FileUtil
    {
        public static object syncObj = new object();
        public static void WriteInLogFile(string linetoWrite)
        {
            try
            {
                lock (syncObj)
                {
                    CheckLogSize();
                    string fechaLog = "(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ")"; //fecha en que se escribio la incidencia
                    string logFilePath = Settings.Default.localLogPath; //ruta obtenida desde el app config
                    string logPath = logFilePath.Substring(0, logFilePath.LastIndexOf(@"\"));

                    if (!System.IO.Directory.Exists(logPath))
                        System.IO.Directory.CreateDirectory(logPath);

                    CreateOrWriteFile(logFilePath, fechaLog + "-" + linetoWrite);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void CheckLogSize()
        {
            string logFilePath = Settings.Default.localLogPath;
            long logSize;
            long configSize;
            System.IO.FileInfo logFile = null;
            try
            {
                logFile = new System.IO.FileInfo(logFilePath);
                logSize = logFile.Length;
                configSize = Settings.Default.logRotateSize;
                if (logSize >= configSize)
                {
                    //renombramos el archivo
                    String newLogName = logFilePath.Replace(".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', '_').Replace(':', '-'));
                    System.IO.File.Move(logFilePath, newLogName + ".txt");
                }

            }
            catch (System.IO.FileNotFoundException)
            {
                string logPath = logFilePath.Substring(0, logFilePath.LastIndexOf(@"\"));
                if (!System.IO.Directory.Exists(logPath))
                    System.IO.Directory.CreateDirectory(logPath);

                CreateOrWriteFile(logFilePath, "Creación del Log->" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            catch (System.IO.IOException e)
            {
                throw new Exception("Error al rotar el log:" + e.Message);
            }
        }

        private static void CreateOrWriteFile(string FileName, string text)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
            {
                file.WriteLine(text);
            }
        }

        public static bool SaveFile(string idFolderName, string fileName, byte[] fileData, bool overWrite, ref string error)
        {
            //get configuration from web.config
            string pathToSave = Settings.Default.nameSettPathToSave.Trim();
            bool result = false;
            string filePath;
            //Validate Root directory from webconfig and add slash if is missing. 
            pathToSave = validateDirectory(pathToSave);
            //Validate directory with idFolderName
            pathToSave = pathToSave + idFolderName;
            pathToSave = validateDirectory(pathToSave);
            //concat. Path and Filename
            filePath = pathToSave + fileName.Trim();


            try
            {
                FileStream fs;
                BinaryWriter bw;
                if (overWrite)
                {
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
                }
                bw = new BinaryWriter(fs);
                bw.Write(fileData);
                bw.Flush();
                bw.Close();
                fs.Close();
                bw = null;
                fs.Dispose();

                result = true;
            }
            catch (IOException ex)
            {
                error = ex.Message;
                result = false;
            }

            return result;
        }

        private static string validateDirectory(string pathToSave)
        {
            char lastchar = pathToSave[pathToSave.Length - 1];
            if (!lastchar.Equals("\\"))
            {
                pathToSave = pathToSave + "\\";
            }
            //Check if the directory exists
            if (!System.IO.Directory.Exists(pathToSave))
            {
                System.IO.Directory.CreateDirectory(pathToSave);
            }
            return pathToSave;
        }
    }
}