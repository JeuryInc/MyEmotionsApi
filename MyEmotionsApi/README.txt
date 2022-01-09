
# Project Name: My Emotions API.
## A short history
In recent years, mental health has been a matter of concern, whether due to confinement due to a pandemic, thinking about that the idea of a shared personal diary was born, where you can write publicly or privately. My Emotions is a personal journal, a safe place, where people can find public emotions from other people. If you feel loving, angry, sad is a good moment to write something, you can define a tag to your post or find other emotions published by clicking on the most popular tags.

## Prerequisites

 - Project targets **.NET Standard 5.0** and **.NET Core 5** at minimum.
 - Docker

## Skills

 - C#.
 - JWT Token Auth.
 - AutoMapper.
 - Clean Code Architecture.
 - Entity framework using Database First.
 - Repository pattern.
 - SQLite (to avoid the need of install any database server).

##  Installation

Clone the repo via git:

    git clone https://github.com/JeuryInc/MyEmotionsApi.git
    cd MyEmotionsApi

 ##  Create a image with docker
    
    cd MyEmotionsApi
    dococker build .

> select the build Id to run the container   "Successfully built **c6724062227a**"

    docker run --name myemotiosapi -d -p 80:80 c6724062227a
    
Go to localhost:80 to access your application in a web browser.
  
## Running the app via VS

Running the migrations:

    dotnet ef migrations add InitialMigration --project MyEmotionsApi -s MyEmotionsApi    
    dotnet ef database update --project MyEmotions.Infrastructure -s MyEmotionsApi

Running the aplication

    dotnet run

Go to https://localhost:5001/swagger/index.html to access your application in a web browser. 