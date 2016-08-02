using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace FH.ReportForms
{
    public partial class InventoryReportForm : Form
    {
        private DateTime fromDate;
        private DateTime toDate;

        public InventoryReportForm(DateTime fromDateTime, DateTime todateTime)
        {
            this.fromDate = fromDateTime;
            this.toDate = todateTime;
            InitializeComponent();
        }

        private void InventoryReportForm_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.SetParameters(new ReportParameterCollection()
            {
                new ReportParameter("FromDate", this.fromDate.ToString()),
                new ReportParameter("ToDate", this.toDate.ToString())
            });
            this.getInventoryDataTableAdapter.Fill(this.InventoryDBDataSet.GetInventoryData, this.fromDate, this.toDate);
            this.reportViewer1.RefreshReport();
        }
    }
}
