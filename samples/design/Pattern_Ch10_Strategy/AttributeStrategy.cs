using System;
using System.Collections.Generic;
using System.Text;

namespace Pattern_Ch10_Strategy
{
    internal abstract class AttributeStrategy
    {
        protected int _nowHp = 0;
        protected int _maxHp = 0;
        protected int _moveSpeed = 0;

        public int NowHp => _nowHp;
        public int MaxHp => _maxHp;
        public int MoveSpeed => _moveSpeed;

        public abstract void Initialize();
        public abstract int GetAttackValue();
        public abstract void AddDamageValue(int damage);
    }

    internal class SoldierAttribute : AttributeStrategy
    {
        protected int _level = 0;

        public override void Initialize()
        {
            _level = 1;
            _maxHp = 100 + _level * 100;
            _nowHp = _maxHp;
            _moveSpeed = 3;
        }

        public override int GetAttackValue()
        {
            return 10;
        }

        public override void AddDamageValue(int damage)
        {
            _nowHp -= damage;
        }
    }

    internal class EnemyAttribute : AttributeStrategy
    {
        protected int _criticalDamage = 0;
        private Random randomCritical = new Random();

        public override void Initialize()
        {
            _maxHp = 100;
            _nowHp = _maxHp;
            _moveSpeed = 1;
            _criticalDamage = 30;
        }

        public override int GetAttackValue()
        {
            return randomCritical.Next(100) > 90 ? 10 + _criticalDamage : 10;
        }

        public override void AddDamageValue(int damage)
        {
            _nowHp -= damage;
        }
    }
}
