# StatisticSearch

Your Solution Documentation

Getting Started

##### The code: 
Clone the code files from https://github.com/ivanpchelnikov/StatisticSearch.git

##### Web page:
https://statisticsearch20200316085645.azurewebsites.net/
##### Web Api:
https://statisticsearch20200316085645.azurewebsites.net/{keywords}/{urltag}

I have use simple serach quiries for each search engine:

for google: /search?q={keywords}&num=100
for bing: /search?q={keywords}&vount=100

It is easy to extend to more search providers

Dependency injection to inject service implementation.

All services uses absreact class where I pass the folloing parameters:

httpclient, _tagSearch for scrapping web page like "//div[contains(@class, 'TbwUpd NJjxre')]"

For scarrping I use HtmlAgilityPack.

For cashing I use MemoryCache.

For UI I use SPA with javascripts to update the result.
