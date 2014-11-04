using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog
{
    class Program
    {
        private static Blog _blog;

        static void Main(string[] args)
        {
            _blog = new Blog();

            while (true)
            {
                ShowMenu();
            }
        }

        /// <summary>
        /// Denna metod skapar ett nytt blogginlägg
        /// </summary>
        /// <returns></returns>
        public static void CreatePost()
        {
            Console.WriteLine("\nAnge rubrik:");
            string subject = Console.ReadLine();
            Console.WriteLine("Ange meddelande:");
            string text = Console.ReadLine();
            Console.WriteLine("Ange namn på bloggare:");
            string author = Console.ReadLine();
            bool addTags = AddTags();

            List<string> tags = new List<string>();
            if (addTags)
                tags = CreateTags();

            Post post = new Post(subject, text, author);
            post.Tags = tags;

            _blog.AddPost(post);
            _blog.Save();

            Console.WriteLine("\nDitt inlägg är nu sparat.");
        }

        /// <summary>
        /// Denna metod frågar om användaren vill ange taggar
        /// </summary>
        /// <returns></returns>
        public static bool AddTags()
        {
            Console.WriteLine("Vill du ange taggar? (j/n)");

            bool tags;
            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "j")
                {
                    tags = true;
                    break;
                }
                else if (input.ToLower() == "n")
                {
                    tags = false;
                    break;
                }
                else
                    Console.WriteLine("\nOgiltigt val.");
            }

            return tags;
        }

        /// <summary>
        /// Denna metod tar skapar en lista av taggar
        /// </summary>
        /// <returns></returns>
        public static List<string> CreateTags()
        {
            Console.WriteLine("Ange taggar (separera med kommatecken):");
            string input = Console.ReadLine();

            //Delar det inmatade värdet i flera delar
            string[] tags = input.Split(',');

            //Gör om arrayen till en lista
            return tags.ToList();
        }

        /// <summary>
        /// Denna metod skriver ut samtliga poster och alla dess kommentarer
        /// </summary>
        public static void PrintAllPosts()
        {
            List<Post> posts = _blog.GetAllPosts();

            if (posts.Count == 0)
                Console.WriteLine("\nDet finns inga inlägg att visa.");

            foreach (var post in posts)
            {
                Console.WriteLine(post.ToString());

                foreach (var comment in post.Comments)
                    Console.WriteLine(comment.ToString());
            }
        }

        /// <summary>
        /// Denna metod visar programmets huvudmeny
        /// </summary>
        public static void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║      o   o o     oooo  oooo  oooo   oooo    o     oooo oooo   ooo  o  o     ║");
            Console.WriteLine("║      o   o o     o   o o    o       o   o o   o  o     o   o o   o o o      ║");
            Console.WriteLine("║      o   o o     oooo  oooo  ooo    o   o o   o  o     oooo  o   o oo       ║");
            Console.WriteLine("║      o   o o     o   o o        o   o   o ooooo  o  oo o   o o   o o o      ║");
            Console.WriteLine("║       ooo  ooooo oooo  oooo oooo    oooo  o   o   oooo oooo   ooo  o  o     ║");
            Console.WriteLine("║                                    _,-._                                    ║");
            Console.WriteLine("║                                   / \\./ \\                                   ║");
            Console.WriteLine("║                                   >-(.)-<                                   ║");
            Console.WriteLine("║                                   \\./ \\./                                   ║");
            Console.WriteLine("║                                     `-'                                     ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                     MENY:                                   ║");
            Console.WriteLine("║                              0. Avsluta                                     ║");
            Console.WriteLine("║                              1. Skapa nytt inlägg                           ║");
            Console.WriteLine("║                              2. Visa alla inlägg                            ║");
            Console.WriteLine("║                              3. Kommentera inlägg                           ║");
            Console.WriteLine("║                              4. Uppdatera inlägg                            ║");
            Console.WriteLine("║                              5. Ta bort inlägg                              ║");
            Console.WriteLine("║                              6. Sök efter inlägg                            ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAnge ditt val");

            string input = Console.ReadLine();
            if (input == "0")
            {
                Environment.Exit(0);
            }
            else if (input == "1")
            {
                CreatePost();
            }
            else if (input == "2")
            {
                PrintAllPosts();
            }
            else if (input == "3")
            {
                CommentPost();
            }
            else if (input == "4")
            {
                UpdatePost();
            }
            else if (input == "5")
            {
                DeletePost();
            }
            else if (input == "6")
            {
                SearchPost();
            }
            else
            {
                Console.WriteLine("\nOgiltigt val.");
            }
            Console.WriteLine("\nTryck på valfri tangent för att återgå till huvudmenyn.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Den här metoden används för att söka efter en post
        /// </summary>
        /// <returns></returns>
        public static Post SearchPost()
        {
        
            Console.WriteLine("Ange rubrik att söka efter");
                string subject = Console.ReadLine();
                Post post = _blog.FindPost(subject);
                    //by writing Post post we take care of post that is returned, if we find one
                Console.Clear();
                if (post != null)
                {
                    Console.WriteLine(post.ToString());
                }
                else
                {
                    Console.WriteLine("\nTyvärr hittade vi inget under denna rubrik.");
                }
                return post;
        }

        /// <summary>
        /// Denna metod tar bort en post
        /// </summary>
        private static void DeletePost()
        {
            Console.WriteLine("\nAnge rubriken på det inlägget du vill ta bort:");
            string subject = Console.ReadLine();

            Post post = _blog.FindPost(subject);
            if (post != null)
            {
                _blog.DeletePost(post);
                _blog.Save();
                Console.WriteLine("\nDitt inlägg är nu borttaget.");
            }
            else
                Console.WriteLine("\nTyvärr hittade vi inget under denna rubrik.");
        }

        /// <summary>
        /// Denna metod kommenterar en post
        /// </summary>
        private static void CommentPost()
        {
            Console.WriteLine("\nAnge rubriken på det inlägget du vill kommentera:");
            string subject = Console.ReadLine();

            Post post = _blog.FindPost(subject);
            if (post != null)
            {
                Console.WriteLine("Ange kommentar:");
                string text = Console.ReadLine();
                Console.WriteLine("Ange namn:");
                string author = Console.ReadLine();

                Comment comment = new Comment(text, author);
                post.Comments.Add(comment);
                _blog.Save();

                Console.WriteLine("\nDin kommentar är nu sparad.");
            }
            else
                Console.WriteLine("\nTyvärr hittade vi inget under denna rubrik.");
        }

        /// <summary>
        /// Dennna metod uppdaterar en post
        /// </summary>
        public static void UpdatePost()
        {
            Console.WriteLine("\nAnge rubriken för det inlägget du vill uppdatera:");
            string subject = Console.ReadLine();

            Post post = _blog.FindPost(subject);
            if (post != null)
            {
                Console.WriteLine("Ange uppdatering:");
                string update = Console.ReadLine();

                post.Text = update;
                post.Updated = DateTime.Now;
                _blog.Save();

                Console.WriteLine("\nDitt inlägg är nu uppdaterat.");
            }
            else
                Console.WriteLine("\nTyvärr hittade vi inget under denna rubrik.");
        }
    }
}
