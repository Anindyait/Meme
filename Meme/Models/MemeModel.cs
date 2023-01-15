using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            public bool type;
            public post(string poster_name, string meme_title, string address, bool type)
            {
                this.poster_name = poster_name;
                this.meme_title = meme_title;
                this.address = address;
                //true for video, false for imgs
                this.type = type;
            }
        }

        public List<post> eachMeme { get; set; }

        public void GetMemes()
        {

            eachMeme = new List<post>();

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();

                MySqlCommand cmd;

                cmd = new MySqlCommand("select * from meme_table order by meme_no desc", con);

               

                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    MySqlConnection con2 = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd");

                    con2.Open();
                    
                    //to get the user name from the user_table using the user_id
                    MySqlCommand cmd2 = new MySqlCommand("select first_name, last_name from user_table where user_id = @User_id", con2);

                    cmd2.Parameters.AddWithValue("@User_id", reader["user_id"].ToString());

                    MySqlDataReader user_name = cmd2.ExecuteReader();

                    string filename = reader["imgs"].ToString().ToLower();

                    string extension = filename.Substring(filename.Length - 4);

                    bool filetype = false;

                    if (extension == ".mp4" || extension == ".mov" || extension == "webm" || extension == "wmv" || extension == "flv")
                    {
                        filetype = true;
                    }

                    if (user_name.Read())
                    {
                        eachMeme.Add(new post(user_name["first_name"].ToString() +" " + user_name["last_name"].ToString(), reader["m_name"].ToString(), reader["imgs"].ToString(), filetype));
                    }
                }


            }

            //        eachMeme.Add(new post("u/aimbot", "Title1", "Memes/meme1.jpg"));
            //eachMeme.Add(new post("u/saibot", "Title2", "Memes/meme2.jpg"));
        }
    }




}