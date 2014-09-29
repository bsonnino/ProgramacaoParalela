using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace _10___CancelamentoProgresso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CancellationTokenSource _cts;
        private int _processados;
        private int _maxNumero;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            CancelaBtn.Visibility = Visibility.Visible;
            ProgressBar.Visibility = Visibility.Visible;
            CalculaBtn.Visibility = Visibility.Collapsed;
            ProgressBar.Value = 0;
            Resultado.Text = "";
            _processados = 0;
            _cts = new CancellationTokenSource();
            var progress = new Progress<int>(v => ProgressBar.Value = v);
            try
            {
                var results = await ProcessaPrimosAsync(_cts.Token, progress);
                Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", results.Sum(p => p.Count), sw.ElapsedMilliseconds);
            }
            catch (OperationCanceledException)
            {
                Resultado.Text = "Operação Cancelada";
            }
            CancelaBtn.Visibility = Visibility.Collapsed;
            ProgressBar.Visibility = Visibility.Collapsed;
            CalculaBtn.Visibility = Visibility.Visible;

        }

        private async Task<List<int>[]> ProcessaPrimosAsync(CancellationToken ct, IProgress<int> progress)
        {

            const int numThreads = 10;
            _maxNumero = numThreads * 5000000;
            var primos = new Task<List<int>>[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                primos[i] = ObtemPrimosAsync(i == 0 ? 2 : i * 5000000 + 1, (i + 1) * 5000000, ct, progress);
            }

            return await Task.WhenAll(primos);
        }

        private async Task<List<int>> ObtemPrimosAsync(int minimo, int maximo, CancellationToken ct, IProgress<int> progress)
        {
            var count = maximo - minimo + 1;
            return await Task.Factory.StartNew(() => Enumerable.Range(minimo, count).Where(n => EhNumeroPrimo(n, ct, progress)).ToList(), ct);
        }

        public bool EhNumeroPrimo(int p, CancellationToken ct, IProgress<int> progress)
        {
            ct.ThrowIfCancellationRequested();
            Interlocked.Increment(ref _processados);
            if (_processados % 100000 == 0)
            {
                var percent = (double)_processados / _maxNumero * 100.0;
                progress.Report((int)percent);
            }

            if (p % 2 == 0)
                return p == 2;
            var topo = (int)Math.Sqrt(p);
            for (int i = 3; i <= topo; i += 2)
            {
                if (p % i == 0) return false;
            }
            return true;
        }

        private void Cancela_Click(object sender, RoutedEventArgs e)
        {
            _cts.Cancel();
        }
    }
}
