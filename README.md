<h1>RentHome</h1>
<h2>ABOUT my web project:</h2>
<ul>
  <li>RentHome is an online platform for renting and managing properties. Owners can list their properties for long-term rental and/or find long-term management solutions.</li>
  <li>The platform meets owners, potential tenants and potential property managers. Quite often, the owners don't live close to all their properties. They could even live in another country. Managing their properties scattered everywhere, is a challenge.</li>
</ul>
<h3>Database</h3>
<p>Microsoft SQL Server along with Entity Framework Core were used to create and store the values. The database schema consists of the following main entities:</p>
<lu>
  <li>ApplicationUser</li>
  <li>Property</li>
  <li>City</li>
  <li>Country</li>
  <li>Rental</li>
  <li>Contract</li>
  <li>Image</li>
  <li>Vote</li>
  <li>Request</li>
</lu>
<p>See the Schema here: </p>
<a href="https://ibb.co/yh3pWWQ"><img src="https://i.ibb.co/wJv7MMz/Untitled.png" alt="Untitled" border="0"></a>

<h2>Backend</h2>
<p>The web project contains:</p>
<lu>
  <li>28+ service methods</li>
  <li>8 controllers</li>
  <li>13+ views</li>
</lu>

<h2>Testing</h2>
<p>The web project contains:</p>
<lu>
  <li>8+ unit tests</li>
  <li>10+ integration tests</li>
  <li>22+ controllers tests with MyTested.AspNetCore.Mvc</li>
</lu>
<p>Tests cover over 65% of business logic.</p>

<h2>Features</h2>
<p>This web platform allows a guest to the website to view and find listings by city, country, min and/or max price or explore by category or listing status.</p>
<p>A guest can also contact the support of the website. In order to send requests to the owners of the listings, a guest must be registered and signed in.</p>
<p>Signed in user has three main choices:</p>
<ul>
  <li>To List a home, thus becomming a role of <b>owner</b></li>
  <li>To request for renting a home, becomming a <b>tenant</b> if the owner approves the request</li>
  <li>To request for managing a home, becomming a <b>manager</b> if the owner approves the request</li>
</ul>
<p>An <b>Owner</b> can:</p>
<ul>
  <li>Overview his/her Listings and Requests</li>
  <li>Create new property</li>
  <li>Edit his/her properties</li>
  <li>Approve/Reject requests for rent/management/cancel rent/cancel management</li>
  <li>Remove Listing</li>
</ul>
<p>An <b>Manager</b> can:</p>
<ul>
  <li>Overview his/her Listings and Requests</li>
  <li>Edit his/her properties</li>
  <li>Approve/Reject requests for rent/management/cancel rent/cancel management</li>
  <li>Remove Listing</li>
</ul>
<p>There is also an <b>Admin</b> who can:</p>
<ul>
  <li>reviews the uploaded properties and approves/disapproves them</li>
</ul>
<h2>Technologies Used</h2>
<p>This website is designed and runs using the main technologies below:</p>
<lu>
  <li>C#</li>
  <li>ASP.NET MVC 5.0</li>
  <li>EntityFrameworkCore 5.0.8</li>
  <li>MS SQL Server</li>
  <li>Bootstrap 4</li>
  <li>JavaScript</li>
  <li>HTML5</li>
  <li>CSS</li>
  <li>MS Visual Studio 2019</li>
  <li>MSSQL Server</li>
  <li>Theme - Locals by Colorlib</li>
  <li>MailKit 2.14.0 (Email sender by Google)</li>
  <li>AJAX real-time Requests</li>
  <li>jQuery</li>
  <li>Areas (Administrator)</li>
  <li>scaffolded Identity</li>
</lu>
Project Template: Nikolay Kostov's Template: https://github.com/NikolayIT/ASP.NET-Core-Template
<hr>
This website has been created solely for educational purposes.
