using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using _1___Síncrono;

namespace _4___BackgroundWorker
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
            var bw = new BackgroundWorker();
            bw.RunWorkerCompleted += (s, ev) =>
                Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", 
                ((List<int>)ev.Result).Count, sw.ElapsedMilliseconds);
            bw.DoWork += (s, ev) => ev.Result = ObtemNumerosPrimos(2, 10000000);
            bw.RunWorkerAsync();
        }

        private List<int> ObtemNumerosPrimos(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList();
        }
    }
}
