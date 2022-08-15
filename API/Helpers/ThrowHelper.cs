﻿namespace API.Helpers
{
    public static class ThrowHelper
    {
        /// <summary>
        /// Throw <see cref="NullReferenceException"/> if obj parameter is null
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NullReferenceException"></exception>
        public static void ThrowIfNull<T>(object obj, Func<T> func = null)
        {
            if (obj == null)
                throw new NullReferenceException(nameof(obj));
            
            if (func != null)
                func();
        }
    }
}