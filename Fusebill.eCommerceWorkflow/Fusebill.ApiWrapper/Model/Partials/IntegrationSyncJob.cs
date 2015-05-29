using System;

namespace Model
{
    public partial class IntegrationSyncJob
    {
        public int BatchSize
        {
            get
            {
                switch (IntegrationType)
                {
                    case IntegrationType.Salesforce:
                        return int.MaxValue;
                    case IntegrationType.Netsuite:
                        return 100;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
