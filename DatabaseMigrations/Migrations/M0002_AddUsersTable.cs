using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(2)]
    public class M0002_AddUsersTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateUsersTable.sql");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE Users");
        }
    }
}
