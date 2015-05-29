using System.Collections.Generic;

namespace Model.Internal
{
    public class NetsuiteConfiguration
    {
        //Corresponds to the AccountChannelId
        public int Id { get; set; }
        public string NetsuiteAccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public bool IsSandbox { get; set; }
        public string RestUri { get; set; }
        public ChannelStatus Status { get; set; }
        public List<ChannelEvent> Events { get; set; }
        public string SubsidiaryId { get; set; }

        //Database fields
        public NetsuiteEntityType DefaultCustomerType { get; set; }

        public string LimitedAuthorization
        {
            get { return string.Format("NLAuth nlauth_email={0}, nlauth_signature={1}", Username, Password); }
        }

        public string Authorization
        {
            get { return string.Format("NLAuth nlauth_account={0}, nlauth_email={1}, nlauth_signature={2}, nlauth_role={3}", NetsuiteAccountId, Username, Password, RoleId); }
        }
    }
}