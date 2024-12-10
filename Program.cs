namespace ConsoleApp24
{
    //завдання 1
    class CreditCard
    {
        private string cardNumber;
        private string fullName;
        private int cvc;
        private DateTime expirationDate;
        private int balance;

        public CreditCard(string cardNumber, string fullName, int cvc, DateTime expirationDate, int initialBalance)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16 || !long.TryParse(cardNumber, out _))
                throw new ArgumentException("Номер картки має бути 16 цифр.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ПІБ власника не може бути порожнім.");

            if (cvc < 100 || cvc > 999)
                throw new ArgumentException("CVC-код має бути три цифри.");

            if (expirationDate < DateTime.Today)
                throw new ArgumentException("Дата завершення терміну дії має бути у майбутньому.");

            if (initialBalance < 0)
                throw new ArgumentException("Сума грошей на картці не може бути від'ємною.");

            this.cardNumber = cardNumber;
            this.fullName = fullName;
            this.cvc = cvc;
            this.expirationDate = expirationDate;
            this.balance = initialBalance;
        }
        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public static CreditCard operator +(CreditCard card, int amount)
        {
            card.balance += amount;
            return card;
        }
        public static CreditCard operator -(CreditCard card, int amount)
        {
            if (card.balance < amount)
                throw new InvalidOperationException("На картці недостатньо коштів.");

            card.balance -= amount;
            return card;
        }
        public static bool operator ==(CreditCard card, int cvcCode)
        {
            return card.cvc == cvcCode;
        }

        public static bool operator !=(CreditCard card, int cvcCode)
        {
            return card.cvc != cvcCode;
        }
        public static bool operator <(CreditCard card, int amount)
        {
            return card.balance < amount;
        }

        public static bool operator >(CreditCard card, int amount)
        {
            return card.balance > amount;
        }
        public override bool Equals(object obj)
        {
            if (obj is CreditCard)
                return this.cvc == ((CreditCard)obj).cvc;
            return false;
        }
        public override int GetHashCode()
        {
            return cvc.GetHashCode();
        }
        public void DisplayCardInfo()
        {
            Console.WriteLine($"Номер картки: {cardNumber}");
            Console.WriteLine($"ПІБ власника: {fullName}");
            Console.WriteLine($"CVC: {cvc}");
            Console.WriteLine($"Термін дії: {expirationDate.ToShortDateString()}");
            Console.WriteLine($"Баланс: {balance}");
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
                DateTime expirationDate = new DateTime(2024, 12, 31);
                int initialBalance = 500;

                CreditCard card = new CreditCard(cardNumber, fullName, cvc, expirationDate, initialBalance);

                Console.WriteLine("\nІнформація про картку:");
                card.DisplayCardInfo();

                Console.WriteLine("\nЗбільшуємо баланс на 100.");
                card = card + 100;
                card.DisplayCardInfo();

                Console.WriteLine("\nЗменшуємо баланс на 50.");
                card = card - 50;
                card.DisplayCardInfo();

                Console.WriteLine("\nПеревіряємо CVC-код:");
                if (card == 123)
                    Console.WriteLine("CVC-код співпадає.");
                else
                    Console.WriteLine("CVC-код не співпадає.");

                Console.WriteLine("\nПеревірка суми грошей:");
                if (card < 1000)
                    Console.WriteLine("Баланс менше 1000.");
                if (card > 200)
                    Console.WriteLine("Баланс більше 200.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
