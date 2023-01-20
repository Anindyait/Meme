# Meme Website in ASP .NET And C#

<!-- PROJECT LOGO -->
<br />
<div align="center">
    <img src="Meme/Content/amongus.png" alt="Logo" width="80" height="80">
    <h3 align="center">SUS</h3>
    <p align="center">
        <!--PROJECT DESCRIPTION--!>
    </p>
</div>

## Requirements

Install the following as per your system's requirements:

- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/older-downloads/#visual-studio-2019-and-other-products)

- [MySQL v8.0](https://dev.mysql.com/downloads/installer/)

## Usage

1.  Clone the repo
   ```sh
   git clone https://github.com/Anindyait/Meme
   ```
2. Open Meme.sln on Visual Studio
3. Password for the connection string has been set as ```abcd```,  
  
   Do change it (in all C# files), to reflect your MySQL's password.
4. Run Startup.cs under Models on to get the database created.
5. You can login with a dummy user or register a user.

   - Dummy User Credentials  
     
     Email: 
     ```
     dummy@meme.com
     ```
     Password: 
     ```
     Qwerty1@
     ```

Now you can locally host the Meme website.

## Features

- User Registration and HTTP Cookie Based Login
- Meme Dashboard with Age Restriction on Memes
- User can upload memes (images & videos under 300 Mb)
  and delete their own memes
- User can like memes
- User can see other users' Profiles

## File Structure

```
.
├── LICENSE
├── Meme...............................Project folder
│   ├── App_Start
│   ├── Content
│   │   ├── aboutUs1.jpg
│   │   ├── aboutUs2.jpg
│   │   ├── amongus.png
│   │   ├── Dev pics
│   │   └── style1.css
│   ├── Controllers
│   │   └── HomeController.cs..........C# for all Views linked to Home
│   ├── favicon.ico
│   ├── Global.asax.cs.................MVC Configuration Setup
│   ├── header.html....................Header for webforms not under MVC Model
│   ├── Login.aspx.....................Webform for Login
│   ├── Login.aspx.cs..................C# for handling Login 
│   ├── Memes
│   ├── Models
│   │   ├── MemeModel.cs...............Handles all meme functionality
│   │   └── Startup.cs.................Creates the database and a dummy user
│   ├── postRegister.html
│   ├── register.aspx..................Webform for Register
│   ├── register.aspx.cs...............C# for handling Register
│   ├── Upload.aspx....................Webform for Upload
│   ├── Upload.aspx.cs.................C# for handling Upload
│   ├── Views
│   │   ├── Home.......................Contains the views handled by HomeController
│   │   │   ├── About.cshtml
│   │   │   ├── Contact.cshtml
│   │   │   ├── Index.cshtml
│   │   │   ├── LikedMemes.cshtml
│   │   │   ├── Profile.cshtml
│   │   │   └── Upload.cshtml
│   │   ├── Shared
│   │   │   ├── Error.cshtml...........Error page to the MVC Views
│   │   │   └── _Layout.cshtml.........Contains Header to the MVC Views
│   │   ├── _ViewStart.cshtml
├── Meme.sln...........................Solution file to open in Visual Studio
└── README.md
```
