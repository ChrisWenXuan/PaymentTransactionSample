namespace PaymentTransactionSample.Exceptions
{
    public class PartnerNotFoundException : Exception
    {
        public PartnerNotFoundException()
            : base("Invalid Partner. Please provide valid Partner Key & Partner Password") { }
    }
}
