using LibraryShop;
using LibraryShop.MechanicalClasses;
using LibraryShop.StyleConsole;

internal class Program
{
    public static void Main(string[] args)
    {
        AllProducts products = new AllProducts();
        bool mainFlag = true;
        do
        {
            Menu switchMain = new Menu(new[] {"\t1. Передать путь к файлу для считывания и записи данных", 
                "\t2. Отсортировать коллекцию объектов по одному из полей", "\t3. " +
                "Выбрать объект и отредактировать в нем выбранное поле", "\t4. Вывести текущие объекты в консоль", 
                "\t5. Завершить работу программы"}, "Чтобы вы хотели сделать?");
            switch (switchMain.ShowMenu())
            {
                case 1:
                    MethodsForMenuMessages.GettingPath(ref products);
                    break;
                case 2:
                    MethodsForMenuMessages.PreparationForSorting(ref products);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    mainFlag = false;
                    break;
                default:
                    Console.WriteLine("Введенное значение может быть от 1 до 4, как выбор пункта для запуска действия, повторите попытку.");
                    break;
            }
        } while (mainFlag);
    }
}