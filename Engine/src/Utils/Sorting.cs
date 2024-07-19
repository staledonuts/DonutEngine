namespace Engine.Utils;
using Engine.Utils.Interfaces;
using Engine.Utils.Extensions;
using Raylib_CSharp.Logging;
using Engine.RenderSystems;

#region Radix Sort
internal class RadixSort<T> : ISortingAlgorithm<T>
{
    private readonly int _base;

    internal RadixSort(int radix = 10)
    {
        _base = radix;
    }

    public T[] Sort(IEnumerable<T> elements)
    {
        Logger.TraceLog(TraceLogLevel.Info, "Initiating Radix sort (LSD)");

        T[] array = elements.ToArray();

        Logger.TraceLog(TraceLogLevel.Info, $"Initial array: {array.JoinWithSpace()}");

        if (array is int[] intArray)
        {
            Sort(intArray);
        }

        Logger.TraceLog(TraceLogLevel.Info, $"Final array: {array.JoinWithSpace()}");

        return array;
    }

    private void Sort(int[] array)
    {
        List<int>[] buckets = new List<int>[_base];

        for (int index = 0; index < buckets.Length; ++index)
        {
            buckets[index] = new List<int>();
        }

        int maxValue = array.Max();

        int placeValue;

        for (int iteration = 0; (placeValue = (int)Math.Pow(_base, iteration)) <= maxValue; ++iteration)
        {
            foreach (int element in array)
            {
                buckets[element / placeValue % _base].Add(element);
            }

            Logger.TraceLog(TraceLogLevel.Debug, $"Buckets: {buckets.Select(bucket => bucket.JoinWithSpace()).Join(" | ")}");

            int arrayCopyIndex = 0;

            foreach (List<int> bucket in buckets)
            {
                bucket.CopyTo(array, arrayCopyIndex);
                arrayCopyIndex += bucket.Count;
                bucket.Clear();
            }

            Logger.TraceLog(TraceLogLevel.Debug, $"Pass {iteration + 1}: {array.JoinWithSpace()}");
        }
    }
}
#endregion
