<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Meme.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
  <link rel="stylesheet" href="Content/style1.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="font">
            <div class="container-fluid">
            
                <textarea name="address" runat="server" class="form-control post-title" id="post_title" placeholder="An interesting title" rows="3" required></textarea>
                <label for="post_age" class="post-title">Sutiable for ages above: </label>
                <input name="age" type="number" min="1" max="100" class="form-control post-title" id="post_age" placeholder="18"/>
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" style="display:block; margin: 0 auto;" ValidateRequestMode="Enabled" ViewStateMode="Enabled"/>
                <asp:Label ID="Label1" runat="server" style="display:block; margin: 0 auto;text-align:center" ></asp:Label>
                <br/>
                <asp:Button class="btn btn-primary form-submit" style="display:block; margin: 0 auto;" ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        
            
                </div>
            </div>
    </form>
</body>
</html>
