namespace P02.CompositePattern.Models
{
    public abstract class GiftBase
    {
        protected string name;
        protected int price;

        protected GiftBase(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public abstract int GetTotalPrice();
    }
}
