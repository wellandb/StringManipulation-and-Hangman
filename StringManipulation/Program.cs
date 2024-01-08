using Microsoft.VisualBasic;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace StringManipulation
{
    class Program
    {
        public class positionOfCharacter
        {
            public string? nameOfWord { get; set; }
            public string? c { get; set; }
            public int pos { get; set; }
        }
        static void Main(string[] args)
        {
                string contents = "";
                List<positionOfCharacter> list = new List<positionOfCharacter>();
                try
                {
                    // Get file name.
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
                    Console.WriteLine( "---------" );
                //Store each word into an array using split on '\n'
                var array = contents.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                string letterToSearch ="o";
                int count = 1;
                foreach (var word in array) 
                {
                    string letter = letterToSearch.ToLower();
                    string wordToSearch = word.ToLower();
                    int index = 0;
                    if(wordToSearch.Contains(letter))
                    {
                        // Find index where letter is
                        if(wordToSearch.IndexOf(letter) != -1)
                        {
                            Console.WriteLine($"{count}: {wordToSearch}");
                            while ((index = wordToSearch.IndexOf(letter, index)) !=-1)
                            {
                                Console.WriteLine(letter + " found at position " + " " + index);
                                index++;
                                list.Add( new positionOfCharacter(){nameOfWord = wordToSearch, c = letter, pos = index});
                            }
                        }
                    }
                    Console.WriteLine("");
                    count++;
                }
                foreach(var li in list)
                {
                // Console.WriteLine($"word: {li.nameOfWord} letter: {li.c} position: { li.pos} ");
                }
                Console.ReadLine();
   
        }
    }
}
