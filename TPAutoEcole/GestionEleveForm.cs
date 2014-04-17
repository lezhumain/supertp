using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TPAutoEcoleDAO;
using TPAutoEcoleDAO.Helpers;

namespace TPAutoEcole
{
    public partial class GestionEleveForm : Form
    {
        public GestionEleveForm()
        {
            InitializeComponent();

            dgvEleve.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvEleve_CellFormatting);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            bindingSourceEleve.DataSource = EleveHelper.Current.GetList();

            EleveHelper.Current.TestLinq();
        }

        private void dgvEleve_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEleve.Columns[e.ColumnIndex].Name == "lessonCount")
            {
                Eleve el = dgvEleve.Rows[e.RowIndex].DataBoundItem as Eleve;

                if (el != null)
                {
                    e.Value = EleveHelper.Current.LessonCount(el);
                }
            }
        }

        private void bindingSourceEleve_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
