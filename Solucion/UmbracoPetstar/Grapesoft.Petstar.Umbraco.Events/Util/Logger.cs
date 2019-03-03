using Grapesoft.Petstar.Events.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Grapesoft.Petstar.Events.Util
{
    public class Logger
    {
        /// <summary>
        /// Semaforo de escritura de log
        /// </summary>
        private static readonly object logLock = new object();

        /// <summary>
        /// Escribe un salto de linea en la consola.
        /// </summary>
        /// <param name="message"></param>
        public static void Enter()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Escribe el log en blanco
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Write(message);
        }
        public static void Log(string message, params object[] parameters)
        {
            Log(string.Format(message, parameters));
        }

        /// <summary>
        /// Escribe el log en Verde
        /// </summary>
        /// <param name="message"></param>
        public static void LogImportant(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LogImportant(string message, params object[] parameters)
        {
            LogImportant(string.Format(message, parameters));
        }

        /// <summary>
        /// Escribe el log en Rojo
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LogError(string message, params object[] parameters)
        {
            LogError(string.Format(message, parameters));
        }

        /// <summary>
        /// Escribe el log en Amarillo
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarning(string message, params object[] parameters)
        {
            LogWarning(string.Format(message, parameters));
        }

        /// <summary>
        /// Agrega fecha y hora en le log
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string setDate(string message)
        {
            try
            {
                return string.Format("{0} -- {1}", DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy"), message);
            }
            catch (Exception ex)
            {
                return "No date";
            }

        }

        private static void Write(string Message)
        {
            try
            {
                if (Settings.Default.LogIsActive)
                    Console.WriteLine(setDate(Message));

                new Thread(() =>
                {
                    lock (logLock)
                    {
                        try
                        {
                            if (Settings.Default.LogIsActive)
                                FileUtil.WriteInLogFile(Message);
                        }
                        catch (Exception)
                        {
                            //Se garantiza no existir excepcion en un elemento de logueo
                        }

                    }

                }).Start();

            }
            catch (Exception)
            {
                //Se garantiza no existir excepcion en un elemento tan importante
            }
        }

        private static void WriteDBLocal(string Message)
        {
            try
            {
                new Thread(() =>
                {
                    lock (logLock)
                    {
                        try
                        {
                            if (Settings.Default.LogIsActive)
                                FileUtil.WriteInLogFile(Message);
                        }
                        catch (Exception)
                        {

                        }

                    }

                }).Start();

            }
            catch (Exception)
            {
                //Se garantiza no existir excepcion en un elemnto tan importante
            }
        }

    }
}