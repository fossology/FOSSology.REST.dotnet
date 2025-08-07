# ChangeLog - FOSSology.REST.dotnet

## 1.5.0 (2025-08-07)

* Support of more scan/decider options:
    * `ConcludeLicenseType`
    * `CopyrightDeactivation`
    * `CopyrightClutterRemoval`
    * `Patent`
    * `Heritage`
    * `Compatibility`
* More Upload properties
    * `AssigneeDate`
    * `ClosingDate`

## 1.4.1 (2024-10-19)

* Dependency updates, especially RestSharp 108.0.3 to 112.1.0 to fix a security vulnerability.
* `GetVersion()` uses now the endpoint `/info`, because `/version` does not exist anymore.

## 1.4.0 (2023-03-21)

* support REST API 1.5.1 feaures:
    * support new maintenance endpoint.
    * support new upload features.
    * new `GetUploadFileById` to download an upload file.
    * new `GetUploadCopyrights` to get the copyrights for an upload.
* not yet implemented:
    * set permissions for a upload in a folder for different groups (`/uploads/{id}/permissions`)
    * get all the groups with their respective permissions for a upload (`/uploads/{id}/perm-groups`)
    * create a new user (`POST /users`)
    * edit user details by id (`PUT /users`)
    * create a new REST API token (`POST /users/tokens`)
    * get all the REST API tokens for a user (`/users/tokens/{type}`)
    * get all jobs created by all users (`/jobs/all`)
    * return jobs for the given upload id (`/jobs/history`)
    * delete group by id (`/groups/{id}/user/{userId}`)
    * selete group member by groupId and userId (`/groups/{id}/user/{userId}`)
    * importing ...
    * get a list of license candidates from the database (`/license/admincandidates`)

## 1.3.0 (2022-12-30)

* all dependencies updates to the latest version.
  * this fixes the security vulnerabilities of RestSharp and NewtonSoft.Json.
  * adaptations to use RestSharp 108.0.3 **and** FOSSology.
* many additional checks for potential null assignments.
* demo application is now .Net 6.
* tested with FOSSology, version 4.2.1.21 (REST API v1.4.3).

## 1.2.0 (2022-03-17)

* improved error handling.
* supports FOSSology REST API v1.4.3 (FOSSology 4.0.0 built @ 2023/03/09).
* new method GetHealth().
* have `FossologyClient` to be a partial class for better overview.
* new method GetGroupList().
* add support for groups for all methods where it did not yet exist.
* new method GetInfo().
* support for licenses added: new methods GetLicenseList(), GetLicense(), CreateLicense().
* code coverage support
* new classes Hash and Obligation.
* improved unit tests.

## 1.1.0 (2020-06-25)

* supports FOSSology REST API v1.0.16 (FOSSology 3.8.0 built @ 2020/06/19).
* new method GetToken().
* new method UploadPackageFromUrl().
* new method UploadPackageFromVcs().
* new method UploadPackageFromServer() (not tested).
* new method GetUploadSummary()
* new method GetUploadLicenses()
* new class UploadSummary.
* new class UploadLicenses.
* new class VcsUpload.
* new class ServerUpload.
* new class UrlUpload.
* class Upload has new property FileSha1
* Integration test improved.
* support of .Net Core (target is netstandard2.0).
    

## 1.0.0 (2020-11-24)

* first version released.
* supports FOSSology REST API v1.0.3 (FOSSology 3.7.0).