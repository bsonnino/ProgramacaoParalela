using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace _1___Síncrono
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            var primes = ObtemPrimos(2, 10000000);
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", primes.Count, sw.ElapsedMilliseconds);
        }

        private static List<int> ObtemPrimos(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList();
        }
    }
}
