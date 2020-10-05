namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.GetById(weapon.Id) != null;
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                IWeapon current = this.weapons[i];
                if (current.Category == category)
                {
                    current.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            IWeapon getWeapon = this.GetById(weapon.Id);
            this.CheckWeaponExists(getWeapon);

            if (getWeapon.Ammunition >= ammunition)
            {
                getWeapon.Ammunition -= ammunition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                IWeapon current = this.weapons[i];
                if (current.Id == id)
                {
                    return current;
                }
            }
            return null;
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            IWeapon getWeapon = this.GetById(weapon.Id);
            this.CheckWeaponExists(getWeapon);

            getWeapon.Ammunition += ammunition;

            if (getWeapon.Ammunition > getWeapon.MaxCapacity)
            {
                getWeapon.Ammunition = getWeapon.MaxCapacity;
            }

            return getWeapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            IWeapon getWeapon = this.GetById(id);
            this.CheckWeaponExists(getWeapon);

            IWeapon result = getWeapon;

            this.weapons.Remove(getWeapon);

            return result;
        }

        public int RemoveHeavy()
        {
            return this.weapons.RemoveAll(w => w.Category == Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> result = new List<IWeapon>();
            for (int i = 0; i < this.Capacity; i++)
            {
                IWeapon current = this.weapons[i];
                if (current.Category >= lower && current.Category <= upper)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int indexOfFirst = this.weapons.IndexOf(firstWeapon);
            int indexOfSecond = this.weapons.IndexOf(secondWeapon);

            if (indexOfFirst == -1 || indexOfSecond == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (this.weapons[indexOfFirst].Category == this.weapons[indexOfSecond].Category)
            {
                IWeapon temp = this.weapons[indexOfFirst];
                this.weapons[indexOfFirst] = this.weapons[indexOfSecond];
                this.weapons[indexOfSecond] = temp;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.weapons.GetEnumerator();
        }

        private void CheckWeaponExists(IWeapon weapon)
        {
            if (weapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
