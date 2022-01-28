using System;

namespace Playfair
{
    public class Program
    {

        static string[,] createGrid(string message, string key)
        {
            //the letter x is excluded so the alphabet is 25 letters long and can fit in the grid
            List<string> alphabet = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "z" };
            string[,] grid = new string[5,5];
            List<string> lettersUsed = new List<string>();

            //format message and key (make lowercase, remove spaces)
            message = message.ToLower();
            message = message.Replace(" ", "");
            key = key.ToLower();
            key = key.Replace(" ", "");

            //loop through rows and columns of the grid
            for (int i = 0; i < 5; i++)
            {
                for (int a = 0; a < 5; a++)
                {
                    bool letterFound = false;
                    string nextLetter = "";

                    //continue until a valid letter is found
                    while (!letterFound)
                    {
                        //assume a valid letter has been found unless proven otherwise
                        letterFound = true;

                        //if there are still parts of the message that haven't been added to the grid yet
                        if (message.Length > 0)
                        {
                            //take the first letter of the message (and remove it for future cycles)
                            nextLetter = message.Substring(0, 1);
                            message = message.Remove(0, 1);

                            //if that letter is already in the grid then the letter is invalid, if not then add that letter to the list of used letters
                            if (lettersUsed.Contains(nextLetter))
                            {
                                letterFound = false;
                            }
                            else
                            {
                                lettersUsed.Add(nextLetter);
                            }
                        }
                        //if there is no message left, use the rest of the alphabet
                        else
                        {
                            //take the first letter of what remains of the alphabet (and remove it for future cycles)
                            nextLetter = alphabet[0];
                            alphabet.RemoveAt(0);

                            //if that letter has already been used in the grid then it's invalid, otherwise add it to list of letters used
                            if (lettersUsed.Contains(nextLetter))
                            {
                                letterFound = false;
                            }
                            else
                            {
                                lettersUsed.Add(nextLetter);
                            }
                        }
                    }

                    //assign letter to grid
                    grid[i,a] = nextLetter;    
                }
            }
            return grid;
        }

        static List<string> splitDigrams(string message)
        {
            List<string> digrams = new List<string>();

            //properly format message
            message = message.ToLower();
            message = message.Replace(" ", "");

            //if message is an odd length, add an x 
            if (message.Length % 2 != 0)
            {
                message += "x";
            }

            //remove 2 letters of the message at a time and add those two letters into the digrams list until the message is gone
            while (message.Length > 0)
            {
                string digram = message.Substring(0, 2);
                message = message.Remove(0, 2);

                //if the two letters are the same make the second to be an x
                if (digram.Substring(0, 1) == digram.Substring(1, 1))
                {
                    digram = digram.Substring(0, 1) + "x";
                }
                digrams.Add(digram);
            }

            return digrams;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Playfair Encrypter!");

                //read message and encryption key
                Console.WriteLine("\nPlease enter your message to encrypt:");
                string message = Console.ReadLine();
                Console.WriteLine("\nPlease enter the encryption key:");
                string key = Console.ReadLine();

                //generate key grid
                string[,] grid = createGrid(message, key);

                //get list of digrams
                List<string> digrams = new List<string>();
                digrams = splitDigrams(message);


                //wait
                Console.ReadLine();
            }
        }
    }
}