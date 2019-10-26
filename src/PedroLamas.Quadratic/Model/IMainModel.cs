namespace PedroLamas.Quadratic.Model
{
    public interface IMainModel
    {
        double ParameterA { get; set; }

        double ParameterB { get; set; }

        double ParameterC { get; set; }

        bool AcceptComplexNumberSolutions { get; set; }
    }
}