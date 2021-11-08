﻿using System.Collections.Generic;

namespace Simple.Dotnet.Cloning.Cloners
{
    internal static class RecurringTypesCloner
    {
        public static LinkedList<T> CloneLinkedList<T>(LinkedList<T> linkedList)
        {
            if (linkedList == null) return null;
            if (linkedList.Count == 0) return new();

            var clone = new LinkedList<T>();
            foreach (var element in linkedList) clone.AddLast(RootCloner<T>.DeepClone(element));

            return clone;
        }

        public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) return null;
            if (dictionary.Count == 0) return new();

            var clone = new Dictionary<TKey, TValue>(dictionary.Count, dictionary.Comparer);
            foreach (var element in dictionary) clone.Add(RootCloner<TKey>.DeepClone(element.Key), RootCloner<TValue>.DeepClone(element.Value));

            return clone;
        }

        public static SortedDictionary<TKey, TValue> CloneSortedDictionary<TKey, TValue>(SortedDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) return null;
            if (dictionary.Count == 0) return new();

            var clone = new SortedDictionary<TKey, TValue>(dictionary.Comparer);
            foreach (var element in dictionary) clone.Add(RootCloner<TKey>.DeepClone(element.Key), RootCloner<TValue>.DeepClone(element.Value));

            return clone;
        }
    }
}
