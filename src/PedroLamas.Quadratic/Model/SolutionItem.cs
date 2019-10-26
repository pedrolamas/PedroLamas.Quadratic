using System.Linq;

namespace PedroLamas.Quadratic.Model
{
    public class SolutionItem
    {
        #region Properties

        public string Header { get; private set; }

        public FormattableItem[] Items { get; private set; }

        public bool DisplayRaw { get; private set; }

        public string Content
        {
            get
            {
                return ToString(DisplayRaw);
            }
        }

        #endregion

        private SolutionItem(string header, bool displayRaw, FormattableItem[] items)
        {
            Header = header;
            DisplayRaw = displayRaw;
            Items = items;
        }

        public static SolutionItem With(string header, FormattableItem[] item)
        {
            return With(header, false, item);
        }
        public static SolutionItem With(string header, bool displayRaw, FormattableItem[] items)
        {
            return new SolutionItem(header, displayRaw, items);
        }

        public static SolutionItem WithString(string header, string content)
        {
            return WithString(header, false, content);
        }
        public static SolutionItem WithString(string header, bool displayRaw, string content)
        {
            return new SolutionItem(header, displayRaw, new FormattableItem[]
            {
                TextItem.With(content)
            });
        }

        public static SolutionItem WithEquation(string header, EquationItem[] leftSideItems)
        {
            return WithEquation(header, false, leftSideItems);
        }
        public static SolutionItem WithEquation(string header, bool displayRaw, params EquationItem[] leftSideItems)
        {
            return WithEquation(header, displayRaw, leftSideItems, new EquationItem[] { });
        }
        public static SolutionItem WithEquation(string header, EquationItem[] leftSideItems, EquationItem[] rightSideItems)
        {
            return WithEquation(header, false, leftSideItems, rightSideItems);
        }
        public static SolutionItem WithEquation(string header, bool displayRaw, EquationItem[] leftSideItems, EquationItem[] rightSideItems)
        {
            return new SolutionItem(header, displayRaw, new FormattableItem[]
            {
                Equation.With(leftSideItems, rightSideItems)
            });
        }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool displayRaw)
        {
            return string.Join(string.Empty, Items.Select(x => x.ToString(displayRaw)));
        }
    }
}