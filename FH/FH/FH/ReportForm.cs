using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FH
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'SalesItemsDataSet.GetSalesData' table. You can move, or remove it, as needed.
            this.GetSalesDataTableAdapter.Fill(this.SalesItemsDataSet.GetSalesData, new DateTime(2014,05,30),new DateTime(2014,06,30) );
           // TODO: This line of code loads data into the 'InventoryDBDataSet.AllInventory' table. You can move, or remove it, as needed.
            this.AllInventoryTableAdapter.Fill(this.InventoryDBDataSet.Inventory);

            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
        }
    }
}
