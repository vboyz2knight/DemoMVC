using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Classes
{
    static class MyRandomizeStringArray
    {
        static Random myRandom = new Random();
        public static T[] RandomizeStrings<T>(T[] array)
        {
            List<KeyValuePair<int, T>> myList = new List<KeyValuePair<int, T>>();

            foreach(T s in array)
            {
                myList.Add(new KeyValuePair<int,T> (myRandom.Next(), s) );
            }

            var sortedList = from item in myList
                             orderby item.Key
                             select item;

            T[] result = new T[array.Length];

            int index = 0;
            foreach (KeyValuePair<int, T> pair in sortedList)
            {
                result[index] = pair.Value;
                index++;
            }

            return result;
        }

        public static T[] RandomizeStringsDurstenfeld<T>(T[] array)
        {
            for(int i=array.Length-1;i>0;i--)
            {
                int j = myRandom.Next(0, i);
                T temp = array[j];

                array[j] = array[i];
                array[i] = temp;
            }

            return array;
        }
    }
}