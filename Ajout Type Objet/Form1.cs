using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Ajout_Type_Objet.Entites;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ajout_Type_Objet
{
    public partial class Form1 : Form
    {
        
        private byte[] fileBytes;
        private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();
            InitialisationGrid();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    textBox1.Text = selectedRow.Cells["objereviType"].Value.ToString();
                    textBox3.Text = selectedRow.Cells["typeobjeLibelle"].Value.ToString();
                    if (!String.IsNullOrEmpty(selectedRow.Cells["typeobjeIcone"].Value.ToString()))
                    fileBytes = (byte[])selectedRow.Cells["typeobjeIcone"].Value;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Selectionner la ligne et non la colonne");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";

            openFileDialog.ShowDialog();

            string filePath = openFileDialog.FileName;
            fileBytes = File.ReadAllBytes(filePath);

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Permet la modification de l'objet typeobjet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string identifiant = dataGridView1.SelectedRows[0].Cells["objereviType"].Value.ToString();
            try
            {
                using (TypeObjetContext context = new TypeObjetContext())
                {

                    TypeObjet typeObjet = context.TypeObjets.Find(identifiant);
                    typeObjet.typeobjeLibelle = dataGridView1.SelectedRows[0].Cells["typeobjeLibelle"].Value.ToString();
                    typeObjet.typeobjeIcone = fileBytes;
                    context.SaveChanges();
                }
                InitialisationGrid();
            }
            catch (Exception)
            {

                MessageBox.Show("Erreur sur la modification");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            fileBytes = null;
            textBox1.Text = null;
            textBox3.Text = null;
        }

        /// <summary>
        /// Permet l'ajout d'un typeobjet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            using (TypeObjetContext context = new TypeObjetContext())
            {
                TypeObjet typeObjet = new TypeObjet
                {
                    objereviType = textBox1.Text,
                    typeobjeIcone = fileBytes,
                    typeobjeLibelle = textBox3.Text
                };

                context.TypeObjets.Add(typeObjet);
                context.SaveChanges();
            }
            InitialisationGrid();
        }

        /// <summary>
        /// Permet la supréssion d'un type objet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string identifiant = dataGridView1.SelectedRows[0].Cells["objereviType"].Value.ToString();

                using (TypeObjetContext context = new TypeObjetContext())
                {
                    TypeObjet typeObjet = context.TypeObjets.Find(identifiant);
                    context.TypeObjets.Remove(typeObjet);
                    context.SaveChanges();
                }
                InitialisationGrid();
            }
            catch (Exception)
            {

                MessageBox.Show("Il faut selectionner une ligne pour pouvoir la suprimer");
            }

        }

        /// <summary>
        /// La méthode InitialisationGrid a pour objectif d'initialiser une grille de données (dataGridView1) en l'implémentant avec 
        /// des données provenant d'une liste d'objets de type TypeObjet. Cette liste est obtenue en appelant une méthode d'une autre classe, Methodes.
        /// </summary>
        public void InitialisationGrid()
        {
            Methodes methodes = new Methodes();
            List<TypeObjet> listTypeObjet = methodes.TypeObjet();

            dataTable = new DataTable();
            dataTable.Columns.Add("objereviType", typeof(string));
            dataTable.Columns.Add("typeobjeIcone", typeof(byte[]));
            dataTable.Columns.Add("typeobjeLibelle", typeof(string));

            dataGridView1.DataSource = dataTable;

            foreach (TypeObjet typeObject in listTypeObjet)
            {
                methodes.AjoutLigne(typeObject.objereviType, typeObject.typeobjeIcone, typeObject.typeobjeLibelle, dataTable);
            }
        }
    }
}
