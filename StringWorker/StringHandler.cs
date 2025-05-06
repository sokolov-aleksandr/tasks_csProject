using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasks_csProject.StringWorker
{
    public class StringHandler
    {
        public StringHandler() { }

        public string ProcessString(string str)
        {
            string result = "";
            
            // Если чётная длинна
            if (str.Length % 2 == 0)
            {
                int half = str.Length / 2;
                string firstHalf = ReverseString(str.Substring(0, half));
                string secondHalf = ReverseString(str.Substring(half));
                result = firstHalf + secondHalf;
            }
            else // Если НЕчётная длинна
            {
                result = ReverseString(str) + str;
            }

            return result;
        }

        private string ReverseString(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
