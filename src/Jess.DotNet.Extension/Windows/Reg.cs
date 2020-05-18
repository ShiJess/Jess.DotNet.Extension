using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jess.DotNet.Extension.Windows
{
    /// <summary>
    /// 注册表相关信息
    /// </summary>
    public class Reg
    {
    }

    /// <summary>
    /// 注册表单元
    /// </summary>
    public enum RegistryHive : uint
    {
        HKEY_CLASSES_ROOT = 0x80000000,
        HKEY_CURRENT_USER = 0x80000001,
        HKEY_LOCAL_MACHINE = 0x80000002,
        HKEY_USERS = 0x80000003,
        HKEY_CURRENT_CONFIG = 0x80000005,
        HKEY_DYN_DATA = 0x80000006,
    }

    /// <summary>
    /// 注册表项数据类型
    /// </summary>
    public enum RegDataType : int
    {
        REG_SZ = 1,
        REG_EXPAND_SZ = 2,
        REG_BINARY = 3,
        REG_DWORD = 4,
        REG_MULTI_SZ = 7,
        REG_QWORD = 11,
    }

}