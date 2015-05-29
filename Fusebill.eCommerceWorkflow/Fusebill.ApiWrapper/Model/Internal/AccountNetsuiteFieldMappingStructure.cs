namespace Model.Internal
{
    public class AccountNetsuiteFieldMappingStructure
    {
        public long Id { get; set; }
        public NetsuiteEntityType NetsuiteEntityType { get; set; }
        public string NetsuiteField { get; set; }
        public string NetsuiteCustomField { get; set; }
        public string FusebillField { get; set; }
    }
}
