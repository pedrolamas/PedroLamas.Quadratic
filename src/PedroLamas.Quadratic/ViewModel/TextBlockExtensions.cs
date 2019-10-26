using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using PedroLamas.Quadratic.Model;

namespace PedroLamas.Quadratic.ViewModel
{
    public class TextBlockExtensions : DependencyObject
    {
        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(TextBlockExtensions), new PropertyMetadata(null, OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBlock = d as TextBlock;

            if (textBlock != null)
            {
                textBlock.Inlines.Clear();

                //foreach (var inline in ConvertToInlines((string)e.NewValue))
                //{
                //    textBlock.Inlines.Add(inline);
                //}

                var si = e.NewValue as SolutionItem;

                if (si != null)
                {
                    textBlock.Text = si.ToString();
                }
                else
                {
                    textBlock.Text = e.NewValue.ToString();
                }
            }
        }

        private static IEnumerable<Inline> ConvertToInlines(string text)
        {
            var items = text.Split('|');

            for (var index = 0; index < items.Length; index += 2)
            {
                var run = new Run()
                {
                    Text = items[index + 1]
                };

                switch (items[index])
                {
                    case "red":
                        run.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    case "blue":
                        run.Foreground = new SolidColorBrush(Colors.Blue);
                        break;
                }

                yield return run;
            }
        }
    }
}