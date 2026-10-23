namespace Ch07_Inherit
{
    internal abstract class Traffic
    {
        protected static int _miles = 0;

        public virtual void SpeedUp()
        { 
            
        }
    }

    internal class Boat : Traffic
    {
        public override void SpeedUp()
        {
            _miles += 5;
            Console.WriteLine($"駕駛船，加速中，前進至{_miles}公里");
        }
    }

    internal class Airplane : Traffic
    {
        public override void SpeedUp()
        {
            _miles += 10;
            Console.WriteLine($"駕駛飛機，加速中，前進至{_miles}公里");
        }
    }
}
