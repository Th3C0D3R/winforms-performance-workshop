using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
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
            var rows = new List<Row>();
            int rowCount = 0;
            const int maxRows = 10000;

            using (var reader = XmlReader.Create(xmlPath))
            {
                while (reader.Read() && rowCount < maxRows)
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "row")
                    {
                        var row = new Row
                        {
                            Id = reader.GetAttribute("_id"),
                            Address = reader.GetAttribute("_address")
                        };

                        if (!reader.IsEmptyElement)
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    string elementName = reader.Name;
                                    if (reader.IsEmptyElement)
                                    {
                                        continue;
                                    }

                                    reader.Read();
                                    string content = reader.HasValue ? reader.Value : null;

                                    switch (elementName)
                                    {
                                        case "vin_1_10":
                                            row.Vin110 = content;
                                            break;
                                        case "county":
                                            row.County = content;
                                            break;
                                        case "city":
                                            row.City = content;
                                            break;
                                        case "state":
                                            row.State = content;
                                            break;
                                        case "zip_code":
                                            row.ZipCode = content;
                                            break;
                                        case "model_year":
                                            row.ModelYear = int.TryParse(content, out int modelYear) ? modelYear : 0;
                                            break;
                                        case "make":
                                            row.Make = content;
                                            break;
                                        case "model":
                                            row.Model = content;
                                            break;
                                        case "ev_type":
                                            row.EvType = content;
                                            break;
                                        case "electric_range":
                                            row.ElectricRange = int.TryParse(content, out int electricRange) ? electricRange : 0;
                                            break;
                                        case "cafv_type":
                                            row.CafvType = content;
                                            break;
                                        case "base_msrp":
                                            row.BaseMsrp = decimal.TryParse(content, out decimal baseMsrp) ? baseMsrp : 0;
                                            break;
                                        case "legislative_district":
                                            row.LegislativeDistrict = int.TryParse(content, out int legislativeDistrict) ? legislativeDistrict : 0;
                                            break;
                                        case "dol_vehicle_id":
                                            row.DolVehicleId = long.TryParse(content, out long dolVehicleId) ? dolVehicleId : 0;
                                            break;
                                        case "geocoded_column":
                                            row.GeocodedColumn = content;
                                            break;
                                        case "electric_utility":
                                            row.ElectricUtility = content;
                                            break;
                                        case "_2020_census_tract":
                                            row.CensusTract = content;
                                            break;
                                    }
                                    // Skip to the end of the current element
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == elementName)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "row")
                                {
                                    // End of this <row>, break out of the loop
                                    break;
                                }
                            }
                        }

                        rows.Add(row);
                        rowCount++;
                    }
                }
            }

            _allRows = rows;

            DisplayData(_allRows);
        }


        private void DisplayData(List<Row> rows)
        {
            var listViewItems = PrepareListViewItems(rows);

            vehicleListView.BeginUpdate();
            vehicleListView.Items.Clear();
            vehicleListView.Items.AddRange(listViewItems);
            vehicleListView.EndUpdate();
        }

        private ListViewItem[] PrepareListViewItems(List<Row> rows)
        {
            var listViewItems = new ListViewItem[rows.Count];

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                listViewItems[i] = new ListViewItem(new[] {
                    row.Id,
                    row.Make,
                    row.Model,
                    row.ModelYear.ToString(),
                    row.EvType,
                    $"{row.ElectricRange} km"
                });
            }

            return listViewItems;
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
