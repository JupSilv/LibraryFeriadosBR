

namespace LibraryFeriadosBrasil
{
    public class Holiday
    {
        public enum HolidayIdentity
        {
            AnoNovo,
            Tiradentes,
            DiaDoTrabalho,
            IdependenciaDoBrasil,
            NossaSenhoraAparecida,
            Finados,
            ProclamacaoDaRepublica,
            Natal,
            Pascoa,
            SextaFeiraSanta,
            Carnaval,
            CorpusChristi
        }

        public enum Months
        {
            Jan = 1,
            Feb,
            Mar,
            Apr,
            May,
            Jun,
            Jul,
            Aug,
            Sep,
            Oct,
            Nov,
            Dec
        }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public HolidayIdentity Identity { get; set; }

        private static List<Holiday> CustomHolidayList { get; set; } = new List<Holiday>();
        public static IList<Holiday> GetAllNext()
        {
            int actualMonth = DateTime.Now.Month;
            IEnumerable<Holiday> collection = from x in GetAllByYear(DateTime.Now.Year)
                                              where x.Date.Month >= actualMonth && x.Date >= DateTime.Now.Date
                                              select x;
            IEnumerable<Holiday> collection2 = from x in GetAllByYear(DateTime.Now.Year + 1)
                                               where x.Date.Month <= actualMonth
                                               select x;
            List<Holiday> list = new List<Holiday>();
            list.AddRange(collection);
            list.AddRange(collection2);
            return list.OrderBy((Holiday x) => x.Date).ToList();
        }
        public static IList<Holiday> GetAllByYear(int? yearParameter = null)
        {
            List<Holiday> list = new List<Holiday>();
            int num = yearParameter ?? DateTime.Now.Year;
            list.Add(new Holiday
            {
                Date = new DateTime(num, 1, 1),
                Title = "Ano Novo",
                Identity = HolidayIdentity.AnoNovo
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 4, 21),
                Title = "Tiradentes",
                Identity = HolidayIdentity.Tiradentes
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 5, 1),
                Title = "Dia do trabalho",
                Identity = HolidayIdentity.DiaDoTrabalho
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 9, 7),
                Title = "Independência do Brasil",
                Identity = HolidayIdentity.IdependenciaDoBrasil
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 10, 12),
                Title = "Nossa Senhora Aparecida",
                Identity = HolidayIdentity.NossaSenhoraAparecida
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 11, 2),
                Title = "Finados",
                Identity = HolidayIdentity.Finados
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 11, 15),
                Title = "Proclamação da República",
                Identity = HolidayIdentity.ProclamacaoDaRepublica
            });
            list.Add(new Holiday
            {
                Date = new DateTime(num, 12, 25),
                Title = "Natal",
                Identity = HolidayIdentity.Natal
            });
            int num2;
            int num3;
            if (num >= 1900 && num <= 2099)
            {
                num2 = 24;
                num3 = 5;
            }
            else if (num >= 2100 && num <= 2199)
            {
                num2 = 24;
                num3 = 6;
            }
            else if (num >= 2200 && num <= 2299)
            {
                num2 = 25;
                num3 = 7;
            }
            else
            {
                num2 = 24;
                num3 = 5;
            }

            int num4 = num % 19;
            int num5 = num % 4;
            int num6 = num % 7;
            int num7 = (19 * num4 + num2) % 30;
            int num8 = (2 * num5 + 4 * num6 + 6 * num7 + num3) % 7;
            int day;
            int month;
            if (num7 + num8 > 9)
            {
                day = num7 + num8 - 9;
                month = 4;
            }
            else
            {
                day = num7 + num8 + 22;
                month = 3;
            }

            DateTime date = new DateTime(num, month, day);
            DateTime date2 = date.AddDays(-2.0);
            DateTime date3 = date.AddDays(-47.0);
            DateTime date4 = date.AddDays(60.0);
            list.Add(new Holiday
            {
                Date = date,
                Title = "Páscoa",
                Identity = HolidayIdentity.Pascoa
            });
            list.Add(new Holiday
            {
                Date = date2,
                Title = "Sexta-Feira Santa",
                Identity = HolidayIdentity.SextaFeiraSanta
            });
            list.Add(new Holiday
            {
                Date = date3,
                Title = "Carnaval",
                Identity = HolidayIdentity.Carnaval
            });
            list.Add(new Holiday
            {
                Date = date4,
                Title = "Corpus Christi",
                Identity = HolidayIdentity.CorpusChristi
            });
            list.AddRange(CustomHolidayList);
            return list.OrderBy((Holiday z) => z.Date).ToList();
        }
        
    }
    public static class Today
    {
        public static bool IsHoliday()
        {
            return DateTime.Today.IsHoliday();
        }

        public static bool IsNotHoliday()
        {
            return !IsHoliday();
        }
    }
    public static class HolidayExtention
    {
        public static bool IsHoliday(this DateTime dateToTest)
        {
            return Holiday.GetAllByYear(dateToTest.Year).Any((Holiday x) => x.Date.Date.Equals(dateToTest.Date));
        }
    }
}
