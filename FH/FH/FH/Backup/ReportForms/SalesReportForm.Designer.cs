using System;
using System.Collections.ObjectModel;
using Microsoft.Reporting.WinForms;

namespace FH.ReportForms
{
    partial class SalesReportForm
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
            this.getSalesDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.salesItemsDataSet = new FH.SalesItemsDataSet();
            this.getSalesDataTableAdapter = new FH.SalesItemsDataSetTableAdapters.GetSalesDataTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.getSalesDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesItemsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // getSalesDataBindingSource
            // 
            this.getSalesDataBindingSource.DataMember = "GetSalesData";
            this.getSalesDataBindingSource.DataSource = this.salesItemsDataSet;
            // 
            // salesItemsDataSet
            // 
            this.salesItemsDataSet.DataSetName = "SalesItemsDataSet";
            this.salesItemsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getSalesDataTableAdapter
            // 
            this.getSalesDataTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "SalesData";
            reportDataSource1.Value = this.getSalesDataBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "FH.ReportForms.SalesReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(784, 562);
            this.reportViewer1.TabIndex = 0;
            // 
            // SalesReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.reportViewer1);
            this.Name = "SalesReportForm";
            this.Text = "SalesReport";
            this.Load += new System.EventHandler(this.SalesReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.getSalesDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesItemsDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource getSalesDataBindingSource;
        private SalesItemsDataSet salesItemsDataSet;
        private SalesItemsDataSetTableAdapters.GetSalesDataTableAdapter getSalesDataTableAdapter;
    }
}