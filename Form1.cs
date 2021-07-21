/* Matthew Delapasse
 * 7/21/2021
 * A complete database management system for the books database.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace AUthorsTableInputForm
{
    public partial class frmAuthors : Form
    {
        public frmAuthors()
        {
            InitializeComponent();
        }
        
        //level declarations that will be used in the frmAuthors_Load
        SqlConnection booksConnection;
        SqlCommand authorsCommand;
        SqlDataAdapter authorsAdapter;
        DataTable authorsTable;
        CurrencyManager authorsManager;

        private void frmAuthors_Load(object sender, EventArgs e)
        {
            //connect to the books database
            string fullfile = Path.GetFullPath("SQLBooksDB");
            booksConnection = new SqlConnection("Data Source=.\\SQLEXPRESS01; AttachDbFilename=" + fullfile + ";Integrated Security=True; Connect Timeout=30; User Instance=True");

            //This tested to see if the connection worked
            //MessageBox.Show("the connection was successfull");

            //establish command object
            authorsCommand = new SqlCommand("SELECT * FROM Authors ORDER BY Author", booksConnection);

            ////connection object established
            //MessageBox.Show("The connection object established.");

            //esablish data adapter/data table
            authorsAdapter = new SqlDataAdapter();
            authorsAdapter.SelectCommand = authorsCommand;
            authorsTable = new DataTable();
            authorsAdapter.Fill(authorsTable);

            //bind controls to data table
            txtAuthorID.DataBindings.Add("Text", authorsTable, "Au_ID");
            txtAuthorName.DataBindings.Add("Text", authorsTable, "Author");
            txtYearBorn.DataBindings.Add("Text", authorsTable, "Year_Born");

            //establish currency manager
            authorsManager = (CurrencyManager)this.BindingContext[authorsTable];
        }

        private void frmAuthors_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close the connection 
            booksConnection.Close();

            //dispose of the objects
            booksConnection.Dispose();
            authorsCommand.Dispose();
            authorsAdapter.Dispose();
            authorsTable.Dispose();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            authorsManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            authorsManager.Position++;
        }
    }
}
