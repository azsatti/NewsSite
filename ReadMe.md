# News Website
## Time spent
    5 hours
    
## What's done (Functional)
- CRUD operations for news are implemented

## What's done (Technical)
### News.API
- Web API project is created to expose the news controller
- Swagger has been used to document and test the API
- API Key middleware has been created (Not full implemented) to secure the API
- CORS policy has been implemented to allow cross domain calls to the API
- Solid principles has been used where possible (Could be improved further)
- Dependency injuction has been implemented using constructor injuction
- In memory database has been used (could be replaced SQL Server/EF code first etc)

### News.Web
- Web MVC project is created to consume the News.API
- MVC controller/views etc has been created and CRUD functionality is fully working

### News.Web.UI
- Vue.JS has been used to consume the News.API (Only get method is complete)
- Vue routing has been implemented

### News.API.UnitTests
- Nunit and Moq has been used to create tests

## Could have done with more time
- Complete remaining functional requirements
- Refactor code 
- More Unit/functional testing






  
