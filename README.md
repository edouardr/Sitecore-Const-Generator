# Sitecore Helix ConstGenerator

This NuGet Package will generate (via T4 template) the static fields representing all templates and relating fields of an Sitecore Helix Module.
It uses the Sitecore Item WebAPI v1 in order to retrieve the template information.

## Dependencies
* log4net 2.0.8
* Newtonsoft.Json v6.0.8 -> in accordance to Sitecore's version
* RestSharp 105.2.3
* Sitecore.Kernel.NoReferences 8.2.170407
* Sitecore.ItemWebAPI.NoReferences 8.2.170407

## Getting started

In your Helix module install the NuGet package

Via the package manager

> Install-Package Sitecore.Helix.ConstGenerator

## How to set it up

After installing the NuGet Package, the following files are added to the project:
* Templates.tt
* App_Config/Include/WebApiDefinition.config

### Patch config
First, update the config file by entering your site name in the patch node 
```
...
<site name="[YOUR_SITE_NAME]">
...
```

Then publish this configuration file to your website. Sitecore Item WebAPI is now active.

### Template config
Open the Template.tt. Enter the correct values in the below sections.

```C#
/************************************************************
*															*
*						Settings							*
*															*
************************************************************/
  const string baseUrl = @"http://YOUR_URL_HERE";
  const string templateRootPath = @"/your/path/here";

/************************************************************
*															*
*					 Authentication							*
*															*
************************************************************/
  const bool useAuthenticatedRequest = false;
  const string userName = @"";
  const string password = @"";
```

An example could be:

```C#
/************************************************************
*															*
*						Settings						    *
*															*
************************************************************/
  const string baseUrl = @"http://habitat.dev.local";
  const string templateRootPath = @"/sitecore/templates/Feature/Media";

/************************************************************
*															*
*					 Authentication						    *
*															*
************************************************************/
  const bool useAuthenticatedRequest = true;
  const string userName = @"admin";
  const string password = @"b";
```

NOTE: It is mandatory to set your credentials if in the config file the 'AllowAnonymousAccess' has been set to false.

## Troubleshooting
