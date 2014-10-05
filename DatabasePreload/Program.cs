using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePreload
{
    class Program
    {
        static void Main(string[] args)
        {
            PreloadCountries(@"C:\Users\Amos\Desktop\Book1.csv");

        }

        public static void PreloadCountries(string countryFilename)
        {
            try
            {
                using (LCDB2Entities dbEntities = new LCDB2Entities())
                {
                    using (var sr = new StreamReader(countryFilename))
                    {
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            if (!line.Contains("\""))
                            {
                                string[] segs = line.Split(',');
                                LCCountry country = new LCCountry()
                                {
                                    CountryRegionCode = segs[2],
                                    CountryName = segs[0]
                                };
                                dbEntities.LCCountries.Add(country);
                            }
                            line = sr.ReadLine();
                        }
                        dbEntities.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file cannot be read: " + e.Message);
            }
        }
    }
}
