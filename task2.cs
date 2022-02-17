using System;
using System.Collections.Generic;
using System.IO;

namespace Task2
{
    class task2
    {
        static string[] stopWords = new string[] { "the", "from", "by", "in", "at", "for", "to", "or", "a", "an", "any", "of", "is", "and" };

        static void Main(string[] args)
        {
            string fullText = File.ReadAllText(@"E:\path\to\the\file\text.txt");
            int i = 0; int j = 0; int k = 0; int s = 0;
            bool isStopWord = false;
            string currWord  = "";
            string[] words = new string[99999];
            string[,] pagesWords = new string[9999, 9999];
            int wordCount = 0;
            int rowCount = 0;
            int pageCount = 0;
            int pageWordCounter = 0;
        get_all_words:
            if ((fullText[i] >= 65) && (fullText[i] <= 90) || (fullText[i] >= 97) && (fullText[i] <= 122) 
                || fullText[i] == 45 || fullText[i] == 234 || fullText[i] == 225 || fullText[i] == 224)
            {
                if ((fullText[i] >= 65) && (fullText[i] <= 90))
                    currWord  += (char)(fullText[i] + 32);                
                else
                    currWord  += fullText[i];
            }
            else
            {
                if (fullText[i] == '\n')
                    rowCount++;
                if (rowCount > 45)
                {
                    pageCount++;
                    pageWordCounter = 0;
                    rowCount = 0;
                }
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

                    words[wordCount] = currWord ;
                    wordCount++;
                    pagesWords[pageCount, pageWordCounter] = currWord ;
                    pageWordCounter++;
                }
                currWord  = "";
            }
            i++;
            if (i < fullText.Length)
                goto get_all_words;
            else
            {
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
                    words[wordCount] = currWord ;
                    wordCount++;
                }
            }
            string[] wordsUnique  = new string[99999];
            int[] wordsCount = new int[99999];            
            i = 0;
            int insertPos = 0;            
            int wordTotal = 0;

        word_count:
            insertPos = 0;
            int current_length = wordsUnique .Length;
            j = 0;
        count_start:
            if (j < current_length && wordsUnique [j] != null)
            {
                if (wordsUnique [j] == words[i])
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
                wordsUnique [i - wordTotal ] = words[i];
                wordsCount[i - wordTotal ] = 1;
            }
            else
            {
                wordsCount[insertPos] += 1;
                wordTotal ++;
            }
            i++;
            if (i < words.Length && words[i] != null)
                goto word_count;

            int length = wordsCount.Length;
            
            string[] wordsLessThan100 = new string[99999];
            int lastInsert = 0;
        words_less_100:
            if (k < length && wordsUnique [k] != null)
            {
                if (wordsCount[k] <= 100)
                {
                    wordsLessThan100[lastInsert] = wordsUnique[k];
                    lastInsert++;
                }
                k++;
                goto words_less_100;
            }
            int write = 0;
            int sort = 0;
            bool toSwapWords = false;
            int counter = 0;
            int curWordLength = 0;
            int nextWordlength = 0;
        sorting1:
            if (write < wordsLessThan100.Length && wordsLessThan100[write] != null)
            {
                sort = 0;
            sorting2:
                if (sort < wordsLessThan100.Length - write - 1 && wordsLessThan100[sort + 1] != null)
                {
                    curWordLength = wordsLessThan100[sort].Length;
                    nextWordlength = wordsLessThan100[sort + 1].Length;

                    int compareLength = curWordLength > nextWordlength ? nextWordlength : curWordLength;

                    toSwapWords = false;
                    counter = 0;
                alphabet_start:

                    if (wordsLessThan100[sort][counter] > wordsLessThan100[sort + 1][counter])
                    {
                        toSwapWords = true;
                        goto alphabet_end;
                    }
                    if (wordsLessThan100[sort][counter] < wordsLessThan100[sort + 1][counter])
                    {
                        goto alphabet_end;
                    }
                    counter++;
                    if (counter < compareLength)
                    {
                        goto alphabet_start;
                    }
                alphabet_end:
                    if (toSwapWords)
                    {
                        string temp = wordsLessThan100[sort];
                        wordsLessThan100[sort] = wordsLessThan100[sort + 1];
                        wordsLessThan100[sort + 1] = temp;
                    }
                    sort++;
                    goto sorting2;
                }
                write++;
                goto sorting1;
            }
            k = 0;

        print:
            if (k < wordsLessThan100.Length && wordsLessThan100[k] != null)
            {
                Console.Write("{0} - ", wordsLessThan100[k]);
                int first = 0;
                int second = 0;
                int[] wordPages = new int[100];
                int pageInsert = 0;

            check_page:
                if (first < 10000 && pagesWords[first, 0] != null)
                {
                    second = 0;
                check_page_word:
                    if (second < 10000 && pagesWords[first, second] != null)
                    {
                        if (pagesWords[first, second] == wordsLessThan100[k])
                        {
                            wordPages[pageInsert] = first + 1;
                            pageInsert++;
                            first++;
                            goto check_page;
                        }
                        second++;
                        goto check_page_word;
                    }

                    first++;
                    goto check_page;
                }
                int counter_ = 0;
            pagination:
                if (counter_ < 100 && wordPages[counter_] != 0)
                {
                    if (counter_ != 99 && wordPages[counter_ + 1] != 0)
                    {
                        Console.Write("{0}, ", wordPages[counter_]);
                    }
                    else
                    {
                        Console.Write("{0}", wordPages[counter_]);
                    }
                    counter_++;
                    goto pagination;
                }
                Console.WriteLine();
                k++;
                goto print;
            }
        }
    }
}
