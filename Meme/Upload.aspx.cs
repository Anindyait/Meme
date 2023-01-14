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
            if(FileUpload1.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
                fileExtension = fileExtension.ToLower();

                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".gif")
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
                }
            }
        
        }
    }
}