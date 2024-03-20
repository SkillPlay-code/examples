using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        const string CommandShowProducts = "1";
        const string CommandBuyProduct = "2";
        const string CommandViewInventory = "3";
        const string CommandExit = "4";

        Seller seller = new Seller();
        Player player = new Player();
        bool isWork = true;

        while (isWork)
        {
            Console.WriteLine("\nМеню: \n" +
                              $"{CommandShowProducts}. Покажи товар! \n" +
                              $"{CommandBuyProduct}. Хочу купить, кое что! \n" +
                              $"{CommandViewInventory}. А что у меня в сумке? \n" +
                              $"{CommandExit}. Выход \n");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case CommandShowProducts:
                    seller.ShowProducts();
                    break;

                case CommandBuyProduct:
                    seller.SalesProcess(player);
                    break;

                case CommandViewInventory:
                    player.ShowProducts();
                    break;

                case CommandExit:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.\n");
                    break;
            }
        }

        Console.WriteLine("Ты... это... Заходи, если что!. \n");
    }
}

class Broker
{
    protected int Gold;
    protected List<Product> Products;

    public Broker()
    {
        Gold = 2000;
        Products = new List<Product>();
    }

    public void ShowProducts()
    {
        Console.WriteLine("Товары:");

        foreach (var product in Products)
        {
            Console.WriteLine($"{product.Name}: {product.Price} золотых.");
        }

        Console.WriteLine($"Осталось золота: {Gold} золотых\n");
    }
}

class Product
{
    public Product(string name, int price)
    {
        this.Name = name;
        this.Price = price;
    }
    
    public string Name { get; private set; }
    public int Price { get; private set; }
    
}

class Player : Broker
{
    public bool CanBuyProduct(int productPrice)
    {
        return Gold >= productPrice;
    }

    public void BuyProduct(Product product)
    {
            Products.Add(product);
            Gold -= product.Price;
            Console.WriteLine($"Вы купили {product.Name} за {product.Price} золотых\n");
    }
}

class Seller : Broker
{
    private int _sellerGold = 0;

    public Seller() : base()
    {
        Products = new List<Product>()
        {
            new Product("меч", 500),
            new Product("щит", 300),
            new Product("кольчуга", 450),
            new Product("штаны", 250),
            new Product("шлем", 200),
            new Product("ботинки", 150)
        };
    }

    public void SalesProcess(Player player)
    {
        Console.Write("Введите название товара для покупки: \n");
        string productName = Console.ReadLine();

        Product productToBuy = Products.Find(product => product.Name == productName);

        if (productToBuy != null)
        {
            if (player.CanBuyProduct(productToBuy.Price))
            {
                player.BuyProduct(productToBuy);
                _sellerGold += productToBuy.Price;
            }
            else
            {
                Console.WriteLine("У вас недостаточно золота для покупки этого товара\n");
            }
        }
        else
        {
            Console.WriteLine("Такого товара нет в магазине\n");
        }
    }
}
