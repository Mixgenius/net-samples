namespace Model.Interfaces
{
    public interface IIntegrationSyncable
    {
        long Id { get; }
        string SalesforceId { get; }
        string NetsuiteId { get; }
    }
}
