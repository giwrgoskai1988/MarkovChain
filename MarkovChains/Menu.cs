using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MarkovChains
{
    class Menu
    {
        bool GotTrained = false;
        public Dictionary<string , List<string>> wordPairs = new Dictionary<string, List<string>>();
        public string PathInput { get; private set; }


        public Menu()
        {
            Run();
        }

        void Run()
        {
            Console.WriteLine("Please enter a proper folder path to train me or press Enter to quit: ");

            PathInput = @"C:\Users\Α\source\repos\MyGit\MarkovChain\";

            while (PathInput == "" || !GotTrained)
            { 
                if (PathExists())
                {
                    Train();
                    GotTrained = true;
                }
                else
                {
                    Console.WriteLine("Path is wrong! Enter proper path or press Enter to quit! : ");
                    PathInput = Console.ReadLine();
                }           

            }
        }


        bool PathExists()
        {
            return Directory.Exists(PathInput);
        }


        void Train()
        {
            string[] files = Directory.GetFiles(PathInput);

            foreach (var file in files)
            {
                StreamReader sr = new StreamReader(new BufferedStream(new FileStream(file, FileMode.Open)));
                string line = "";
                while((line = sr.ReadLine())!= null)
                {
                    string[] words = line.Split(' ');

                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        if(wordPairs.ContainsKey(words[i].ToLower()))
                        {
                            wordPairs[words[i].ToLower()].Add(words[i + 1].ToLower());
                        }
                        else
                        {
                            wordPairs.Add(words[i].ToLower(), new List<string> { words[i + 1].ToLower() });
                        }
                    }                 
                }
                sr.Close();
            }
        }


        public void RandomText()
        {
            Random rnd = new Random();
            Console.WriteLine("Enter text and press space to get a random word! Or press Escape anytime to quit!");
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo input;
            bool flag = true;
            while(flag)
            { 
                while((input = Console.ReadKey()).KeyChar !=' ')
                {
                    if(input.KeyChar == 27)
                    {
                        flag = false;
                        break;
                    }
                    sb.Append(input.KeyChar);
                }

                if(wordPairs.ContainsKey(sb.ToString().ToLower()))
                {
                    string output = "";
                    output = wordPairs[sb.ToString().ToLower()][rnd.Next(wordPairs[sb.ToString().ToLower()].Count)];
                    Console.Write(output + " ");
                    
                }
                sb.Clear();

            }
        
           

     


            //ConsoleKeyInfo input = Console.ReadKey();
            //Console.WriteLine((int)input.KeyChar);
        }

    }
}
