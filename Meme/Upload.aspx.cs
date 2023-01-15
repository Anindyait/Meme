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
                        Debug.WriteLine(FileUpload1.FileName.ToString());

                        string imgs = FileUpload1.FileName.ToString();

                        using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=meme;port=3306;password=abcd"))
                        {

                            try
                            {
                                con.Open();

                                MySqlCommand cmd;


                                cmd = new MySqlCommand("select imgs from meme_table where imgs = @Imgs;", con);
                                cmd.Parameters.AddWithValue("@Imgs", "Memes/" + imgs);

                                MySqlDataReader reader = cmd.ExecuteReader();

                                if (reader.Read())
                                {
                                    Debug.WriteLine("Found duplicate " + imgs);
                                    imgs = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + imgs;
                                    Debug.WriteLine("Found duplicate " + imgs);

                                }


                            }
                            catch (MySqlException ex)
                            {
                                Debug.WriteLine(ex);
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

                                //string imgs = "Memes/" + FileUpload1.FileName.ToString();

                                if (Request.Form["age"] == null || Request.Form["age"] == "")
                                {
                                    cmd = new MySqlCommand("insert into meme_table(m_name, imgs, user_id) values (@M_name, @Imgs, @UID)", con);

                                    cmd.Parameters.AddWithValue("@M_name", post_title.Value);
                                    cmd.Parameters.AddWithValue("@Imgs", "Memes/" + imgs);
                                    cmd.Parameters.AddWithValue("@UID", cookie.Value.Substring(4));
                                }
                                else
                                {
                                    cmd = new MySqlCommand("insert into meme_table(m_name, imgs, age, user_id) values (@M_name, @Imgs, @Age, @UID)", con);

                                    cmd.Parameters.AddWithValue("@M_name", post_title.Value);
                                    cmd.Parameters.AddWithValue("@Imgs", "Memes/" + imgs);
                                    cmd.Parameters.AddWithValue("@Age", Request.Form["age"]);
                                    cmd.Parameters.AddWithValue("@UID", cookie.Value.Substring(4));
                                }

                                cmd.ExecuteNonQuery();
                                FileUpload1.SaveAs(Request.PhysicalApplicationPath + "/Memes/" + imgs);


                            }
                            catch (MySqlException ex)
                            {
                                Debug.WriteLine(ex);
                            }
                            finally
                            {
                                con.Close();
                            }
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