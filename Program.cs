namespace ConsoleApp25
{
    //завдання 1
    class Money
    {
        private int wholePart;
        private int fractionalPart;
        public Money(int wholePart, int fractionalPart)
        {
            if (wholePart < 0 || fractionalPart < 0 || fractionalPart >= 100)
                throw new ArgumentException("Неправильні значення для грошей.");

            this.wholePart = wholePart;
            this.fractionalPart = fractionalPart;
        }
        public int WholePart
        {
            get { return wholePart; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Ціла частина не може бути від'ємною.");
                wholePart = value;
            }
        }
        public int FractionalPart
        {
            get { return fractionalPart; }
            set
            {
                if (value < 0 || value >= 100)
                    throw new ArgumentException("Дрібна частина має бути в межах від 0 до 99.");
                fractionalPart = value;
            }
        }
        public void Display()
        {
            Console.WriteLine($"Сума: {wholePart}.{fractionalPart:00}");
        }
        public void SetValues(int whole, int fractional)
        {
            WholePart = whole;
            FractionalPart = fractional;
        }
    }

    class Product
    {
        private string name;
        private Money price;
        public Product(string name, Money price)
        {
            this.name = name ?? throw new ArgumentException("Назва продукту не може бути порожньою.");
            this.price = price ?? throw new ArgumentException("Ціна не може бути null.");
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва продукту не може бути порожньою.");
                name = value;
            }
        }
        public Money Price
        {
            get { return price; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Ціна не може бути null.");
                price = value;
            }
        }
        public void ReducePrice(int reduction)
        {
            if (reduction < 0)
                throw new ArgumentException("Зменшення ціни має бути невід'ємним.");

            int totalCents = price.WholePart * 100 + price.FractionalPart - reduction;

            if (totalCents < 0)
                throw new InvalidOperationException("Ціна не може бути від'ємною.");

            price.WholePart = totalCents / 100;
            price.FractionalPart = totalCents % 100;
        }

        public void DisplayProductInfo()
        {
            Console.WriteLine($"Назва продукту: {name}");
            Console.Write("Ціна продукту: ");
            price.Display();
        }
    }

    //завдання 2

    class Device
    {
        protected string Name { get; set; }
        protected string Characteristics { get; set; }
        public Device(string name, string characteristics)
        {
            Name = name;
            Characteristics = characteristics;
        }
        public virtual void Sound()
        {
            Console.WriteLine("Пристрій видає звук.");
        }
        public void Show()
        {
            Console.WriteLine($"Назва пристрою: {Name}");
        }
        public void Desc()
        {
            Console.WriteLine($"Опис пристрою: {Characteristics}");
        }
    }

    class Kettle : Device
    {
        public Kettle(string name, string characteristics) : base(name, characteristics) { }

        public override void Sound()
        {
            Console.WriteLine("Шшш...");
        }
    }

    class Microwave : Device
    {
        public Microwave(string name, string characteristics) : base(name, characteristics) { }

        public override void Sound()
        {
            Console.WriteLine("Біп-біп");
        }
    }
    class Car : Device
    {
        public Car(string name, string characteristics) : base(name, characteristics) { }

        public override void Sound()
        {
            Console.WriteLine("Бррр...");
        }
    }
    class Steamboat : Device
    {
        public Steamboat(string name, string characteristics) : base(name, characteristics) { }

        public override void Sound()
        {
            Console.WriteLine("Ту-тууу!");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            //завдання 1
            try
            {
                Money productPrice = new Money(19, 70);

                Product product = new Product("Жуйка", productPrice);

                Console.WriteLine("Інформація про продукт:");
                product.DisplayProductInfo();

                Console.WriteLine("\nЗменшення на 50 копійок.");
                product.ReducePrice(50);

                Console.WriteLine("Оновлена інформація про продукт:");
                product.DisplayProductInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            //завдання 2

            Device kettle = new Kettle("Чайник", "Електричний, 2 л");
            Device microwave = new Microwave("Мікрохвильовка", "Потужність 900 Вт");
            Device car = new Car("Автомобіль", "Електричний, Tesla Model 3");
            Device steamboat = new Steamboat("Пароплав", "Пасажирський, 300 місць");

            Device[] devices = { kettle, microwave, car, steamboat };

            foreach (var device in devices)
            {
                device.Show();
                device.Desc();
                device.Sound();
                Console.WriteLine();
            }


        }
    }
}
