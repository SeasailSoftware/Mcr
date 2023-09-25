﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ThreeNH.Communication.Utility
{
    public static class StructConverter
    {
        public static byte[] StructToBytes<StructType>(StructType structure)
        {
            int size = Marshal.SizeOf(structure);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static Object BytesToStruct(IList<byte> bytes, Type structType, int startIndex=0)
        {
            int size = Marshal.SizeOf(structType);
            if (bytes.Count < size)
            {
                Trace.TraceError($"数据长度不够。需要：{size}，实际：{bytes.Count}");
                throw new ArgumentException("the length of the bytes is not enough");
            }
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes.ToArray(), startIndex, buffer, size);
                return Marshal.PtrToStructure(buffer, structType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }


        public static StructType BytesToStruct<StructType>(IList<byte> bytes, int startIndex = 0)
        {
            return (StructType)BytesToStruct(bytes, typeof(StructType), startIndex);
        }
    }
}
