
namespace Fusebill.ApiWrapper.Contracts
{
    public interface IExtractDataFromHeaders
    {
        Dto.Get.PagingHeaderData TryExtractPaginationDataFromHeader();
        long ExtractInt64ValueFromHeader(string key);
        string ExtractStringValueFromHeader(string key);
    }
}
