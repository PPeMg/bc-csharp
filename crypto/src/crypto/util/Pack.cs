using System;
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
using System.Buffers.Binary;
#endif
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Org.BouncyCastle.Crypto.Utilities
{
    internal static class Pack
    {
        internal static void UInt16_To_BE(ushort n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt16BigEndian(bs, n);
#else
            bs[0] = (byte)(n >> 8);
            bs[1] = (byte)n;
#endif
        }

        internal static void UInt16_To_BE(ushort n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt16BigEndian(bs.AsSpan(off), n);
#else
            bs[off] = (byte)(n >> 8);
            bs[off + 1] = (byte)n;
#endif
        }

        internal static void UInt16_To_BE(ushort[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt16_To_BE(ns[i], bs, off);
                off += 2;
            }
        }

        internal static void UInt16_To_BE(ushort[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                UInt16_To_BE(ns[nsOff + i], bs, bsOff);
                bsOff += 2;
            }
        }

        internal static byte[] UInt16_To_BE(ushort n)
        {
            byte[] bs = new byte[2];
            UInt16_To_BE(n, bs, 0);
            return bs;
        }

        internal static byte[] UInt16_To_BE(ushort[] ns)
        {
            return UInt16_To_BE(ns, 0, ns.Length);
        }

        internal static byte[] UInt16_To_BE(ushort[] ns, int nsOff, int nsLen)
        {
            byte[] bs = new byte[2 * nsLen];
            UInt16_To_BE(ns, nsOff, nsLen, bs, 0);
            return bs;
        }

        internal static ushort BE_To_UInt16(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt16BigEndian(bs.AsSpan(off));
#else
            uint n = (uint)bs[off] << 8
                | bs[off + 1];
            return (ushort)n;
#endif
        }

        internal static void BE_To_UInt16(byte[] bs, int bsOff, ushort[] ns, int nsOff)
        {
            ns[nsOff] = BE_To_UInt16(bs, bsOff);
        }

        internal static ushort[] BE_To_UInt16(byte[] bs)
        {
            return BE_To_UInt16(bs, 0, bs.Length);
        }

        internal static ushort[] BE_To_UInt16(byte[] bs, int off, int len)
        {
            if ((len & 1) != 0)
                throw new ArgumentException("must be a multiple of 2", "len");

            ushort[] ns = new ushort[len / 2];
            for (int i = 0; i < len; i += 2)
            {
                BE_To_UInt16(bs, off + i, ns, i >> 1);
            }
            return ns;
        }

        internal static void UInt32_To_BE(uint n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt32BigEndian(bs, n);
#else
            bs[0] = (byte)(n >> 24);
            bs[1] = (byte)(n >> 16);
            bs[2] = (byte)(n >> 8);
            bs[3] = (byte)n;
#endif
        }

        internal static void UInt32_To_BE(uint n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt32BigEndian(bs.AsSpan(off), n);
#else
            bs[off] = (byte)(n >> 24);
            bs[off + 1] = (byte)(n >> 16);
            bs[off + 2] = (byte)(n >> 8);
            bs[off + 3] = (byte)n;
#endif
        }

        internal static void UInt32_To_BE(uint[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt32_To_BE(ns[i], bs, off);
                off += 4;
            }
        }

        internal static void UInt32_To_BE(uint[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                UInt32_To_BE(ns[nsOff + i], bs, bsOff);
                bsOff += 4;
            }
        }

        internal static byte[] UInt32_To_BE(uint n)
        {
            byte[] bs = new byte[4];
            UInt32_To_BE(n, bs, 0);
            return bs;
        }

        internal static byte[] UInt32_To_BE(uint[] ns)
        {
            byte[] bs = new byte[4 * ns.Length];
            UInt32_To_BE(ns, bs, 0);
            return bs;
        }

        internal static uint BE_To_UInt32(byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt32BigEndian(bs);
#else
            return (uint)bs[0] << 24
                | (uint)bs[1] << 16
                | (uint)bs[2] << 8
                | bs[3];
#endif
        }

        internal static uint BE_To_UInt32(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt32BigEndian(bs.AsSpan(off));
#else
            return (uint)bs[off] << 24
                | (uint)bs[off + 1] << 16
                | (uint)bs[off + 2] << 8
                | bs[off + 3];
#endif
        }

        internal static uint BE_To_UInt32_Partial(byte[] bs, int off, int len)
        {
            Debug.Assert(1 <= len && len <= 4);

            uint result = bs[off];
            for (int i = 1; i < len; ++i)
            {
                result <<= 8;
                result |= bs[off + i];
            }
            return result;
        }

        internal static void BE_To_UInt32(byte[] bs, int off, uint[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = BE_To_UInt32(bs, off);
                off += 4;
            }
        }

        internal static void BE_To_UInt32(byte[] bs, int bsOff, uint[] ns, int nsOff, int nsLen)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                ns[nsOff + i] = BE_To_UInt32(bs, bsOff);
                bsOff += 4;
            }
        }

        internal static byte[] UInt64_To_BE(ulong n)
        {
            byte[] bs = new byte[8];
            UInt64_To_BE(n, bs, 0);
            return bs;
        }

        internal static void UInt64_To_BE(ulong n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt64BigEndian(bs, n);
#else
            UInt32_To_BE((uint)(n >> 32), bs);
            UInt32_To_BE((uint)n, bs, 4);
#endif
        }

        internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt64BigEndian(bs.AsSpan(off), n);
#else
            UInt32_To_BE((uint)(n >> 32), bs, off);
            UInt32_To_BE((uint)n, bs, off + 4);
#endif
        }

        internal static byte[] UInt64_To_BE(ulong[] ns)
        {
            byte[] bs = new byte[8 * ns.Length];
            UInt64_To_BE(ns, bs, 0);
            return bs;
        }

        internal static void UInt64_To_BE(ulong[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt64_To_BE(ns[i], bs, off);
                off += 8;
            }
        }

        internal static void UInt64_To_BE(ulong[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                UInt64_To_BE(ns[nsOff + i], bs, bsOff);
                bsOff += 8;
            }
        }

        internal static ulong BE_To_UInt64(byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt64BigEndian(bs);
#else
            uint hi = BE_To_UInt32(bs);
            uint lo = BE_To_UInt32(bs, 4);
            return ((ulong)hi << 32) | (ulong)lo;
#endif
        }

        internal static ulong BE_To_UInt64(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt64BigEndian(bs.AsSpan(off));
#else
            uint hi = BE_To_UInt32(bs, off);
            uint lo = BE_To_UInt32(bs, off + 4);
            return ((ulong)hi << 32) | (ulong)lo;
#endif
        }

        internal static ulong BE_To_UInt64_Partial(byte[] bs, int off, int len)
        {
            Debug.Assert(1 <= len && len <= 8);

            ulong result = bs[off];
            for (int i = 1; i < len; ++i)
            {
                result <<= 8;
                result |= bs[off + i];
            }
            return result;
        }

        internal static void BE_To_UInt64(byte[] bs, int off, ulong[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = BE_To_UInt64(bs, off);
                off += 8;
            }
        }

        internal static void BE_To_UInt64(byte[] bs, int bsOff, ulong[] ns, int nsOff, int nsLen)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                ns[nsOff + i] = BE_To_UInt64(bs, bsOff);
                bsOff += 8;
            }
        }

        internal static void UInt16_To_LE(ushort n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt16LittleEndian(bs, n);
#else
            bs[0] = (byte)n;
            bs[1] = (byte)(n >> 8);
#endif
        }

        internal static void UInt16_To_LE(ushort n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt16LittleEndian(bs.AsSpan(off), n);
#else
            bs[off] = (byte)n;
            bs[off + 1] = (byte)(n >> 8);
#endif
        }

        internal static byte[] UInt16_To_LE(ushort n)
        {
            byte[] bs = new byte[2];
            UInt16_To_LE(n, bs, 0);
            return bs;
        }

        internal static ushort LE_To_UInt16(byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt16LittleEndian(bs);
#else
            uint n = bs[0]
                | (uint)bs[1] << 8;
            return (ushort)n;
#endif
        }

        internal static ushort LE_To_UInt16(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt16LittleEndian(bs.AsSpan(off));
#else
            uint n = bs[off]
                | (uint)bs[off + 1] << 8;
            return (ushort)n;
#endif
        }

        internal static byte[] UInt32_To_LE(uint n)
        {
            byte[] bs = new byte[4];
            UInt32_To_LE(n, bs, 0);
            return bs;
        }

        internal static void UInt32_To_LE(uint n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt32LittleEndian(bs, n);
#else
            bs[0] = (byte)n;
            bs[1] = (byte)(n >> 8);
            bs[2] = (byte)(n >> 16);
            bs[3] = (byte)(n >> 24);
#endif
        }

        internal static void UInt32_To_LE(uint n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt32LittleEndian(bs.AsSpan(off), n);
#else
            bs[off] = (byte)n;
            bs[off + 1] = (byte)(n >> 8);
            bs[off + 2] = (byte)(n >> 16);
            bs[off + 3] = (byte)(n >> 24);
#endif
        }

        internal static byte[] UInt32_To_LE(uint[] ns)
        {
            byte[] bs = new byte[4 * ns.Length];
            UInt32_To_LE(ns, bs, 0);
            return bs;
        }

        internal static void UInt32_To_LE(uint[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt32_To_LE(ns[i], bs, off);
                off += 4;
            }
        }

        internal static void UInt32_To_LE(uint[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                UInt32_To_LE(ns[nsOff + i], bs, bsOff);
                bsOff += 4;
            }
        }

        internal static uint LE_To_UInt24(byte[] bs, int off)
        {
            return bs[off]
                | (uint)bs[off + 1] << 8
                | (uint)bs[off + 2] << 16;
        }

        internal static uint LE_To_UInt32(byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt32LittleEndian(bs);
#else
            return bs[0]
                | (uint)bs[1] << 8
                | (uint)bs[2] << 16
                | (uint)bs[3] << 24;
#endif
        }

        internal static uint LE_To_UInt32(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt32LittleEndian(bs.AsSpan(off));
#else
            return bs[off]
                | (uint)bs[off + 1] << 8
                | (uint)bs[off + 2] << 16
                | (uint)bs[off + 3] << 24;
#endif
        }

        internal static void LE_To_UInt32(byte[] bs, int off, uint[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = LE_To_UInt32(bs, off);
                off += 4;
            }
        }

        internal static void LE_To_UInt32(byte[] bs, int bOff, uint[] ns, int nOff, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                ns[nOff + i] = LE_To_UInt32(bs, bOff);
                bOff += 4;
            }
        }

        internal static uint[] LE_To_UInt32(byte[] bs, int off, int count)
        {
            uint[] ns = new uint[count];
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = LE_To_UInt32(bs, off);
                off += 4;
            }
            return ns;
        }

        internal static byte[] UInt64_To_LE(ulong n)
        {
            byte[] bs = new byte[8];
            UInt64_To_LE(n, bs, 0);
            return bs;
        }

        internal static void UInt64_To_LE(ulong n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt64LittleEndian(bs, n);
#else
            UInt32_To_LE((uint)n, bs);
            UInt32_To_LE((uint)(n >> 32), bs, 4);
#endif
        }

        internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt64LittleEndian(bs.AsSpan(off), n);
#else
            UInt32_To_LE((uint)n, bs, off);
            UInt32_To_LE((uint)(n >> 32), bs, off + 4);
#endif
        }

        internal static byte[] UInt64_To_LE(ulong[] ns)
        {
            byte[] bs = new byte[8 * ns.Length];
            UInt64_To_LE(ns, bs, 0);
            return bs;
        }

        internal static void UInt64_To_LE(ulong[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt64_To_LE(ns[i], bs, off);
                off += 8;
            }
        }

        internal static void UInt64_To_LE(ulong[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                UInt64_To_LE(ns[nsOff + i], bs, bsOff);
                bsOff += 8;
            }
        }

        internal static ulong LE_To_UInt64(byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt64LittleEndian(bs);
#else
            uint lo = LE_To_UInt32(bs);
            uint hi = LE_To_UInt32(bs, 4);
            return ((ulong)hi << 32) | (ulong)lo;
#endif
        }

        internal static ulong LE_To_UInt64(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt64LittleEndian(bs.AsSpan(off));
#else
            uint lo = LE_To_UInt32(bs, off);
            uint hi = LE_To_UInt32(bs, off + 4);
            return ((ulong)hi << 32) | (ulong)lo;
#endif
        }

        internal static void LE_To_UInt64(byte[] bs, int off, ulong[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = LE_To_UInt64(bs, off);
                off += 8;
            }
        }

        internal static void LE_To_UInt64(byte[] bs, int bsOff, ulong[] ns, int nsOff, int nsLen)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                ns[nsOff + i] = LE_To_UInt64(bs, bsOff);
                bsOff += 8;
            }
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint BE_To_UInt32(ReadOnlySpan<byte> bs)
        {
            return BinaryPrimitives.ReadUInt32BigEndian(bs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void BE_To_UInt32(ReadOnlySpan<byte> bs, Span<uint> ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = BE_To_UInt32(bs);
                bs = bs[4..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint BE_To_UInt32_Partial(ReadOnlySpan<byte> bs)
        {
            int len = bs.Length;
            Debug.Assert(1 <= len && len <= 4);

            uint result = bs[0];
            for (int i = 1; i < len; ++i)
            {
                result <<= 8;
                result |= bs[i];
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong BE_To_UInt64(ReadOnlySpan<byte> bs)
        {
            return BinaryPrimitives.ReadUInt64BigEndian(bs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void BE_To_UInt64(ReadOnlySpan<byte> bs, Span<ulong> ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = BE_To_UInt64(bs);
                bs = bs[8..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong BE_To_UInt64_Partial(ReadOnlySpan<byte> bs)
        {
            int len = bs.Length;
            Debug.Assert(1 <= len && len <= 8);

            ulong result = bs[0];
            for (int i = 1; i < len; ++i)
            {
                result <<= 8;
                result |= bs[i];
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ushort LE_To_UInt16(ReadOnlySpan<byte> bs)
        {
            return BinaryPrimitives.ReadUInt16LittleEndian(bs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint LE_To_UInt32(ReadOnlySpan<byte> bs)
        {
            return BinaryPrimitives.ReadUInt32LittleEndian(bs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LE_To_UInt32(ReadOnlySpan<byte> bs, Span<uint> ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = LE_To_UInt32(bs);
                bs = bs[4..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong LE_To_UInt64(ReadOnlySpan<byte> bs)
        {
            return BinaryPrimitives.ReadUInt64LittleEndian(bs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LE_To_UInt64(ReadOnlySpan<byte> bs, Span<ulong> ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = LE_To_UInt64(bs);
                bs = bs[8..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt16_To_BE(ushort n, Span<byte> bs)
        {
            BinaryPrimitives.WriteUInt16BigEndian(bs, n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt16_To_LE(ushort n, Span<byte> bs)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(bs, n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt32_To_BE(uint n, Span<byte> bs)
        {
            BinaryPrimitives.WriteUInt32BigEndian(bs, n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt32_To_BE(ReadOnlySpan<uint> ns, Span<byte> bs)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt32_To_BE(ns[i], bs);
                bs = bs[4..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt32_To_LE(uint n, Span<byte> bs)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(bs, n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt32_To_LE(ReadOnlySpan<uint> ns, Span<byte> bs)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt32_To_LE(ns[i], bs);
                bs = bs[4..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt64_To_BE(ulong n, Span<byte> bs)
        {
            BinaryPrimitives.WriteUInt64BigEndian(bs, n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt64_To_BE(ReadOnlySpan<ulong> ns, Span<byte> bs)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt64_To_BE(ns[i], bs);
                bs = bs[8..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt64_To_LE(ulong n, Span<byte> bs)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(bs, n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UInt64_To_LE(ReadOnlySpan<ulong> ns, Span<byte> bs)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt64_To_LE(ns[i], bs);
                bs = bs[8..];
            }
        }
#endif
    }
}
