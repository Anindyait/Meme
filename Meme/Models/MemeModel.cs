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
            public string uid;
            public string poster_name;
            public string meme_title;
            public string address;
            public bool type;
            public post(string uid, string poster_name, string meme_title, string address, bool type)
            {
                this.uid = uid;
                this.poster_name = poster_name;
                this.meme_title = meme_title;
                this.address = address;
                //true for video, false for imgs
                this.type = type;
            }
        }

        public struct user_details
        {
            public string uid;
            public string first_name;
            public string last_name;
            public string email;
            public string phone;
            public user_details(string uid="", string first_name="a", string last_name="b", string email="c", string phone="1")
            {
                this.uid = uid;
                this.first_name = first_name;
                this.last_name = last_name;
                this.email = email;
                this.phone = phone;
            }
        }

        string allMeme = "select * from meme_table where (age <= @Age or age is null) order by meme_no desc";


        public List<post> eachMeme { get; set; }

        public user_details user { get; set; }

        public string GetUID()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["meme_cookie"];

            string uid = null;

            if(cookie!=null)
            {
                uid = cookie.Value.Substring(4);
            }
            return uid;
        }

        public void GetMemes(String uid = null)
        {

            eachMeme = new List<post>();

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();

                MySqlCommand cmd;
                int age = 0;

                cmd = new MySqlCommand("select dob from user_table where user_id = @User_id", con);
                cmd.Parameters.AddWithValue("@User_id", GetUID());
                MySqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    // Getting age of user
                    DateTime dob = Convert.ToDateTime(read["dob"].ToString());
                    DateTime now = DateTime.Now;
                    age = now.Year - dob.Year;
                    if (now < dob.AddYears(age)) age--;
                    Debug.WriteLine("Age: " + age);
                }

                con.Close();
                con.Open();

                if (uid == null)
                {
                    cmd = new MySqlCommand(allMeme, con);
                    cmd.Parameters.AddWithValue("@Age", age);
                }

                else
                {
                    // While viewing someone else's profile
                    if (uid != GetUID())
                    {
                        cmd = new MySqlCommand("select * from meme_table where user_id = @User_id and (age is null or age <= @Age) order by meme_no desc", con);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@User_id", uid);
                    }
                    // See all posts from your profile irrespective of age
                    else
                    {
                        cmd = new MySqlCommand("select * from meme_table where user_id = @User_id order by meme_no desc", con);
                        cmd.Parameters.AddWithValue("@User_id", uid);
                    }
                }

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
                        eachMeme.Add(new post(reader["user_id"].ToString(), user_name["first_name"].ToString() + " " + user_name["last_name"].ToString(), reader["m_name"].ToString(), reader["imgs"].ToString(), filetype));
                    }

                    con2.Close();
                }

                con.Close();
            }

        }


        public void GetUserDetails(String uid)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();

                MySqlCommand cmd;

                cmd = new MySqlCommand("select * from user_table where user_id = @User_id", con);
                cmd.Parameters.AddWithValue("@User_id", uid);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new user_details(uid, reader["first_name"].ToString(), reader["last_name"].ToString(), reader["email"].ToString(), reader["phone"].ToString());
                }

                con.Close();
            }
        }
    }
}