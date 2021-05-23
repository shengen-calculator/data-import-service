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
