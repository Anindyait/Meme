using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Startup.Models
{
    public class StartupModel
    {
        public void CreateTables()
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;port=3306;uid=root;password=abcd"))
            {
                try
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("create database if not exists meme;", con);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Database created successfully!");

                }

                catch (MySqlException ex) 
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                try
                {
                    con.Open();

                    MySqlCommand cmd;

                    cmd = new MySqlCommand("create table if not exists user_table(user_id integer auto_increment," +
                                            "first_name varchar(255) not null," +
                                            "last_name varchar(255)," +
                                            "email varchar(255) unique not null," +
                                            "phone varchar(30) unique not null," +
                                            "dob date," +
                                            "liked varchar(255)," +
                                            "disliked varchar(255)," +
                                            "password varchar(255) not null," +
                                            "primary key(user_id));", con);

                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("create table if not exists meme_table(meme_no integer auto_increment, " +
                                           "m_name varchar(255) unique not null," +
                                           "user_id integer," +
                                           "liked_no varchar(1024)," +
                                           "disliked_no varchar(1024)," +
                                           "imgs varchar(255) not null," +
                                           "age integer(20)," +
                                           "primary key(meme_no)," +
                                           "foreign key(user_id) references user_table(user_id));", con);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Tables created successfully!");

                    cmd = new MySqlCommand("insert ignore into user_table(first_name, last_name, email, phone, dob, password) " +
                                            "values('Dummy', 'User', 'dummy@meme.com', '1234567890', '2023-01-01', 'Qwerty1@');", con);

                    cmd.ExecuteNonQuery();

                    con.Close();
                    con.Open();

                    cmd = new MySqlCommand("select user_id from user_table where email = 'dummy@meme.com';", con);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    int uid = 0;

                    if(reader.Read())
                    {
                        uid = (int)reader["user_id"];

                    }

                    con.Close();
                    con.Open();


                    cmd = new MySqlCommand("insert ignore into meme_table(m_name, user_id, imgs) " +
                                           "values('Benefits of not having a room', @uid, 'Memes/roomless.jpg');", con);
                    cmd.Parameters.AddWithValue("@uid", uid);

                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("insert ignore into meme_table(m_name, user_id, imgs) " +
                                           "values('When see the project name', @uid, 'Memes/the-rock-meme-360.mp4');", con);
                    cmd.Parameters.AddWithValue("@uid", uid);

                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("insert ignore into meme_table(m_name, user_id, imgs) " +
                                           "values('Scary', @uid, 'Memes/non-binary.jpg');", con);
                    cmd.Parameters.AddWithValue("@uid", uid);

                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("insert ignore into meme_table(m_name, user_id, imgs) " +
                                           "values('When you clear unclosed tabs on your mom''s phone and it no longer hangs', @uid, 'Memes/Hackerman.jpg');", con);
                    cmd.Parameters.AddWithValue("@uid", uid);

                    cmd.ExecuteNonQuery();


                }
                catch (MySqlException ex) 
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

        }
    }

}