using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using _1___Síncrono;

namespace _5___Tasks
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
            var primos = new Task<List<int>>[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                int index = i;
                primos[i] = Task.Factory.StartNew(() => ObtemPrimos(index == 0 ? 2 : index * 1000000 + 1, (index + 1) * 1000000));
            }
            Task.WaitAll(primos);
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", primos.Sum(p => p.Result.Count), sw.ElapsedMilliseconds);
        }

        private static List<int> ObtemPrimos(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList();
        }
    }
}
