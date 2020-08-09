using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSFromTXT
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\Delete\sdfaa\checkout.txt";
            string savePath = @"D:\Delete\sdfaa\Processed.txt";

            string[] allLines = File.ReadAllLines(filePath).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            List<string> allProcesList = GetFormatedLines(allLines);

            File.WriteAllLines(savePath, allProcesList);

        }


        private static List<string> GetFormatedLines(string[] allLineText)
        {
            List<string> processedLines = new List<string>();

            for (int i = 0; i < allLineText.Length; i++)
            {
                if (allLineText[i].StartsWith("$"))
                {
                    for (int j = i + 1; j < allLineText.Length && !allLineText[j].StartsWith("$"); j++)
                    {
                        processedLines.Add(GetFormatString($"{allLineText[i]}/{allLineText[j]}", ", "));
                    }
                }
            }

            return processedLines;
        }

        private static string GetFormatString(string rawStr, string seperator)
        {
            StringBuilder formatedString = new StringBuilder();

            int whiteSpaceCounter = 0;
            char? previousChar = null;
            foreach (char eachLetter in rawStr)
            {
                if (string.IsNullOrWhiteSpace(eachLetter.ToString()))
                {
                    whiteSpaceCounter++;
                }

                //if(whiteSpaceCounter > 2)
                //{
                //    continue;
                //}

                if (previousChar != null && string.IsNullOrWhiteSpace(previousChar.ToString()) && !string.IsNullOrWhiteSpace(eachLetter.ToString()))
                {
                    formatedString.Append(previousChar);
                }

                if (!string.IsNullOrWhiteSpace(eachLetter.ToString()))
                {
                    if (formatedString.Length > 0 && whiteSpaceCounter > 1)
                    {
                        formatedString.Append(seperator);
                    }

                    formatedString.Append(eachLetter);
                    whiteSpaceCounter = 0;
                }

                previousChar = eachLetter;
            }


            return formatedString.ToString();
        }
    }
}
