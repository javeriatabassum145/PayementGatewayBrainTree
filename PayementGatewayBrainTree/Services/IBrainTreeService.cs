using Braintree;

namespace PaymentGatewayBrainTree.Services
{
    public interface IBrainTreeService
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
