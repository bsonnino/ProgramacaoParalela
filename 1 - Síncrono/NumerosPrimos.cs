using System;

namespace _1___Síncrono
{
    public static class NumerosPrimos
    {
        public static bool EhNumeroPrimo(int p)
        {
            if (p % 2 == 0)
                return p == 2;
            var topo = (int)Math.Sqrt(p);
            for (int i = 3; i <= topo; i += 2)
            {
                if (p % i == 0) return false;
            }
            return true;
        }
    }
}
