using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace FH.ReportForms
{
    public partial class SalesReportForm : Form
    {
        private DateTime fromDate;
        private DateTime toDate;
        public SalesReportForm(DateTime fromDateTime, DateTime toDateTime)
        {
            this.fromDate = fromDateTime;
            this.toDate = toDateTime;
            InitializeComponent();
        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.SetParameters(new ReportParameterCollection()
            {
                new ReportParameter("FromDate", this.fromDate.ToString()),
                new ReportParameter("ToDate", this.toDate.ToString())
            });
            this.getSalesDataTableAdapter.Fill(this.salesItemsDataSet.GetSalesData, this.fromDate, this.toDate);
            this.reportViewer1.RefreshReport();
        }
    }
}
