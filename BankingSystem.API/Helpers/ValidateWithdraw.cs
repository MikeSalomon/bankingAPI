namespace BankingSystem.API.Helpers
{
    public static class ValidateWithdraw
    {
        public static bool IsValidWithdraw(decimal currentBalance, decimal withdrawAmount)
        {
            if (currentBalance - withdrawAmount < 100.00M)
                return false;

            //calculate max withdraw
            var maxAllowedWithraw = currentBalance * 0.90M;
            if (withdrawAmount > maxAllowedWithraw)
                return false;

            return true; 
        }
    }
}
