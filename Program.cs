using System.Transactions;


namespace OOP
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello");
            List<Auto> vsevolods_autopark = new List<Auto>()
            {
                new Auto("BMW", "DARK", 100, 30, 86, 0, 0, 0),
                new Auto("AUDI", "BLUE", 120, 45, 64, 0, 1, 0),
                new Auto("LADA", "NO", 50, 19, 25, 0, 2, 0),
            };
            for (int i = 0; i < vsevolods_autopark.Count; i++)
            {
                while(true)
                {
                    Console.WriteLine($"Enter fuel tank size of auto №{i+1}");
                    float fuel_size = float.Parse(Console.ReadLine());
                    if ( fuel_size <= 0 )
                    {
                        Console.WriteLine("Incorrect fuel tank size, Press any key to enter again: ");
                        Console.ReadKey();
                        continue;
                    }
                    
                    Console.WriteLine($"Enter how many fuel auto №{i + 1} have now (0-{fuel_size}): ");
                    float fuel_now = float.Parse(Console.ReadLine());
                    if ( fuel_now < 0 || fuel_now > fuel_size )
                    {
                        Console.WriteLine("Incorrect fuel now, Press any key to enter again: ");
                        Console.ReadKey();
                        continue;
                    }

                    vsevolods_autopark[i].modernFuelTank(fuel_size);
                    vsevolods_autopark[i].setFuelNow(fuel_now);
                    break;
                }
                
            }
            int selected_auto = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine($"Q - Show auto info\nW - Change color\nE - Modern fuel tank\nR - Refuel\nT - Go\nY - Change selected auto (1-3)\nU - Up speed\nI - Down speed");
                    ConsoleKey input_key = Console.ReadKey().Key;
                    Console.WriteLine("\n");
                    int counter = 0;
                    for (int i = 0; i < vsevolods_autopark.Count; i++)
                    {
                        if (vsevolods_autopark[i].getHealth() <= 0)
                        {
                            counter++;
                        }
                    }
                    if (counter == 3)
                    {
                        Console.WriteLine("All autos was total crashed! You Lose! Bye!");
                        return;
                    }

                    if (vsevolods_autopark[selected_auto].getHealth() <= 0)
                    {
                        try
                        {
                            Console.WriteLine($"Auto № {selected_auto + 1} full crashed, enter new number of auto (at start you had 3): ");
                            selected_auto = int.Parse(Console.ReadLine()) - 1;
                            continue;
                        }
                        catch
                        {
                            Console.WriteLine("Something went wrong! Press any key to try again: ");
                            Console.ReadKey();
                            continue;
                        }
                    }

                    switch (input_key)
                    {
                        case ConsoleKey.Q:
                            Console.WriteLine(vsevolods_autopark[selected_auto]);
                            next();
                            Console.Clear();
                            break;

                        case ConsoleKey.W:
                            while (true)
                            {
                                Console.WriteLine("Enter new color: ");
                                string new_color = Console.ReadLine();
                                if (new_color.Length == 0)
                                {
                                    Console.WriteLine("0 Length colors isn't supporting!!! Try again");
                                    continue;
                                }

                                vsevolods_autopark[selected_auto].setNewColor(new_color);
                                Console.Clear();
                                break;
                            }
                            break;

                        case ConsoleKey.E:
                            while(true)
                            {
                                float fuel_size = float.Parse(Console.ReadLine());
                                if (fuel_size <= 0)
                                {
                                    Console.WriteLine("Incorrect fuel tank size, Press any key to enter again: ");
                                    Console.ReadKey();
                                    continue;
                                }
                                vsevolods_autopark[selected_auto].modernFuelTank(fuel_size);
                                Console.Clear();
                                break;
                            }
                            break;

                        case ConsoleKey.R:
                            vsevolods_autopark[selected_auto].refuel();
                            next();
                            Console.Clear();
                            break;

                        case ConsoleKey.T:
                            while(true)
                            {
                                Console.WriteLine("Enter integer x y coords like 5 1 or 9 5: ");
                                string coords = Console.ReadLine();
                                if (coords.Length == 0 || coords.Count(x => x == ' ') != 1)
                                {
                                    Console.WriteLine("Error, something wrong in your coords, try again!");
                                    continue;
                                }
                                vsevolods_autopark[selected_auto].goTo(int.Parse(coords.Split(' ')[0]), int.Parse(coords.Split(' ')[1]));
                                Console.Clear();
                                break;
                            }
                            break;

                        case ConsoleKey.Y:
                            while(true)
                            {
                                Console.WriteLine("Enter new selected auto: ");
                                int new_sel_auto = int.Parse(Console.ReadLine()) - 1;
                                if (new_sel_auto < 0 || new_sel_auto > 2)
                                {
                                    Console.WriteLine("You have only 3 autos, pls enter 1 or 2 or 3!");
                                    continue;
                                }

                                selected_auto = new_sel_auto;
                                Console.Clear();
                                break;
                            }
                            break;

                        case ConsoleKey.U:
                            Console.WriteLine($"Enter on how many you want to up speed (now is {vsevolods_autopark[selected_auto].getSpeed()})");
                            vsevolods_autopark[selected_auto].speedUp(float.Parse(Console.ReadLine()));
                            Console.Clear();
                            break;

                        case ConsoleKey.I:
                            Console.WriteLine($"Enter on how many you want to down speed (now is {vsevolods_autopark[selected_auto].getSpeed()})");
                            vsevolods_autopark[selected_auto].speedDown(float.Parse(Console.ReadLine()));
                            Console.Clear();
                            break;

                        default:
                            Console.Clear();
                            break;
                    }
                } catch (Exception ex)
                {
                    continue;
                }
            }
        }

        static void next()
        {
            Console.WriteLine("\nPress any keys to go to menu: ");
            Console.ReadKey();
        }
    }
}