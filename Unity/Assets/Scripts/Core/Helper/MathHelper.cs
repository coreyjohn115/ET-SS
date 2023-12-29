﻿using System;
using System.Runtime.CompilerServices;

namespace ET
{
    public static class MathHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Ceil(this double value)
        {
            return (long)Math.Ceiling(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Ceil(this float value)
        {
            return (long)Math.Ceiling(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Floor(this double value)
        {
            return (long)Math.Floor(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Floor(this float value)
        {
            return (long)Math.Floor(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Ceil(this long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceil(this int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHit(this long value)
        {
            if (value >= 10000)
            {
                return true;
            }

            return value >= RandomGenerator.RandomNumber(1, 10000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHit(this int value)
        {
            if (value >= 10000)
            {
                return true;
            }

            return value >= RandomGenerator.RandomNumber(1, 10000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out int v))
            {
                return v;
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToLong(this string value)
        {
            if (long.TryParse(value, out long v))
            {
                return v;
            }

            return 0;
        }
    }
}