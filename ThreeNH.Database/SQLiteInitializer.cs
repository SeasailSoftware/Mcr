using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Database
{
    internal class SQLiteInitializer : SqliteCreateDatabaseIfNotExists<ColorDatabase>
    {
        public SQLiteInitializer(DbModelBuilder modelBuilder) : base(modelBuilder,true)
        {
        }

    }
}
