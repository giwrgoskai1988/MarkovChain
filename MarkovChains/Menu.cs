using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MarkovChains
{
    class Menu
    {
        public Dictionary<string , List<string>> wordPairs = new Dictionary<string, List<string>>();
        public string PathInput { get; private set; }


        public Menu()
        {
            Run();
        }

        void Run()
        {
            Console.WriteLine("Please enter a proper folder path to train me or press Enter to quit: ");

            PathInput = @"C:\Users\Α\source\repos"; ;

            while (PathInput != "")
            {
                if (PathExists())
                {
                    Train();
                }
                else
                {
                    Console.WriteLine("Path is wrong! Enter proper path or press Enter to quit! : ");
                }

                PathInput = Console.ReadLine();

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
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            ConsoleKeyInfo input;
        
            while (((input = Console.ReadKey()).KeyChar) != ' ')
            {
                sb.Append(input.KeyChar);          
            }


            for (int i = 0; i < 50; i++)
            {
                var b = wordPairs[sb.ToString().ToLower()].GroupBy(str => str);
            }


            //ConsoleKeyInfo input = Console.ReadKey();
            //Console.WriteLine((int)input.KeyChar);
        }

    }
}
