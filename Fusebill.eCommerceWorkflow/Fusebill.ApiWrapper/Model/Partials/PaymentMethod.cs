using Model.Internal;

namespace Model
{
    public partial class PaymentMethod
    {
        public PaymentCollectOptions PaymentCollectOptions { get; set; }

        public string Source { get; set; }
    }
}
