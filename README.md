# StatisticSearch

Getting Started

##### The code: 
Clone the code files from 

1. git clone https://github.com/ivanpchelnikov/StatisticSearch.git
2. Build in the Visiual Studio or 
   from CLI inside solution folder: StatisticSearch:
      <b>run dotnet</b>

##### Web page:
```
https://statisticsearch20200316085645.azurewebsites.net/
```
##### Web Api:
```
https://statisticsearch20200316085645.azurewebsites.net/{keywords}/{urltag}
```
I have use simple serach quiries for each search engine:

for google: 
```
/search?q={keywords}&num=100
```
for bing: 
```
/search?q={keywords}&vount=100
```
It is easy to extend to more search providers
##### Technical details:

- Dependency injection to separate service implementation from servcie interface.
- Inheritance: all services uses absract class where I pass the folloing parameters:
- Using MemoryCache for keeping results for 60 minutes.
- For scraping html page use .Net core library  HtmlAgilityPack.
  We have to prepare html tags, which repeats on html page and wrapp the search result. 
  Sample is 
 ```
   <div class= "TbwUpd NJjxre" > 
     searchresult ...
   </div>
 ```  
converts to "//div[contains(@class, 'TbwUpd NJjxre')]"



For UI I use SPA with javascripts to update the result.
