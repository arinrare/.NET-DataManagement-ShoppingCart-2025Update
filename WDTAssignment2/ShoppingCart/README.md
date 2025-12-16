# ShoppingCart — Setup & Run (Windows / Visual Studio)


## Overview
•	ASP.NET MVC (.NET Framework 4.8) shopping-cart sample.
•	Entity Framework 6 (database-first EDMX).
•	Uses a SQL Server database shipped as an MDF (in App_Data) or an attached SQL Server Express database.

## Prerequisites
•	Visual Studio 2019/2022/2023/2026 with .NET Framework 4.8 workload.
•	SQL Server Express (instance e.g. .\SQLEXPRESS) or LocalDB installed.
•	SQL Server Management Studio (SSMS) recommended.
•	NuGet package restore enabled.

## Repository layout (important files)
•	ShoppingCart/ShoppingCart.sln — solution file
•	Products.edmx — EF model (CSDL/SSDL/MSL embedded)
•	Products.mdf — database file shipped with project
•	Web.config — connection strings & EF config
•	ShoppingCart/Controllers/* — app controllers
•	ShoppingCart/Models/* — model classes (including ProductPartialClass.cs)

## Quick setup (recommended)

1.	Clone the repo and open the solution in Visual Studio:
•	git clone <repo-url>
•	Open ShoppingCart/ShoppingCart.sln.

2.	Restore NuGet packages:
•	Visual Studio will usually restore on solution open.
•	Or run: nuget restore ShoppingCart.sln or use Visual Studio Package Manager -> Restore.

3.	Prepare the database: Option A — use local MDF in App_Data (default)
•	Confirm Products.mdf and App_Data/aspnet-ShoppingCart-20150715091434.mdf exist.
•	Ensure Web.config uses LocalDB connection:
•	Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|Products.mdf;... Option B — attach MDF to SQL 

Server Express (recommended if LocalDB unavailable)
•	In SSMS connect to .\SQLEXPRESS (or MIKE-PC\SQLEXPRESS).
•	Right-click Databases → Attach → Add → select Products.mdf.
•	Choose an attach name (e.g. ShoppingCartProducts).
•	Update Web.config connection string ProductsEntities to:
•	Data Source=.\SQLEXPRESS;Initial Catalog=ShoppingCartProducts;Integrated Security=True;
•   MultipleActiveResultSets=True;App=EntityFramework
•   Grant access to the database for your Windows account (if using Integrated Security):
   - In SSMS: Security ? Logins ? find your Windows user (e.g. `DOMAIN\\user`), right click ? Properties ? User Mapping ? check `Products` and give `db_datareader`, `db_datawriter` (or `db_owner` for development).

•Confirm `Web.config` connection string points to the right instance / database. Example (attached DB named `Products` on SQLEXPRESS):
   ```xml
   <add name="ConnectionString" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=ShoppingCartProducts;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" providerName="System.Data.SqlClient" />
   ```

4.	Verify EF connection name:
•	The generated EF context class name is ShoppingCartProductsEntities (or ProductsEntities if you rename the model container). Ensure the connection string name matches the context or rename the context/container in the EDMX designer.

5.	Rebuild and run:
•	Build → Rebuild Solution.
•	Start Debugging (F5). Navigate to /Shop to verify storefront.
Regenerating the EDMX (if you change DB or schema)

## If you need to regenerate the model:

1.	Delete the old Products.edmx and generated files (Products.Context.cs, Products.Designer.cs) if you want a clean regenerate (keep custom partial classes).
2.	Right-click Models → Add → New Item → ADO.NET Entity Data Model → EF Designer from database.
3.	Connect to the DB (use .\SQLEXPRESS and the database name).
4.	Select tables and finish.
5.	Save and Run Custom Tool on Products.edmx if necessary.
6.	Rebuild.

### Notes about code & runtime
•	Product is used as a dictionary key; a partial class ProductPartialClass.cs implements Equals(object)/GetHashCode().
•	The generated context in this repo is ShoppingCartProductsEntities — controllers were updated to use it. If you change the context name, update controllers or connection string accordingly.
•	EDMX is embedded in the assembly (res://*/Models.Products.csdl|...). If you edit EDMX manually, regenerate code and re-embed resources (save + rebuild).

### Troubleshooting
•	Schema errors (EDMX validation):
•	Ensure EDMX CSDL properties match SSDL keys (case-sensitive) and primary keys have StoreGeneratedPattern="Identity" when appropriate.
•	If you see "Properties referred by the Dependent Role ... must be a subset of the key" — regenerate the EDMX from the database instead of manual edits.
•	Database connection errors:
•	Verify .\SQLEXPRESS is running (Services or Get-Service).
•	Start SQL Server Browser if Visual Studio cannot discover instances.
•	Try MIKE-PC\SQLEXPRESS or localhost\SQLEXPRESS as server name if discovery fails.
•	If using LocalDB, ensure (LocalDb)\MSSQLLocalDB or (LocalDb)\v11.0 is installed.
•	File locks on build:
•	Close Visual Studio / IIS Express instances, delete bin/obj, clear %TEMP%\Temporary ASP.NET Files and rebuild.
•	If EDMX designer shows namespace/version inconsistencies:
•	Recreate the EDMX via wizard (EF6 target) rather than editing XML manually.

### Useful commands (PowerShell / developer prompt)
•	Clean and rebuild solution:
•	msbuild ShoppingCart.sln /t:Clean
•	msbuild ShoppingCart.sln /t:Rebuild
•	Remove build artifacts (close Visual Studio first):
•	Delete bin/, obj/ under project folder.
•	View SQL services:
•	Get-Service *SQL* | Select Name, Status, DisplayName