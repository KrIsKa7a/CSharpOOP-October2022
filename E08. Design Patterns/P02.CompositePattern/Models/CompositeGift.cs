namespace P02.CompositePattern.Models
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class CompositeGift : GiftBase, IGiftOperations
    {
        private ICollection<GiftBase> nestedGifts;

        public CompositeGift(string name, int price) 
            : base(name, price)
        {
            this.nestedGifts = new HashSet<GiftBase>();
        }

        public void Add(GiftBase giftBase)
        {
            this.nestedGifts.Add(giftBase);
        }

        public override int GetTotalPrice()
        {
            int total = 0;

            Console.WriteLine($"{this.name} contains the following products with prices:");
            foreach (GiftBase nestedGift in this.nestedGifts)
            {
                total += nestedGift.GetTotalPrice();
            }

            return total;
        }

        public void Remove(GiftBase giftBase)
        {
            this.nestedGifts.Remove(giftBase);
        }
    }
}
