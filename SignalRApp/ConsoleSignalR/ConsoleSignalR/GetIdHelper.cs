using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;

namespace ConsoleSignalR
{
    public class GetIdHelper
    {
        public static string GetCpuId()
        {
            string cpuId = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject cpu in searcher.Get())
            {
                cpuId = cpu["ProcessorId"].ToString();
                break;
            }
            return cpuId;
        }

        public static string GetSystemUUID()
        {
            string uuid = string.Empty;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");

            foreach (ManagementObject managementObject in mos.Get())
            {
                uuid = managementObject["UUID"].ToString();
                break;
            }
            return uuid;
        }

        public static void GetOsVersion()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                Console.WriteLine("Caption: " + managementObject["Caption"]);  // オペレーティングシステムの名前
                Console.WriteLine("Version: " + managementObject["Version"]);  // バージョン番号
                Console.WriteLine("BuildNumber: " + managementObject["BuildNumber"]);  // ビルド番号
                Console.WriteLine("ServicePackMajorVersion: " + managementObject["ServicePackMajorVersion"]);  // サービスパックのメジャーバージョン
                Console.WriteLine("ServicePackMinorVersion: " + managementObject["ServicePackMinorVersion"]);  // サービスパックのマイナーバージョン
                Console.WriteLine("OSArchitecture: " + managementObject["OSArchitecture"]);  // オペレーティングシステムのアーキテクチャ (例: 32-bit, 64-bit)
            }
        }

        public static string GetDeviceId()
        {
            string deviceId = string.Empty;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\SQMClient"))
                {
                    if (key != null)
                    {
                        object val = key.GetValue("MachineId");
                        if (val != null)
                        {
                            deviceId = val.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("An error occurred while retrieving the device ID: " + ex.Message);
            }

            string stringWithoutBraces = deviceId.Replace("{", "").Replace("}", "");

            return stringWithoutBraces;
        }

    }
}
