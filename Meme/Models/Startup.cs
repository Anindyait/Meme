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
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
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
                                       "liked_no integer(20)," +
                                       "disliked_no integer(20)," +
                                       "imgs varchar(50) not null," +
                                       "age integer(20)," +
                                       "primary key(meme_no)," +
                                       "foreign key(user_id) references user_table(user_id));", con);

                cmd.ExecuteNonQuery();

                con.Close();
                con.Open();

            }

        }
    }

}