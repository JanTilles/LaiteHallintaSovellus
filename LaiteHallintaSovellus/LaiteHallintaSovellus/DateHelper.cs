using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaiteHallintaSovellus
{
    public class DateHelper
    {
        public static void Main()
        {
            DateTime dt = DateTime.Now;
            // Sets the CurrentCulture property to U.S. English.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            // Displays dt, formatted using the ShortDatePattern
            // and the CurrentThread.CurrentCulture.
            Console.WriteLine(dt.ToString("d"));

            // Creates a CultureInfo for German in Germany.
            CultureInfo ci = new CultureInfo("de-DE");
            // Displays dt, formatted using the ShortDatePattern
            // and the CultureInfo.
            Console.WriteLine(dt.ToString("d", ci));
        }

    }
}
