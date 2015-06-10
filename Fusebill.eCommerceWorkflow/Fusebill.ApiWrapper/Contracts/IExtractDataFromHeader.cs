
namespace Fusebill.ApiWrapper.Contracts
{
    public interface IExtractDataFromHeaders
    {
        Dto.Get.PagingHeaderData ExtractPaginationDataFromHeader();
        long ExtractInt64ValueFromHeader(string key);
        string ExtractStringValueFromHeader(string key);
    }
}
