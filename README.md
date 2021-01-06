# HistoryNET

## About 

This application scrapes data from Wikipedia regarding important events on a certain day, saves them into a database  and exposes them from API endpoints. 
Information about the endpoints can be found in the swagger page.

The Web API is written using .NET Core, uses EFCore to store the scraped data and is documment using Swagger.

There is also a front-end part written in ReactJS to display data fetched from the API endpoints.The API must be started for the React App to work properly (the code for the client app is in history.webclient).


## Usage

###### Steps to run the .NET API:
1. Run Update-Database to generate the database.
2. Run History.API to generate the data if it is the first time you are running the project. This may take a while as it looks for all days from the year! 
3. (Optional) Modify PageScraper.cs if you don't want to download all data from Wikipedia(whole year) or you want only certain Events.
4. That's it! You can now access the database using the endpoints.

###### Steps to run the ReactJS front-end:

1.Ensure you have the latest npm and nodejs versions installed.
2.Ensure the API is running.
3.Enter the folder and type in the command line 'npm install' and then 'npm start'
4.That's it!

## Example

#### .NET API

##### Swagger documentation
![Swagger Main Page](https://user-images.githubusercontent.com/16376173/103785305-b84cbd80-5043-11eb-8f70-e13a87a6a643.PNG)
##### /api/events/GetAllEventsForDay endpoint example
![GetAllEventsForDay endpoint](https://user-images.githubusercontent.com/16376173/103785306-b8e55400-5043-11eb-95b3-26a20b721d52.PNG)
##### /api/events/{id} endpoint example
![/api/event/{id}](https://user-images.githubusercontent.com/16376173/103785308-b8e55400-5043-11eb-95b8-e404dc985d4e.PNG)

### ReactJS App
##### Main Page
![Main Page](https://user-images.githubusercontent.com/16376173/103785309-b97dea80-5043-11eb-997a-d1d16776476d.PNG)
##### event-list Page
![List Events](https://user-images.githubusercontent.com/16376173/103785299-b7b42700-5043-11eb-875d-a155bfbe7203.PNG)


