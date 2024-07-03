/*namespace Engine.Utils;
using Engine.Utils.Interfaces;
using Engine.Utils.Extensions;
using Raylib_cs;
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
            //Raylib.TraceLog(TraceLogLevel.Info, "Initiating Radix sort (LSD)");

            T[] array = elements.ToArray();

            //Raylib.TraceLog(TraceLogLevel.Info, $"Initial array: {array.JoinWithSpace()}");

            if (array is int[] intArray)
            {
                Sort(intArray);
            }
            else if (array is Rendering2D.IRenderSorting[] renderArray)
            {
                SortFramebuffer(renderArray);
                //SortLayers(renderArray);
            }

            Raylib.TraceLog(TraceLogLevel.Info, $"Final array: {array.JoinWithSpace()}");

            return array;
        }

        private void SortLayers(Rendering2D.IRenderSorting[] array)
        {
            List<Rendering2D.IRenderSorting>[] buckets = new List<Rendering2D.IRenderSorting>[_base];

            for (int index = 0; index < buckets.Length; ++index)
            {
                buckets[index] = new List<Rendering2D.IRenderSorting>();
            }

            int maxValue = array.Max(p => p.Layer);

            int placeValue;

            for (int iteration = 0; (placeValue = (int)Math.Pow(_base, iteration)) <= maxValue; ++iteration)
            {
                foreach (Rendering2D.IRenderSorting element in array)
                {
                    buckets[element.Layer / placeValue % _base].Add(element);
                }

                //Raylib.TraceLog(TraceLogLevel.Debug, $"Buckets: {buckets.Select(bucket => bucket.JoinWithSpace()).Join(" | ")}");

                int arrayCopyIndex = 0;

                foreach (List<Rendering2D.IRenderSorting> bucket in buckets)
                {
                    bucket.CopyTo(array, arrayCopyIndex);
                    arrayCopyIndex += bucket.Count;
                    bucket.Clear();
                }

                //Raylib.TraceLog(TraceLogLevel.Debug, $"Pass {iteration + 1}: {array.JoinWithSpace()}");
            }
        }

        private void SortFramebuffer(Rendering2D.IRenderSorting[] array)
        {
            List<Rendering2D.IRenderSorting>[] buckets = new List<Rendering2D.IRenderSorting>[_base];

            for (int index = 0; index < buckets.Length; ++index)
            {
                buckets[index] = new List<Rendering2D.IRenderSorting>();
            }
            
            int maxValue = array.Max(p => p.Framebuffer);

            int placeValue;

            for (int iteration = 0; (placeValue = (int)Math.Pow(_base, iteration)) <= maxValue; ++iteration)
            {
                foreach (Rendering2D.IRenderSorting element in array)
                {
                    buckets[element.Framebuffer / placeValue % _base].Add(element);
                }

                //Raylib.TraceLog(TraceLogLevel.Debug, $"Buckets: {buckets.Select(bucket => bucket.JoinWithSpace()).Join(" | ")}");

                int arrayCopyIndex = 0;

                foreach (List<Rendering2D.IRenderSorting> bucket in buckets)
                {
                    bucket.CopyTo(array, arrayCopyIndex);
                    arrayCopyIndex += bucket.Count;
                    bucket.Clear();
                }

                //Raylib.TraceLog(TraceLogLevel.Debug, $"Pass {iteration + 1}: {array.JoinWithSpace()}");
            }
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

                //Raylib.TraceLog(TraceLogLevel.Debug, $"Buckets: {buckets.Select(bucket => bucket.JoinWithSpace()).Join(" | ")}");

                int arrayCopyIndex = 0;

                foreach (List<int> bucket in buckets)
                {
                    bucket.CopyTo(array, arrayCopyIndex);
                    arrayCopyIndex += bucket.Count;
                    bucket.Clear();
                }

                //Raylib.TraceLog(TraceLogLevel.Debug, $"Pass {iteration + 1}: {array.JoinWithSpace()}");
            }
        }
    }
#endregion
*/