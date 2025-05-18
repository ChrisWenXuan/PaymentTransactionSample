using PaymentTransactionSample.Dtos;

namespace PaymentTransactionSample.Data
{
    public static class TestData
    {
        public static List<PartnerDto> Partners = new List<PartnerDto>
        {
            new PartnerDto
            {
                PartnerKey = "FAKEGOOGLE",
                PartnerPassword = "RkFLRVBBU1NXT1JEMTIzNA==", // coverted in Base64, orinital : FAKEPASSWORD1234
            },
             new PartnerDto
            {
                PartnerKey = "FAKEPEOPLE",
                PartnerPassword = "RkFLRVBBU1NXT1JENDU3OA==", // coverted in Base64, orinital : FAKEPASSWORD4578
            }
        };
    }
}
