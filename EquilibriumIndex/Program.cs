using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquilibriumIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Equilibrium Finder Console Application");
            ConsoleKeyInfo loopFlag = new ConsoleKeyInfo();
            while (loopFlag.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Do you know the length of array? (y/n):");
                bool fixedLength = Console.ReadKey(true).KeyChar == 'y';
                if (fixedLength)
                {
                    #region Fixed Size Array
                    Console.Write("Length of array:");
                    string lengthOfArray = Console.ReadLine();
                    int length;
                    bool isValidLength = int.TryParse(lengthOfArray, out length);
                    if (!isValidLength || length < 3)
                    {
                        loopFlag = ShowMessage("error", "Length of array must be a positive integer and bigger than 2");
                        continue;
                    }

                    int[] array = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        Console.Write("Enter an integer for array[{0}]:", i);
                        string entry = Console.ReadLine();
                        bool isValidInt = int.TryParse(entry, out array[i]);
                        while (!isValidInt)
                        {
                            Console.Write("You have to enter an integer for array[{0}]:", i);
                            entry = Console.ReadLine();
                            isValidInt = int.TryParse(entry, out array[i]);
                        }
                    }

                    int index = FindEquilibriumIndex(array);
                    loopFlag = ShowMessage("result", "First equilibrium index of array: " + index);
                    #endregion
                }
                else
                {
                    #region Dynamic Array
                    List<int> array = new List<int>();
                    string entry = "";
                    while (entry != "ok")
                    {
                        Console.Write("if array is completed enter \"ok\" or enter an integer for array[{0}]:", array.Count);
                        entry = Console.ReadLine();
                        if (entry == "ok")
                            continue;

                        int element;
                        bool isValidInt = int.TryParse(entry, out element);
                        while (!isValidInt)
                        {
                            Console.Write("You have to enter an integer for array[{0}]:", array.Count);
                            entry = Console.ReadLine();
                            isValidInt = int.TryParse(entry, out element);
                        }
                        array.Add(element);
                    }

                    if (array.Count < 3)
                    {
                        loopFlag = ShowMessage("error", "Length of array must be bigger than 2");
                        continue;
                    }

                    int index = FindEquilibriumIndex(array.ToArray());
                    loopFlag = ShowMessage("result", "First equilibrium index of array: " + index);
                    #endregion
                }
            }
        }

        private static int FindEquilibriumIndex(int[] array)
        {
            int length = array.Length;
            int lastIteration = length - 1;
            for (int i = 1; i < lastIteration; i++)
            {
                int sumOfLeft = array.Take(i).Sum();
                int sumOfRight = array.Skip(i + 1).Take(lastIteration - i).Sum();
                if (sumOfLeft == sumOfRight)
                    return i;
            }
            return -1;
        }

        private static ConsoleKeyInfo ShowMessage(string messageType = "", string message = "")
        {
            if (message != "")
            {
                if (messageType == "error")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (messageType == "result")
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(messageType + ": " + message);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPlease press any key to restart application or press escape (ESC) to exit.");
            Console.ResetColor();
            return Console.ReadKey();
        }
    }
}
