# StrangeGt.ForzaMotorsport
Create a UDP Client for Forzamotorsport 7  
## Common Dll .Net Standard 2.0 Libraries
### StrangeGt.ForzaMotorsport.Listener
Class for UDP Client, and data struct
### StrangeGt.ForzaMotorsport.Data
DBContext for save data to sqlite using EntityFramework
## Console aplicacion .Net Core
### StrangeGt.ForzaMotorsport.Cli
.Net Core console main project, read data amd write to console and sqlite  
Tested on W10 and Mac  
## Xamarin Forms Projects
Projects to read data  
### StrangeGt.ForzaMotorsport.App
Library with the Xamarin forms, only present data, no sql save yet.  
### StrangeGt.ForzaMotorsport.App.Android
Android project  
### StrangeGt.ForzaMotorsport.App.iOS
iOs project  
### StrangeGt.ForzaMotorsport.App.UWP
Windows version  
