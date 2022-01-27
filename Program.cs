using System;

namespace Playfair
{
    public class Program
    {

        static string[,] createGrid(string message, string key)
        {
            //the letter x is excluded so the alphabet is 25 letters long and can fit in the grid
            List<string> alphabet = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y" };
            string[,] grid = new string[5,5];
            List<string> lettersUsed = new List<string>();

            //format message and key (make lowercase, remove spaces)
            message = message.ToLower();
            message = message.Replace(" ", "");
            key = key.ToLower();
            key = key.Replace(" ", "");


            string nextLetter = "";

            //loop through rows and columns of the grid
            for (int i = 0; i < 5; i++)
            {
                for (int a = 0; a < 5; a++)
                {
                    if (message.Length > 1)
                    {
                        //need to find a letter that hasn't already been used
                        bool foundLetter = false;

                        while (!foundLetter)
                        {
                            Console.WriteLine("searching for a letter");
                            //assume a letter has been found unless proven otherwise
                            foundLetter = true;

                            //make the next letter be the first letter of the remaining message
                            if (message.Length > -1)
                            {
                                Console.WriteLine("wow");
                                nextLetter = message.Substring(0, 1);
                                message = message.Remove(0, 1);
                            }
                            else
                            {
                                nextLetter = alphabet[0];
                                alphabet.RemoveAt(0);
                            }

                            //check if the nextletter matches a letter that has already been used...
                            foreach (string letter in lettersUsed)
                            {
                                if (letter == nextLetter)
                                {
                                    //...and if it does, we didn't actually find a usable next letter!
                                    foundLetter = false;
                                }
                            }
                        }

                        lettersUsed.Add(nextLetter);
                        Console.WriteLine(nextLetter);
                    }
                    else
                    {
                        
                        
                    }
                    
                }
            }

            message = message.Remove(0,1);
            Console.WriteLine(message);

            return grid;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Playfair Encrypter!");
                Console.WriteLine("\nPlease enter your message to encrypt:");
                string message = Console.ReadLine();
                Console.WriteLine("\nPlease enter the encryption key:");
                string key = Console.ReadLine();
                string[,] grid = createGrid(message, key);
                Console.ReadLine();
            }
        }
    }
}