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
    
    
}