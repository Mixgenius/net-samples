namespace Fusebill.ApiWrapper
{
    public interface IRestUriBuilder
    {
        string BuildUri(string entity);
        string BuildUri(string entity, long entityId);
        string BuildUri(string entity, long entityId, QueryOptions queryOptions);
        string BuildUri(string entity, QueryOptions queryOptions);
        string BuildUri(string entity, long entityId, string childEntity);
        string BuildUri(string entity, string childEntity);
        string BuildUri(string entity, long entityId, string childEntity, QueryOptions queryOptions);
        string BuildUri<T>(string entity, long entityId, string childEntity, QueryOptions queryOptions, T advancedSearch);

    }
}
