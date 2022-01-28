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
                        if (key.Length > 0)
                        {
                            //take the first letter of the message (and remove it for future cycles)
                            nextLetter = key.Substring(0, 1);
                            key = key.Remove(0, 1);

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
                    digram = digram.Substring(0, 1) + "q";
                }
                digrams.Add(digram);
            }

            return digrams;
        }

        static List<int> getCoordinates(string letter, string[,] grid)
        {
            List<int> coordinates = new List<int>();

            //loop through rows and columns
            for (int i = 0; i < 5; i++)
            {
                for (int a = 0; a < 5; a++)
                {
                    if (grid[i,a] == letter)
                    {
                        coordinates.Add(i);
                        coordinates.Add(a);
                    }
                }
            }

            return coordinates;

        }

        static string encodeDigram(string digram, string[,] grid)
        {
            string encodedDigram = "";
            Console.WriteLine("Encoding digram: " + digram.Substring(0,1) + digram.Substring(1,1));
            List<int> coordsLetter1 = new List<int>();
            coordsLetter1 = getCoordinates(digram.Substring(0,1), grid);
            List<int> coordsLetter2 = new List<int>();
            coordsLetter2 = getCoordinates(digram.Substring(1,1), grid);

            //"If the letters appear on the same row of your table, replace them with the letters to their immediate right respectively
            //(wrapping around to the left side of the row if a letter in the original pair was on the right side of the row)."
            if (coordsLetter1[0] == coordsLetter2[0])
            {
                Console.WriteLine("they're on the same row");
                Console.WriteLine("letter 1");
                //replace letter 1 with letter to its right (if its on the end, wrap around)
                if (coordsLetter1[1] == 4)
                {
                    encodedDigram += grid[coordsLetter1[0], 0];
                }
                else
                {
                    encodedDigram += grid[coordsLetter1[0], coordsLetter1[1] + 1];
                }

                Console.WriteLine("letter 2");
                //replace letter 2 with letter to its right (if its on the end, wrap around)
                if (coordsLetter2[1] == 4)
                {
                    Console.WriteLine("its on the end");
                    encodedDigram += grid[coordsLetter2[0], 0];
                }
                else
                {
                    encodedDigram += grid[coordsLetter2[0], coordsLetter2[1] + 1];
                }
            }
            //"If the letters appear on the same column of your table, replace them with the letters immediately below respectively
            //(wrapping around to the top side of the column if a letter in the original pair was on the bottom side of the column)."
            else if(coordsLetter1[1] == coordsLetter2[1])
            {
                //replace letter1 with letter below it (if letter is on bottom, wrap around)
                if (coordsLetter1[0] == 4)
                {
                    Console.WriteLine("its at the bottom");
                    encodedDigram += grid[0, coordsLetter1[1]];
                }
                else
                {
                    encodedDigram += grid[coordsLetter1[0] + 1, coordsLetter1[1]];
                }

                //replace letter2 with letter below it (if letter is on bottom, wrap around) 
                if (coordsLetter2[0] == 4)
                {
                    Console.WriteLine("its at the bottom");
                    encodedDigram += grid[0, coordsLetter2[1]];
                }
                else
                {
                    encodedDigram += grid[coordsLetter2[0] + 1, coordsLetter2[1]];
                }
            }
            else
            {
                //replace letter 1 with letter on the same row but in the column of letter2
                encodedDigram += grid[coordsLetter1[0], coordsLetter2[1]];

                //replace letter 2 with letter on the same row but in the column of letter1
                encodedDigram += grid[coordsLetter2[0], coordsLetter1[1]];
            }

            Console.WriteLine(encodedDigram);

            return encodedDigram;
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

                //encode each digram
                string encodedMessage = "";
                foreach (string digram in digrams)
                {
                    string encodedDigram = encodeDigram(digram, grid);
                    encodedMessage += encodedDigram;
                }

                Console.WriteLine(encodedMessage);

                

                //wait
                Console.ReadLine();
            }
        }
    }
}