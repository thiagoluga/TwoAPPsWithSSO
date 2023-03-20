# TwoAPPsWithSSO

First you may need to create a folder for the database:
C:\db\Identity.sqlite

You can change this in the appsettings of IdentityApp

Now set both Apps to run in Visual Studio

<h1>IdentityWithReactLogin</h1>
This app is responsible for Create And Login Users

For now it is only possible to Create User with Swagger. But when it is created, open the ReactApp and Login with the new User. When it is logged, you will be redirected to the PersonalInfo App.

Here are the URLs:
* API Swagger: http://localhost:4001
* React App: https://localhost:4002

<h1>PersonalInfoAppWithReact</h1>
This app is responsible for fetch data when User is authenticated

Here are the URLs:
* API Swagger: http://localhost:9001
* React App: https://localhost:9002