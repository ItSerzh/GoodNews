using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAnalyzer.DAL.Core.Migrations
{
    public partial class RssSuorces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into rsssource(Id, Name, Url) values('8D9F31B8-8028-4E7C-89AB-48309B3448A7', 'Shazoo', 'https://shazoo.ru/feed/rss')");
            migrationBuilder.Sql("insert into rsssource(Id, Name, Url) values('E3512D7D-381A-4655-8B60-584C08D9254A', 'Onliner', 'https://onliner.by/feed')");
            migrationBuilder.Sql("insert into rsssource(Id, Name, Url) values('6917CA9A-1996-44E9-9C04-6D17FE22ACAB', '4pda', 'https://4pda.to/feed/')");
            migrationBuilder.Sql("insert into rsssource(Id, Name, Url) values('4B2F67F4-5F2F-453A-ABC4-D59F30F85DC0', 'Wylsa', 'https://wylsa.com/feed/')");
            migrationBuilder.Sql("insert into rsssource(Id, Name, Url) values('05216775-77D7-4D91-BDCA-218C448E4CE8', 'Igromania', 'https://www.igromania.ru/rss/all.rss')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from rsssource where Id = '8D9F31B8-8028-4E7C-89AB-48309B3448A7')");
            migrationBuilder.Sql("delete from rsssource where Id = 'E3512D7D-381A-4655-8B60-584C08D9254A')");
            migrationBuilder.Sql("delete from rsssource where Id = '6917CA9A-1996-44E9-9C04-6D17FE22ACAB')");
            migrationBuilder.Sql("delete from rsssource where Id = '4B2F67F4-5F2F-453A-ABC4-D59F30F85DC0')");
            migrationBuilder.Sql("delete from rsssource where Id = '05216775-77D7-4D91-BDCA-218C448E4CE8')");
        }
    }
}
