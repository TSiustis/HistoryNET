using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Helper
{
    public static class ExtensionMethods
    {
        public static void Populate(this int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
        }
    }
}
