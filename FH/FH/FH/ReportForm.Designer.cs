using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using ReportParameter = Microsoft.Reporting.WinForms.ReportParameter;

namespace FH
{
    partial class ReportForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.AllInventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InventoryDBDataSet = new FH.InventoryDBDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AllInventoryTableAdapter = new FH.InventoryDBDataSetTableAdapters.InventoryTableAdapter();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.GetSoldItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SalesItemsDataSet = new FH.SalesItemsDataSet();
            this.GetSoldItemsTableAdapter = new FH.SalesItemsDataSetTableAdapters.GetSalesDataTableAdapter();
            this.GetSalesDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GetSalesDataTableAdapter = new FH.SalesItemsDataSetTableAdapters.GetSalesDataTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.AllInventoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetSoldItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesItemsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetSalesDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // AllInventoryBindingSource
            // 
            this.AllInventoryBindingSource.DataMember = "AllInventory";
            this.AllInventoryBindingSource.DataSource = this.InventoryDBDataSet;
            // 
            // InventoryDBDataSet
            // 
            this.InventoryDBDataSet.DataSetName = "InventoryDBDataSet";
            this.InventoryDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.AllInventoryBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "FH.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1018, 486);
            this.reportViewer1.TabIndex = 0;
            // 
            // AllInventoryTableAdapter
            // 
            this.AllInventoryTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "SalesItems";
            reportDataSource2.Value = this.GetSalesDataBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "FH.Report2.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(1018, 486);
            this.reportViewer2.TabIndex = 1;
            // 
            // GetSoldItemsBindingSource
            // 
            this.GetSoldItemsBindingSource.DataMember = "GetSoldItems";
            this.GetSoldItemsBindingSource.DataSource = this.SalesItemsDataSet;
            // 
            // SalesItemsDataSet
            // 
            this.SalesItemsDataSet.DataSetName = "SalesItemsDataSet";
            this.SalesItemsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GetSoldItemsTableAdapter
            // 
            this.GetSoldItemsTableAdapter.ClearBeforeFill = true;
            // 
            // GetSalesDataBindingSource
            // 
            this.GetSalesDataBindingSource.DataMember = "GetSalesData";
            this.GetSalesDataBindingSource.DataSource = this.SalesItemsDataSet;
            // 
            // GetSalesDataTableAdapter
            // 
            this.GetSalesDataTableAdapter.ClearBeforeFill = true;
            // 
            // ReportForm
            // 
            this.reportViewer2.LocalReport.SetParameters(new Collection<ReportParameter>()
            {
                new ReportParameter("FromDate", new DateTime(2014,05,30).ToShortDateString()),
                new ReportParameter("ToDate", new DateTime(2014,06,30).ToShortDateString())
            });
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 486);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllInventoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetSoldItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesItemsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetSalesDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AllInventoryBindingSource;
        private InventoryDBDataSet InventoryDBDataSet;
        private InventoryDBDataSetTableAdapters.InventoryTableAdapter AllInventoryTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource GetSoldItemsBindingSource;
        private SalesItemsDataSet SalesItemsDataSet;
        private SalesItemsDataSetTableAdapters.GetSalesDataTableAdapter GetSoldItemsTableAdapter;
        private System.Windows.Forms.BindingSource GetSalesDataBindingSource;
        private SalesItemsDataSetTableAdapters.GetSalesDataTableAdapter GetSalesDataTableAdapter;
    }
}