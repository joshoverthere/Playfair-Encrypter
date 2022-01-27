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
            new List<string> lettersUsed = new List<string>();

            //format message and key (make lowercase, remove spaces)
            message = message.ToLower();
            message = message.Replace(" ", "");
            key = key.ToLower();
            key = key.Replace(" ", "");


            string nextLetter;

            //loop through rows and columns of the grid
            for (int i = 0; i < 5; i++)
            {
                for (int a = 0; a < 5; a++)
                {
                    if (message.Length > 1)
                    {
                        //need to find a letter that hasn't already been used
                        bool foundLetter


                        nextLetter = message.Substring(0, 1);
                        message = message.Remove(0, 1);
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