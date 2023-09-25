using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Database
{
    public class ColorDatabase : DbContext
    {
        public ColorDatabase() : base("Colors.db") { }

        public ColorDatabase(DbConnection connection)
            : base(connection, true) { }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // SQLite EF6的实现不会自动创建，所以要设置
            System.Data.Entity.Database.SetInitializer(new SQLiteInitializer(modelBuilder));

            base.OnModelCreating(modelBuilder);
        }

        public async Task AddInstrument(InstrumentEntity entity)
        {
            var temp = await Instruments.FirstOrDefaultAsync(o => o.Name == entity.Name);
            if (temp != null) throw new Exception("Instrument name exits");
            temp = await Instruments.FirstOrDefaultAsync(o => o.SN == entity.SN);
            if (temp != null)
                throw new Exception("Instrument SN exits");
            Instruments.Add(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteInstrument(int id)
        {
            var entity = Instruments.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                Instruments.Remove(entity);
                await SaveChangesAsync();
            }
        }

        public InstrumentEntity FindInstrumentBySN(string sN)
        {
            return Instruments.FirstOrDefault(x => x.SN == sN);
        }

        public void UpdateWhiteboardData(int id, string sample)
        {
            var instrument = Instruments.FirstOrDefault(o => o.Id == id);
            if (instrument != null)
            {
                instrument.WhiteboardData = sample;
                SaveChanges();
            }
        }

        public DbSet<InstrumentEntity> Instruments { get; set; }
    }
}
