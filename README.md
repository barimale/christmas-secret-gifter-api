# Mini-server configuration
```
Public IP: 94.132.173.156
Local IP: 192.168.2.100

username: albergue
password: albergue 
```

```
scp -r ./bin/Release/net5.0/publish/* albergue@192.168.2.100:/var/www/albergue.administrator

sudo cp ./Properties/albergue-administrator.service /etc/systemd/system/albergue-administrator.service
sudo systemctl daemon-reload
cd /etc/systemd/system
sudo systemctl enable albergue-administrator.service
sudo sudo journalctl -fu albergue-administrator.service

```

https://localhost:5021;

SQLite:
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet ef database update --connection "Data Source=My.db"
```

# Algorithm
```
https://developers.google.com/optimization/assignment/assignment_example
```