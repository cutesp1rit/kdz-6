using LibraryShop;
using LibraryShop.MechanicalClasses;
using LibraryShop.StyleConsole;

internal class Program
{
    // Советую раскрыть панельку побольше, чтобы все работало красиво!!
    // В папке уже есть файлик, который был дан по условию, его можно получить по пути: .\3V.json
    public static void Main(string[] args)
    {
        AllProducts products = new AllProducts();
        // получение данных в самом начале!
        MethodsForMenuMessages.GettingPath(ref products);
        bool mainFlag = true;
        do
        {
            try
            {
                // Основное меню для работы с данными
                Menu switchMain = new Menu(new[]
                {
                    "\t1. Передать путь к файлу для считывания и записи данных",
                    "\t2. Отсортировать коллекцию объектов по одному из полей", 
                    "\t3. Выбрать объект и отредактировать в нем выбранное поле",
                    "\t4. Вывести текущие объекты в консоль",
                    "\t5. Завершить работу программы"
                }, "Чтобы вы хотели сделать?");
                switch (switchMain.ShowMenu())
                {
                    case 1:
                        MethodsForMenuMessages.GettingPath(ref products);
                        break;
                    case 2:
                        MethodsForMenuMessages.PreparationForSorting(ref products);
                        break;
                    case 3:
                        MethodsForMenuMessages.PreparationForChanges(ref products);
                        break;
                    case 4:
                        MethodsForMenuMessages.PrintProducts(products);
                        break;
                    case 5:
                        // Завершает работу программы
                        mainFlag = false;
                        break;
                    default:
                        Console.WriteLine(
                            "Введенное значение может быть от 1 до 5, как выбор пункта для запуска действия, повторите попытку.");
                        break;
                }
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
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неизвестная ошибка.");
            }
            finally
            {
                Console.ResetColor();
            }
        } while (mainFlag);
    }
}