// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime;

using Internal.Runtime.CompilerServices;

namespace System.Threading
{
    //
    // Methods for accessing memory with volatile semantics.
    //
    public static unsafe class Volatile
    {
        #region Boolean
        private struct VolatileBoolean { public volatile bool Value; }

        public static bool Read(ref bool location)
        {
            return Unsafe.As<bool, VolatileBoolean>(ref location).Value;
        }

        public static void Write(ref bool location, bool value)
        {
            Unsafe.As<bool, VolatileBoolean>(ref location).Value = value;
        }
        #endregion

        #region Byte
        private struct VolatileByte { public volatile byte Value; }

        public static byte Read(ref byte location)
        {
            return Unsafe.As<byte, VolatileByte>(ref location).Value;
        }

        public static void Write(ref byte location, byte value)
        {
            Unsafe.As<byte, VolatileByte>(ref location).Value = value;
        }
        #endregion

        #region Double
        public static double Read(ref double location)
        {
            long result = Read(ref Unsafe.As<double, long>(ref location));
            return *(double*)&result;
        }

        public static void Write(ref double location, double value)
        {
            Write(ref Unsafe.As<double, long>(ref location), *(long*)&value);
        }
        #endregion

        #region Int16
        private struct VolatileInt16 { public volatile short Value; }

        public static short Read(ref short location)
        {
            return Unsafe.As<short, VolatileInt16>(ref location).Value;
        }

        public static void Write(ref short location, short value)
        {
            Unsafe.As<short, VolatileInt16>(ref location).Value = value;
        }
        #endregion

        #region Int32
        private struct VolatileInt32 { public volatile int Value; }

        public static int Read(ref int location)
        {
            return Unsafe.As<int, VolatileInt32>(ref location).Value;
        }

        public static void Write(ref int location, int value)
        {
            Unsafe.As<int, VolatileInt32>(ref location).Value = value;
        }
        #endregion

        #region Int64
        public static long Read(ref long location)
        {
#if BIT64
            return (Int64)Unsafe.As<Int64, VolatileIntPtr>(ref location).Value;
#else
            return Interlocked.CompareExchange(ref location, 0, 0);
#endif
        }

        public static void Write(ref long location, long value)
        {
#if BIT64
            Unsafe.As<Int64, VolatileIntPtr>(ref location).Value = (IntPtr)value;
#else
            Interlocked.Exchange(ref location, value);
#endif
        }
        #endregion

        #region IntPtr
        private struct VolatileIntPtr { public volatile IntPtr Value; }

        public static IntPtr Read(ref IntPtr location)
        {
            return Unsafe.As<IntPtr, VolatileIntPtr>(ref location).Value;
        }

        public static void Write(ref IntPtr location, IntPtr value)
        {
            fixed (IntPtr* p = &location)
            {
                ((VolatileIntPtr*)p)->Value = value;
            }
        }
        #endregion

        #region SByte
        private struct VolatileSByte { public volatile sbyte Value; }

        [CLSCompliant(false)]
        public static sbyte Read(ref sbyte location)
        {
            return Unsafe.As<sbyte, VolatileSByte>(ref location).Value;
        }

        [CLSCompliant(false)]
        public static void Write(ref sbyte location, sbyte value)
        {
            Unsafe.As<sbyte, VolatileSByte>(ref location).Value = value;
        }
        #endregion

        #region Single
        private struct VolatileSingle { public volatile float Value; }

        public static float Read(ref float location)
        {
            return Unsafe.As<float, VolatileSingle>(ref location).Value;
        }

        public static void Write(ref float location, float value)
        {
            Unsafe.As<float, VolatileSingle>(ref location).Value = value;
        }
        #endregion

        #region UInt16
        private struct VolatileUInt16 { public volatile ushort Value; }

        [CLSCompliant(false)]
        public static ushort Read(ref ushort location)
        {
            return Unsafe.As<ushort, VolatileUInt16>(ref location).Value;
        }

        [CLSCompliant(false)]
        public static void Write(ref ushort location, ushort value)
        {
            Unsafe.As<ushort, VolatileUInt16>(ref location).Value = value;
        }
        #endregion

        #region UInt32
        private struct VolatileUInt32 { public volatile uint Value; }

        [CLSCompliant(false)]
        public static uint Read(ref uint location)
        {
            return Unsafe.As<uint, VolatileUInt32>(ref location).Value;
        }

        [CLSCompliant(false)]
        public static void Write(ref uint location, uint value)
        {
            Unsafe.As<uint, VolatileUInt32>(ref location).Value = value;
        }
        #endregion

        #region UInt64
        [CLSCompliant(false)]
        public static ulong Read(ref ulong location)
        {
            return (ulong)Read(ref Unsafe.As<ulong, long>(ref location));
        }

        [CLSCompliant(false)]
        public static void Write(ref ulong location, ulong value)
        {
            Write(ref Unsafe.As<ulong, long>(ref location), (long)value);
        }
        #endregion

        #region UIntPtr
        private struct VolatileUIntPtr { public volatile UIntPtr Value; }

        [CLSCompliant(false)]
        public static UIntPtr Read(ref UIntPtr location)
        {
            return Unsafe.As<UIntPtr, VolatileUIntPtr>(ref location).Value;
        }

        [CLSCompliant(false)]
        public static void Write(ref UIntPtr location, UIntPtr value)
        {
            Unsafe.As<UIntPtr, VolatileUIntPtr>(ref location).Value = value;
        }
        #endregion

        #region T
        private struct VolatileObject { public volatile object Value; }

        public static T Read<T>(ref T location) where T : class
        {
            return Unsafe.As<T>(Unsafe.As<T, VolatileObject>(ref location).Value);
        }

        public static void Write<T>(ref T location, T value) where T : class
        {
            Unsafe.As<T, VolatileObject>(ref location).Value = value;
        }
        #endregion
    }
}
