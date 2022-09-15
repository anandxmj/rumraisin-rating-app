# rumraisin-rating-app

RSG: rumraisin 
Sub: Azure subscription 1
SubID: 9697bd55-1f87-4128-a7c7-c82bc0afa083 


Function Apps:
rumraisin-rating-app (lin, custom)
https://rumraisin-rating-app.azurewebsites.net

rumraisin-getratings (win, .net 6)
https://rumraisin-getratings.azurewebsites.net

rumraisin-getrating (win, .net 6)
https://rumraisin-getrating.azurewebsites.net


Logic App:
rumraisin-logicapp (consumption)
https://prod-25.centralus.logic.azure.com:443/workflows/9d81c49b741745279a12e89fc0b6f8ca

App Insights:
rumraisinappinsight
Instrumentation Key: a47de9b3-c896-4d47-92ad-3d9b88a609e1
Workspace: DefaultWorkspace-9697bd55-1f87-4128-a7c7-c82bc0afa083-CUS


Key Vault
Used to store API endpoints (getusers, getproducts)
rumraisinkv
https://rumraisinkv.vault.azure.net/
SECRETS
"getusers"
"getproducts"


Storage (gen purp, v2)
rumraisinstorage
Connection Sting (long):
BlobEndpoint=https://rumraisinstorage.blob.core.windows.net/;QueueEndpoint=https://rumraisinstorage.queue.core.windows.net/;FileEndpoint=https://rumraisinstorage.file.core.windows.net/;TableEndpoint=https://rumraisinstorage.table.core.windows.net/;SharedAccessSignature=sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D


SAS Token:
?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D


Blob service SAS URL:
https://rumraisinstorage.blob.core.windows.net/?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D


File service SAS URL:
https://rumraisinstorage.file.core.windows.net/?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D


Queue service SAS URL:
https://rumraisinstorage.queue.core.windows.net/?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D


Table service SAS URL:
https://rumraisinstorage.table.core.windows.net/?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D

TABLE:
https://rumraisinstorage.table.core.windows.net/ratings/?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-09-17T22:00:59Z&st=2022-09-14T14:00:59Z&spr=https,http&sig=1z%2BQ%2Bc%2B1ub7Z7%2BipDIYrjHYMdr1YsBb7T6fkSwSwqzA%3D



MANAGED IDs
rumraisin-rating-app
ede8d234-db1f-4c0b-88cc-8453cc8f9fe0

rumraisin-logicapp
e15d9c7e-7995-4dec-bc3d-d8b384ffbab1


CosmosDB Info
rumraisin-cosmosmongodb

HOST: rumraisin-cosmosmongodb.mongo.cosmos.azure.com

PORT: 10255

USERNAME: rumraisin-cosmosmongodb

PRIMARY PASSWORD: LRCc2dBrv6BPO30yqRF3BcnrAshSJ68AcZcexrZRTQnaP2P7tpXD1y0jc9rTA6i66ukAlzeiXWNs22EOpLWv7Q==

.NET CONNECTION STRING
string connectionString = 
  @"mongodb://rumraisin-cosmosmongodb:LRCc2dBrv6BPO30yqRF3BcnrAshSJ68AcZcexrZRTQnaP2P7tpXD1y0jc9rTA6i66ukAlzeiXWNs22EOpLWv7Q==@rumraisin-cosmosmongodb.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@rumraisin-cosmosmongodb@";
MongoClientSettings settings = MongoClientSettings.FromUrl(
  new MongoUrl(connectionString)
);
settings.SslSettings = 
  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
var mongoClient = new MongoClient(settings);


PRIMARY CONNECTION STRING
mongodb://rumraisin-cosmosmongodb:LRCc2dBrv6BPO30yqRF3BcnrAshSJ68AcZcexrZRTQnaP2P7tpXD1y0jc9rTA6i66ukAlzeiXWNs22EOpLWv7Q==@rumraisin-cosmosmongodb.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@rumraisin-cosmosmongodb@



App Settings/Env Vars/os.environment Variables
"sas_token" = just the sas token
"table_connection_string" = the full table connection string

