using Braintree;

namespace PaymentGatewayBrainTree.Services
{
    public class BrainTreeService : IBrainTreeService
    {
        private readonly IConfiguration _config;

        public BrainTreeService(IConfiguration config)
        {
            _config = config;
        }


        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey = _config.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey = _config.GetValue<string>("BraintreeGateway:PrivateKey")
            };

            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return CreateGateway();

        }
    }
}
