using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PP2022
{
    /// <summary>
    /// Логика взаимодействия для editingZacaz.xaml
    /// </summary>
    public partial class editingZacaz : Window
    {
        private int IdEmpl;

        public editingZacaz(int a, int IdEmplForNeeds)
        {
            InitializeComponent();

            IdEmpl = IdEmplForNeeds;
            emplNumber.Text = IdEmplForNeeds.ToString();

            switch (a)
            {
                case 1:
                    editZacaz.IsEnabled = false;
                    editZacaz.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    addZacaz.IsEnabled = false;
                    addZacaz.Visibility = Visibility.Collapsed;
                    editZacaz.IsSelected = true;
                    break;
            }

            numberZacaza.ItemsSource = PP2022Entities.GetContext().Zacazs.ToList();
        }

        // РЕДАКТИРОВАНИЕ
        // редактирование статуса
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if(numberZacaza.SelectedIndex != -1 && statusZacaza.SelectedIndex != -1)
            {
                // Взять нужные ID строк
                int numberZacaz = numberZacaza.SelectedIndex;
                int numberStatus = statusZacaza.SelectedIndex;
                // Взять все записи в массивы
                var zacazArray = PP2022Entities.GetContext().Zacazs.ToArray();
                var sostiyaniyaArray = PP2022Entities.GetContext().Sostoyanies.ToArray();
                // Уточнить изменения
                zacazArray[numberZacaz].IDSostoyanie = sostiyaniyaArray[numberStatus].ID;
                // Сохранить их
                PP2022Entities.GetContext().Zacazs.AddOrUpdate(zacazArray[numberZacaz]);
                PP2022Entities.GetContext().SaveChanges();
                MessageBox.Show("Выполнено успешно.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                ZapisDeistie.ZapisDeistiya(5, IdEmpl);
                Close();
            }
            else MessageBox.Show("У вас есть пустые поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void numberZacaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            statusZacaza.IsEnabled = true;
            statusZacaza.ItemsSource = PP2022Entities.GetContext().Sostoyanies.ToList();
        }

        // ДОБАВЛЕНИЕ
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if(zacazchikNumber.Text != "" && emplNumber.Text != "" && costNumber.Text != "" && dateOzidPicker.SelectedDate != null)
            {
                Zacaz zacaz = new Zacaz();
                zacaz.IDZacazchik = int.Parse(zacazchikNumber.Text);
                zacaz.IDRabotnik = int.Parse(emplNumber.Text);
                zacaz.Cost = int.Parse(costNumber.Text);
                zacaz.DateZacaz = DateTime.UtcNow;
                zacaz.DateOzidaetsya = (DateTime)dateOzidPicker.SelectedDate;
                zacaz.IDSostoyanie = 1;

                PP2022Entities.GetContext().Zacazs.Add(zacaz);
                PP2022Entities.GetContext().SaveChanges();
                MessageBox.Show("Выполнено успешно.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                ZapisDeistie.ZapisDeistiya(6, IdEmpl);
                Close();
            }
            else MessageBox.Show("У вас есть пустые поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // ДОП ФУНКЦИИ
        // Только для ввода чисел
        private void zacazchikNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }

        private void emplNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }

        private void costNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }
    }
}
