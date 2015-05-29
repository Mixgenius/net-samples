namespace Model
{
    public interface ICsvCommonReportFields
    {
        long Fusebill_ID { get; set; }
        string Customer_ID { get; set; }
        string Customer_First_Name { get; set; }
        string Customer_Last_Name { get; set; }
        string Customer_Company_Name { get; set; }
        string Phone1 { get; set; }
        string Email1 { get; set; }
        string Phone2 { get; set; }
        string Email2 { get; set; }
        string Address_Line1 { get; set; }
        string Address_Line2 { get; set; }
        string Country { get; set; }
        string State { get; set; }
        string City { get; set; }
        string Zip { get; set; }
        string Ref1 { get; set; }
        string Ref2 { get; set; }
        string Ref3 { get; set; }
        string Accounting_Status__applicable_at_effective_date_of_report_ { get; set; }
        string Status__applicable_at_effective_date_of_report_ { get; set; }
        string SalesTrackingCode1Code { get; set; }
        string SalesTrackingCode1Name { get; set; }
        string SalesTrackingCode2Code { get; set; }
        string SalesTrackingCode2Name { get; set; }
        string SalesTrackingCode3Code { get; set; }
        string SalesTrackingCode3Name { get; set; }
        string SalesTrackingCode4Code { get; set; }
        string SalesTrackingCode4Name { get; set; }
        string SalesTrackingCode5Code { get; set; }
        string SalesTrackingCode5Name { get; set; }
    }
}
