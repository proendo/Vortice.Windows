// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing two 32 bit signed integer components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}")]
public struct Int2 : IEquatable<Int2>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public int X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public int Y;

    internal const int Count = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="Int2"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Int2(int value) : this(value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int2" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    public Int2(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int2" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Int2(ReadOnlySpan<int> values)
    {
        if (values.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 2 uint values.");
        }

        this = Unsafe.ReadUnaligned<Int2>(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int2" /> struct.
    /// </summary>
    /// <param name="value"><see cref="Double2"/> containing X and Y components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int2(Double2 value)
    {
        X = (int)value.X;
        Y = (int)value.Y;
    }

    /// <summary>
    /// A <see cref="Int2"/> with all of its components set to zero.
    /// </summary>
    public static Int2 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Int2"/> (1, 0).
    /// </summary>
    public static Int2 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Int2"/> (0, 1).
    /// </summary>
    public static Int2 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1);
    }

    /// <summary>
    /// A <see cref="Int2"/> with all of its components set to one.
    /// </summary>
    public static Int2 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1);
    }

    public int this[int index]
    {
        get => GetElement(this, index);
        set => this = WithElement(this, index, value);
    }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(int[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(int[] array, int index)
    {
        if (array is null)
        {
            throw new NullReferenceException(nameof(array));
        }

        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = X;
        array[index + 1] = Y;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<int> destination)
    {
        if (destination.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Int32}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<int> destination)
    {
        if (destination.Length < 2)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);

        return true;
    }

    /// <summary>
    /// Performs an explicit conversion from <see cref="Int2" /> to <see cref="Vector2"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Vector2(Int2 value) => new(value.X, value.Y);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Double2" /> to <see cref="Int2"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Int2(Double2 value) { return new Int2(value); }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Int2 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Int2"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Int2"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int2 other)
    {
        return X == other.X
            && Y == other.Y;
    }

    /// <summary>
    /// Compares two <see cref="Int2"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Int2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Int2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int2 left, Int2 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Int2"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Int2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Int2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int2 left, Int2 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Int2)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)} }}";

    internal static int GetElement(Int2 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetElementUnsafe(ref Int2 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Int2, int>(ref vector), index);
    }

    /// <summary>Sets the element at the specified index.</summary>
    /// <param name="vector">The vector of the element to get.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value of the element to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Int2 WithElement(Int2 vector, int index, int value)
    {
        if ((uint)index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Int2 result = vector;
        SetElementUnsafe(ref result, index, value);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void SetElementUnsafe(ref Int2 vector, int index, int value)
    {
        Debug.Assert(index is >= 0 and < Count);

        Unsafe.Add(ref Unsafe.As<Int2, int>(ref vector), index) = value;
    }
}
