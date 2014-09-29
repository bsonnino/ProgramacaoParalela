using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using _1___Síncrono;

namespace _7___PLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            var primos = ObtemPrimos(2, 10000000);
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", primos.Count, sw.ElapsedMilliseconds);
        }

        private static List<int> ObtemPrimos(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return Enumerable.Range(minimo, count).AsParallel().Where(NumerosPrimos.EhNumeroPrimo).ToList();
        }
    }
}
