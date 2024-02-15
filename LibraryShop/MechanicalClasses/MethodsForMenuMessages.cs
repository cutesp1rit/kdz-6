using LibraryShop.StyleConsole;

namespace LibraryShop.MechanicalClasses;

public static class MethodsForMenuMessages
{
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
    /// <returns></returns>
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
    /// <returns></returns>
    public static double GetPosDouble()
    {
        double newDouble;
        while (!double.TryParse(Console.ReadLine(), out newDouble) || newDouble<0)
        {
            Console.WriteLine("Некорретные данные! Введите неотрицательное значение:");
        }
        return newDouble;
    }
    
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
    
    public static void GettingPath(ref AllProducts products)
    {
        bool flagPath = true;
        do
        {
            Console.WriteLine("Введите путь, который собираетесь использовать в дальнейшем: ");
            Console.ReadLine();
            string path = ".\\3V.json"; // GetNotEmptyString();
            Menu switchMethod = new Menu(new[]
                {
                    "\t1. Получить данные по этому пути", 
                    "\t2. Записать текущие данные в файл"
                },
                "Чтобы вы хотели использовать файл?");
            try
            {
                switch (switchMethod.ShowMenu())
                {
                    case 1:
                        products = new AllProducts(WorkJson.JsonDeserialization(path));
                        break;
                    case 2:
                        if (products == null || products.Products.Count == 0)
                        {
                            Console.WriteLine("Объекты еще не заданы, поэтому заново введите путь к файлу и" +
                                              " выберите первый пункт для их получения.");
                            continue;
                        }
                        else
                        {
                            // Здесь должен быть метод для сериализации данных    
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
                Console.WriteLine("Возникла ошибка при открытии файла, повторите попытку: ");
            }
            catch (IOException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введено некорректное название файла или он находится не в текущей директории, повторите попытку: ");
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку: ");
            }
            finally
            {
                Console.ResetColor();
            }
        } while (flagPath);
    }

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

    public static void PrintProducts(AllProducts products)
    {
        foreach (Product product in products.Products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("Для возвращения к меню нажмите любую кнопку.");
        Console.ReadLine();
    }
    public static void PreparationForChanges(ref AllProducts products)
    {
        string[] arrayObjectForSwitch = new string[products.Products.Count];
        for (int i = 0; i < products.Products.Count; i++)
        {
            arrayObjectForSwitch[i] = $"\t{i + 1}. " + products.Products[i];
        }
        Menu switchMethod = new Menu(arrayObjectForSwitch, "Какой объект вы бы хотели отредактировать?");
        int indexOfObject = switchMethod.ShowMenu() - 1;
        Menu switchMethod2 = new Menu(new[]
            {
                "\t1. name", "\t2. quantity", 
                "\t3. price", "\t4. isAvailable", "\t5. manufactureDate",
                "\t6. specifications"
            }, "Какое поле вы бы хотели изменить?");
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
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].Price}. Введите новое:");
                products.Products[indexOfObject].Price = GetPosDouble();
                break;
            case 4:
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].IsAvailable}. Введите новое:");
                products.Products[indexOfObject].IsAvailable = GetBoolValue();
                break;
            case 5:
                Console.WriteLine($"Текущее значение: {products.Products[indexOfObject].ManufactureDate}. Введите новое:");
                products.Products[indexOfObject].ManufactureDate = GetNotEmptyString();
                break;
            case 6:
                Product thisProduct = products.Products[indexOfObject];
                ChangeAddInf(ref thisProduct);
                products.Products[indexOfObject] = thisProduct;
                break;
        }
    }

    public static void ChangeAddInf(ref Product product)
    {
        Console.WriteLine("Это массив вложенных объектов, поэтому вам нужно будет еще раз выбрать объект и поле:");
        string[] arrayObjectForSwitch = new string[product.Specifications.Length];
        for (int i = 0; i < product.Specifications.Length; i++)
        {
            arrayObjectForSwitch[i] = $"\t{i + 1}. " + product.Specifications[i];
        }
        Menu switchMethod = new Menu(arrayObjectForSwitch, "Какой объект вы бы хотели отредактировать?");
        int indexOfObject = switchMethod.ShowMenu() - 1;
        Menu switchMethod2 = new Menu(new[]
        {
            "\t6.1 specifications: specName", "\t6.2. specifications: isCustom"
        }, "Какое поле вы бы хотели изменить?");
        switch (switchMethod2.ShowMenu())
        {
            case 1:
                Console.WriteLine($"Текущее значение: {product.Specifications[indexOfObject].SpecName}. Введите новое:");
                product.Specifications[indexOfObject].SpecName = GetNotEmptyString();
                break;
            case 2:
                Console.WriteLine($"Текущее значение: {product.Specifications[indexOfObject].IsCustom}. Введите новое:");
                product.Specifications[indexOfObject].IsCustom = GetBoolValue();
                break;
        }
    }
}