using Model.Internal;

namespace Model.FactoryMethods
{
    public class SaveOptionsFactoryMethods
    {
        public static SaveOptions SuppressCommPlatformEvents()
        {
            return new SaveOptions{SuppressCommunicationPlatformEvents = true};
        }
    }
}
