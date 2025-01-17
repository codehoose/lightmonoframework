﻿using System;
using System.Collections.Generic;

namespace GameFramework.General.Linq
{
    public static class ArrayExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
