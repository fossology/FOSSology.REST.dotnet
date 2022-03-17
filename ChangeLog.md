# ChangeLog - FOSSology.REST.dotnet

## 1.2.0
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