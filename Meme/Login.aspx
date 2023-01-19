<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Meme.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"
			integrity="sha256-2Kok7MbOyxpgUVvAk/HJ2jigOSYS2auK4Pfzbm7uH60="
			crossorigin="anonymous">
	</script>
	 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
     <script src="https://kit.fontawesome.com/13deb536c6.js" crossorigin="anonymous"></script>

     <link rel="stylesheet" href="Content/style1.css">
    
	<meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Login</title>

      <script>

          var allQuotes = ["\"They don't have racism in America anymore. When they voted for Obama they sorted all of that out.\" - Philomena Cunk",
              "\"Why do we cry when it's the onions that are getting hurt?\" - Philomena Cunk",
              "\" I have all the characteristics of a human being: blood, flesh, skin, hair; but not a single, clear, identifiable emotion, except for greed and disgust.\" - Patrick Bateman",
              "\"Your compassion is a weakness your enemies will not share.\"<br>\"That's why it's so important. It seperates us from them.\" - Batman Begins",
              "\"Why do we fall, Sir? So we can learn to pick ourselves up.\" - Alfred",
              "\"It's not who I am underneath, but what I do that defines me.\" - Batman",
              "\"Would you like to hear today's specials?\"<br>\"Not if you want to keep your spleen.\" - Patrick Bateman",
              "\"..some men aren't looking for anything logical, like money. They can't be bought, bullied, reasoned, or negotiated with. Some men just want to watch the world burn.\" - Alfred",
              "\"You Always Fear What You Don't Understand.\" - Falcone",
              "\"Madness, as you know, is a lot like gravity. All it takes is a little push.\" - The Joker"]
          function getQuote() {
              var quote = document.getElementById("quote");
              var quoter = document.getElementById("quoter");
              var currentQuote = allQuotes[Math.floor(Math.random() * allQuotes.length)];
              currentQuote = currentQuote.split("-");
              console.log(currentQuote[0]);
              quote.innerHTML = currentQuote[0];
              quoter.innerHTML = "- " + currentQuote[1];
          }
      </script>


      <script>
            $(function () {
                $("#header").load("header.html");
    });
    </script>


</head>
<body onload="getQuote()">



    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
    
    <div class="font">
		<div class="container-fluid">
			<div id="header"></div>
			   <div class="header-adjustment"></div>

                <div class="row d-flex align-items-center">
                    <div class="col-lg">
                        <div class="quote-bg">
                            
                            <p id="quote"></p>
                            <br>
                            <br>
                            <h5 id="quoter"></h5>
                        </div>
                    </div>
                    <div class="col">
                    
                        <div class = "login-bg">
                                <h2 style="opacity:90%;">Login</h2>
                                
                                <br>
                                
                                <div runat="server" class="server-side-check" style="text-align: left;" id="incorrect_pwd" >Incorrect Password!</div>
                                <div runat="server" class="server-side-check" style="text-align: left;" id="incorrect_email">Email ID not found!</div>
                                <br>
                                <form id="form1" runat="server" class="needs-validation" novalidate>
                                    <div class="form-floating mb-3" >
                                        <input runat="server" name="email" type = "email" class = "form-control"  id = "email" placeholder="mikehunt@panda.com" required>
                                        <label for = "email" style="opacity:70%;">Email ID</label> 

                                        <div class="invalid-feedback" style="text-align: left;">
                                        Enter valid email ID
                                    </div>
                                    
                                    </div>
                                    <br>
                                    <div class="form-floating mb-3">
                                        <input runat="server" name="password" type = "password" class = "form-control"  id = "password" placeholder="1234" required>
                                        <label for = "password" style="opacity:70%;">Password</label>
                                        <div class="invalid-feedback" style="text-align: left;">
                                            Enter password
                                        </div> 
                                    </div>
                                    <br>
                                    <div class="row">
                                        <div class="col">
                                           
                                            <asp:Button ID="Button1" runat="server" Text="Login" type="submit" class="btn btn-dark form-submit" style="margin-bottom: 10px;" OnClick="Button1_Click" />
                                        </div>
                                        <div class="col">
                                            <a href="register.aspx">
                                                <div type="sub" class="btn btn-outline-dark form-submit">Sign up</div>
                                            </a>
                                        </div>
                                    </div>

                                </form>
                            </div>
                    </div>
                    </div>

			</div>
		</div>

        <script>
            // Example starter JavaScript for disabling form submissions if there are invalid fields
            (() => {
                'use strict'

                // Fetch all the forms we want to apply custom Bootstrap validation styles to
                const forms = document.querySelectorAll('.needs-validation')


                // Loop over them and prevent submission
                Array.from(forms).forEach(form => {
                    form.addEventListener('submit', event => {

                        if (!form.checkValidity()) {
                            event.preventDefault()
                            event.stopPropagation()
                        }


                        form.classList.add('was-validated')

                    }, false)
                })

            })()
        </script>
    <div id="footer"></div>
	</body>
</html>
