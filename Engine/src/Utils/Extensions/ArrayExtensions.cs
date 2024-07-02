namespace Engine.Utils.Extensions;
using System;

internal static class ArrayExtensions
{
    internal static void Swap<T>(this T[] array, int indexFirst, int indexSecond)
    {
        T temporary = array[indexFirst];
        array[indexFirst] = array[indexSecond];
        array[indexSecond] = temporary;
    }

    internal static void ShiftElement<T>(this T[] array, int oldIndex, int newIndex)
    {
        T temporary = array[oldIndex];

        if (newIndex < oldIndex)
        {
            // Need to move part of the array "up" to make room
            Array.Copy(array, newIndex, array, newIndex + 1, oldIndex - newIndex);
        }
        else
        {
            // Need to move part of the array "down" to fill the gap
            Array.Copy(array, oldIndex + 1, array, oldIndex, newIndex - oldIndex);
        }

        array[newIndex] = temporary;
    }
}