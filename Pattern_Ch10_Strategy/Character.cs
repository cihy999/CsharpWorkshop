using System;
using System.Collections.Generic;
using System.Text;

namespace Pattern_Ch10_Strategy
{
    enum CharacterType 
    { 
        None,
        Soldier,
        Enemy
    }

    internal class Character
    {
        private CharacterType _type;
        private string _name;
        private AttributeStrategy? _attribute;

        public string Name => _name;

        public Character(CharacterType type, string name)
        {
            _type = type;
            _name = name;
            switch (_type)
            {
                case CharacterType.Soldier:
                    _attribute = new SoldierAttribute();
                    break;
                case CharacterType.Enemy:
                    _attribute = new EnemyAttribute();
                    break;
            }
        }

        public void Initialize() 
        {
            if (_attribute != null)
            {
                _attribute.Initialize();

                Console.WriteLine($"{_name}: Hp/MaxHp={_attribute.NowHp}/{_attribute.MaxHp}, Speed={_attribute.MoveSpeed}");
            }
        }

        public void Attack(Character target)
        {
            int attackValue = _attribute != null ? _attribute.GetAttackValue() : 0;
            Console.WriteLine($"{_name} attack {target.Name}, damage={attackValue}");
            target.AddDamage(attackValue);
        }

        public void AddDamage(int damage)
        {
            _attribute?.AddDamageValue(damage);
            Console.WriteLine($"{_name} got damage={damage}, hp={_attribute?.NowHp}/{_attribute?.MaxHp}");
        }
    }
}
