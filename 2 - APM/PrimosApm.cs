using System;
using System.Collections.Generic;
using System.Linq;
using _1___Síncrono;

namespace _2_APM
{
    public class PrimosApm
    {
        private List<int> ObtemPrimos(int minimo, int maximo)
        {
            var count = maximo - minimo + 1;
            return Enumerable.Range(minimo, count).Where(NumerosPrimos.EhNumeroPrimo).ToList();
        }

        private delegate List<int> ObtemPrimosDelegate(int min, int count);
        private ObtemPrimosDelegate _obtemPrimosDelegate;

        public IAsyncResult BeginObtemPrimos(int minimo, int maximo, AsyncCallback callback, object userState)
        {
            _obtemPrimosDelegate = ObtemPrimos;
            return _obtemPrimosDelegate.BeginInvoke(minimo, maximo, callback, userState);
        }
        public List<int> EndObtemPrimos(IAsyncResult result)
        {
            return _obtemPrimosDelegate.EndInvoke(result);
        }

    }
}
