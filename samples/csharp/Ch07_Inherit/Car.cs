namespace Ch07_Inherit
{
    internal class Car
    {
        public static int Total { get; set; }
        public int No { get; set; }
        public string Name { get; set; }

        public Car() 
        {
            Total++;
            No = Total;
            Name = "";
        }

        public Car(string name)
        {
            Total++;
            No = Total;
            Name = name;
        }

        ~Car() 
        {
            Total--;
        }

        public static string GetTotalCarString() 
        {
            return $"現在共有 {Total} 部車";
        }

        public string GetCarNoString()
        {
            return $"{Name} 是第 {No} 部車";
        }
    }
}
