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
            public string meme_no;
            public string poster_name;
            public string meme_title;
            public string address;
            public bool type;
            public string liked;
            public post(string uid, string meme_no, string poster_name, string meme_title, string address, bool type, string liked)
            {
                this.uid = uid;
                this.meme_no = meme_no;
                this.poster_name = poster_name;
                this.meme_title = meme_title;
                this.address = address;
                //true for video, false for imgs
                this.type = type;
                //whether user liked the post or not
                this.liked = liked;
            }
        }

        public struct user_details
        {
            public string uid;
            public string first_name;
            public string last_name;
            public string email;
            public string phone;
            public user_details(string uid = "", string first_name = "a", string last_name = "b", string email = "c", string phone = "1")
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

            if (cookie != null)
            {
                uid = cookie.Value.Substring(4);
            }
            return uid;
        }

        public void GetMemes(string profile_uid, string user_uid, string show = null)
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


                if (show.Equals("All"))
                {
                    cmd = new MySqlCommand(allMeme, con);
                    cmd.Parameters.AddWithValue("@Age", age);
                }

                else
                {
                    
                    // While viewing someone else's profile
                    if (profile_uid != GetUID())
                    {
                        cmd = new MySqlCommand("select * from meme_table where user_id = @User_id and (age is null or age <= @Age) order by meme_no desc", con);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@User_id", profile_uid);
                    }
                    // See all posts from your profile irrespective of age
                    else
                    {
                        cmd = new MySqlCommand("select * from meme_table where user_id = @User_id order by meme_no desc", con);
                        cmd.Parameters.AddWithValue("@User_id", profile_uid);
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

                    string liked = ""; //to store "checked" or ""

                    string filename = reader["imgs"].ToString().ToLower();

                    string extension = filename.Substring(filename.Length - 4);

                    bool filetype = false;

                    int noOfLikes =  NoOfLikes(reader["meme_no"].ToString());

                    Debug.WriteLine(reader["meme_no"].ToString() + " " + noOfLikes);

                    if (reader["liked_no"].ToString().Contains("," + user_uid + ","))
                    {
                        liked = "checked";
                    }

                    if (extension == ".mp4" || extension == ".mov" || extension == "webm" || extension == "wmv" || extension == "flv")
                    {
                        filetype = true;
                    }

                    if (user_name.Read())
                    {
                        eachMeme.Add(new post(reader["user_id"].ToString(), reader["meme_no"].ToString(), user_name["first_name"].ToString() + " " + user_name["last_name"].ToString(), reader["m_name"].ToString(), reader["imgs"].ToString(), filetype, liked));
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

        public string LikeMeme(string job, string meme_no, string uid)
        {
            string likedString = null;

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();


                MySqlCommand cmd;

                cmd = new MySqlCommand("select liked_no from meme_table where meme_no = @Meme_No", con);
                cmd.Parameters.AddWithValue("@Meme_No", meme_no);

                MySqlDataReader liked = cmd.ExecuteReader();

                if (liked.Read())
                {
                    likedString = liked["liked_no"].ToString();
                }

                con.Close();
                con.Open();

                if (job.Equals("Like"))
                {
                    likedString = "," + uid + "," + likedString;

                }

                else if (job.Equals("Unlike"))
                {
                    likedString = likedString.Replace("," + uid + ",", "");
                }

                cmd = new MySqlCommand("update meme_table set liked_no = @Liked where meme_no = @Meme_No", con);

                cmd.Parameters.AddWithValue("@Liked", likedString);
                cmd.Parameters.AddWithValue("@Meme_No", meme_no);

                cmd.ExecuteNonQuery();


            }

            return likedString;
        }

        public int NoOfLikes(string meme_no)
        {
            int noOfLikes = 0;

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();

                string likedString = null;


                MySqlCommand cmd;

                cmd = new MySqlCommand("select liked_no from meme_table where meme_no = @Meme_No", con);
                cmd.Parameters.AddWithValue("@Meme_No", meme_no);

                MySqlDataReader liked = cmd.ExecuteReader();

                if (liked.Read())
                {
                    likedString = liked["liked_no"].ToString();
                }

                if (likedString != null)
                {
                    noOfLikes = likedString.Count(f => (f == ','));

                    if (noOfLikes > 0)
                        noOfLikes = noOfLikes / 2;
                }
            }

            return noOfLikes;
        }
    }
}