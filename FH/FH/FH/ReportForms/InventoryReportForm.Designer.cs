using Microsoft.Reporting.WinForms;

namespace FH.ReportForms
{
    partial class InventoryReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.GetInventoryDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InventoryDBDataSet = new FH.InventoryDBDataSet();
            this.getInventoryDataTableAdapter = new FH.InventoryDBDataSetTableAdapters.GetInventoryDataTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetInventoryDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Inventory";
            reportDataSource1.Value = this.GetInventoryDataBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "FH.ReportForms.InventoryReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(784, 562);
            this.reportViewer1.TabIndex = 0;
            // 
            // GetSalesDataBindingSource
            // 
            this.GetInventoryDataBindingSource.DataMember = "GetInventoryData";
            this.GetInventoryDataBindingSource.DataSource = this.InventoryDBDataSet;
            // 
            // SalesItemsDataSet
            // 
            this.InventoryDBDataSet.DataSetName = "InventoryDBDataSet";
            this.InventoryDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GetSalesDataTableAdapter
            // 
            this.getInventoryDataTableAdapter.ClearBeforeFill = true;
            // 
            // SalesReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.reportViewer1.ZoomMode = ZoomMode.FullPage;
            this.Controls.Add(this.reportViewer1);
            this.Name = "InventoryReport";
            this.Text = "InventoryReport";
            this.Load += new System.EventHandler(this.InventoryReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetInventoryDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource GetInventoryDataBindingSource;
        private InventoryDBDataSet InventoryDBDataSet;
        private InventoryDBDataSetTableAdapters.GetInventoryDataTableAdapter getInventoryDataTableAdapter;
    }
}