namespace CalcoloSpeseBenzina.Components
{
    public class Vehicle(string name, double consumption, string image = "https://wips.plug.it/cips/libero.it/magazine/cms/2022/04/gianni-morandi.jpg?w=785&h=494&a=c")
    {
        public string Name { get; } = name;
        public double Consumption { get; } = consumption; //Consumo in litri per km
        public string Image { get; } = image;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Vehicle)obj);
        }

        protected bool Equals(Vehicle other)
        {
            return Name == other.Name && Consumption.Equals(other.Consumption) && Image == other.Image;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Consumption, Image);
        }
    }
}
