using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Meme
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["meme_cookie"];

            if (cookie != null)
            {
                //get the uid from cookies, comes in the form "uid=1", hence Substring(4)
                Debug.WriteLine(cookie.Value.Substring(4));

                //get the post_title from the text field.
                Debug.WriteLine(post_title.Value);


                if (FileUpload1.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
                    fileExtension = fileExtension.ToLower();

                    String[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".webm", ".mp4", ".mov", "flv"};

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        Label1.Text = "Not an image";
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        Label1.Text = "Image uploaded";
                        Label1.ForeColor = System.Drawing.Color.Green;
                        FileUpload1.SaveAs(Request.PhysicalApplicationPath + "/Memes/" + FileUpload1.FileName.ToString());
                        Debug.WriteLine(FileUpload1.FileName.ToString());

                        using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
                        {
                            con.Open();

                            MySqlCommand cmd;
                            if (Request.Form["age"] == null || Request.Form["age"] == "")
                            {
                                cmd = new MySqlCommand("insert into meme_table(m_name, imgs, user_id) values (@M_name, @Imgs, @UID)", con);

                                cmd.Parameters.AddWithValue("@M_name", post_title.Value);
                                cmd.Parameters.AddWithValue("@Imgs", "Memes/" + FileUpload1.FileName.ToString());
                                cmd.Parameters.AddWithValue("@UID", cookie.Value.Substring(4));
                            }
                            else
                            {
                                cmd = new MySqlCommand("insert into meme_table(m_name, imgs, age, user_id) values (@M_name, @Imgs, @Age, @UID)", con);

                                cmd.Parameters.AddWithValue("@M_name", post_title.Value);
                                cmd.Parameters.AddWithValue("@Imgs", "Memes/" + FileUpload1.FileName.ToString());
                                cmd.Parameters.AddWithValue("@Age", Request.Form["age"]);
                                cmd.Parameters.AddWithValue("@UID", cookie.Value.Substring(4));
                            }

                            cmd.ExecuteNonQuery();

                            con.Close();
                            con.Open();



                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }

        }
    }
}