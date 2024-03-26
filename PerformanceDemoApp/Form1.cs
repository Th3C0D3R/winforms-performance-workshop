using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using PerformanceDemoApp.Entities;

namespace PerformanceDemoApp
{
    public partial class Form1 : Form
    {
        private List<Row> _allRows = new List<Row>();

        public Form1()
        {
            InitializeComponent();

            Shown += OnForm1Shown;
            vehicleListView.SelectedIndexChanged += OnVehicleListViewChanged;

            vehicleListView.View = View.Details;
            vehicleListView.Columns.Add("ID", -2, HorizontalAlignment.Left);
            vehicleListView.Columns.Add("Marke", -2, HorizontalAlignment.Left);
            vehicleListView.Columns.Add("Modell", -2, HorizontalAlignment.Left);
            vehicleListView.Columns.Add("Modelljahr", -2, HorizontalAlignment.Left);
            vehicleListView.Columns.Add("EV Typ", -2, HorizontalAlignment.Left);
            vehicleListView.Columns.Add("Elektrische Reichweite (km)", -2, HorizontalAlignment.Left);
        }
        
        private void OnForm1Shown(object sender, EventArgs e)
        {
            LoadXmlData();
            UpdateWatchedVehiclesDisplay();
            UpdateButtonStates();
        }

        private void LoadXmlData()
        {
            var xmlPath = Path.Combine(Application.StartupPath, "electric-vehicle-population.xml");
            var xDocument = XDocument.Load(xmlPath);

            _allRows = xDocument.Descendants("row")
                                .Select(element => new Row
                                {
                                    Id = element.Attribute("_id")?.Value ?? string.Empty,
                                    Uuid = element.Attribute("_uuid")?.Value ?? string.Empty,
                                    Position = Convert.ToInt32(element.Attribute("_position")?.Value),
                                    Address = element.Attribute("_address")?.Value ?? string.Empty,
                                    Vin110 = element.Element("vin_1_10")?.Value ?? string.Empty,
                                    County = element.Element("county")?.Value ?? string.Empty,
                                    City = element.Element("city")?.Value ?? string.Empty,
                                    State = element.Element("state")?.Value ?? string.Empty,
                                    ZipCode = element.Element("zip_code")?.Value ?? string.Empty,
                                    ModelYear = Convert.ToInt32(element.Element("model_year")?.Value),
                                    Make = element.Element("make")?.Value ?? string.Empty,
                                    Model = element.Element("model")?.Value ?? string.Empty,
                                    EvType = element.Element("ev_type")?.Value ?? string.Empty,
                                    CafvType = element.Element("cafv_type")?.Value ?? string.Empty,
                                    ElectricRange = Convert.ToInt32(element.Element("electric_range")?.Value),
                                    BaseMsrp = Convert.ToDecimal(element.Element("base_msrp")?.Value),
                                    LegislativeDistrict = Convert.ToInt32(element.Element("legislative_district")?.Value),
                                    DolVehicleId = Convert.ToInt64(element.Element("dol_vehicle_id")?.Value),
                                    GeocodedColumn = element.Element("geocoded_column")?.Value ?? string.Empty,
                                    ElectricUtility = element.Element("electric_utility")?.Value ?? string.Empty,
                                    CensusTract = element.Element("_2020_census_tract")?.Value ?? string.Empty
                                })
                                .Take(10000)
                                .ToList();

            DisplayData(_allRows);
        }


        private void DisplayData(List<Row> rows)
        {
            vehicleListView.Items.Clear();

            foreach (var row in rows)
            {
                vehicleListView.Items.Add(new ListViewItem(new [] {
                    row.Id,
                    row.Make,     
                    row.Model,    
                    row.ModelYear.ToString(),
                    row.EvType,  
                    row.ElectricRange + " km"
                }));
            }
        }

        private void OnVehicleListViewChanged(object sender, EventArgs e)
        {
            if (vehicleListView.SelectedItems.Count == 0) return;

            var selectedItem = vehicleListView.SelectedItems[0];
            var selectedId = selectedItem.SubItems[0].Text;

            var details = GetDetailsById(selectedId);
            if (details != null)
            {
                labelVin.Text = "VIN: " + details.Vin110;
                labelCounty.Text = "Landkreis: " + details.County;
                labelCity.Text = "Stadt: " + details.City;
                labelState.Text = "Bundesstaat: " + details.State;
                labelZipCode.Text = "PLZ: " + details.ZipCode;
                labelCafvType.Text = "CAFE-Typ: " + details.CafvType;
                labelBaseMsrp.Text = "Basis-MSRP: " + details.BaseMsrp.ToString("C"); // Format als Währung
                labelElectricUtility.Text = "Elektrizitätswerk: " + details.ElectricUtility;
                labelLegislativeDistrict.Text = "Legislativbezirk: " + details.LegislativeDistrict.ToString();
                labelDolVehicleId.Text = "DOL-Fahrzeug-ID: " + details.DolVehicleId.ToString();
                labelGeocodedColumn.Text = "Geokodierte Spalte: " + details.GeocodedColumn;
                labelCensusTract.Text = "Zensus-Trakt: " + details.CensusTract;
            }
            else
            {
                ClearDetailLabels();
            }

            UpdateButtonStates();
        }

        private void ClearDetailLabels()
        {
            labelVin.Text = "VIN:";
            labelCounty.Text = "Landkreis:";
            labelCity.Text = "Stadt:";
            labelState.Text = "Bundesstaat:";
            labelZipCode.Text = "PLZ:";
            labelCafvType.Text = "CAFE-Typ:";
            labelBaseMsrp.Text = "Basis-MSRP:";
            labelElectricUtility.Text = "Elektrizitätswerk:";
            labelLegislativeDistrict.Text = "Legislativbezirk:";
            labelDolVehicleId.Text = "DOL-Fahrzeug-ID:";
            labelGeocodedColumn.Text = "Geokodierte Spalte:";
            labelCensusTract.Text = "Zensus-Trakt:";
        }
        
        private Row GetDetailsById(string id)
        {
            return _allRows.FirstOrDefault(row => row.Id == id);
        }

        private void btnWatchVehicle_Click(object sender, EventArgs e)
        {
            if (vehicleListView.SelectedItems.Count > 0)
            {
                var selectedId = vehicleListView.SelectedItems[0].Text;
                using (var conn = new SQLiteConnection("Data Source=AppData.db;Version=3;"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("INSERT INTO WatchedVehicles (Id) VALUES (@Id)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }

                UpdateWatchedVehiclesDisplay();
                UpdateButtonStates();
            }
        }

        private void UpdateWatchedVehiclesDisplay()
        {
            using (var conn = new SQLiteConnection("Data Source=AppData.db;Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id FROM WatchedVehicles", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var watchedIds = new List<string>();
                        while (reader.Read())
                        {
                            watchedIds.Add(reader.GetString(0));
                        }

                        foreach (ListViewItem item in vehicleListView.Items)
                        {
                            item.BackColor = SystemColors.Window; 
                        }

                        foreach (ListViewItem item in vehicleListView.Items)
                        {
                            if (watchedIds.Contains(item.SubItems[0].Text)) 
                            {
                                item.BackColor = Color.LightGreen;
                            }
                        }
                    }
                }
            }
        }

        private void btnUnfollow_Click(object sender, EventArgs e)
        {
            if (vehicleListView.SelectedItems.Count > 0)
            {
                var selectedId = vehicleListView.SelectedItems[0].SubItems[0].Text;
                using (var conn = new SQLiteConnection("Data Source=AppData.db;Version=3;"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("DELETE FROM WatchedVehicles WHERE Id = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", selectedId);
                        cmd.ExecuteNonQuery();

                        UpdateButtonStates();
                        UpdateWatchedVehiclesDisplay();
                    }
                }
            }
        }

        private void UpdateButtonStates()
        {
            if (vehicleListView.SelectedItems.Count > 0)
            {
                var selectedId = vehicleListView.SelectedItems[0].SubItems[0].Text;
                using (var conn = new SQLiteConnection("Data Source=AppData.db;Version=3;"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM WatchedVehicles WHERE Id = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", selectedId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        bool isWatched = count > 0;

                        btnWatchVehicle.Enabled = !isWatched;
                        btnUnfollow.Enabled = isWatched;
                    }
                }
            }
            else
            {
                btnWatchVehicle.Enabled = false;
                btnUnfollow.Enabled = false;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                DisplayData(_allRows);
                UpdateWatchedVehiclesDisplay();
                return;
            }

            var searchResults = _allRows.Where(row =>
                    row.Make.IndexOf(textBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    row.Model.IndexOf(textBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            DisplayData(searchResults);
            UpdateWatchedVehiclesDisplay();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            textBoxSearch_TextChanged(sender, e);
        }
    }
}
