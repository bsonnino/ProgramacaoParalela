using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using _1___Síncrono;

namespace _8___Async
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
            List<int> primos = await ObtemPrimosAsync(2, 10000000);
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", primos.Count, sw.ElapsedMilliseconds);
        }

        private static async Task<List<int>> ObtemPrimosAsync(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return await Task.Factory.StartNew(() => Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList());
        }
    }
}
