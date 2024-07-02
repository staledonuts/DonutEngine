namespace Engine.Utils.Interfaces;
using System.Collections.Generic;

internal interface ISortingAlgorithm<T>
{
    T[] Sort(IEnumerable<T> elements);
}