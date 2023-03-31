﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutomaticParking.Common.Extensions
{
    public static class RandomExtensions
    {
        public static Vector3 RandomizeHorizontalPosition(this Vector3 position, float maxOffset) =>
            position + new Vector3(Random.Range(-maxOffset, maxOffset), default, Random.Range(-maxOffset, maxOffset));

        public static Quaternion RandomizeVerticalRotation(this Quaternion rotation, float maxAngleOffset) =>
            rotation * Quaternion.Euler(default, Random.Range(-maxAngleOffset, maxAngleOffset), default);

        public static T RandomItem<T>(this ICollection<T> source)
        {
            source.ThrowExceptionIfArgumentIsNull(nameof(source)).ThrowExceptionIfNoElements();
            return source.ElementAt(source.RandomIndex());
        }

        public static int NextRandomIndex<T>(this ICollection<T> source, int currentIndex)
        {
            currentIndex.ThrowExceptionIfArgumentOutOfRange(nameof(currentIndex), source);
            return Random.Range(currentIndex, source.Count);
        }

        public static int RandomIndex<T>(this ICollection<T> source)
        {
            source.ThrowExceptionIfArgumentIsNull(nameof(source)).ThrowExceptionIfNoElements();
            return Random.Range(default, source.Count);
        }

        public static IEnumerable<T> PickRandomItems<T>(this IEnumerable<T> source, int count) =>
            source.ToArray().Shuffle(count).Take(count);

        public static IList<T> Shuffle<T>(this IList<T> source, int count)
        {
            for (var i = 0; i < count; i++)
                source.SwapItemWithNextRandom(i);
            return source;
        }

        public static IList<T> SwapItemWithNextRandom<T>(this IList<T> source, int itemIndex) =>
            source.SwapItems(itemIndex, source.NextRandomIndex(itemIndex));
    }
}