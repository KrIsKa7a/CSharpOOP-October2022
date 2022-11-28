namespace P02.CompositePattern.Models
{
    using System;

    public class SingleGift : GiftBase
    {
        public SingleGift(string name, int price) 
            : base(name, price)
        {

        }

        public override int GetTotalPrice()
        {
            Console.WriteLine($"{this.name} with the price {this.price}");

            return this.price;
        }
    }
}
