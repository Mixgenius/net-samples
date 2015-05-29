using CommunicationPlatformMessages.Dto;
using Newtonsoft.Json;

namespace Model.Internal
{
    public class EmailMessageOverride : EmailMessage
    {
        [JsonIgnore]
        public CustomerEmailLog CustomerEmailLog { get; set; }

        public new long RelatedId
        {
            get { return CustomerEmailLog.Id; }
        }
    }
}
