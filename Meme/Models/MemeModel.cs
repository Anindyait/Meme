using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meme.Models
{
    public class MemeModel
    {

        public struct post
        {
            public string poster_name;
            public string meme_title;
            public string address;

            public post(string poster_name, string meme_title, string address)
            {
                this.poster_name = poster_name;
                this.meme_title = meme_title;
                this.address = address;
            }
        }

        public List<post> eachMeme { get; set; }

        public void GetMemes()
        {
  

            eachMeme = new List<post>();



            eachMeme.Add(new post("u/aimbot", "Title1", "Memes/meme1.jpg"));
            eachMeme.Add(new post("u/saibot", "Title2", "Memes/meme2.jpg"));
        }
    }




}