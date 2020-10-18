using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp11
{

	class Game
	{
		LinkedList<Car> cars;
		public int Distance { get; set; }

		public delegate void Start();
		public delegate void End();
		public delegate void WhereMe();
		public delegate void RerendomSpeed();
		public event  RerendomSpeed reSpeed;
		public event WhereMe whereMe;
		public event Start start;
		public event End end;
		public Game( int distance)
		{
			cars = new LinkedList<Car>();
			Distance = distance;
			SportCar SCar = new SportCar();
			TrackCar TCar = new TrackCar();
			Bus BCar = new Bus();
			LightCar LCar = new LightCar();
			cars.AddLast(SCar);
			cars.AddLast(TCar);
			cars.AddLast(BCar);
			cars.AddLast(LCar);
			ChekDist();
			Final();
			ReSpeed();

		}
		private void ReadyCheck()
		{
			foreach (Car item in cars)
			{
				start += item.Ready;
			}
			start();
			
		}
		private void ChekDist()
		{
			foreach (Car item in cars)
			{
				whereMe += item.WhereIam;
			}
			
		}
		private void Final()
		{
			foreach (Car item in cars)
			{
				end += item.Finish;
			}


		}
		private void ReSpeed()
		{
			foreach (Car item in cars)
			{
				reSpeed += item.SetSpeed;
			}
		}
		
		public void Play()
		{
			ReadyCheck();
			Thread.Sleep(1000);
			Console.Clear();
			Console.WriteLine("Race started");
			int timer = 0;
		
			do
			{

				int deley = 1000;
				Thread.Sleep(deley);				
				timer++;
				foreach (Car item in cars)
				{
					item.TreveledDistance += ((item.Speed/14) *((deley/1000)*100))/this.Distance  ;   // deley в данном случае = единица времени,  а TreveledDistance в процентах считается
				}
				if (timer % 5 == 0)
				{
					Console.Clear();
					reSpeed();
					Console.WriteLine("\t\tSpeed is changed\n\n");
					whereMe();
				}
					

				foreach (Car item in cars)
				{
					if (item.TreveledDistance>=100)
					{
						Console.Clear();
						end();
						Console.ReadKey();
						Environment.Exit(0);
					}
				}


				


			} while (true);

		}

		



	}
	abstract class Car
	{
	
		public int Speed { get; set; }
		public int TreveledDistance { get; set; } = 0;
		public string Name { get; set; }
		

		public void WhereIam() =>Console.WriteLine($"{Name}'s progres = {TreveledDistance}%");
		public void SetSpeed()
		{

			int minSp = this.Speed - 200;
			int maxSp = this.Speed + 200;
			Random r = new Random();
			this.Speed = r.Next(minSp, maxSp) - 1;
			if (Speed<100)
			{
				Speed = 200;
			}

		}

		public void Ready() =>	Console.WriteLine($"{Name} is ready for race");
		
		public void Finish()
		{
			if (TreveledDistance>=100)
			{
				Console.WriteLine($"\tCONGRATULATION, {Name} Arrived \n");
			}
			else
			{
				Console.WriteLine($"{Name}, Race is done, pls Stop");
				Speed = 0;
			}
		}

		public void move() =>	Console.WriteLine($"начал двигатся со скоростью {Speed}");
		
		public Car(int speed, string name )
		{
			Name = name;
			Speed = speed;
			SetSpeed();
		}
		public override string ToString()
		{
			return $"{Name} Ride with speed: {Speed}";
		}
	}  

	class SportCar : Car
	{
		public SportCar(int speed=400, string name = "Sport Car") : base(speed, name) { }		
	}

	class TrackCar : Car
	{
		public TrackCar(int speed =320, string name = "Track Car") : base(speed, name) { }
	}
	class LightCar : Car
	{
		public LightCar(int speed =350, string name = "Light Car") : base(speed, name) { }
	}

	class Bus : Car
	{
		public Bus(int speed =300, string name = "Bus") : base(speed, name) { }
		
	}
}
