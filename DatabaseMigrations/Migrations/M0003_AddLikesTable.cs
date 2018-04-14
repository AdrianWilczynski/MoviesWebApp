using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(3)]
    public class M0003_AddLikesTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateLikesTable.sql");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE Likes");
        }
    }
}
