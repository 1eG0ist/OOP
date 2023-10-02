using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Auto
    {
        private static int number_of_auto = 1;
        private static List<int[]> autos_positions = new List<int[]>();
        private int n_of_auto;
        private string color;
        private string name;
        private float fuel_tank_size;
        private float coms_per_100_km;
        private float coms_per_km_multiplier;
        private float fuel_now;
        private float mileage;
        private int x;
        private int y;
        private float speed;
        private float health;


        public Auto(string name, string color, float fuel_tank_size, float coms_per_100_km, float fuel_now, float mileage, int x, int y)
        {
            this.color = color;
            this.name = name;
            this.fuel_tank_size = fuel_tank_size;
            this.coms_per_100_km = coms_per_100_km;
            this.fuel_now = fuel_now;
            this.mileage = mileage;
            this.x = x;
            this.y = y;
            int[] _coords = new int[] { x, y };
            autos_positions.Add(_coords);
            health = 100;
            speed = 0;
            coms_per_km_multiplier = 1;
            n_of_auto=number_of_auto-1;
            number_of_auto++;
        }

        public void goTo(int x, int y)
        {
            if (speed == 0)
            {
                Console.WriteLine("You have 0 speed, pls up your speed to travel, press any key to continue");
                Console.ReadKey();
                return;
            }
            float distance = calculateDistance(x, y);
            float needs_fuel = distance * coms_per_100_km * coms_per_km_multiplier / 100;
            if (needs_fuel > fuel_now)
            {
                Console.WriteLine("You don't have enough fuel, you want to refuel? (1 - Yes, 2 -No)");
                string answer = Console.ReadLine();
                if (answer == "1") {
                    refuel();
                    goTo(x, y);
                } else
                {
                    Console.WriteLine("You haven't gone anywhere");
                }
            } else
            {
                for (int i = 0; i < autos_positions.Count; i++)
                {
                    if (autos_positions[i][0] == x && autos_positions[i][1] == y)
                    {
                        autoCrash();
                        break;
                    }
                }
                setPosition(x, y);
                autos_positions[n_of_auto] = new int[] { x, y };
                this.mileage += distance;
                fuel_now -= needs_fuel;
            }
        } 

        public float calculateDistance(int x, int y)
        {
            return (float)Math.Sqrt(Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2));
        }

        public void speedUp(float up_on)
        {
            if (speed + up_on >= 500)
            {
                Console.WriteLine("Too big speed");
            } else
            {
                speed += up_on;
                updateComsPerKmMultiplier();
            }
        }

        public void autoCrash()
        {
            health -= 20;
            Console.WriteLine("The auto crashed into another auto!!! Press any key to continue: ");
            Console.ReadKey();
        }

        public void speedDown(float down_on)
        {
            if (speed - down_on < 0)
            {
                Console.WriteLine("Error: speed is under of 0, seted speed = 0");
                speed = 0;
            } else
            {
                speed -= down_on;
                updateComsPerKmMultiplier();
            }
        }

        public void updateComsPerKmMultiplier()
        {
            if (speed < 50) coms_per_km_multiplier = 1;
            else if (speed >= 50 && speed < 100) coms_per_km_multiplier = 1.1f;
            else if (speed >= 100 && speed < 150) coms_per_km_multiplier = 1.2f;
            else if (speed >= 150 && speed < 250) coms_per_km_multiplier = 1.3f;
            else if (speed >= 250 && speed < 350) coms_per_km_multiplier = 1.4f;
            else if (speed >= 350) coms_per_km_multiplier = 1.5f;
        }

        public void setPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void changeColor(string new_color)
        {
            color = new_color;
        }

        public void setFuelNow(float fuel_now)
        {
            this.fuel_now = fuel_now;
        }

        public void refuel()
        {
            fuel_now = fuel_tank_size;
        }

        public void setNewColor(string new_color)
        {
            color = new_color;
        }

        public void modernFuelTank(float new_fuel_tank_size)
        {
            fuel_tank_size = new_fuel_tank_size;
        }

        public float getSpeed()
        {
            return speed;
        }

        public float getHealth()
        {
            return health;
        }

        public override string ToString()
        {
            return $"Number of created auto - {n_of_auto}\nName - {name}\nColor - {color}\nFuel tank size - {fuel_tank_size}\n" +
                $"Coms per 100 km - {coms_per_100_km}\nFuel now - {fuel_now}\nMileage - {mileage}\nX - {x}\nY - {y}\nSpeed - {speed}\nHealth - {health}";
        }
    }
}
