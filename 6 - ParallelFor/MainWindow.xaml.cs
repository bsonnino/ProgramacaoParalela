using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using _1___Síncrono;

namespace _6___ParallelFor
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
            const int numThreads = 10;
            var primos = new List<int>[numThreads];
            Parallel.For(0, numThreads, i => primos[i] = ObtemPrimos(i == 0 ? 2 : i * 1000000 + 1, (i + 1) * 1000000));
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", primos.Sum(p => p.Count), sw.ElapsedMilliseconds);
        }

        private static List<int> ObtemPrimos(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList();
        }
    }
}
