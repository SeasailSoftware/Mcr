﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Communication.Utility
{
    public static class Extensions
    {
        public static int IndexOf<T>(this IList<T> list, IList<T> sublist, int start = 0)
        {
            for (int i = start; i <= list.Count - sublist.Count; i++)
            {
                int j = 0;
                while (j < sublist.Count)
                {
                    if (!list[i + j].Equals(sublist[j])) break;
                    if (j < sublist.Count) j++;
                }

                if (j == sublist.Count) return i;
            }

            return -1;
        }
    }
}
