using UnityEngine;
using System.Collections;
using System;
using System.IO;
namespace BW {

    /// <summary>
    /// GameDebug
    /// </summary>
    public static class GameDebug {

        static StreamWriter logFile = null;

        static bool forwardToDebug = true;  // 是否发送到Debug

        public static void Init(string logfilePath,string logBaseName) {

            forwardToDebug = Application.isEditor;
            Application.logMessageReceived += LogCallBack;

            try {
                logFile = File.CreateText(Path.Combine(logfilePath, logBaseName+DateTime.Now.ToLongDateString()));
                logFile.AutoFlush = true;                
            } catch (Exception) {

                throw;
            }

        }

        private static void LogCallBack(string message, string stackTrace, LogType type) {
            switch (type) {
                default:
                case LogType.Log: 
                    _Log(message);
                    break;
                case LogType.Error:
                    _LogError(message);
                    break;
                case LogType.Warning:
                    _LogWarning(message);
                    break;
                case LogType.Exception:
                    break;
            }
        }


        public static void Log(string msg) {
            if (forwardToDebug)
                Debug.Log(msg);
            else
                _Log(msg);
        }
        private static void _Log(string message) {

            Console.Write($"{Time.frameCount}: {message}");
            logFile?.WriteLine($"{Time.frameCount}: {message}.\n");
        }

        public static void LogError(string message) {
            if (forwardToDebug)
                Debug.LogError(message);
            else
                _LogError(message);
        }


        private static void _LogError(string message) {
            Console.Write($"{Time.frameCount} : [ERR]   {message}");
            logFile?.WriteLine($"{Time.frameCount} : [ERR]   {message}\n");
        }

        public static void LogWarning(string message) {
            if (forwardToDebug)
                Debug.LogWarning(message);
            else
                _LogWarning(message);
        }

        private static void _LogWarning(string message) {
            Console.Write($"{Time.frameCount} : [WARN]   {message}");
            logFile?.WriteLine($"{Time.frameCount} : [WARN]   {message}\n");
        }



        public static void ShutDown() {
            Application.logMessageReceived -= LogCallBack;
            logFile?.Close();
            logFile = null;
        }




    }



}