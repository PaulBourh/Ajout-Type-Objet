using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ajout_Type_Objet.Entites;

namespace Ajout_Type_Objet
{
    public class Methodes
    {
        public List<TypeObjet> TypeObjet()
        {

            List<TypeObjet> listTypeObjet = new List<TypeObjet>();

            using (TypeObjetContext context = new TypeObjetContext())
            {

                // Lire les entités
                listTypeObjet = context.TypeObjets.ToList();
            }
            return (listTypeObjet);
        }

        public void AjoutLigne(string objereviType, byte[] typeobjeIcone, string typeobjeLibelle, DataTable dataTable)
        {
            dataTable.Rows.Add(objereviType, typeobjeIcone, typeobjeLibelle);
        }

    }
}
