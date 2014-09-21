using KMorcinek.YetAnotherTodo.Models;
using SisoDb;
using SisoDb.SqlCe4;
using System.Collections.Generic;

namespace KMorcinek.YetAnotherTodo
{
    public class DbRepository
    {
        public static ISisoDatabase GetDb()
        {
            return "Data source=|DataDirectory|NotesDB.sdf;".CreateSqlCe4Db();
        }

        public static void Initialize()
        {
            var db = DbRepository.GetDb();

            if (db.Exists() == false)
            {
                Topic forNow = new Topic
                {
                    Name = "For now",
                    IsShown = true,
                    Notes = new List<Note>{
                        new Note{Id = 1, Content = "Drink"},
                        new Note{Id = 2, Content = "Eat"},
                    }
                };

                Topic forLate = new Topic
                {
                    Name = "For later",
                    IsShown = true,
                    Notes = new List<Note>{
                        new Note{Id = 1, Content = "Sleep"},
                        new Note{Id = 2, Content = "Sleep more"},
                    }
                };

                Topic obsoleteStuff = new Topic
                {
                    Name = "Obsolete stuff",
                    IsShown = false,
                    Notes = new List<Note>{
                        new Note{Id = 1, Content = "Learn spanish"},
                        new Note{Id = 2, Content = "Learn PHP"},
                    }
                };

                db.EnsureNewDatabase();
                db.UseOnceTo().Insert(forNow);
                db.UseOnceTo().Insert(forLate);
                db.UseOnceTo().Insert(obsoleteStuff);

            }
        }
    }
}