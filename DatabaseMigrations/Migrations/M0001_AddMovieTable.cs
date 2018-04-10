using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(1)]
    public class M0001_AddMovieTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateMovieTable.sql");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE Movie");
        }
    }
}
