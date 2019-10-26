namespace PedroLamas.Quadratic.Model
{
    public class TextItem : FormattableItem
    {
        #region Properties

        public string Text { get; private set; }

        #endregion

        private TextItem(string text)
        {
            Text = text;
        }

        public static TextItem With(string text)
        {
            return new TextItem(text);
        }

        public override string ToString(bool displayRaw)
        {
            return Text;
        }
    }
}