using System;
using System.Linq;
using Cimbalino.Phone.Toolkit.Extensions;

namespace PedroLamas.Quadratic.Model
{
    public class EquationItem : FormattableItem
    {
        #region Properties

        public static int Decimals { get; set; }

        public string Format { get; private set; }

        public double[] Values { get; private set; }

        public bool Complex { get; private set; }

        #endregion

        private EquationItem(string format, bool complex, params double[] values)
        {
            Format = format;
            Complex = complex;
            Values = values;
        }

        public static EquationItem With(string format, params double[] values)
        {
            return new EquationItem(format, false, values);
        }

        public static EquationItem With(string format, bool complex, params double[] values)
        {
            return new EquationItem(format, complex, values);
        }

        public override string ToString(bool displayRaw)
        {
            if (!displayRaw && Values.Length == 1)
            {
                if (Format.StartsWith("{0}") && Format.Length > 3)
                {
                    if (Values[0] == -1)
                    {
                        return Format.FormatWithInvariantCulture("-");
                    }
                    if (Values[0] == 1)
                    {
                        return Format.FormatWithInvariantCulture(string.Empty);
                    }
                }
                if (Values[0] == 0)
                {
                    return null;
                }
            }

            var values = Values
                .Select(x => Math.Round(x, Decimals))
                .Cast<object>()
                .ToArray();

            return Format.FormatWithInvariantCulture(values) + (Complex ? "i" : string.Empty);
        }
    }
}