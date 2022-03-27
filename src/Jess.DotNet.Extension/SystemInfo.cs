using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public sealed class SystemInfo
    {
        public static string BIOSSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_BIOS");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var item in moc)
            {
                var temp = item.Properties["SerialNumber"].Value;
                if (temp != null)
                    return temp.ToString();
            }
            return string.Empty;
        }

        public static string CPUID()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var item in moc)
            {
                var temp = item.Properties["ProcessorId"].Value;
                if (temp != null)
                    return temp.ToString();
            }
            return string.Empty;
        }

        public static string HDSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var item in moc)
            {
                var temp = item.Properties["SerialNumber"].Value;
                if (temp != null)
                    return temp.ToString();
            }
            return HDSignature();
        }

        public static string HDSignature()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var item in moc)
            {
                var temp = item.Properties["SerialNumber"].Value;
                if (temp != null)
                    return temp.ToString();
                else
                {
                    var sig = (uint)item.Properties["Signature"].Value;
                    return sig.ToString("X8");
                }
            }
            return string.Empty;
        }

    }
}
