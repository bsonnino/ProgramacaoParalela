using System;
using System.Diagnostics;
using System.Windows;
using _2_APM;

namespace _2___APM
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
            _sw = new Stopwatch();
            _sw.Start();
            _primosApm = new PrimosApm();
            _primosApm.BeginObtemPrimos(2, 10000000, ObtemPrimosCallback, null);
        }

        private static PrimosApm _primosApm;
        private static Stopwatch _sw;

        private void ObtemPrimosCallback(IAsyncResult ar)
        {
            Dispatcher.Invoke (() =>
            Resultado.Text = string.Format("Números Primos Encontrados: {0}\nTempo Total: {1}",
                _primosApm.EndObtemPrimos(ar).Count, _sw.ElapsedMilliseconds));
        }
    }
}
