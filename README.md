# MinimalSelfHostApiClient
This repo contains source code for a series of articles focused on building out an OWIN/Katana-based web API application from scratch. Each article builds upon the work done in the last. In order that the source for each article make sense in the context of the article, I have separated branches relevant to each article.

* **Branch: Master -** Will always contain all of the changes, and be up to date with the most recent article in the series.

The following branch is referenced from the first post in the series, and implements a very basic API client, with no authentication, and no async methods. The post can be found at [ASP.NET Web Api 2.2: Create a Self-Hosted OWIN-Based Web Api from Scratch](http://typecastexception.com/post/2015/01/11/ASPNET-Web-Api-22-Create-a-Self-Hosted-OWIN-Based-Web-Api-from-Scratch.aspx)

* **api-basic -** Implement basic API client to make simple, unauthenticated call to the web API and output to console.

The next branch is referenced from the second article in the series, where we have added a simple example of OWIN-based authentication and authorization to the Web Api. The post can be found at [ASP.NET Web Api: Understanding OWIN/Katana Authentication/Authorization Part I: Concepts](http://typecastexception.com/post/2015/01/19/ASPNET-Web-Api-Understanding-OWINKatana-AuthenticationAuthorization-Part-I-Concepts.aspx)

* **Branch: owin-auth -** Update API to request an access token, and make authenticated calls to the API. 
