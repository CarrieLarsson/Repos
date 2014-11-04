using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Blog
{
    public class Blog
    {
        private const string FileName = "Posts.xml"; //Namnet på vår xml-fil där vi lagrar alla blogginlägg
        private List<Post> _posts;

        public Blog()
        {
            _posts = ReadPostsFromFile();
        }

        /// <summary>
        /// Den här metoden används för att lägga till en ny bloggpost i listan av poster
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        /// <summary>
        /// Den här metoden hämtar alla bloggposter
        /// </summary>
        /// <returns></returns>
        public List<Post> GetAllPosts()
        {
            return _posts;
        }

        /// <summary>
        /// Denna metod möjligör att vi kan ta bort en bloggpost
        /// </summary>
        /// <param name="post"></param>
        public void DeletePost(Post post)
        {
            _posts.Remove(post);
        }

        /// <summary>
        /// Den här metoden sparar ned listan av bloggposter i en fil
        /// </summary>
        public void Save()
        {
            //Skapar ett serializer objekt som serialiserar en lista av poster
            XmlSerializer serializer = new XmlSerializer(typeof(List<Post>));

            //Skapar ett objekt av typen stringwriter som används tillsammans med XML Serializer
            StringWriter stringWriter = new StringWriter();

            //Serialiserar listan av blogposter och sparar resultatet i stringwritern
            serializer.Serialize(stringWriter, _posts);

            //Lagrar XML-strängen i en variabel som vi kallar xml
            string xml = stringWriter.ToString();

            //Sparar XML-strängen i en fil
            File.WriteAllText(FileName, xml);
        }

        /// <summary>
        /// Den här metoden läser upp samtliga bloggposter från en fil
        /// </summary>
        /// <returns></returns>
        private List<Post> ReadPostsFromFile()
        {
            //Skapar en XML Serializer och anger vilken typ av objekt som skall serialiseras
            XmlSerializer serializer = new XmlSerializer(typeof(List<Post>));

            //Om filen inte existerar så returnerar vi en tom lista
            if (!File.Exists(FileName))
                return new List<Post>();

            //Läser all text ifrån fil och lagrar det i en variabel
            string xml = File.ReadAllText(FileName);

            //Skapar en StringReader för att kunna deserialisera XML'en tillbaks till ett objekt 
            TextReader reader = new StringReader(xml);

            //Resultatet av deserialiseringen är ett objekt
            object result = serializer.Deserialize(reader);

            //Gör om resultatet till en lista av poster igen.
            List<Post> posts = result as List<Post>;

            return posts;
        }

        /// <summary>
        /// Går igenom listan av poster och söker efter angiven rubrik
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public Post FindPost(string subject)
        {
            foreach (var post in _posts)
                if (post.Subject.ToLower() == subject.ToLower())
                    return post;

            return null;
        }
    }
}