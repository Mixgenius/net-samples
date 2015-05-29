using System;

namespace Model.Avalara
{
    public enum AddressType
    {
        //1, // Street
        //2, // Street Served by Route and GD
        //3, // Lock Box
        //4, // Route Service
        //5, // General Delivery
        B, // LVR Street
        C, // Government Street
        D, // LVR Lock Box
        E, // Government Lock Box
        F, // Firm or company address
        G, // General Delivery address
        H, // High-rise or business complex
        L, // LVR General Delivery
        K, // Building        
        P, // PO box address
        R, // Rural route address
        S // Street or residential address
    }

    [Serializable]
    public class Address
    {
        // Address can be determined for tax calculation by Line1, City, Region, PostalCode, Country OR Latitude/Longitude OR TaxRegionId
        public string AddressCode { get; set; } // Input for GetTax only, not by address validation

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string County { get; set; } // Output for ValidateAddress only

        public string FipsCode { get; set; } // Output for ValidateAddress only

        public string CarrierRoute { get; set; } // Output for ValidateAddress only

        public string PostNet { get; set; } // Output for ValidateAddress only

        public string AddressType { get; set; } // Output for ValidateAddress only

        public decimal? Latitude { get; set; } // Input for GetTax only

        public decimal? Longitude { get; set; } // Input for GetTax only

        public string TaxRegionId { get; set; } // Input for GetTax only

        public override string ToString()
        {
            string querystring = "?";

            querystring += (Line1 == null) ? string.Empty : "Line1=" + Line1.Replace(" ", "+");
            querystring += (Line2 == null) ? string.Empty : "&Line2=" + Line2.Replace(" ", "+");
            querystring += (Line3 == null) ? string.Empty : "&Line3=" + Line3.Replace(" ", "+");
            querystring += (City == null) ? string.Empty : "&City=" + City.Replace(" ", "+");
            querystring += (Region == null) ? string.Empty : "&Region=" + Region.Replace(" ", "+");
            querystring += (PostalCode == null) ? string.Empty : "&PostalCode=" + PostalCode.Replace(" ", "+");
            querystring += (Country == null) ? string.Empty : "&Country=" + Country.Replace(" ", "+");

            return querystring;
        }
    }
}
