namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> enemies;

        public Legion()
        {
            this.enemies = new OrderedSet<IEnemy>();
        }

        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.GetByAttackSpeed(enemy.AttackSpeed) != null;
        }

        public void Create(IEnemy enemy)
        {
            this.enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            for (int i = 0; i < this.Size; i++)
            {
                IEnemy current = this.enemies[i];
                if (current.AttackSpeed == speed)
                {
                    return current;
                }
            }

            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> faster = new List<IEnemy>();
            IEnemy getEnemy = this.GetByAttackSpeed(speed);
            int index = this.enemies.IndexOf(getEnemy);

            for (int i = index + 1; i < this.Size; i++)
            {
                faster.Add(this.enemies[i]);
            }
            
            return faster;
        }

        public IEnemy GetFastest()
        {
            this.CheckNotEmpty();

            return this.enemies.GetLast();
        }

        public IEnemy[] GetOrderedByHealth()
        {

            return this.Size == 0 ? new IEnemy[0] : this.enemies.OrderByDescending(e => e.Health).ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> slower = new List<IEnemy>();
            IEnemy getEnemy = this.GetByAttackSpeed(speed);
            int index = this.enemies.IndexOf(getEnemy);

            for (int i = 0; i < index; i++)
            {
                slower.Add(this.enemies[i]);
            }

            return slower;
        }

        public IEnemy GetSlowest()
        {
            this.CheckNotEmpty();

            return this.enemies.GetFirst();
        }

        public void ShootFastest()
        {
            this.CheckNotEmpty();

            this.enemies.RemoveLast();
        }

        public void ShootSlowest()
        {
            this.CheckNotEmpty();

            this.enemies.RemoveFirst();
        }

        private void CheckNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
