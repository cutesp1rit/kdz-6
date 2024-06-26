using LibraryShop.StyleConsole;
namespace LibraryShop.MechanicalClasses;

public static class MethodsForMenuMessages
{
    /// <summary>
    /// Метод получения НЕ null и НЕ пустой строки
    /// </summary>
    /// <returns>Нужная строка</returns>
    public static string GetNotEmptyString()
    {
        string newString = Console.ReadLine();
        while (newString == null || newString == String.Empty)
        {
            Console.WriteLine("Введите не пустое значение и не null:");
            newString = Console.ReadLine();
        }
        return newString;
    }
    
    /// <summary>
    /// Метод для получения неотрицательного целого значения
    /// </summary>
    /// <returns>Нужное число</returns>
    public static int GetPosInt()
    {
        int newInt;
        while (!int.TryParse(Console.ReadLine(), out newInt) || newInt<0)
        {
            Console.WriteLine("Некорретные данные! Введите целое неотрицательное значение:");
        }
        return newInt;
    }
    
    /// <summary>
    /// Метод для получения неотрицательного числа типа double
    /// </summary>
    /// <returns>Нужное число</returns>
    public static double GetPosDouble()
    {
        double newDouble;
        while (!double.TryParse(Console.ReadLine(), out newDouble) || newDouble<0)
        {
            Console.WriteLine("Некорретные данные! Введите положительное вещественное значение:");
        }
        return newDouble;
    }
    
    /// <summary>
    /// Метод получения bool значения
    /// </summary>
    /// <returns>Нужное bool значение</returns>
    public static bool GetBoolValue()
    {
        string newBool = Console.ReadLine();
        while (newBool != "true" &&  newBool != "false")
        {
            Console.WriteLine("Вы можете ввести только два значения: \"true\" или \"false\"");
            newBool = Console.ReadLine();
        }
        if (newBool == "true")
        {
            return true;    
        }
        return false;
    }
    
    /// <summary>
    /// Получение пути и запись/получение данных по этому пути
    /// </summary>
    /// <param name="products">Объект с данными из json</param>
    public static void GettingPath(ref AllProducts products)
    {
        bool flagPath = true;
        do
        {
            Console.WriteLine("Введите путь, который собираетесь использовать в дальнейшем: ");
            string path = GetNotEmptyString();
            Menu switchMethod = new Menu(new[]
                {
                    "\t1. Получить данные по этому пути", 
                    "\t2. Записать текущие данные в файл по этому пути"
                },
                "Как бы вы хотели использовать путь?");
            try
            {
                switch (switchMethod.ShowMenu())
                {
                    case 1:
                        products = new AllProducts(WorkJson.JsonDeserialization(path));
                        break;
                    case 2:
                        if (products == null || products.Products.Count == 0) // в случае если данных еще нет
                        {
                            Console.WriteLine("Объекты еще не заданы, поэтому заново введите путь к файлу и" +
                                              " выберите первый пункт для их получения.");
                            continue;
                        }
                        else
                        {
                            WorkJson.JsonSerialization(products.Products, path);  
                        }
                        break;
                }
                flagPath = false;
            }
            catch (FormatException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentNullException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка обработки файла, в нем некорректные данные, пожалуйста," +
                                  " выберите другой путь.");
            }
            finally
            {
                Console.ResetColor();
            }
        } while (flagPath);
    }

    /// <summary>
    /// Выбор поля для сортировки и запуск методов
    /// </summary>
    /// <param name="products">Объект с данными из json</param>
    public static void PreparationForSorting(ref AllProducts products)
    {
        Menu switchMethod = new Menu(new[]
            {
                "\t1. widgetId", "\t2. name", "\t3. quantity", 
                "\t4. price", "\t5. isAvailable", "\t6. manufactureDate"
            },
            "По какому полю вы бы хотели произвести сортировку?");
        switch (switchMethod.ShowMenu())
        {
            case 1:
                products.SortForString("widgetId");
                break;
            case 2:
                products.SortForString("name");
                break;
            case 3:
                products.SortForQuantity();
                break;
            case 4:
                products.SortForPrice();
                break;
            case 5:
                products.SortForIsAvailable();
                break;
            case 6:
                products.SortForString("manufactureDate");
                break;
        }
        Console.WriteLine("Сортировка прошла успешно. Полученный результат: ");
        PrintProducts(products);
    }

    /// <summary>
    /// Метод для вывода объектов на экран
    /// </summary>
    /// <param name="products">Объект с данными из json</param>
    public static void PrintProducts(AllProducts products)
    {
        foreach (Product product in products.Products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("Для возвращения к меню нажмите любую кнопку.");
        Console.ReadLine();
    }
    
    /// <summary>
    /// Метод для изменения данных полей
    /// </summary>
    /// <param name="products">Объект с данными из json</param>
    public static void PreparationForChanges(ref AllProducts products)
    {
        // Создаем массив объектов в string формате
        string[] arrayObjectForSwitch = new string[products.Products.Count];
        for (int i = 0; i < products.Products.Count; i++)
        {
            arrayObjectForSwitch[i] = $"\t{i + 1}. " + products.Products[i];
        }
        // Передаем в меню, чтобы пользователь выбрал нужный
        Menu switchMethod = new Menu(arrayObjectForSwitch, "Какой объект вы бы хотели отредактировать?");
        int indexOfObject = switchMethod.ShowMenu() - 1;
        // Даем пользователю выбрать поле
        Menu switchMethod2 = new Menu(new[]
            {
                "\t1. name", "\t2. quantity", 
                "\t3. isAvailable", "\t4. manufactureDate", "\t5. specifications"
            }, "Какое поле вы бы хотели изменить?");
        // Меняем в соответствии с выбором нужное поле
        switch (switchMethod2.ShowMenu())
        {
            case 1:
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].Name}. Введите новое:");
                products.Products[indexOfObject].Name = GetNotEmptyString();
                break;
            case 2:
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].Quantity}. Введите новое:");
                products.Products[indexOfObject].Quantity = GetPosInt();
                break;
            case 3:
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].IsAvailable}. Введите новое:");
                products.Products[indexOfObject].IsAvailable = GetBoolValue();
                break;
            case 4:
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].ManufactureDate}. Введите новое:");
                products.Products[indexOfObject].ManufactureDate = GetNotEmptyString();
                break;
            case 5:
                // Если выбрали поле с вложенными объектами, то повторяем процедуру
                Product thisProduct = products.Products[indexOfObject];
                ChangeAddInf(ref thisProduct);
                products.Products[indexOfObject] = thisProduct;
                break;
        }
    }

    /// <summary>
    /// Метод схожий предыдущему, но для изменения объекта класса ProductAddInf
    /// </summary>
    /// <param name="product">Объект, с которым производятся манипуляции</param>
    public static void ChangeAddInf(ref Product product)
    {
        Console.WriteLine("Это массив вложенных объектов, поэтому вам нужно будет еще раз выбрать объект и поле:");
        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить..");
        Console.ReadLine();
        // Создаем массив объектов в string формате
        string[] arrayObjectForSwitch = new string[product.Specifications.Length];
        for (int i = 0; i < product.Specifications.Length; i++)
        {
            arrayObjectForSwitch[i] = $"\t{i + 1}. " + product.Specifications[i];
        }
        // Передаем в меню, чтобы пользователь выбрал нужный
        Menu switchMethod = new Menu(arrayObjectForSwitch, "Какой объект вы бы хотели отредактировать?");
        int indexOfObject = switchMethod.ShowMenu() - 1;
        // Даем пользователю выбрать поле
        Menu switchMethod2 = new Menu(new[]
        {
            "\t6.1 specifications: specName", "\t6.2. specifications: specPrice", "\t6.3. specifications: isCustom"
        }, "Какое поле вы бы хотели изменить?");
        // Меняем в соответствии с выбором нужное поле
        switch (switchMethod2.ShowMenu())
        {
            case 1:
                Console.WriteLine($"Текущее значение: {product.Specifications[indexOfObject].SpecName}. Введите новое:");
                product.Specifications[indexOfObject].SpecName = GetNotEmptyString();
                break;
            case 2:
                Console.WriteLine($"Текущее значение: {product.Specifications[indexOfObject].SpecPrice}. Введите новое:");
                product.Specifications[indexOfObject].SpecPrice = GetPosDouble();
                break;
            case 3:
                Console.WriteLine($"Текущее значение: {product.Specifications[indexOfObject].IsCustom}. Введите новое:");
                product.Specifications[indexOfObject].IsCustom = GetBoolValue();
                break;
        }
    }
}