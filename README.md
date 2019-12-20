# Video Locadora

Aplicação de Vídeo Locadora desenvolvida em ASP.NET MVC, Entity Framework, Dapper, Owin e Bootstrap.

## Ferramentas utilizadas

Visual Studio 2017, Banco de Dados SQL Server e Microsoft Azure.

## Instruções

No arquivo Web.config está configurado a connectionString do LocalDB

```
<add name="VideoLocadora" providerName="System.Data.SqlClient" connectionString="Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=VideoLocadora;Integrated Security=True;"/>
```
Caso queira rodar utilizando a base do Azure, deve-se mudar a connectionString conforme exemplo abaixo. Por motivo de segurança não deixei a senha correta :-)

```
<add name="VideoLocadora" providerName="System.Data.SqlClient" connectionString="Server=tcp:pubazure.database.windows.net,1433;Initial Catalog=VideoLocadora;Persist Security Info=False;User ID=pubazure;Password=senha;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" />
```
Execute o Clean Solution e depois o Build Solution no projeto e utilize a aplicação.

