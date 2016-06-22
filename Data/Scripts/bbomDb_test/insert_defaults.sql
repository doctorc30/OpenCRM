use bbomDb_test
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [parent_id], [ActiveTemplateId], [VideoLink], [Foot], [InvitedUser], [DateRegistration], [Name], [Suname], [Altname]) VALUES (N'f4bef59a-0ba8-4c22-8094-a0221ad7d7df', N'cost34rus@gmail.com', 0, N'AI+sA1Np0y5YWumkzRnCqBQoX1u0nO/hYStnSNdS1EPVoJOlcgb6mLfydaVEmdOJcQ==', N'942e7cb0-0c9e-41b3-830f-f56b07a79121', NULL, 0, 0, NULL, 1, 0, N'www', NULL, 3, N'https://www.youtube.com/embed/MVM1a29ZVNk', NULL, NULL, N'2016-01-01 00:00:00', NULL, NULL, NULL)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [parent_id], [ActiveTemplateId], [VideoLink], [Foot], [InvitedUser], [DateRegistration], [Name], [Suname], [Altname]) VALUES (N'11318fcb-e375-4fdb-a4ed-8ba24e3e3279', N'qwe@q8.ru', 0, N'APSg78dCwRFcLYPmkpoX5j0/nh6VPRJXuAzTrQjuJ/xrxesv+MLB4UO+Ou4CJTxlrQ==', N'369295a7-fc2d-4761-b438-d19e99069d3a', NULL, 0, 0, NULL, 1, 0, N'qwe8', N'f4bef59a-0ba8-4c22-8094-a0221ad7d7df', 3, NULL, 0, N'f4bef59a-0ba8-4c22-8094-a0221ad7d7df', N'2016-02-01 00:00:00', NULL, NULL, NULL)

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7138ef18-c696-450f-aac4-06a692e5f75c', N'admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dd836c76-bcba-4700-bdd8-33d7dc608c8a', N'notUser')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'87f990cc-cbf5-4db2-8d5d-a8ad93782b0b', N'user')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f4bef59a-0ba8-4c22-8094-a0221ad7d7df', N'7138ef18-c696-450f-aac4-06a692e5f75c')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'11318fcb-e375-4fdb-a4ed-8ba24e3e3279', N'87f990cc-cbf5-4db2-8d5d-a8ad93782b0b')

INSERT INTO [dbo].[DefaultTemplates] ([Header], [Body], [Footer], [Id], [Name], [IsDefault], [UserId], [Links]) VALUES (N'<div class="navbar-wrapper">
      <div class="container">

        <div class="navbar navbar-inverse navbar-static-top" role="navigation">
          <div class="container">
            <div class="navbar-header">
              <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
              </button>
              <a class="navbar-brand" href="#">Project name</a>
            </div>
            <div class="navbar-collapse collapse">
              <ul class="nav navbar-nav">
                <li class="active"><a href="#">Home</a></li>
                <li><a href="#about">About</a></li>
                <li><a href="#contact">Contact</a></li>
                <li class="dropdown">
                  <a href="#" class="dropdown-toggle" data-toggle="dropdown">Dropdown <b class="caret"></b></a>
                  <ul class="dropdown-menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li class="dropdown-header">Nav header</li>
                    <li><a href="#">Separated link</a></li>
                    <li><a href="#">One more separated link</a></li>
                  </ul>
                </li>
              </ul>
            </div>
          </div>
        </div>

      </div>
    </div>', N'<!-- Carousel
    ================================================== -->
    <div id="myCarousel" class="carousel slide" data-ride="carousel">
      <!-- Indicators -->
      <ol class="carousel-indicators">
        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
        <li data-target="#myCarousel" data-slide-to="1"></li>
        <li data-target="#myCarousel" data-slide-to="2"></li>
      </ol>
      <div class="carousel-inner">
        <div class="item active">
          <img data-src="holder.js/900x500/auto/#777:#7a7a7a/text:First slide" alt="First slide">
          <div class="container">
            <div class="carousel-caption">
              <h1>Example headline.</h1>
              <p>Note: If you''re viewing this page via a <code>file://</code> URL, the "next" and "previous" Glyphicon buttons on the left and right might not load/display properly due to web browser security rules.</p>
              <p><a class="btn btn-lg btn-primary" href="#" role="button">Sign up today</a></p>
            </div>
          </div>
        </div>
        <div class="item">
          <img data-src="holder.js/900x500/auto/#666:#6a6a6a/text:Second slide" alt="Second slide">
          <div class="container">
            <div class="carousel-caption">
              <h1>Another example headline.</h1>
              <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
              <p><a class="btn btn-lg btn-primary" href="#" role="button">Learn more</a></p>
            </div>
          </div>
        </div>
        <div class="item">
          <img data-src="holder.js/900x500/auto/#555:#5a5a5a/text:Third slide" alt="Third slide">
          <div class="container">
            <div class="carousel-caption">
              <h1>One more for good measure.</h1>
              <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
              <p><a class="btn btn-lg btn-primary" href="#" role="button">Browse gallery</a></p>
            </div>
          </div>
        </div>
      </div>
      <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a>
      <a class="right carousel-control" href="#myCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
    </div><!-- /.carousel -->



    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    <div class="container marketing">

      <!-- Three columns of text below the carousel -->
      <div class="row">
        <div class="col-lg-4">
          <img class="img-circle" data-src="holder.js/140x140" alt="Generic placeholder image">
          <h2>Heading</h2>
          <p>Donec sed odio dui. Etiam porta sem malesuada magna mollis euismod. Nullam id dolor id nibh ultricies vehicula ut id elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Praesent commodo cursus magna.</p>
          <p><a class="btn btn-default" href="#" role="button">View details &raquo;</a></p>
        </div><!-- /.col-lg-4 -->
        <div class="col-lg-4">
          <img class="img-circle" data-src="holder.js/140x140" alt="Generic placeholder image">
          <h2>Heading</h2>
          <p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Cras mattis consectetur purus sit amet fermentum. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh.</p>
          <p><a class="btn btn-default" href="#" role="button">View details &raquo;</a></p>
        </div><!-- /.col-lg-4 -->
        <div class="col-lg-4">
          <img class="img-circle" data-src="holder.js/140x140" alt="Generic placeholder image">
          <h2>Heading</h2>
          <p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>
          <p><a class="btn btn-default" href="#" role="button">View details &raquo;</a></p>
        </div><!-- /.col-lg-4 -->
      </div><!-- /.row -->


      <!-- START THE FEATURETTES -->

      <hr class="featurette-divider">

      <div class="row featurette">
        <div class="col-md-7">
          <h2 class="featurette-heading">First featurette heading. <span class="text-muted">It''ll blow your mind.</span></h2>
          <p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Vestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur. Fusce dapibus, tellus ac cursus commodo.</p>
        </div>
        <div class="col-md-5">
          <img class="featurette-image img-responsive" data-src="holder.js/500x500/auto" alt="Generic placeholder image">
        </div>
      </div>

      <hr class="featurette-divider">

      <div class="row featurette">
        <div class="col-md-5">
          <img class="featurette-image img-responsive" data-src="holder.js/500x500/auto" alt="Generic placeholder image">
        </div>
        <div class="col-md-7">
          <h2 class="featurette-heading">Oh yeah, it''s that good. <span class="text-muted">See for yourself.</span></h2>
          <p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Vestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur. Fusce dapibus, tellus ac cursus commodo.</p>
        </div>
      </div>

      <hr class="featurette-divider">

      <div class="row featurette">
        <div class="col-md-7">
          <h2 class="featurette-heading">And lastly, this one. <span class="text-muted">Checkmate.</span></h2>
          <p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Vestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur. Fusce dapibus, tellus ac cursus commodo.</p>
        </div>
        <div class="col-md-5">
          <img class="featurette-image img-responsive" data-src="holder.js/500x500/auto" alt="Generic placeholder image">
        </div>
      </div>

      <hr class="featurette-divider">

      <!-- /END THE FEATURETTES -->', N'<!-- FOOTER -->
      <footer>
        <p class="pull-right"><a href="#">Back to top</a></p>
        <p>&copy; 2014 Company, Inc. &middot; <a href="#">Privacy</a> &middot; <a href="#">Terms</a></p>
      </footer>

    </div><!-- /.container -->', 0, N'Clear', 1, NULL, N'<link href="Content/bootstrap.min.css" rel="stylesheet">
<link href="Content/Templates/Default/Clear/carousel.css" rel="stylesheet">
	<script src="Scripts/jquery-2.1.4.min.js""></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/Templates/Default/Shared/docs.min.js"></script>')
INSERT INTO [dbo].[DefaultTemplates] ([Header], [Body], [Footer], [Id], [Name], [IsDefault], [UserId], [Links]) VALUES (N' ', N'<div class="site-wrapper">

      <div class="site-wrapper-inner">

        <div class="cover-container">

          <div class="masthead clearfix">
            <div class="inner">
              <h3 class="masthead-brand">Cover</h3>
              <ul class="nav masthead-nav">
                <li class="active"><a href="#">Home</a></li>
                <li><a href="#">Features</a></li>
                <li><a href="#">Contact</a></li>
              </ul>
            </div>
          </div>

          <div class="inner cover">
            <h1 class="cover-heading">Cover your page.</h1>
            <p class="lead">Cover is a one-page template for building simple and beautiful home pages. Download, edit the text, and add your own fullscreen background photo to make it your own.</p>
            <p class="lead">
              <a href="#" class="btn btn-lg btn-default">Learn more</a>
            </p>
          </div>

          <div class="mastfoot">
            <div class="inner">
              <p>Cover template for <a href="http://getbootstrap.com">Bootstrap</a>, by <a href="https://twitter.com/mdo">@mdo</a>.</p>
            </div>
          </div>

        </div>

      </div>

    </div>', N' ', 1, N'Mini', 1, NULL, N'<link href="Content/bootstrap.min.css" rel="stylesheet">
<link href="Content/Templates/Default/Mini/cover.css" rel="stylesheet">
	<script src="Scripts/jquery-2.1.4.min.js""></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/Templates/Default/Shared/docs.min.js"></script>')
INSERT INTO [dbo].[DefaultTemplates] ([Header], [Body], [Footer], [Id], [Name], [IsDefault], [UserId], [Links]) VALUES (N' ', N'<section class="navigation">
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-8 col-md-offset-2 col-sm-8 col-sm-offset-2 col-xs-8 col-xs-offset-2">
              <nav class="pull">
                <ul class="top-nav">
                  <li><a href="#getstarted">Get Started <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                  <li><a href="#media">Media <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                  <li><a href="#features">Features <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                  <li class="nav-last"><a href="#design">Design <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                </ul>
              </nav>
            </div>
          </div>
        </div>
      </section>
      <section class="hero" id="hero">
        <div class="container">
          <div class="row">
            <div class="col-md-12 text-right">
              <a href="#"><i class="fa fa-navicon fa-2x nav_slide_button"></i></a>
            </div>
          </div>
          <div class="row">
            <div class="col-md-8 col-md-offset-2 text-center">
              <h1 class="animated bounceInDown">Реальный заработок с реальным бизнесом.</h1>
              <p class="animated fadeInUpDelay">Как? Ответ на этот вопрос можно получить, просмотрев краткий видеоролик:</p>
            </div>
          </div>
          <div class="row">
            <div class="col-md-2 col-md-offset-5 text-center">
              <a id="userVideoLink" href="" class="hero-play-btn various fancybox.iframe">Play Button</a>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-md-offset-3 text-center">
              <a href="" class="get-started-btn">Начать зарабатывать!</a>
            </div>
          </div>
        </div>
      </section>', N'<footer>
<div class="container">
<div class="row">
<div class="col-md-4">
<p>Nam mi enim, auctor non ultricies a, fringilla eu risus. Praesent vitae lorem et elit tincidunt accumsan suscipit eu libero. Maecenas diam est, venenatis vitae dui in, vestibulum mollis arcu. Donec eu nibh tincidunt, dapibus sem eu, aliquam dolor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum consectetur commodo eros, vitae laoreet lectus aliq</p>
</div>
<div class="col-md-3">
<p>aliquam dolor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum consectetur commodo eros, vitae laoreet lectus aliq</p>
</div>
<div class="col-md-2 col-md-offset-3">
<ul class="footer-nav">
<li><a href="#">Get Started Tutorial</a></li>
<li><a href="#">Introduction Video</a></li>
<li><a href="#">See the Features</a></li>
<li><a href="#">Download a Trial</a></li>
<li><a href="#">Get in Touch!</a></li>
</ul>
</div>
</div>
<div class="row">
<div class="col-md-12 text-center">
<a href="#" class="badge-btn">Badge Button</a>
<p class="footer-credit">Design by <a href="#">RIRAM studio</a> </p>
</div>
</div>
</div>
</footer>', 2, N'Starninght', 1, NULL, N'<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Starnight - A Sexy As Hell Free HTML5/CSS3 Template</title>
    <!-- Bootstrap -->
    <link href="Content/bootstrap.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">
    <link rel="stylesheet" href="Content/Templates/Default/Starninght/flexslider.css" type="text/css">
    <link href="Content/Templates/Default/Starninght/styles.css" rel="stylesheet">
    <link href="Content/Templates/Default/Starninght/queries.css" rel="stylesheet">
    <link href="Content/Templates/Default/Starninght/jquery.fancybox.css" rel="stylesheet">
    <link href=''https://fonts.googleapis.com/css?family=Exo+2&subset=latin,cyrillic'' rel=''stylesheet'' type=''text/css''>
    <!-- Fonts -->
    <link href=''http://fonts.googleapis.com/css?family=Lato:100,300,400,700,900,100italic,300italic,400italic,700italic,900italic'' rel=''stylesheet'' type=''text/css''>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn''t work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap''s JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="Scripts/Templates/Default/Starninght/jquery.fancybox.pack.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/Templates/Default/Starninght/scripts.js?v=1.7"></script>
    <script src="Scripts/Templates/Default/Starninght/jquery.flexslider.js"></script>
    <script src="Scripts/Templates/Default/Starninght/jquery.smooth-scroll.js"></script>
    <script src="Scripts/Templates/Default/Starninght/modernizr.js"></script>
    <script src="Scripts/Templates/Default/Starninght/waypoints.min.js"></script>')
INSERT INTO [dbo].[DefaultTemplates] ([Header], [Body], [Footer], [Id], [Name], [IsDefault], [UserId], [Links]) VALUES (N' ', N'<section class="navigation">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-8 col-md-offset-2 col-sm-8 col-sm-offset-2 col-xs-8 col-xs-offset-2">
                <nav class="pull">
                    <ul class="top-nav">
                        <li><a href="#getstarted">Get Started <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                        <li><a href="#media">Media <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                        <li><a href="#features">Features <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                        <li class="nav-last"><a href="#design">Design <span class="indicator"><i class="fa fa-angle-right"></i></span></a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>
</section>
<section class="hero" id="hero">
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-right">
                <a href="#"><i class="fa fa-navicon fa-2x nav_slide_button"></i></a>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 col-md-offset-2 text-center">
                <h1 class="animated bounceInDown">Реальный заработок с реальным бизнесом.</h1>
                <p class="animated fadeInUpDelay">Как? Ответ на этот вопрос можно получить, просмотрев краткий видеоролик:</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-md-offset-5 text-center">
                <a id="userVideoLink" href="" class="hero-play-btn various fancybox.iframe">Play Button</a>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-3 text-center">
                <a id="userRegistrLink" href="" class="get-started-btn">Начать зарабатывать!</a>
            </div>
        </div>
    </div>
</section>', N'<footer>
<div class="container">
<div class="row">
<div class="col-md-4">
<p>Nam mi enim, auctor non ultricies a, fringilla eu risus. Praesent vitae lorem et elit tincidunt accumsan suscipit eu libero. Maecenas diam est, venenatis vitae dui in, vestibulum mollis arcu. Donec eu nibh tincidunt, dapibus sem eu, aliquam dolor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum consectetur commodo eros, vitae laoreet lectus aliq</p>
</div>
<div class="col-md-3">
<p>aliquam dolor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum consectetur commodo eros, vitae laoreet lectus aliq</p>
</div>
<div class="col-md-2 col-md-offset-3">
<ul class="footer-nav">
<li><a href="#">Get Started Tutorial</a></li>
<li><a href="#">Introduction Video</a></li>
<li><a href="#">See the Features</a></li>
<li><a href="#">Download a Trial</a></li>
<li><a href="#">Get in Touch!</a></li>
</ul>
</div>
</div>
<div class="row">
<div class="col-md-12 text-center">
<a href="#" class="badge-btn">Badge Button</a>
<p class="footer-credit">Design by <a href="#">RIRAM studio</a> </p>
</div>
</div>
</div>
</footer>', 3, N'Starlight_default', 1, NULL, N'<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Starnight - A Sexy As Hell Free HTML5/CSS3 Template</title>
<!-- Bootstrap -->
<link href="Content/bootstrap.min.css" rel="stylesheet">
<link href="http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">
<link rel="stylesheet" href="Content/Templates/Default/Starninght/flexslider.css" type="text/css">
<link href="Content/Templates/Default/Starlight_default/styles.css" rel="stylesheet">
<link href="Content/Templates/Default/Starninght/queries.css" rel="stylesheet">
<link href="Content/Templates/Default/Starninght/jquery.fancybox.css" rel="stylesheet">
<link href=''https://fonts.googleapis.com/css?family=Exo+2&subset=latin,cyrillic'' rel=''stylesheet'' type=''text/css''>
<!-- Fonts -->
<link href=''http://fonts.googleapis.com/css?family=Lato:100,300,400,700,900,100italic,300italic,400italic,700italic,900italic'' rel=''stylesheet'' type=''text/css''>
<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn''t work if you view the page via file:// -->
<!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
<![endif]-->
<!-- jQuery (necessary for Bootstrap''s JavaScript plugins) -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
<script src="Scripts/Templates/Default/Starninght/jquery.fancybox.pack.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="Scripts/bootstrap.min.js"></script>
<script src="Scripts/Templates/Default/Starninght/scripts.js?v=1.7"></script>
<script src="Scripts/Templates/Default/Starninght/jquery.flexslider.js"></script>
<script src="Scripts/Templates/Default/Starninght/jquery.smooth-scroll.js"></script>
<script src="Scripts/Templates/Default/Starninght/modernizr.js"></script>
<script src="Scripts/Templates/Default/Starninght/waypoints.min.js"></script>')

