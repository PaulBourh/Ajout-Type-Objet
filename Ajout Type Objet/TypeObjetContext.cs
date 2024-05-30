using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ajout_Type_Objet.Entites;

namespace Ajout_Type_Objet
{
    public class TypeObjetContext : DbContext
    {
        public TypeObjetContext() : base("TypeObjetContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeObjet>().ToTable("TYPEOBJET");
            modelBuilder.Entity<TypeObjet>().HasKey(x => x.objereviType);
            modelBuilder.Entity<TypeObjet>().Property(x => x.typeobjeIcone);
            modelBuilder.Entity<TypeObjet>().Property(x => x.typeobjeLibelle);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TypeObjet> TypeObjets { get; set; }
    }
}
