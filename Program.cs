/*
 * 1. Cделать подключение к базе данных
 * 2. Проектирование базы данных (предп. 2 таблицы)
 * 3. Продумать структуру проекта 

///////////////////////////////////////////////////
Разработать консольное .NET приложение для склада, удовлетворяющее следующим требованиям:
- Построить иерархию классов, описывающих объекты на складе - паллеты и коробки:
    - Помимо общего набора стандартных свойств (ID, ширина, высота, глубина, вес), паллета может содержать в себе коробки.
    - У коробки должен быть указан срок годности или дата производства. Если указана дата производства, то срок годности вычисляется из даты производства плюс 100 дней.
        - Срок годности и дата производства — это конкретная дата без времени (например, 01.01.2023).
    - Срок годности паллеты вычисляется из наименьшего срока годности коробки, вложенной в паллету. Вес паллеты вычисляется из суммы веса вложенных коробок + 30кг.
    - Объем коробки вычисляется как произведение ширины, высоты и глубины.
    - Объем паллеты вычисляется как сумма объема всех находящихся на ней коробок и произведения ширины, высоты и глубины паллеты.
    - Каждая коробка не должна превышать по размерам паллету (по ширине и глубине).
- Консольное приложение:
    - Получение данных для приложения можно организовать одним из способов:
        - Генерация прямо в приложении
        - Чтение из файла или БД
        - Пользовательский ввод
    - Вывести на экран:
        - Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.
        - 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.
- (Опционально) Покрыть функционал unit-тестами.
- (Очень желательно) Код должен быть написан в соответствии с https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
- (Совершенно не обязательно) Вместо консольного приложения сделать полноценный пользовательский интерфейс. На оценку решения никак не влияет.

*/

// Program.cs
using System;
using System.Collections.Generic;
using System.Data.Common;
using Console_test_task;
using Console_test_task.Models;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        
        // Группировка по сроку
        Console.WriteLine("Паллеты, сгруппированные по сроку годности \n ");

          foreach (var group in UsingDB.pallets
               .GroupBy(p => GetPalletExpirationDate(p))
               .OrderBy(g => g.Key))
           {
               foreach (var pallet in group
                   .OrderBy(p => GetPalletWeight(p))
                   )
               {
                   Console.WriteLine($"Паллет #{pallet.Id}\n" +
                   $"Вес: {GetPalletWeight(pallet)} кг");
                   Console.WriteLine($"Срок годности: {group.Key:dd-MM-yy}\n\n");
               }
           }
        


        // 3 паллеты с коробками, имеющими максимальный срок годности, отсортированные по объёму
        Console.WriteLine("\n\nТоп 3 паллеты с максимальным сроком годности коробок");

        var top3 = UsingDB.pallets
            .Where(p => p.Boxes.Count > 0)
            .OrderByDescending(p => GetMaxBoxExpiration(p))
            .Take(3)
            .OrderBy(p => GetPalletVolume(p));

        foreach (var pallet in top3)
        {
            Console.WriteLine($"\n\nПаллет #{pallet.Id} \n" +
                $" Макс. срок годности коробки: {GetMaxBoxExpiration(pallet):yyyy-MM-dd},\n" +
                $"Объем: {Math.Round(GetPalletVolume(pallet))}");
        }
    }

   
    
    static DateTime? GetBoxExpiration(Box box)
    {
        return box.ExpirationDate ?? box.ProductionDate?.AddDays(100);
    }

    static DateTime? GetPalletExpirationDate(Pallet pallet)
    {
        var expirations = pallet.Boxes
            .Select(GetBoxExpiration)
            .Where(d => d.HasValue)
            .Select(d => d.Value);

        return expirations.Any() ? expirations.Min() : null;
    }

    static DateTime? GetMaxBoxExpiration(Pallet pallet)
    {
        var expirations = pallet.Boxes
            .Select(GetBoxExpiration)
            .Where(d => d.HasValue)
            .Select(d => d.Value);

        return expirations.Any() ? expirations.Max() : null;
    }

    static decimal GetBoxVolume(Box box) =>
        box.Width * box.Height * box.Depth;

    static decimal GetPalletVolume(Pallet pallet) =>
        pallet.Width * pallet.Height * pallet.Depth +
        pallet.Boxes.Sum(GetBoxVolume);

    static decimal GetPalletWeight(Pallet pallet) =>
        pallet.Boxes.Sum(b => b.Weight) + 30;
}
