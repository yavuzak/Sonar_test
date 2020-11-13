using System;

namespace Prime.Services
{
    public class PrimeService
    {
        public bool IsPrime(int candidate)
        {
            int kullanilmayan_ve_uzun_bir_isme_sahip_degisken = 1;
            if (candidate < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(candidate); divisor++)
            {
                if (candidate % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
