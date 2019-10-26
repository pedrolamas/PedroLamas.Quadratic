namespace PedroLamas.Quadratic.Model
{
    public class MainModel : IMainModel
    {
        #region Properties

        public double ParameterA { get; set; }

        public double ParameterB { get; set; }

        public double ParameterC { get; set; }

        public bool AcceptComplexNumberSolutions { get; set; }

        #endregion

#if DEBUG
        public MainModel()
        {
            ParameterA = 2;
            ParameterB = 5;
            ParameterC = -3;
        }
#endif
    }
}