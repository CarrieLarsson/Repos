using System;
using System.Collections.Generic;

namespace Blog
{
    //Attributet krävs för att kunna serialisera klassen till XML
    [Serializable]
    public class Post : Message
    {
        //Tom konstruktor krävs för serialisering
        public Post()
        {
        }

        public Post(string subject, string text, string author)
            : base(text, author)
        {
            Subject = subject;
            Tags = new List<string>();
            Comments = new List<Comment>();
        }

        public string Subject { get; set; }
        public DateTime Updated { get; set; }
        public List<string> Tags { get; set; }
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Denna metod beskriver objektets egenskaper med hjälp av en sträng
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Om det har gjorts en uppdatering, ange uppdateringen, annars ange en tom sträng
            string updated = string.Empty;
            if (Updated != DateTime.MinValue)
                updated = Updated.ToLongDateString() + " " + Updated.ToLongTimeString();


            string temp =
                "RUBRIK:\t\t " + Subject +
                "\nMEDDELANDE:\t " + Text +
                "\nSkappad av:\t " + Author +
                "\nTaggar:\t\t " + string.Join(",", Tags) +
                "\nSkapad:\t\t " + Created.ToLongDateString() + " " + Created.ToLongTimeString() +
                "\nUppdaterad:\t\t " + updated;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n════════════════════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Yellow;

            return temp;
        }
    }
}