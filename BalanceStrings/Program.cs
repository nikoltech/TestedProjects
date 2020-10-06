namespace BalanceStrings
{
    using System;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    PerformWorkOnStrings();

                    if (IsRequestedExit()) { return; }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR:");
                    Console.WriteLine(ex.Message);

                    if (IsRequestedExit()) { break; }
                }
                finally
                {
                    Console.Clear();
                }
            }
        }

        static bool IsRequestedExit()
        {
            bool isEscape = false;

            Console.WriteLine();
            Console.WriteLine("__________________________________________________");
            Console.WriteLine("Note: press Escape for exit or ANY for continue...");
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Bye bye!!!");
                System.Threading.Thread.Sleep(1300);
                isEscape = true;
            }

            return isEscape;
        }

        // TODO: Make program more dynamic with constraints. Exceptions
        /// <summary>
        /// Entering data & invoke processing
        /// </summary>
        static void PerformWorkOnStrings()
        {
            #region Constraints
            // Constraints
            int strCountConstraint = (int)Math.Pow(10, 2);
            int expressionsLengthConstraint = (int)Math.Pow(10, 5);
            int maxReplacementsLengthConstraint = (int)Math.Pow(10, 5);
            #endregion

            // Perform
            Console.WriteLine("\nEnter number of strings:");
            int numOfStrings;

            bool isNumber = Int32.TryParse(Console.ReadLine(), out numOfStrings);
            if (isNumber)
            {
                if (numOfStrings <= 0 || numOfStrings > strCountConstraint)
                {
                    throw new Exception($"String constraints: Count - {strCountConstraint}");
                }

                string[] strArray = new string[numOfStrings];
                for (int i = 0; i < numOfStrings; i++)
                {
                    Console.WriteLine($"Write a string [{i + 1}]:");
                    strArray[i] = Console.ReadLine();

                    if (strArray[i].Length > expressionsLengthConstraint)
                    {
                        throw new Exception($"String constraints: Length - {expressionsLengthConstraint}");
                    }
                }

                int[] maxReplacementsArray = new int[numOfStrings];
                for (int i = 0; i < numOfStrings; i++)
                {
                    Console.WriteLine($"Write a maxReplacements value for string [{i + 1}]:");

                    bool success = Int32.TryParse(Console.ReadLine(), out maxReplacementsArray[i]);
                    if (!success)
                    {
                        Console.WriteLine("Error: Try again");
                        i--;
                        continue;
                    }

                    if (maxReplacementsArray[i] > maxReplacementsLengthConstraint)
                    {
                        throw new Exception($"maxReplacements constraints: Count - {maxReplacementsLengthConstraint}");
                    }
                }

                // Processing
                int[] result = TryBalanceStrings(numOfStrings, strArray, maxReplacementsArray);

                Console.WriteLine("RESULT:");
                Console.WriteLine(string.Join("\n", result));
            }
            else
            {
                throw new Exception("Not a number!");
            }
        }

        /// <summary>
        /// Check balance array of strings
        /// </summary>
        /// <returns>Array of integers</returns>
        static int[] TryBalanceStrings(int numOfStrings, string[] strArray, int[] maxReplacementsArray)
        {
            int[] result = new int[numOfStrings];

            for (int i = 0; i < numOfStrings; i++)
            {
                result[i] = BalanceString(strArray[i], maxReplacementsArray[i]);
            }

            return result;
        }

        /* <><><>  --1
         * <<>>    --1
         * <<<>>>  --1
         * <>>><<<><> --0
         * <<>><>> --0
         * <><<><>
         * <<>>>   - can replace! Unique position
         * 
         * REPLACEMENT:  To balance a string, we can replace only > character with <> 
         */
         /// <summary>
         /// Algorithm
         /// </summary>
         /// <returns>1 - success, 0 - fail</returns>
        static int BalanceString(string strInput, int maxReplacements)
        {
            int balanced = 1;
            string str = GetArrowsFromString(strInput);

            int openArrowCount = 0;
            bool isIncreasingPattern = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '>')
                {
                    // Replacing in upper level
                    if (openArrowCount == 0 && maxReplacements > 0)
                    {
                        str = str.Insert(i, "<");
                        i++;
                        maxReplacements--;
                    }
                    else if (openArrowCount > 0)
                    {
                        if (isIncreasingPattern)
                        {
                            isIncreasingPattern = false;

                            // Check unique position
                            // <<<<>>>>> - [5]
                            // <<<>>> - [2]
                            if (IsNextCharsEquals(str[i], str, i, openArrowCount) && maxReplacements > 0)
                            {
                                str = str.Insert(i, "<"); // +1 char & length. Insert "<>"
                                i++; // +1 index
                                i = i + openArrowCount; // Next iteration will try get new pattern from next char
                                openArrowCount = 0;
                                maxReplacements--;
                            }
                            else // UnMatch: Next Chars are not Equals current char {>}
                            {
                                balanced = 0;
                                break; 
                            }
                        }
                    }
                    else
                    {
                        balanced = 0;
                        break;
                        // throw new Exception($"Something wrong with algorithm: {openArrowCount}");
                    }
                }
                // start check pattern match
                else if (str[i] == '<')
                {
                    if (openArrowCount == 0)
                    {
                        isIncreasingPattern = true;
                        openArrowCount++;
                    }
                    else if (openArrowCount > 0)
                    {
                        if (isIncreasingPattern)
                        {
                            openArrowCount++;
                        }
                        else // UnMatch: <<><... {<}, expected <<>>... {>}. Can`t correct replace.
                        {
                            balanced = 0;
                            break; 
                        }
                    }
                    else
                    {
                        throw new Exception($"Something wrong with algorithm: {openArrowCount} less than zero");
                    }
                }
            }

            balanced = openArrowCount > 0 ? 0 : balanced;

            return balanced;
        }

        static bool IsNextCharsEquals(char c, string str, int startIndex, int countChars)
        {
            bool isSame = true;
            int end = startIndex + countChars;

            if (end > str.Length) { return false; }

            for (int i = startIndex; i < end && isSame == true; i++)
            {
                isSame = str[i] == c;
            }

            return isSame;
        }

        /// <summary>
        /// Clear string from unnecessary characters
        /// </summary>
        static string GetArrowsFromString(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in str)
            {
                if (item == '<' || item == '>')
                {
                    builder.Append(item);
                }
            }

            return builder.ToString();
        }
    }
}
