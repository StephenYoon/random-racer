# Random Racer
A fun application for exploring various backend and frontend technologies.  
I'm just getting started but you'll see comments and descriptions as I make progress.
  
The general plan at the moment is to take the concepts from the Android dice game I created a while ago of rolling three dice to determine metrics for your racer.
  
This also gives us the opportunity to try out Google's authentication, mainly for the cool factor but also nice to learn.

## Prerequisites
- .NET Core 2.2
- Node.js
- Visual Studio Code or Visual Studio 2017/19
- Microsoft Azure account (Azure DevOps Services and Pipelines)
- Google developer account (Cloud Firebase)

## Data store
Initial thoughts are to use a SQL Server database (Azure) for storing the game metric data, however if the free tier fills up quickly I will switch over to Google's Cloud Firestore as it has the ability to keep our data in sync across client apps through realtime listeners.

## Web API
Nothing fancy here, your standard RESTful .NET Core Web API to `GET` access to various game related resources and to `POST` updates back to the data store.

## Frontend
This will most likely be Angular as I like the framework for keeping things organized with it's dependency injection, components/directives and for getting to know TypeScript and Observables.

## Deployments
CI/CD will be accomplished with Azure DevOps, where commits to master will trigger new Builds and a successful build will trigger a Release.
