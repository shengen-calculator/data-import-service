## data-import-service

**Create a Windows Service**
```
sc create DemoService DisplayName="Demo Service" binPath="C:\full\path\to\Demo.exe"
```
**Start a Windows Service**
```
sc start DemoService
```
**Stop a Windows Service**
```
sc stop DemoService
```
**Delete a Windows Service**
```
sc delete DemoService
```
**Publish Windows Service**
```
cd DataImport.Worker
dotnet publish -r win-x64 -c Release --self-contained
```
**Allow access to gmail**
```
1 - enable the pop at Configurations / Forwarding and POP/IMAP
2 - follow this link: https://www.google.com/settings/security/lesssecureapps 
    and activate for less secure apps.
```
