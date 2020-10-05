namespace _02.LegionSystem.Models
{
    using System;
    using _02.LegionSystem.Interfaces;

    public class Enemy : IEnemy
    {
        public Enemy(int attackSpeed, int health)
        {
            this.AttackSpeed = attackSpeed;
            this.Health = health;
        }

        public int AttackSpeed { get; set; }

        public int Health { get; set; }

        public int CompareTo(object obj)
        {
            IEnemy other = (IEnemy)obj;

            return this.AttackSpeed.CompareTo(other.AttackSpeed);
        }

        public override bool Equals(object obj)
        {
            IEnemy other = (IEnemy)obj;

            return this.AttackSpeed == other.AttackSpeed;
        }

        public override int GetHashCode()
        {
            return -26413112 + AttackSpeed.GetHashCode();
        }
    }
}
