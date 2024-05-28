namespace CalcoloSpeseBenzina.Components
{
    public class Vehicle(string name, double consumption, string image = "https://wips.plug.it/cips/libero.it/magazine/cms/2022/04/gianni-morandi.jpg?w=785&h=494&a=c")
    {
        public string Name { get; set; } = name;
        public double Consumption { get; set; } = consumption; //Consumo in litri per km
        public string Image { get; set; } = image;



    }
}
