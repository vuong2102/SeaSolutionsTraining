﻿using MyWordle_Game;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;


public class Program()
{
    static Dictionary<int, char> correctCharResults = new Dictionary<int, char>();
    static Dictionary<int, char> inCorrectCharResults = new Dictionary<int, char>();
    static List<char> correctLetters = new List<char>();
    static List<char> usedLetters = new List<char>();

    static void Main()
    {
        Console.WriteLine("\n");

        string wordle = RanDumWordle();
        MyWordleHome();
        SelectHome(wordle);


        Console.WriteLine(new string('\n', 10));
    }

    static string RanDumWordle()
    {
        string jsonContent = File.ReadAllText(@"D:\Code\Sea-Solutions Training\Sea-soluiton-console\MyWordle-Game\word.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        List<WordEntry> wordEntries = JsonSerializer.Deserialize<List<WordEntry>>(jsonContent, options);
        List<string> WORD_LIST = new List<string>();
        foreach (var entry in wordEntries)
        {
            WORD_LIST.Add(entry.Word);
        }

        Random random = new Random();
        return WORD_LIST[random.Next(WORD_LIST.Count)];
    }

    static void MyWordleHome()
    {
        Console.WriteLine("\nGAME: MY WORDLE");
        Console.WriteLine("Author: Trinh Vuong");
        Console.WriteLine("This is my own work in the SeaSolutions Training Times.");
        Console.WriteLine("\n-------------------------------------------------------");
        Console.WriteLine("--                  My Wordle!                       --");
        Console.WriteLine("--          Guess the Wordle in 6 tries              --");
        Console.WriteLine("-------------------------------------------------------");
    }

    static void SelectHome(string wordle)
    {
        Console.Write("\nWould you like to play My Wordle [y|n]: ");
        string key = Console.ReadLine()?.ToLower();
        switch (key)
        {
            case "y":
                Play(wordle);
                break;
            case "n":
                Console.WriteLine("\nNo worries... another time perhaps... :)");
                break;
            default:
                Console.Write("Invalid input. Please enter 'y' or 'n': ");
                while (key != "y" && key != "n")
                {
                    key = Console.ReadLine()?.ToLower();
                }
                if(key == "y") Play(wordle);
                else
                {
                    Console.WriteLine("\nNo worries... another time perhaps... :)");
                }
                break;
        }
    }

    static void Play(string wordle)
    {
        Console.WriteLine(wordle);
        int wordleLength = wordle.Length;
        for (int i = 1; i <= 6; i++)
        {
            Console.WriteLine(new string('-', wordleLength * 2 + 3));
            Console.WriteLine("| " + string.Join(" ", new string[wordleLength].Select(_ => "-")) + " |");
            Console.Write("Please enter your guess - attempt " + i + ": ");
            string guessString = Console.ReadLine()?.ToLower();
            while (guessString != null && guessString.Length != wordleLength)
            {
                Console.Write("The length of your guess does not match the Wordle word length (" + wordleLength + "). Please try again - attempt" + i + " : ");
                guessString = Console.ReadLine()?.ToLower();
            }
            LogicProcess(guessString, wordle, i);
            correctCharResults.Clear();
            inCorrectCharResults.Clear();
            Console.WriteLine("\n");
        }
    }

    static void LogicProcess(string guessString, string wordle, int timeTry)
    {
        int guessLength = guessString.Length;
        CompareCorrectResult(guessString, wordle);
        CompareIncorrectPosition(guessString, wordle);

        if (correctCharResults.Count == guessLength)
        {
            Console.WriteLine("Solved in " + timeTry + " tries!  Well done! ");
            AskPlayAgain(timeTry);
        }
        else if (timeTry == 6)
        {
            Console.WriteLine("\nOh no!Better luck next time!");
            Console.WriteLine("The wordle was: " + wordle);
            AskPlayAgain(timeTry);
        }

        Console.WriteLine("\n");
        Console.WriteLine(new string('-', guessLength * 2 + 3));
        Console.WriteLine("| " + string.Join(" ", guessString.ToCharArray()) + " |");
        Console.Write("| ");
        for(int i = 0; i < guessString.Length; i++)
        {
            if(correctCharResults.Count > 0 && correctCharResults.ContainsKey(i))
            {
                Console.Write("^");
            }
            else if(inCorrectCharResults.Count > 0 && inCorrectCharResults.ContainsKey(i))
            {
                Console.Write("*");
            }
            else Console.Write("-");
            Console.Write(" ");
        }
        Console.WriteLine("|");
        Console.WriteLine("|");
        Console.WriteLine("| Correct spot(^): " + correctCharResults.Count());
        Console.WriteLine("| Wrong spot(*): " + inCorrectCharResults.Count());
        Console.WriteLine("|");
        Console.WriteLine("| Correct letters(^): " + string.Join(" ", correctLetters));
        Console.WriteLine("| Used letters: " + string.Join(" ", usedLetters));
    }

    static void CompareCorrectResult(string guessString, string wordle)
    {
        for (int i = 0; i < wordle.Length; i++)
        {
            if (guessString[i] == wordle[i])
            {
                correctCharResults.Add(i, guessString[i]);
                if(!correctLetters.Contains(guessString[i])) correctLetters.Add(guessString[i]);
            }
        }
    }

    static void CompareIncorrectPosition(string guessString, string wordle)
    {
        var positionCorrect = correctCharResults?.Select(c => c.Key).ToList() ?? new List<int>();

        char[] modifiedWordle = wordle.ToCharArray();
        foreach (int pos in positionCorrect)
        {
            modifiedWordle[pos] = '\0';
        }

        for (int i = 0; i < guessString.Length; i++)
        {
            if (!positionCorrect.Contains(i))
            {
                if (modifiedWordle.Contains(guessString[i]) && guessString[i] != wordle[i])
                {
                    inCorrectCharResults.Add(i, guessString[i]);
                    int index = Array.IndexOf(modifiedWordle, guessString[i]);
                    if (index >= 0)
                    {
                        modifiedWordle[index] = '\0';
                    }
                }
            }
            if (!wordle.Contains(guessString[i]) && !usedLetters.Contains(guessString[i]))
            {
                usedLetters.Add(guessString[i]);
            }
        }

    }


    static void AskPlayAgain(int timeTry)
    {
        string wordle = RanDumWordle();
        Console.Write("\nWould you like to play again [y|n]: ");
        string key = Console.ReadLine()?.ToLower();
        switch (key)
        {
            case "y":
                Play(wordle);
                correctCharResults.Clear();
                inCorrectCharResults.Clear();
                break;
            case "n":
                Summary(timeTry);
                break;
            default:
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                break;
        }
    }

    static void Summary(int timeTry)
    {
        if(timeTry > 0)
        {
            Console.WriteLine("\n      My Wordle Summary.     ");
            Console.WriteLine("=================================");
            Console.WriteLine("\nYou played" + timeTry + "games:");
            Console.WriteLine("|--> Number of wordles solved: 1");
            Console.WriteLine("|--> Number of wordles unsolved: 1");
            Console.WriteLine("\nThanks for playing!");
        }
    }
}







