using MyWordle_Game;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;


public class Program()
{

    static void Main()
    {
        Dictionary<int, char> correctCharResults = new Dictionary<int, char>();
        Dictionary<int, char> inCorrectCharResults = new Dictionary<int, char>();
        List<char> correctLetters = new List<char>();
        List<char> usedLetters = new List<char>();
        List<string> WORD_LIST = new List<string>();
        int wordlesSolved = 0;
        int wordlesUnSolved = 0;


        Console.WriteLine("\n");

        LoadWordList();
        Random random = new Random();
        string wordle = WORD_LIST[random.Next(WORD_LIST.Count)];
        MyWordleHome();
        SelectHome(wordle);

        Console.WriteLine(new string('\n', 10));


        void LoadWordList()
        {
            string jsonContent = File.ReadAllText(@"D:\Code\Sea-Solutions Training\Sea-soluiton-console\MyWordle-Game\word.json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<WordEntry> wordEntries = JsonSerializer.Deserialize<List<WordEntry>>(jsonContent, options);
            foreach (var entry in wordEntries)
            {
                WORD_LIST.Add(entry.Word);
            }
        }

        void MyWordleHome()
        {
            Console.WriteLine("\nGAME: MY WORDLE");
            Console.WriteLine("Author: Trinh Vuong");
            Console.WriteLine("This is my own work in the SeaSolutions Training Times.");
            Console.WriteLine("\n-------------------------------------------------------");
            Console.WriteLine("--                  My Wordle!                       --");
            Console.WriteLine("--          Guess the Wordle in 6 tries              --");
            Console.WriteLine("-------------------------------------------------------");
        }

        void SelectHome(string wordle)
        {
            Console.Write("\nWould you like to play My Wordle [y|n]: ");
            string key = Console.ReadLine()?.ToLower();
            if (key == "y")
            {
                Play(wordle);
            }
            else if (key == "n")
            {
                Console.WriteLine("\nNo worries... another time perhaps... :)");
            }
            else
            {
                Console.Write("Invalid input. Please enter 'y' or 'n': ");
                SelectHome(wordle);
            }
        }


        void Play(string wordle)
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
                    Console.Write("5 letter words only please" + " : ");
                    guessString = Console.ReadLine()?.ToLower();
                }
                while (!WORD_LIST.Contains(guessString))
                {
                    Console.WriteLine("Not in word list!");
                    Console.Write("Please enter your guess again - attempt " + i + ": ");
                    guessString = Console.ReadLine()?.ToLower();
                }
                LogicProcess(guessString, wordle, i);
                correctCharResults.Clear();
                inCorrectCharResults.Clear();
                Console.WriteLine("\n");
            }
        }


        void LogicProcess(string guessString, string wordle, int timeTry)
        {
            int guessLength = guessString.Length;
            CompareCorrectResult(guessString, wordle);
            CompareIncorrectPosition(guessString, wordle);

            if (correctCharResults.Count == guessLength)
            {
                wordlesSolved++;
                Console.WriteLine("Solved in " + timeTry + " tries!  Well done! ");
                AskPlayAgain(timeTry);
            }
            else if (timeTry == 6)
            {
                wordlesUnSolved++;
                Console.WriteLine("\nOh no!Better luck next time!");
                Console.WriteLine("The wordle was: " + wordle);
                AskPlayAgain(timeTry);
            }

            Console.WriteLine("\n");
            Console.WriteLine(new string('-', guessLength * 2 + 3));
            Console.WriteLine("| " + string.Join(" ", guessString.ToCharArray()) + " |");
            Console.Write("| ");
            for (int i = 0; i < guessString.Length; i++)
            {
                if (correctCharResults.Count > 0 && correctCharResults.ContainsKey(i))
                {
                    Console.Write("^");
                }
                else if (inCorrectCharResults.Count > 0 && inCorrectCharResults.ContainsKey(i))
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

        void CompareCorrectResult(string guessString, string wordle)
        {
            for (int i = 0; i < wordle.Length; i++)
            {
                if (guessString[i] == wordle[i])
                {
                    correctCharResults.Add(i, guessString[i]);
                    if (!correctLetters.Contains(guessString[i])) correctLetters.Add(guessString[i]);
                }
            }
        }

        void CompareIncorrectPosition(string guessString, string wordle)
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


        void AskPlayAgain(int timeTry)
        {
            Random random = new Random();
            string wordle = WORD_LIST[random.Next(WORD_LIST.Count)];
            Console.Write("\nWould you like to play again [y|n]: ");
            string key = Console.ReadLine()?.ToLower();
            switch (key)
            {
                case "y":
                    correctCharResults.Clear();
                    inCorrectCharResults.Clear();
                    Play(wordle);
                    break;
                case "n":
                    correctCharResults.Clear();
                    inCorrectCharResults.Clear();
                    Summary(timeTry);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    break;
            }
        }

        void Summary(int timeTry)
        {
            if (timeTry > 0)
            {
                Console.WriteLine("\n      My Wordle Summary.     ");
                Console.WriteLine("=================================");
                Console.WriteLine("\nYou played" + timeTry + "games:");
                Console.WriteLine("|--> Number of wordles solved: " + wordlesSolved);
                Console.WriteLine("|--> Number of wordles unsolved: " + wordlesUnSolved);
                Console.WriteLine("\nThanks for playing!");
                wordlesSolved = 0;
                wordlesUnSolved = 0;
            }
        }
    }


}







