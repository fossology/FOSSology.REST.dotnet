# ChangeLog - FOSSology.REST.dotnet

## 1.1.0 (NEXT)
* supports FOSSOlogy REST API v1.0.16 (FOSSology 3.8.0).
* ?? /uploads/{id}/summary:
* ?? /uploads/{id}/licenses:
* ?? search using groupName
* ?? new param groupName for report generation
* ?? ojo for license decider

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
    

## 1.0.0 (2020-11-24)
* first version released.
* supports FOSSOlogy REST API v1.0.3 (FOSSology 3.7.0).