using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PP2022
{
    public static class Proverki
    {
        // Только для ввода чисел
        public static void controlNumbersInText(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Проверка даты на пустоту
        public static bool IsDateTimeNullorEmpty(DateTime? date)
        {
            return date == null ? true : false;
        }
    }

    public static class ZapisDeistie
    {
        public static bool ZapisDeistiya(int deistvieID, int IDzapis)
        {
            bool proverka = false;
            int[] arrayDeistviya = Enumerable.Range(1, 6).ToArray();
            int[] arrayRabotnik = Enumerable.Range(1, 5).ToArray();

            if (arrayDeistviya.Contains(deistvieID) && arrayRabotnik.Contains(IDzapis))
            {
                Deistviya deistviya = new Deistviya();
                deistviya.IDRabotnik = IDzapis;
                deistviya.IDDeistvie = deistvieID;
                deistviya.Data = DateTime.Now;
                PP2022Entities.GetContext().Deistviyas.Add(deistviya);
                PP2022Entities.GetContext().SaveChanges();
                proverka = true;
            }
            else proverka = false;

            return proverka;
        }
    }
}
