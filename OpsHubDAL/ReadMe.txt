Use below link to explore MySQL ORM provider
https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli
SCAFFOLDING Link for MYSQL
https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html

PM> Scaffold-DbContext "server=3.6.247.201;port=3306;user=devstaging;password=RLsp@ixoc!ucRu6riyi;database=OpsHub_1.0.0.4" MySql.Data.EntityFrameworkCore -o DataModel -f
Build started...
Build succeeded.
DEV: Scaffold-DbContext "server=3.6.247.201;port=3306;user=devstaging;password=RLsp@ixoc!ucRu6riyi;database=OpsHub_1.0.0.5" MySql.Data.EntityFrameworkCore -o DataModel -f -Context OpsHubContext
QA : Scaffold-DbContext "server=3.6.247.201;port=3306;user=devstaging;password=RLsp@ixoc!ucRu6riyi;database=OpsHub_1.0.0.4" MySql.Data.EntityFrameworkCore -o DataModel -f -Context OpsHubContext
QF : Scaffold-DbContext "server=3.6.247.201;port=3306;user=devstaging;password=RLsp@ixoc!ucRu6riyi;database=OpsHub" MySql.Data.EntityFrameworkCore -o DataModel -f -Context OpsHubContext
FIFO : Scaffold-DbContext "server=3.6.247.201;port=3306;user=devstaging;password=RLsp@ixoc!ucRu6riyi;database=OpsHub_1.0.0.6" MySql.Data.EntityFrameworkCore -o DataModel -f -Context OpsHubContext

QA TERMINAL CD DAL : dotnet ef dbcontext scaffold "server=localhost;Port=3306;user=root;password=root;database=ops_hub_db" Pomelo.EntityFrameworkCore.MySql -o DataModel -f --context OpsHubContext --no-pluralize