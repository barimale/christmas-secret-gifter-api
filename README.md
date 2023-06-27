# Todos
check why constraints and objectives - maybe not needed to use 
local refs for them as it doesn't look meaningfully.

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

# Format and lint
```
dotnet tool install -g dotnet-format
```
Usage:
```
dotnet-format .\Christmas.Secret.Gifter.API\
```

# TypeGen
Install:
```
dotnet tool install --global dotnet-typegen
```
Rebuild the solution and then by being in the root directory execute:
```
dotnet-typegen --project-folder  ./Christmas.Secret.Gifter.Domain generate
```
# Frontend Extensions - VS Code
## Generate barrels
```
TypeScript Barrel Generator
```
## Firebase setup
Install firebaseCLI from the official website, then execute:
```
npm install -g firebase-tools
```
## ESLint & Prettier - VS Code
```
npm install -D eslint prettier eslint-config-prettier --force
npx eslint --init
```

# Database.SQLite:
navigate to the database project directory first.
Then execute as follows:
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet ef database update --connection "Data Source=gifter.db"
```

# Known issues
Nuget: invalid data while decoding:
```
dotnet nuget locals all --clear
```

# Algorithm
```
https://developers.google.com/optimization/assignment/assignment_example
```

# Heroku deployment
## Prereqs
```
heroku-cli
docker
```
# First steps
```
choco install heroku-cli
heroku login
heroku container:login
heroku create
heroku stack:set container
```
As a result the app in Heroku is created.
## Startup
Each time, you want to deploy the app to Heroku:
```
heroku login
heroku container:login
heroku container:push web -a lit-spire-23553
heroku container:release web -a lit-spire-23553
```
or:
```
heroku login
heroku container:login
heroku git:remote -a lit-spire-23553
heroku stack:set container
git push heroku main
```
In case of any error:
```
heroku logs --tail
heroku ps:scale web=1
```

# Private Server
```
https://askubuntu.com/questions/716429/how-to-put-my-server-on-the-internet
```