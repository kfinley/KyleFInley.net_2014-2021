# KyleFinley.net (2014 - 2021) [ARCHIVED]

This is the last snapshot of the source code that ran my personal website (kylefinley.net) from early 2014 to 2021. The site used an early version of my personal .NET framework platform code. I no longer work in pre .net core code and I've migrated bits from these versions that I still use many years ago so I've decided to release the source here. 

This source includes a lot of my earlier ideas on command dispatching using custom task managment (async w/o writing async/await code), dynamic model binding using AutoMapper in asp.net mvc, and various best practices in .net layered design such as an example of EF Repository pattern code first based asp.net mvc application development in .NET 4.6, and 

There's also a reverse engineered implementation of the now closed Goo.gl url shortener analytics service. Short URLs were created for sharing articles and registered with the goo.gl service api when an article is created. Then when reviewing articles in the CMS portal the click stats for these social media platform targeted URLs were retrieved and displayed in the page using the goo.gl service JS and using a style that matched the UI from the Goo.gl site. This code can be found in the 928.UrlShortener project and in KyleFinley.Web/Views/ShortUrlAnalytics.

This code is old and not used anymore so I'm releasing it here as an archive for educational and reference purposes.

There is no license on any of this code. Use at your own risk. 
