using System.Collections.Generic;
using DataCommon.Models;

namespace Model.FactoryMethods
{
    public class ResultListFactoryMethod
    {
        public static ResultList<T> BuildResultList<T>(List<T> entities, PagingHeaderData pagingHeaderData)
        {
            return new ResultList<T>
            {
                PagingHeaderData = pagingHeaderData,
                Results = entities
            };
        }
    }
}
