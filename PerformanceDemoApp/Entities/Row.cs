namespace PerformanceDemoApp.Entities
{
    internal class Row
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public int Position { get; set; }
        public string Address { get; set; }
        public string Vin110 { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string EvType { get; set; }
        public string CafvType { get; set; }
        public int ElectricRange { get; set; }
        public decimal BaseMsrp { get; set; }
        public int LegislativeDistrict { get; set; }
        public long DolVehicleId { get; set; }
        public string GeocodedColumn { get; set; }
        public string ElectricUtility { get; set; }
        public string CensusTract { get; set; }
    }
}
