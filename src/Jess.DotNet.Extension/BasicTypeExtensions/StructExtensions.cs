using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Jess.DotNet.Extension
{
    public static class StructExtensions
    {
        /// <summary>
        /// 结构体对象转byte数组 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="structObj"></param>
        /// <returns></returns>
        public static byte[] StructToBytes<T>(this T structObj) where T : struct
        {
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];

            IntPtr intPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObj, intPtr, false);
            Marshal.Copy(intPtr, bytes, 0, size);

            Marshal.FreeHGlobal(intPtr);
            return bytes;
        }

        /// <summary>
        /// bype数组转结构体
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object BytesToStruct(this byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
                throw new NotSupportedException();

            IntPtr intPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, intPtr, size);
            object obj = Marshal.PtrToStructure(intPtr, type);
            Marshal.FreeHGlobal(intPtr);

            return obj;
        }

        public static T BytesToStruct<T>(this byte[] bytes) where T : struct
        {
            var type = typeof(T);
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
                throw new NotSupportedException();

            IntPtr intPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, intPtr, size);
            object obj = Marshal.PtrToStructure(intPtr, type);
            Marshal.FreeHGlobal(intPtr);

            return (T)obj;
        }

    }
}
