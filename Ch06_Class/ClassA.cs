using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch06_Class
{
    internal class ClassA
    {
        private int _number = 0;

        public int Number 
        { 
            get 
            { 
                return _number; 
            } 
            set 
            { 
                if (value < 0) _number = 0;
                _number = value;
            } 
        }

        public string Name { get; private set; } = "";

        public ClassA()
        {
            Name = "Object A";
        }

        public ClassA(string name)
        {
            Name = name;
        }

        // 示範解構
        /*
        ~ClassA() 
        { 
            
        }
        */

        public void SayHello() 
        {
            Console.WriteLine($"{Name}: Hello, my number = {Number}");
        }

        public void SayHello(string greeting)
        {
            Console.WriteLine($"{Name}: {greeting}, my number = {Number}");
        }
    }
}
