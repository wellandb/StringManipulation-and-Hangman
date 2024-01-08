using System;
using System.Diagnostics.Metrics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman
{
    public class positionOfCharacter
    {
        public string? nameOfWord { get; set; }
        public string? c { get; set; }
        public int pos { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string contents = "";
            List<positionOfCharacter> list = new List<positionOfCharacter>();
            try
            {
                // Get file name.
                // Needed To use ../../../ as it kept opening in the bin/Debug/net7.0 folder instead of a relative folder
                string path = @"../../../Files/Words.txt";
                // Get path name.
                string filename = Path.GetFileName(path);
                // Open the text file using a stream reader. Read into a string
                using (var sr = new StreamReader(path))
                {
                    // Read the stream as a string, and write the string to the console.
                    contents = sr.ReadToEnd();
                }

            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                
            }
            Console.WriteLine("---------");
            //Store each word into an array using split on '\n'
            var array = contents.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            // Generate random number to get a random word from array
            Random rand = new Random();
            int num = rand.Next(0, array.Length-1);
            // Get random word as hangman word, make it lower case and replace line endings with ""
            string hangmanWord = array[num].ToLower().ReplaceLineEndings("");
            // Generate hangman clue as underscores of same length as hangman word
            string hangmanClue = "";
            for(int i=0; i<hangmanWord.Length; i++)
            {
                hangmanClue += "_";
            }
            // String builder to replace underscores with guessed letters
            StringBuilder sb = new StringBuilder(hangmanClue);
            // Boolean correct value stores if word has been fully guessed
            bool correct = false;
            // Int wrong stores incorrect guesses
            int wrong = 0;
            // List stores already guessed letters
            List<string> guessedLetters = new List<string>();
            // Game Loop
            while (!correct & wrong != 7) 
            {
                Console.WriteLine($"Clue: {hangmanClue}");
                Console.Write("What letter do you guess: ");
                // Get player input letter
                string letter = Console.ReadLine().ToLower();
                char[] cLetter = letter.ToCharArray();
                // Check if correct Length
                if(cLetter.Length > 1 | cLetter.Length == 0)
                {
                    Console.WriteLine("Must be a letter. Try Again.");
                    continue;
                }
                // Check if already guessed
                if(guessedLetters.Contains(letter)) 
                { 
                    Console.WriteLine($"Already guessed {letter}. Try Again.");
                    continue;
                }
                // Add to guessed letters if not
                guessedLetters.Add(letter);
                // Loop through word with index
                int index = 0;
                if (hangmanWord.Contains(letter))
                {
                    // Find index where letter is
                    if (hangmanWord.IndexOf(letter) != -1)
                    {
                        while ((index = hangmanWord.IndexOf(letter, index)) != -1)
                        {
                            Console.WriteLine(letter + " found at position " + " " + index);
                            // Replace _ with letter in string builder
                            sb[index] = cLetter[0];
                            // Copy to hangman Clue
                            hangmanClue = sb.ToString();
                            index++;
                            list.Add(new positionOfCharacter() { nameOfWord = hangmanWord, c = letter, pos = index });
                        }
                        // Check whether hangman Clue == hangmanWord
                        if(hangmanClue == hangmanWord)
                        {
                            correct = true;
                        }
                    }
                }
                else
                {
                    // Wrong letter guessed
                    Console.WriteLine("Incorrect try again.");
                    wrong++;
                }

            }
            // End message
            if (correct)
            {
                Console.WriteLine($"Well done the word was {hangmanWord}, you got it with {wrong} wrong :)");
            } else
            {
                Console.WriteLine($"Unlucky the word was {hangmanWord} better luck next time :(");
            }
            Console.ReadLine();

        }
    }
   
}
