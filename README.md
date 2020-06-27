# FOSSology.REST.dotnet

This is a .NET implementation of the REST API of FOSSology.

## Project Build Status ##
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/badge/nuget%20package-v1.1.0-blue.svg)](https://www.nuget.org/packages/Fossology.Rest.Dotnet/)

FOSSology is a open source license compliance software system.
It is used to analyze source code and to determine licenses,
copyrights and other keywords within the given source code.
More information about FOSSology can be found [here](https://www.fossology.org/.)

## Library ##
* **Fossology.Rest.Dotnet** - The REST API implementation.
* **Fossology.Rest.Dotnet.Model** - The object model.
* **Fossology.Rest.Dotnet.Test** - Unit tests.

## Test Application ##

**FossyApiDemo** - A simple WinForms based demo application.

## Documentation on the FOSSology REST API

* Read https://www.fossology.org/get-started/basic-rest-api-calls/
* Open Swagger editor at http://editor.swagger.io/
* Load FOSSology API description from 
  https://raw.githubusercontent.com/fossology/fossology/master/src/www/ui/api/documentation/openapi.yaml
* Modify in the editor the server URL so match your instance
* Use the access token generated by FOSSology to authorize
  Swagger to access your FOSSology instance.

## How to setup FOSSology

* Install Docker for Windows
* Switch to Linux Containers
* Open a PowerShell window and run 
  ```docker pull fossology/fossology```
* Start FOSSology via the command
  ```docker run -p 8081:80 fossology/fossology```
* Open the following URL in a browser: 
  http://localhost:8081/repo/
* Login via the admin account
  user = ```fossy```, pwd = ```fossy```
* Go to ```admin/users/edit user account``` and create an API token or
  use the appropriate REST call to generate a token.
* Optionally create a folder and upload a package 

## License

Copyright (C) 2019-2020 T. Graf

Licensed under the **MIT License** (the "License");  
you may not use this file except in compliance with the License.  
You may obtain a copy of the License at

       https://opensource.org/licenses/MIT

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and limitations under the License.
