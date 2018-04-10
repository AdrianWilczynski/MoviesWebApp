using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(1)]
    public class M0001_AddMoviesTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateMoviesTable.sql");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE Movies");
        }
    }
}
