using System;

namespace Blog
{
    //Attributet krävs för att kunna serialisera klassen till XML
    [Serializable]
    public class Message //Detta är basklassen för vår applikation
    {
        //Tom konstruktor krävs för serialisering
        public Message() {}

        public Message(string text, string author)
        {
            Text = text;
            Author = author;
            Created = DateTime.Now; 
        }

        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }

        /// <summary>
        /// Denna metod beskriver objektets egenskaper med hjälp av en sträng
        /// </summary>
        /// <returns></returns>
        public virtual string ToString()
        {
            return
                "\nText: " + Text +
                "\nSkapad: " + Created.ToLongDateString() +
                "\nSkapad av: " + Author;
        }
    }
}
