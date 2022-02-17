using System;
using System.IO;

namespace Task1
{
    class task1
    {
        static string[] stopWords = new string[] { "the", "from", "by", "in", "at", "for", "to", "or", "a", "an",  "any", "of", "is", "and"};

        static void Main(string[] args)
        {
            string fullText = File.ReadAllText(@"E:\path\to\the\file\text.txt");
            int textLength = fullText.Length;
            int outputMaxCount = 10;
            int i = 0; int j = 0; int s = 0;
            bool isStopWord = false;
            string currWord = "";
            string[] words = new string[99999];
            int wordCount = 0;
        get_all_words:
            if ((fullText[i] >= 65) && (fullText[i] <= 90) || 
                (fullText[i] >= 97) && (fullText[i] <= 122) || fullText[i] == 45)// A-Z a-z or '-'
            {
                if ((fullText[i] >= 65) && (fullText[i] <= 90))                
                    currWord += (char)(fullText[i] + 32); // to lower                
                else                
                    currWord += fullText[i];                
            }
            else            
            {
                //checking special symbols and stop words
                s = 0;
                isStopWord = false;
            stop_words_loop:
                if (stopWords[s] == currWord)
                    isStopWord = true;

                s++;
                if (s < stopWords.Length)
                    goto stop_words_loop;


                if (!isStopWord && currWord != "" && currWord != null && currWord != "-" && currWord != "\"" && currWord != "\n" && currWord != "\r" && currWord != "\r\n" && currWord != "\n\r")
                {
                    words[wordCount] = currWord;
                    wordCount++;
                }          
                
                currWord = "";
            }

            i++;

            if (i < textLength)            
                goto get_all_words;
          
            string[] wordsUnique = new string[99999];
            int[] wordsCount = new int[99999];

            i = 0;
            int insertPos = 0;            
            int wordTotal = 0;

        word_count:
            insertPos = 0;            
            j = 0;

        count_start:
            if (j < wordsUnique.Length && wordsUnique[j] != null)
            {
                if (wordsUnique[j] == words[i])
                {
                    insertPos = j;
                    goto count_end;
                }

                j++;
                goto count_start;
            }
        count_end:
            if (insertPos == 0)
            {
                wordsUnique[i - wordTotal] = words[i];
                wordsCount[i - wordTotal] = 1;
            }
            else
            {
                wordsCount[insertPos] += 1;
                wordTotal++;
            }

            i++;
            if (i < words.Length && words[i] != null)
            {
                goto word_count;
            }
            int length = wordsCount.Length;
            j = 0;
            int u = 0;

        sorting1:
            if (j < length && wordsCount[j] != 0)
            {
                u = 0;
            sorting2:
                if (u < length - j - 1 && wordsCount[u] != 0)
                {
                    if (wordsCount[u] < wordsCount[u + 1])
                    {
                        int temp = wordsCount[u];
                        wordsCount[u] = wordsCount[u + 1];
                        wordsCount[u + 1] = temp;
                        string temp2 = wordsUnique[u];
                        wordsUnique[u] = wordsUnique[u + 1];
                        wordsUnique[u + 1] = temp2;
                    }
                    u++;
                    goto sorting2;
                }

                j++;
                goto sorting1;
            }
            int k = 0;
        print:
            if (k < length && wordsUnique[k] != null && k < outputMaxCount)
            {
                Console.WriteLine("{0} - {1}", wordsUnique[k], wordsCount[k]);
                k++;
                goto print;
            }
        }
    }
}
