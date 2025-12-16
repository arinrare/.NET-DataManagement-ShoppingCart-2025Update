# DataManagement (ShoppingCart) � README

Short guide to get this ASP.NET Web Forms project running locally.

## Requirements
- Visual Studio 2019/2022/2026 with .NET desktop & web development workloads
- .NET Framework 4.8
- SQL Server Express (or LocalDB) � SQL Server Management Studio (SSMS) recommended
- NuGet (Visual Studio integrated)

## Project layout
- `DataManagement/` � project folder
  - `App_Data/Products.mdf` � database file used by the app
  - `Web.config` � connection string and EF config
  - `Model1.edmx` � Entity Framework model for the database
  - `DataManagement.aspx` � main page (default document)

## Quick start
1. Open the solution in Visual Studio: open `DataManagement/DataManagement.csproj`.
2. Restore NuGet packages:
   - Right-click the solution ? `Restore NuGet Packages`, or
   - Tools ? NuGet Package Manager ? Package Manager Console and run:
     `Update-Package -reinstall`
3. Ensure the database is available:
   - Recommended: install SQL Server Express and SSMS.
   - In SSMS connect to the instance (common instance name: `.\\SQLEXPRESS`).
   - Attach the MDF file: Databases ? right-click ? `Attach...` ? select `App_Data/Products.mdf`.
   - Rename the attached database to `Products` (right-click ? `Rename`) so it matches the default connection string.

   Alternative: use LocalDB. If you prefer Runtime attach instead of attaching in SSMS, update `Web.config` connection string to use LocalDB:
   ```text
   Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Products.mdf;Integrated Security=True;Multiple Active Result Sets=True;Application Name=EntityFramework
   ```

4. Grant access to the database for your Windows account (if using Integrated Security):
   - In SSMS: Security ? Logins ? find your Windows user (e.g. `DOMAIN\\user`), right click ? Properties ? User Mapping ? check `Products` and give `db_datareader`, `db_datawriter` (or `db_owner` for development).

5. Confirm `Web.config` connection string points to the right instance / database. Example (attached DB named `Products` on SQLEXPRESS):
   ```xml
   <add name="ConnectionString" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" providerName="System.Data.SqlClient" />
   ```

6. Rebuild the project (Build ? Rebuild Solution). Fix any missing references by reinstalling EF if necessary:
   - Tools ? NuGet Package Manager ? Package Manager Console:
     `Update-Package EntityFramework -Reinstall -Project DataManagement`

7. Run the app (F5). If you get a directory listing error, open `http://localhost:<port>/DataManagement.aspx` or ensure the default document is set to `DataManagement.aspx` in `Web.config`.

## Common issues & fixes
- `HTTP Error 403.14 - Forbidden`: Navigate directly to `DataManagement.aspx` or ensure the `<system.webServer><defaultDocument>` is configured (already present in project Web.config).
- `Login failed for user 'YOURPC\\user'`: grant the Windows login access to the `Products` DB in SSMS (User Mapping) or switch to SQL Auth and update `Web.config` (not recommended for dev).
- `Entity Framework` missing / build errors referencing `System.Data.Entity`: restore NuGet packages or run `Update-Package -reinstall`.
- If the attached database name shows the full path, rename it in SSMS to `Products` or update `Initial Catalog` in `Web.config` to match the exact DB name.

## Useful T-SQL (run in SSMS -> New Query)
- Attach an MDF to the instance:
  ```sql
  CREATE DATABASE Products ON (FILENAME = 'G:\Programming\WDTAssignment2\WDTAssignment2\DataManagement\DataManagement\App_Data\Products.mdf') FOR ATTACH;
  ```
- Create a login and map user (Windows login example):
  ```sql
  CREATE LOGIN [MYPC\micha] FROM WINDOWS;
  USE Products;
  CREATE USER [MYPC\micha] FOR LOGIN [MYPC\micha];
  EXEC sp_addrolemember 'db_owner', [MYPC\micha];
  ```

## Notes
- The app uses stored procedure `ProductsSPROC` for all CRUD operations. Ensure the stored procedure exists in the attached database.
- The front-end is a separate solution; point its connection string to the same SQL instance (or reuse the MDF with LocalDB).

If you want, I can add a walkthrough that updates `Web.config` automatically to target LocalDB or `.\SQLEXPRESS`, or create a small PowerShell script to attach the MDF to `SQLEXPRESS`.
