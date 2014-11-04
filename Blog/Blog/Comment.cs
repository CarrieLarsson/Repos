using System;

namespace Blog
{
    //Attributet krävs för att kunna serialisera klassen till XML
    [Serializable]
    public class Comment : Message
    {
        //Tom konstruktor krävs för serialisering
        public Comment(){}

        public Comment(string text, string author)
            : base(text, author)
        {
            Text = text;
            Author = author;
        }

        /// <summary>
        /// Denna metod beskriver objektets egenskaper med hjälp av en sträng
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "\tKOMMENTAR" +
                   "\n\tText:\t\t " + Text +
                   "\n\tSkapad:\t\t " + Created.ToLongDateString() + " " + Created.ToLongTimeString() +
                   "\n\tSkapad av:\t " + Author;
        }
    }
}