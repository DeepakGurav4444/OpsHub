Use below link to explore MySQL ORM provider
https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli
SCAFFOLDING Link for MYSQL
https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html

QA TERMINAL CD DAL : dotnet ef dbcontext scaffold "server=localhost;Port=3306;user=root;password=root;database=ops_hub_db" Pomelo.EntityFrameworkCore.MySql -o DataModel -f --context OpsHubContext --no-pluralize