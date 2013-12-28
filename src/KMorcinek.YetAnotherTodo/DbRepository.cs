using SisoDb;
using SisoDb.SqlCe4;

namespace KMorcinek.YetAnotherTodo
{
    public class DbRepository
    {
        public static ISisoDatabase GetDb()
        {
            return "Data source=|DataDirectory|NotesDB.sdf;".CreateSqlCe4Db();
        }
    }
}