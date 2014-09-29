using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using _1___Síncrono;

namespace _9___AsyncParalelo
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            const int numThreads = 10;
            var primos = new Task<List<int>>[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                primos[i] = ObtemPrimosAsync(i == 0 ? 2 : i * 1000000 + 1, (i + 1) * 1000000);
            }
            var results = await Task.WhenAll(primos);
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", results.Sum(p => p.Count), sw.ElapsedMilliseconds);
        }

        private static async Task<List<int>> ObtemPrimosAsync(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return await Task.Factory.StartNew(() => Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList());
        }
    }
}
