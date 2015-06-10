using Fusebill.ApiWrapper.Contracts;
using System;

namespace Fusebill.ApiWrapper
{
    public class RestUriBuilder : IRestUriBuilder
    {
        private readonly string _baseUrl;

        public RestUriBuilder(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string BuildUri(string entity)
        {
            return BuildUri(entity, 0);
        }

        public string BuildUri(string entity, long entityId)
        {
            return BuildUri(entity, entityId, String.Empty);
        }

        public string BuildUri(string entity, QueryOptions queryOptions)
        {
            return BuildUri(entity, 0, String.Empty, queryOptions);
        }

        public string BuildUri(string entity, long entityId, QueryOptions queryOptions)
        {
            return BuildUri(entity, entityId, String.Empty, queryOptions);
        }

        public string BuildUri(string entity, string childEntity)
        {
            return BuildUri(entity, 0, childEntity);
        }

        public string BuildUri(string entity, long entityId, string childEntity)
        {
            return BuildUri(entity, entityId, childEntity, null);
        }

        public string BuildUri(string entity, long entityId, string childEntity, QueryOptions queryOptions)
        {
            return BuildUri<object>(entity, entityId, childEntity, queryOptions, null);
        }

        public string BuildUri<T>(string entity, long entityId, string childEntity, QueryOptions queryOptions, T advancedSearch)
        {
            // BaseUrl: https://local.fusebill.com/, Entity: /Plans or Customers/, EntityId: 1, ChildEntity: /Subscriptions
            var uri = string.Format("{0}/{1}", _baseUrl, entity);

            if (entityId != 0)
            {
                uri = uri + "/" + entityId;
            }

            if (!string.IsNullOrEmpty(childEntity))
            {
                uri = uri + "/" + childEntity;
            }

            if (queryOptions != null)
            {
                uri = uri + queryOptions.ToString();
            }

            if (advancedSearch != null)
            {
                if (queryOptions == null)
                    uri += "?";
                else
                    uri += "&";

                uri += advancedSearch.ToString();
            }

            return uri;
        }


    }
}
