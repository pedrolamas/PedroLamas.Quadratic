using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PedroLamas.Quadratic.Model
{
    public class Equation : FormattableItem
    {
        #region Properties

        public EquationItem[] LeftSide { get; set; }

        public EquationItem[] RightSide { get; set; }

        #endregion

        private Equation(EquationItem[] leftSide, EquationItem[] rightSide)
        {
            LeftSide = leftSide;
            RightSide = rightSide;
        }

        public static Equation With(EquationItem[] leftSide)
        {
            return With(leftSide, new EquationItem[] { });
        }
        public static Equation With(EquationItem[] leftSide, EquationItem[] rightSide)
        {
            return new Equation(leftSide, rightSide);
        }

        public override string ToString(bool displayRaw)
        {
            return EquationItemsToString(LeftSide, displayRaw) + " = " + EquationItemsToString(RightSide, displayRaw);
        }

        private string EquationItemsToString(IEnumerable<EquationItem> items, bool displayRaw)
        {
            var itemsStrings = items
                .Select(x => x.ToString(displayRaw))
                .Where(x => x != null)
                .ToArray();

            if (itemsStrings.Length > 0)
            {
                if (displayRaw)
                {
                    return string.Join(" + ", itemsStrings);
                }

                var t = new StringBuilder();

                foreach (var itemString in itemsStrings)
                {
                    var itemString2 = itemString;

                    if (t.Length > 0)
                    {
                        if (itemString.StartsWith("-"))
                        {
                            t.Append(" - ");

                            itemString2 = itemString.Substring(1);
                        }
                        else if (itemString.StartsWith("±"))
                        {
                            t.Append(" ± ");

                            itemString2 = itemString.Substring(1);
                        }
                        else
                        {
                            t.Append(" + ");
                        }
                    }

                    t.Append(itemString2);
                }

                return t.ToString();
            }

            return "0";
        }
    }
}