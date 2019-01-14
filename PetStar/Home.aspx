﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PetStar.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/menu.css" rel="stylesheet" />
    <link href="Content/header.css" rel="stylesheet" />
    <link href="Content/footer.css" rel="stylesheet" />

    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    
    <%--<script src="Scripts/popper.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div id="containerStatic">
            <div id="containerMain">
                <div>
                    <header>
                        <div id="barMain">
                            <div id="contSocial">
                                <a href="/" class="btnSocial">
                                    <img src="Resources/facebook.png" /></a>
                                <a href="/" class="btnSocial">
                                    <img src="Resources/twitter.png" /></a>
                                <a href="/" class="btnSocial">
                                    <img src="Resources/youtube.png" /></a>
                                <a href="/" class="btnSocial">
                                    <img src="Resources/instagram.png" /></a>
                            </div>
                            <div id="contIcon">
                                <img src="Resources/logo.png" />
                            </div>
                        </div>
                        <div id="sliderMain" class="carousel slide" data-ride="carousel">

                            <!-- Indicators -->
                            <ul class="carousel-indicators">
                                <li data-target="#demo" data-slide-to="0" class="active"></li>
                                <li data-target="#demo" data-slide-to="1"></li>
                                <li data-target="#demo" data-slide-to="2"></li>
                            </ul>

                            <!-- The slideshow -->
                            <div class="carousel-inner">
                                <div class="carousel-item active">
                                    <img src="Resources/diving-cabo-pulmo.jpg" alt="Los Angeles" width="1100" height="500">
                                </div>
                                <div class="carousel-item">
                                    <img src="Resources/buceo.jpg" alt="Chicago" width="1100" height="500">
                                </div>
                                <div class="carousel-item">
                                    <img src="Resources/diving-cabo-pulmo.jpg" alt="New York" width="1100" height="500">
                                </div>
                            </div>
                        </div>
                    </header>
                    <section>
                        <p>
                            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                        </p>
                    </section>
                    <footer>
                        <div class="row">
                            <div class="col-md-3 col-sm-4"></div>
                            <div class="col-md-3 col-sm-4"></div>
                            <div class="col-md-3 col-sm-4"></div>

                        </div>
                    </footer>
                </div>
            </div>
            <div id="containerNav">
                <nav class="navbar navbar-expand-lg navbar-light bg-light">
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarNavDropdown">
                        <ul class="navbar-nav">
                            <li class="nav-item active">
                                <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">Features</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">Pricing</a>
                            </li>
                            <li class="nav-item dropup">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown link
                                </a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" href="#">Action</a>
                                    <a class="dropdown-item" href="#">Another action</a>
                                    <a class="dropdown-item" href="#">Something else here</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <a class="navbar-brand" href="#">
                        <img src="Resources/Icon_green.png" />
                    </a>
                </nav>
            </div>
        </div>
    </form>
</body>
</html>
