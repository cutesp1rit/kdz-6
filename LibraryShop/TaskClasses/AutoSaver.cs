using LibraryShop.MechanicalClasses;

namespace LibraryShop;

public class AutoSaver
{
    private DateTime _oldData;
    private List<Product> _products;
    
    public AutoSaver(List<Product> products)
    {
        _products = products ?? throw new ArgumentNullException(nameof(products),
            "Ошибка инициализации \"products\"");
        _oldData = new DateTime();
    }

    public AutoSaver()
    {
        _products = new List<Product>();
        _oldData = new DateTime();
    }
    
    /// <summary>
    /// Метод для сравнения двух дат. В случае если прошло не больше чем 15 секунд, запуск записи файла
    /// </summary>
    /// <param name="sender">Объкт вызывающий событие</param>
    /// <param name="args">Новая дата</param>
    public void WhenItChanged(object sender, DataEventArgs args)
    {
        if (_oldData == new DateTime()) // если старое значение еще не задано
        {
            _oldData = args.DataNewChanged;
            return;
        }

        if ((args.DataNewChanged - _oldData).TotalSeconds <= 15)
        {
            Console.WriteLine("Вы произвели несколько изменений не больше чем за 15 секунд, поэтому они будут записы в файл!");
            WorkJson.JsonSerialization(_products, ".\\Products_tmp.json");
        }

        _oldData = args.DataNewChanged;
    }
}