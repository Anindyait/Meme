using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Meme
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            incorrect_email.Visible = false;
            incorrect_pwd.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select user_id, email, password from user_table where email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", Request.Form["email"]);

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    if (reader["password"].ToString().Equals(Request.Form["password"]))
                    {
                        HttpCookie cookie = new HttpCookie("meme_cookie");
                        cookie["uid"] = reader["user_id"].ToString();

                        cookie.Expires = DateTime.Now.AddDays(5);
                        Response.Cookies.Add(cookie);

                        Response.Redirect("/");
                    }
                    else
                    {
                        incorrect_pwd.Visible = true;
                    }
                }
                else
                {
                    incorrect_email.Visible = true;
                }

               

                con.Close();
            }
        }
    }
}