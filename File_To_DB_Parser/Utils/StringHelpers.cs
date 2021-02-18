using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_To_DB_Parser.Utils
{
    public class StringHelpers
    {
        public static List<int> AllIndexesOf(string str, string substr)
        {
            var indexes = new List<int>();
            if (!string.IsNullOrWhiteSpace(str) || !string.IsNullOrWhiteSpace(substr))
            {
                int index = 0;

                while ((index = str.IndexOf(substr, index)) != -1)
                {
                    indexes.Add(index++);
                }
            }

            return indexes;
        }

        public static List<string> SplitByIndexes(string line, int[] indexes)
        {
            List<string> parts = new List<string>();

            for (int i = -1, m = indexes.Length - 1; i < indexes.Length; i++)
            {
                int from = i < 0 ? 0 : indexes[i];
                int to = i == m ? line.Length : indexes[i + 1];
                string part = line.Substring(from, to - from).Replace("!", "").Trim();
                if (part != "")
                {
                    parts.Add(part);
                }

            }
            return parts;
        }
    }
}