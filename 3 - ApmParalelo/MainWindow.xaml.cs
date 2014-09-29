using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using _2_APM;

namespace _3___ApmParalelo
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

        private static PrimosApm[] _primosApm;
        private static Stopwatch _sw;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _sw = new Stopwatch();
            _sw.Start();
            const int numThreads = 10;
            _primosApm = new PrimosApm[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                _primosApm[i] = new PrimosApm();
                _primosApm[i].BeginObtemPrimos(i == 0 ? 2 : i * 1000000 + 1, (i + 1) * 1000000, ObtemPrimosCallback, i);
            }

        }

        private static int _numItem;
        private static int _qtdPrimos;
        private void ObtemPrimosCallback(IAsyncResult ar)
        {
            Interlocked.Increment(ref _numItem);
            var threadNo = (int)ar.AsyncState;
            Interlocked.Add(ref _qtdPrimos, _primosApm[threadNo].EndObtemPrimos(ar).Count);

            if (_numItem == 10)
                Dispatcher.Invoke(() => 
                    Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}", _qtdPrimos, _sw.ElapsedMilliseconds));
        }
    }
}
