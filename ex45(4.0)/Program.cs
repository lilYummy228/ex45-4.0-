using System;
using System.Collections.Generic;

namespace ex45_4._0_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDirection = "1";
            const string CommandShowAllDirections = "2";
            const string CommandExit = "3";

            Train train = new Train();
            bool isOpen = true;            

            while (isOpen)
            {
                Console.CursorVisible = true;
                Console.WriteLine("Конфигуратор пассажирских поездов");
                Console.Write($"\n{CommandAddDirection} - создать направление\n" +
                    $"{CommandShowAllDirections} - показать все направления\n" +
                    $"{CommandExit} - выйти из программы\n" +
                    $"\nВаш ввод: ");

                switch (Console.ReadLine())
                {
                    case CommandAddDirection:
                        train.AddDirection();
                        break;

                    case CommandShowAllDirections:
                        train.ShowInfo();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;
                }

                Console.Clear();
            }
        }

        class Train
        {
            List<Direction> _directions = new List<Direction>();
            List<string> _train = new List<string>();
            List<string> _trains = new List<string>();

            Wagon[] _wagons = { new Wagon(20, "S"), new Wagon(50, "M"), new Wagon(100, "L") };

            public void AddDirection()
            {
                if (IsDirectionAlreadyExist() == false)
                {
                    CreateTrain();
                }
            }

            public void ShowInfo()
            {
                int position = 1;
                Console.Clear();

                if (_trains.Count > 0)
                {
                    Console.WriteLine("Направления: ");

                    foreach (Direction direction in _directions)
                    {
                        direction.ShowInfo();
                    }

                    Console.SetCursorPosition(40, 0);
                    Console.Write("Поезда: ");

                    foreach (string train in _trains)
                    {
                        Console.SetCursorPosition(40, position);
                        Console.Write($"{train}[)");
                        position++;
                    }
                }
                else
                {
                    Console.WriteLine("Пока направлений нет...");
                }

                Console.ReadKey();
            }

            public string DrawTrain()
            {
                string train = "";

                foreach (string wagon in _train)
                {
                    train += $"[{wagon}]-";
                }

                Console.Write($"{train}[)");
                return train;
            }

            private bool IsDirectionAlreadyExist()
            {
                Console.Write("\nВпишите точку отправления: ");
                string departure = Console.ReadLine();
                Console.Write("Впишите точку прибытия: ");
                string arrival = Console.ReadLine();
                bool isExist = false;

                foreach (Direction direction in _directions)
                {
                    if (direction.PointOfDeparture == departure && direction.PointOfArrival == arrival)
                    {
                        isExist = true;
                    }
                }

                if (isExist == false)
                {
                    GetDirection(departure, arrival);
                    return false;
                }
                else
                {
                    Console.WriteLine("Такое направление уже есть...");
                    Console.ReadKey();
                    return true;
                }
            }

            private void GetDirection(string departure, string arrival)
            {
                _directions.Add(new Direction(departure, arrival));
            }

            private int GetPassengersCount()
            {
                Random random = new Random();
                int passengers = random.Next(100, 501);
                return passengers;
            }

            private void CreateTrain()
            {
                const int CommandAddSmallWagon = 1;
                const int CommandAddMediumWagon = 2;
                const int CommandAddLargeWagon = 3;
                const int CommandSendTrain = 4;

                List<string> train = new List<string>();
                _train = train;

                int passengersCount = GetPassengersCount();
                int smallWagonIndex = 0;
                int mediumWagonIndex = 1;
                int largeWagonIndex = 2;
                bool isFilled = false;

                while (isFilled == false)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 10);
                    DrawTrain();
                    Console.SetCursorPosition(0, 0);

                    if (passengersCount > 0)
                    {
                        Console.WriteLine($"Нужно добавить места для {passengersCount} пассажиров");
                    }
                    else
                    {
                        int freePlaces = passengersCount * -1;
                        Console.WriteLine($"В поезде {freePlaces} свободных мест");
                    }

                    Console.Write($"{CommandAddSmallWagon} - добавить вагон на {_wagons[smallWagonIndex].Capacity} мест\n" +
                        $"{CommandAddMediumWagon} - добавить вагон на {_wagons[mediumWagonIndex].Capacity} мест\n" +
                        $"{CommandAddLargeWagon} - добавить вагон на {_wagons[largeWagonIndex].Capacity} мест\n" +
                        $"{CommandSendTrain} - отправить поезд\n" +
                        $"\nСоставьте поезд: ");

                    if (int.TryParse(Console.ReadLine(), out int number))
                    {
                        if (number != CommandSendTrain)
                        {
                            if (number - 1 <= _wagons.Length)
                            {
                                _train.Add(_wagons[number - 1].Mark);
                                passengersCount -= _wagons[number - 1].Capacity;
                            }
                            else
                            {
                                Console.WriteLine("Такого вагона нет...");
                            }
                        }
                        else
                        {
                            if (passengersCount <= 0)
                            {
                                Console.CursorVisible = false;
                                Console.Clear();
                                Console.WriteLine("Все пассажиры размещены, отправляем поезд...");
                                _trains.Add(DrawTrain());
                                isFilled = true;
                            }
                            else
                            {
                                Console.WriteLine($"Еще {passengersCount} пассажиров ждут своих мест. Добавьте больше вагонов...");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неккоректный ввод...");
                    }

                    Console.ReadKey();
                }
            }
        }

        class Direction
        {
            public Direction(string pointOfDeparture, string pointOfArrival)
            {
                PointOfDeparture = pointOfDeparture;
                PointOfArrival = pointOfArrival;
            }

            public string PointOfDeparture { get; private set; }
            public string PointOfArrival { get; private set; }

            public void ShowInfo()
            {
                Console.WriteLine($"{PointOfDeparture} - {PointOfArrival}");
            }
        }

        class Wagon
        {
            public Wagon(int capacity, string mark)
            {
                Capacity = capacity;
                Mark = mark;
            }

            public string Mark { get; private set; }

            public int Capacity { get; private set; }
        }
    }
}
