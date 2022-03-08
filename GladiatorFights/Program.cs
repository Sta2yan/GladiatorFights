using System;
using System.Collections.Generic;

namespace GladiatorFights
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new Battle();
            battle.Start(battle.GetListWarriors());
        }
    }

    class Battle
    {
        public void Start(List<Warrior> warriors)
        {
            while (warriors.Count > 1)
            {
                Warrior warrior1;
                Warrior warrior2;

                do
                {
                    warrior1 = GetWarrior(warriors);
                    warrior2 = GetWarrior(warriors);
                } while (warrior1 == warrior2);

                Console.WriteLine("Встречаем бойцов:");
                Console.WriteLine();
                warrior1.ShowInfo();
                Console.WriteLine("       VS");
                warrior2.ShowInfo();
                Console.ReadKey(true);
                Console.Clear();

                StartFighting(warriors, warrior1, warrior2);

                Console.ReadKey(true);
                Console.Clear();
            }

            Console.WriteLine("Победителем турнира выходит воин:");
            warriors[0].ShowInfo();
            Console.WriteLine("Поздравляем!!!");
            Console.ReadKey(true);
        }

        public List<Warrior> GetListWarriors()
        {
            List<Warrior> warriors = new List<Warrior>();

            warriors.Add(new Viking("Викинг", 300, 100, 20, 2));
            warriors.Add(new Barbarian("Варвар", 250, 110, 30, 2));
            warriors.Add(new Archer("Лучник", 150, 100, 3));
            warriors.Add(new Witcher("Ведьмак", 200, 170, 50, 2));
            warriors.Add(new Elder("Старейшина", 100, 200));

            return warriors;
        }
        
        private Warrior GetWarrior(List<Warrior> warriors)
        {
            Warrior warrior;
            Random random = new Random();

            warrior = warriors[random.Next(0, warriors.Count)];

            return warrior;
        }

        private void StartFighting(List<Warrior> warriors, Warrior warrior1, Warrior warrior2)
        {
            while (warrior1.NumberHealth > 0 && warrior2.NumberHealth > 0)
            {
                if (warrior1.NumberHealth > 0)
                {
                    Console.WriteLine($"Атака воина {warrior1.Name}\n");
                    warrior2.TakeDamage(warrior1);
                    warrior2.ShowInfo();

                    if (warrior2.NumberHealth == 0)
                    {
                        Console.WriteLine($"Воин {warrior2.Name} пал ...");
                    }
                }

                if (warrior2.NumberHealth > 0)
                {
                    Console.WriteLine($"\n\nАтака воина {warrior2.Name}\n");
                    warrior1.TakeDamage(warrior2);
                    warrior1.ShowInfo();

                    if (warrior1.NumberHealth == 0)
                    {
                        Console.WriteLine($"Воин {warrior1.Name} пал ...");
                    }
                }

                Console.ReadKey(true);
                Console.Clear();
            }

            if (warrior1.NumberHealth > 0)
            {
                warriors.Remove(warrior2);
                Console.WriteLine($"Победил Воин {warrior1.Name}!");
                warrior1.ShowInfo();
            }

            if (warrior2.NumberHealth > 0)
            {
                warriors.Remove(warrior1);
                Console.WriteLine($"Победил Воин {warrior2.Name}!");
                warrior2.ShowInfo();
            }
        }
    }

    abstract class Warrior
    {
        protected Random random = new Random();
        protected int Health;
        protected int Damage;
        protected int Armor;
        protected int HitChance;
        private string _name;
        
        public string Name { get { return _name; } }
        public int NumberHealth { get { return Health; } }

        public Warrior(string name, int health, int damage, int armor, int hitchance)
        {
            _name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
            HitChance = hitchance;
        }

        abstract public void TakeDamage(Warrior warrior);
        abstract public int Attack();

        public void ShowInfo()
        {
            Console.WriteLine($"-----------------" +
                              $"\nИмя бойца - {Name}" +
                              $"\nЗдоровье - {Health}" +
                              $"\nУрон - {Damage}" +
                              $"\nБроня - {Armor}" +
                              $"\nШанс попадания - {HitChance}" +
                              $"\n-----------------");
        }
    }

    class Viking : Warrior
    {
        public Viking(string name, int health, int damage, int armor, int hitchance) :
            base(name, health, damage, armor, hitchance) { }

        public override int Attack()
        {
            return Damage * random.Next(0, HitChance);
        }

        public override void TakeDamage(Warrior warrior)
        {
            if (Health > 0)
            {
                int damage = warrior.Attack() - Armor;

                if (damage < 0)
                {
                    damage = 0;
                }

                Health -= damage;
                
                if (Health < 0)
                {
                    Health = 0;
                }
                
                Console.WriteLine($"Боец - {warrior.Name} нанес - {damage} урона Бойцу - {Name}");
            }
        }
    }

    class Barbarian : Warrior
    {
        public Barbarian(string name, int health, int damage, int armor, int hitchance) :
            base(name, health, damage, armor, hitchance) { }

        public override int Attack()
        {
            return Damage * random.Next(0, HitChance);
        }

        public override void TakeDamage(Warrior warrior)
        {
            if (Health > 0)
            {
                int damage = warrior.Attack() - Armor;

                if (damage < 0)
                {
                    damage = 0;
                }

                Health -= damage;

                if (Health < 0)
                {
                    Health = 0;
                }

                Console.WriteLine($"Боец - {warrior.Name} нанес - {damage} урона Бойцу - {Name}");
            }
        }
    }

    class Archer : Warrior
    {
        public Archer(string name, int health, int damage, int hitchance) :
            base(name, health, damage, 0, hitchance) { }

        public override int Attack()
        {
            return Damage * random.Next(0, HitChance);
        }

        public override void TakeDamage(Warrior warrior)
        {
            if (Health > 0)
            {
                int damage = warrior.Attack();

                if (damage < 0)
                {
                    damage = 0;
                }

                Health -= damage;

                if (Health < 0)
                {
                    Health = 0;
                }

                Console.WriteLine($"Боец - {warrior.Name} нанес - {damage} урона Бойцу - {Name}");
            }
        }
    }

    class Witcher : Warrior
    {
        public Witcher(string name, int health, int damage, int armor, int hitchance) :
            base(name, health, damage, armor, hitchance) { }

        public override int Attack()
        {
            int chanceScream = 7;

            if (random.Next(0, chanceScream) > 5)
            {
                Scream();
            }

            return Damage * random.Next(0, HitChance) - Armor / 2;
        }

        public override void TakeDamage(Warrior warrior)
        {
            if (Health > 0)
            {
                int damage = warrior.Attack() / random.Next(1, 2) - Armor;

                if (damage < 0)
                {
                    damage = 0;
                }

                Health -= damage;

                if (Health < 0)
                {
                    Health = 0;
                }

                Console.WriteLine($"Боец - {warrior.Name} нанес - {damage} урона Бойцу - {Name}");
            }
        }

        private void Scream()
        {
            int screamDamage = 50;
            Damage += screamDamage;
            Console.WriteLine($"{Name} пришел в ярость и увеличил силу удара на {screamDamage} единиц ...");
        }
    }

    class Elder : Warrior
    {
        public Elder(string name, int health, int damage) :
            base(name, health, damage, 0, 100) { }

        public override int Attack()
        {
            int chancePray = 5;

            if (random.Next(0, chancePray) > 2)
            {
                Pray();
            }

            return Damage;
        }

        public override void TakeDamage(Warrior warrior)
        {
            if (Health > 0)
            {
                int damage = warrior.Attack();

                if (damage < 0)
                {
                    damage = 0;
                }

                Health -= damage;

                if (Health < 0)
                {
                    Health = 0;
                }

                Console.WriteLine($"Боец - {warrior.Name} нанес - {damage} урона Бойцу - {Name}");
            }
        }

        private void Pray()
        {
            int healing = 50;
            Health += healing;
            Console.WriteLine($"{Name} помолился и восстановил {healing} очков здоровья ...");
        }
    }
}