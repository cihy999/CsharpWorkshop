namespace Pattern_Ch10_Strategy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Character soldier = new Character(CharacterType.Soldier, "Sam");
            Character enemy = new Character(CharacterType.Enemy, "BT");
            soldier.Initialize();
            enemy.Initialize();
            soldier.Attack(enemy);
            enemy.Attack(soldier);
        }
    }
}
