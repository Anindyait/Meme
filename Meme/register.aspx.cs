using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;

namespace Meme
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            phone_exists.Visible = false;
            email_exists.Visible = false;
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            int flag = 0;
            string emailID = email.Value;
            string phn = phone.Value;
            string dd = Request.Form["day"];
            string mm = Request.Form["month"];
            string yyyy = Request.Form["year"];

            string dob = yyyy + "-" + mm + "-" + dd;


            Debug.WriteLine(firstname.Value);
            Debug.WriteLine(Request.Form["lastname"]);

           
            Debug.WriteLine("year = " + Request.Form["year"]);

            Debug.WriteLine("month = " + month.Value);
            Debug.WriteLine(gender.Value);
            Debug.WriteLine(year.Value);


            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select email from user_table where email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", Request.Form["email"]);

                MySqlDataReader reader_email = cmd.ExecuteReader();


                if (reader_email.Read())
                {
                    email_exists.Visible = true;
                    flag++;
                }

                con.Close();
                con.Open();
                cmd = new MySqlCommand("select phone from user_table where phone = @Phone", con);
                cmd.Parameters.AddWithValue("@Phone", Request.Form["phone"]);

                MySqlDataReader reader_phone = cmd.ExecuteReader();


                if (reader_phone.Read())
                {
                    phone_exists.Visible = true;
                    flag++;
                }

                if(flag==0)
                {
                    con.Close();
                    con.Open();
                    cmd = new MySqlCommand("insert into user_table(first_name,last_name,email,phone,dob,password) values (@FirstName,@LastName,@Email,@Phone,@DOB,@Password)", con);

                    cmd.Parameters.AddWithValue("@FirstName", Request.Form["firstname"]);
                    cmd.Parameters.AddWithValue("@LastName", Request.Form["lastname"]);
                    cmd.Parameters.AddWithValue("@Email", Request.Form["email"]);
                    cmd.Parameters.AddWithValue("@Phone", Request.Form["phone"]);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@Password", Request.Form["password1"]);

                    cmd.ExecuteNonQuery();

                    Response.Redirect("postRegister.html");
                }
            }
        }
    }
}