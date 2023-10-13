# ManageRegStds

### Configuration

1. Open the `appsettings.json` file.
2. Locate the `ConnectionStrings` section and set the `ManageRegStdsContextConnection` connection string to your SQL Server database.
3. Save the changes.

### Database Migration

1. Open the Package Manager Console.
2. Run the following commands to create and apply the database migrations:
   
   ```bash
   Add-Migration InitialCreate
   Update-Database
