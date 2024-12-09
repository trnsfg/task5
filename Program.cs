namespace ConsoleApp24
{
    //завдання 1
    class CreditCard
    {
        private string cardNumber;
        private string fullName;
        private int cvc;
        private DateTime expirationDate;

        public CreditCard(string cardNumber, string fullName, int cvc, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16 || !long.TryParse(cardNumber, out _))
                throw new ArgumentException("Номер картки має бути 16 цифр.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ПІБ власника не може бути порожнім.");

            if (cvc < 100 || cvc > 999)
                throw new ArgumentException("CVC-код має бути три цифри.");

            if (expirationDate < DateTime.Today)
                throw new ArgumentException("Дата завершення терміну дії має бути у майбутньому.");

            this.cardNumber = cardNumber;
            this.fullName = fullName;
            this.cvc = cvc;
            this.expirationDate = expirationDate;
        }

        public void DisplayCardInfo()
        {
            Console.WriteLine($"Номер картки: {cardNumber}");
            Console.WriteLine($"ПІБ власника: {fullName}");
            Console.WriteLine($"CVC: {cvc}");
            Console.WriteLine($"Термін дії: {expirationDate.ToShortDateString()}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //завдання 1
            try
            {
                string cardNumber = "1234567812345678";
                string fullName = "Кузнєцова Дарія";
                int cvc = 123;
                DateTime expirationDate = new DateTime(2024, 12, 09);

                CreditCard card = new CreditCard(cardNumber, fullName, cvc, expirationDate);

                card.DisplayCardInfo();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
