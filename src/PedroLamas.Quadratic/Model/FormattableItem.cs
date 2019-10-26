namespace PedroLamas.Quadratic.Model
{
    public abstract class FormattableItem
    {
        public override string ToString()
        {
            return ToString(false);
        }

        public abstract string ToString(bool displayRaw);
    }
}