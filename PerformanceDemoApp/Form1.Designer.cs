using System.Windows.Forms;

namespace PerformanceDemoApp
{
    partial class Form1
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
            this.vehicleListView = new System.Windows.Forms.ListView();
            this.detailsPanel = new System.Windows.Forms.Panel();
            this.labelVin = new System.Windows.Forms.Label();
            this.labelCounty = new System.Windows.Forms.Label();
            this.labelCity = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.labelZipCode = new System.Windows.Forms.Label();
            this.labelCafvType = new System.Windows.Forms.Label();
            this.labelBaseMsrp = new System.Windows.Forms.Label();
            this.labelElectricUtility = new System.Windows.Forms.Label();
            this.labelLegislativeDistrict = new System.Windows.Forms.Label();
            this.labelDolVehicleId = new System.Windows.Forms.Label();
            this.labelGeocodedColumn = new System.Windows.Forms.Label();
            this.labelCensusTract = new System.Windows.Forms.Label();
            this.btnWatchVehicle = new System.Windows.Forms.Button();
            this.btnUnfollow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.detailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // vehicleListView
            // 
            this.vehicleListView.HideSelection = false;
            this.vehicleListView.Location = new System.Drawing.Point(12, 40);
            this.vehicleListView.Name = "vehicleListView";
            this.vehicleListView.Size = new System.Drawing.Size(776, 172);
            this.vehicleListView.TabIndex = 0;
            this.vehicleListView.UseCompatibleStateImageBehavior = false;
            this.vehicleListView.View = System.Windows.Forms.View.Details;
            // 
            // detailsPanel
            // 
            this.detailsPanel.AutoScroll = true;
            this.detailsPanel.Controls.Add(this.labelVin);
            this.detailsPanel.Controls.Add(this.labelCounty);
            this.detailsPanel.Controls.Add(this.labelCity);
            this.detailsPanel.Controls.Add(this.labelState);
            this.detailsPanel.Controls.Add(this.labelZipCode);
            this.detailsPanel.Controls.Add(this.labelCafvType);
            this.detailsPanel.Controls.Add(this.labelBaseMsrp);
            this.detailsPanel.Controls.Add(this.labelElectricUtility);
            this.detailsPanel.Controls.Add(this.labelLegislativeDistrict);
            this.detailsPanel.Controls.Add(this.labelDolVehicleId);
            this.detailsPanel.Controls.Add(this.labelGeocodedColumn);
            this.detailsPanel.Controls.Add(this.labelCensusTract);
            this.detailsPanel.Location = new System.Drawing.Point(12, 218);
            this.detailsPanel.Name = "detailsPanel";
            this.detailsPanel.Size = new System.Drawing.Size(376, 314);
            this.detailsPanel.TabIndex = 1;
            // 
            // labelVin
            // 
            this.labelVin.Location = new System.Drawing.Point(10, 10);
            this.labelVin.Name = "labelVin";
            this.labelVin.Size = new System.Drawing.Size(300, 20);
            this.labelVin.TabIndex = 0;
            this.labelVin.Text = "VIN:";
            // 
            // labelCounty
            // 
            this.labelCounty.Location = new System.Drawing.Point(10, 35);
            this.labelCounty.Name = "labelCounty";
            this.labelCounty.Size = new System.Drawing.Size(300, 20);
            this.labelCounty.TabIndex = 1;
            this.labelCounty.Text = "Landkreis:";
            // 
            // labelCity
            // 
            this.labelCity.Location = new System.Drawing.Point(10, 60);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(300, 20);
            this.labelCity.TabIndex = 2;
            this.labelCity.Text = "Stadt:";
            // 
            // labelState
            // 
            this.labelState.Location = new System.Drawing.Point(10, 85);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(300, 20);
            this.labelState.TabIndex = 3;
            this.labelState.Text = "Bundesstaat:";
            // 
            // labelZipCode
            // 
            this.labelZipCode.Location = new System.Drawing.Point(10, 110);
            this.labelZipCode.Name = "labelZipCode";
            this.labelZipCode.Size = new System.Drawing.Size(300, 20);
            this.labelZipCode.TabIndex = 4;
            this.labelZipCode.Text = "PLZ:";
            // 
            // labelCafvType
            // 
            this.labelCafvType.Location = new System.Drawing.Point(10, 135);
            this.labelCafvType.Name = "labelCafvType";
            this.labelCafvType.Size = new System.Drawing.Size(300, 20);
            this.labelCafvType.TabIndex = 5;
            this.labelCafvType.Text = "CAFE-Typ:";
            // 
            // labelBaseMsrp
            // 
            this.labelBaseMsrp.Location = new System.Drawing.Point(10, 160);
            this.labelBaseMsrp.Name = "labelBaseMsrp";
            this.labelBaseMsrp.Size = new System.Drawing.Size(300, 20);
            this.labelBaseMsrp.TabIndex = 6;
            this.labelBaseMsrp.Text = "Basis-MSRP:";
            // 
            // labelElectricUtility
            // 
            this.labelElectricUtility.Location = new System.Drawing.Point(10, 185);
            this.labelElectricUtility.Name = "labelElectricUtility";
            this.labelElectricUtility.Size = new System.Drawing.Size(300, 20);
            this.labelElectricUtility.TabIndex = 7;
            this.labelElectricUtility.Text = "Elektrizitätswerk:";
            // 
            // labelLegislativeDistrict
            // 
            this.labelLegislativeDistrict.Location = new System.Drawing.Point(10, 210);
            this.labelLegislativeDistrict.Name = "labelLegislativeDistrict";
            this.labelLegislativeDistrict.Size = new System.Drawing.Size(300, 20);
            this.labelLegislativeDistrict.TabIndex = 8;
            this.labelLegislativeDistrict.Text = "Legislativbezirk:";
            // 
            // labelDolVehicleId
            // 
            this.labelDolVehicleId.Location = new System.Drawing.Point(10, 235);
            this.labelDolVehicleId.Name = "labelDolVehicleId";
            this.labelDolVehicleId.Size = new System.Drawing.Size(300, 20);
            this.labelDolVehicleId.TabIndex = 9;
            this.labelDolVehicleId.Text = "DOL-Fahrzeug-ID:";
            // 
            // labelGeocodedColumn
            // 
            this.labelGeocodedColumn.Location = new System.Drawing.Point(10, 260);
            this.labelGeocodedColumn.Name = "labelGeocodedColumn";
            this.labelGeocodedColumn.Size = new System.Drawing.Size(300, 20);
            this.labelGeocodedColumn.TabIndex = 10;
            this.labelGeocodedColumn.Text = "Geokodierte Spalte:";
            // 
            // labelCensusTract
            // 
            this.labelCensusTract.Location = new System.Drawing.Point(10, 285);
            this.labelCensusTract.Name = "labelCensusTract";
            this.labelCensusTract.Size = new System.Drawing.Size(300, 20);
            this.labelCensusTract.TabIndex = 11;
            this.labelCensusTract.Text = "Zensus-Trakt:";
            // 
            // btnWatchVehicle
            // 
            this.btnWatchVehicle.Location = new System.Drawing.Point(394, 218);
            this.btnWatchVehicle.Name = "btnWatchVehicle";
            this.btnWatchVehicle.Size = new System.Drawing.Size(140, 23);
            this.btnWatchVehicle.TabIndex = 2;
            this.btnWatchVehicle.Text = "Fahrzeug beobachten";
            this.btnWatchVehicle.UseVisualStyleBackColor = true;
            this.btnWatchVehicle.Click += new System.EventHandler(this.btnWatchVehicle_Click);
            // 
            // btnUnfollow
            // 
            this.btnUnfollow.Location = new System.Drawing.Point(540, 218);
            this.btnUnfollow.Name = "btnUnfollow";
            this.btnUnfollow.Size = new System.Drawing.Size(162, 23);
            this.btnUnfollow.TabIndex = 3;
            this.btnUnfollow.Text = "Nicht mehr beobachten";
            this.btnUnfollow.UseVisualStyleBackColor = true;
            this.btnUnfollow.Click += new System.EventHandler(this.btnUnfollow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Suche:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(59, 10);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(187, 20);
            this.textBoxSearch.TabIndex = 5;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(252, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Suche";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 551);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUnfollow);
            this.Controls.Add(this.btnWatchVehicle);
            this.Controls.Add(this.detailsPanel);
            this.Controls.Add(this.vehicleListView);
            this.Name = "Form1";
            this.Text = "Bestand an Elektrofahrzeugen - Schneckenversion";
            this.detailsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        private System.Windows.Forms.ListView vehicleListView;
        private System.Windows.Forms.Panel detailsPanel;
        private System.Windows.Forms.Label labelVin;
        private System.Windows.Forms.Label labelCounty;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelZipCode;
        private System.Windows.Forms.Label labelCafvType;
        private System.Windows.Forms.Label labelBaseMsrp;
        private System.Windows.Forms.Label labelElectricUtility;
        private System.Windows.Forms.Label labelLegislativeDistrict;
        private System.Windows.Forms.Label labelDolVehicleId;
        private System.Windows.Forms.Label labelGeocodedColumn;
        private System.Windows.Forms.Label labelCensusTract;
        private Button btnWatchVehicle;
        private Button btnUnfollow;
        private Label label1;
        private TextBox textBoxSearch;
        private Button btnSearch;
    }
}

