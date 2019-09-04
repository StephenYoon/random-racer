# Random Racer
A fun application for exploring various backend and frontend technologies.  
I'm just getting started but you'll see comments and descriptions as I make progress.
The general plan at the moment is to take the concepts from the Android dice game created a while ago of rolling three dice to determine metrics for your racer.

## Data store:
Initial thoughts are to use a SQL Server database (Azure) for storing the game metric data, however if the free tier fills up quickly I will switch over to Google's Cloud Firestore as it has the ability to keep our data in sync across client apps through realtime listeners.

## Frontend:
This will most likely be Angular as I like the framework for keeping things organized with it's dependency injection, components/directives and for getting to know TypeScript and Observables.

## Deployments:
CI/CD will be accomplished with Azure DevOps, where commits to master will trigger new Builds and a successful build will trigger a Release.
