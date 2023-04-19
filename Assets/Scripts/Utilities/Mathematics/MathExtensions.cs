using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Mathematics
{
    public static class MathExtensions
    {
        public static float MaxVector3CoordinateOnPlane(this Vector3 vector) =>
            vector.x > vector.y ? vector.x : vector.y;

        public static Vector3 ToVector3(this int angle)
        {
            float radianAngle = angle * Mathf.PI / 180.0f;
            float x = Mathf.Cos(radianAngle);
            float y = Mathf.Sin(radianAngle);
            return new Vector3(x, y, 0);
        }
        public static Vector3 ToVector3(this float angle)
        {
            float radianAngle = angle * Mathf.PI / 180.0f;
            float x = Mathf.Cos(radianAngle);
            float y = Mathf.Sin(radianAngle);
            return new Vector3(x, y, 0);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.Select(x => new { Item = x, Value = selector(x) }).Min().Item;
        }
    }
}