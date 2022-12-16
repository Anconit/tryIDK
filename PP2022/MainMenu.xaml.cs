using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace PP2022
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        int IDzapis = 0;

        //Доступ к приложению
        public MainMenu(int type, int ID)
        {
            InitializeComponent();

            IDzapis = ID;

            switch (type)
            {
                case 1: //Менеджер
                    DeistviyaTab.IsEnabled = false;
                    RabotnikiTab.IsEnabled = false;
                    prihodTab.IsEnabled = false;
                    RashodTab.IsEnabled = false;
                    DeistviyaTab.Visibility = Visibility.Collapsed;
                    RabotnikiTab.Visibility = Visibility.Collapsed;
                    prihodTab.Visibility = Visibility.Collapsed;
                    RashodTab.Visibility = Visibility.Collapsed;
                    break;
                case 3: //Директор

                    break;
                case 4: //Заместитель директора
                    DeistviyaTab.IsEnabled = false;
                    DeistviyaTab.Visibility = Visibility.Collapsed;

                    ZarplataColumn.Width = 0; //Скрытие зарплаты
                    break;
                case 5: //Старший кладовщик
                    DeistviyaTab.IsEnabled = false;
                    RabotnikiTab.IsEnabled = false;
                    DeistviyaTab.Visibility = Visibility.Collapsed;
                    RabotnikiTab.Visibility = Visibility.Collapsed;
                    addZacaz.Visibility = Visibility.Collapsed;
                    editZacaz.Visibility = Visibility.Collapsed;
                    break;
                case 6: //Кладовщик
                    DeistviyaTab.IsEnabled = false;
                    RabotnikiTab.IsEnabled = false;
                    zacazTab.IsEnabled = false;
                    DeistviyaTab.Visibility = Visibility.Collapsed;
                    RabotnikiTab.Visibility = Visibility.Collapsed;
                    zacazTab.Visibility = Visibility.Collapsed;
                    break;
            }

            ZapisDeistie.ZapisDeistiya(1, ID);
            Update();
            deistvieDateEmpl.SelectedDate = DateTime.Now;
            articulBox.ItemsSource = PP2022Entities.GetContext().Tovars.ToList();
            articulBox2.ItemsSource = PP2022Entities.GetContext().Tovars.ToList();
            brakView.ItemsSource = PP2022Entities.GetContext().Braks.ToList();
        }

        // СКЛАД / ТОВАР
        // Переменные для передачи значений

        // Поиск в базе информации товаров со склада (можно сделать после создания базы)
        private void NumberSklad_TextChanged(object sender, TextChangedEventArgs e)
        {
            tovarItemsSelect();
        }

        // Поиск товара в базе (только при наличии склада)
        private void FiltrTovar_TextChanged(object sender, TextChangedEventArgs e)
        {
            tovarItemsSelect();
        }

        private void Brak_Unchecked(object sender, RoutedEventArgs e)
        {
            tovarItemsSelect();
        }

        private void Brak_Checked(object sender, RoutedEventArgs e)
        {
            tovarItemsSelect();
        }

        private void Ostatki_Checked(object sender, RoutedEventArgs e)
        {
            tovarItemsSelect();
        }

        private void Ostatki_Unchecked(object sender, RoutedEventArgs e)
        {
            tovarItemsSelect();
        }

        void tovarItemsSelect()
        {
            bool brak = (bool)Brak.IsChecked;
            bool ostatki = (bool)Ostatki.IsChecked;

            if (NumberSklad.Text == "")
            {
                Update();
            }
            else
            {
                int skladNumber = int.Parse(NumberSklad.Text);
                if (FiltrTovar.Text != "")
                {
                    if (brak == true && ostatki == false)
                        TovarView.ItemsSource = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDSklad == skladNumber && a.NalichieBraka == true && a.Tovar.Name.ToLower().Contains(FiltrTovar.Text.ToLower())).ToList();
                    else if (ostatki == true && brak == false)
                        TovarView.ItemsSource = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDSklad == skladNumber && a.NalichieBraka == false && a.Tovar.Name.ToLower().Contains(FiltrTovar.Text.ToLower())).ToList();
                    else
                        TovarView.ItemsSource = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDSklad == skladNumber && a.Tovar.Name.ToLower().Contains(FiltrTovar.Text.ToLower())).ToList();
                }
                else
                {
                    if (brak == true && ostatki == false)
                        TovarView.ItemsSource = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDSklad == skladNumber && a.NalichieBraka == true).ToList();
                    else if (ostatki == true && brak == false)
                        TovarView.ItemsSource = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDSklad == skladNumber && a.NalichieBraka == false).ToList();
                    else
                        TovarView.ItemsSource = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDSklad == skladNumber).ToList();
                }
            }
        }

        // ЗАКАЗЫ
        // Поиск в базе информации о заказе (можно сделать после создания базы)
        private void NumberZacaz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberZacaz.Text != "")
            {
                int number = int.Parse(NumberZacaz.Text);
                ZacazView.ItemsSource = PP2022Entities.GetContext().ZacazTovars.Where(a => a.IDZacaz == number).ToList();
                InfoZacaz.ItemsSource = PP2022Entities.GetContext().ZacazTovars.Where(a => a.IDZacaz == number).ToList();
            }
            else Update();
        }

        // РАБОТНИКИ
        // Поиск работника в базе
        private void FiltrEmpl_TextChanged(object sender, TextChangedEventArgs e)
        {
            RabotnikView.ItemsSource = PP2022Entities.GetContext().Rabotnikis.Where(a => a.Name.ToLower().Contains(FiltrEmpl.Text.ToLower())).ToList();
            if (FiltrEmpl.Text == "")
            {
                Update();
            }
        }

        // ДЕЙСТВИЯ
        // Выборка действий
        private void deistvieNameEmpl_TextChanged(object sender, TextChangedEventArgs e)
        {
            deistvieItemSelect();
        }

        private void deistvieFamEmpl_TextChanged(object sender, TextChangedEventArgs e)
        {
            deistvieItemSelect();
        }

        private void deistvieOtchEmpl_TextChanged(object sender, TextChangedEventArgs e)
        {
            deistvieItemSelect();
        }

        
        private void deistvieDateEmpl_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            deistvieItemSelect();
        }

        void deistvieItemSelect()
        {
            string nameEmpl = deistvieNameEmpl.Text;
            string famEmpl = deistvieFamEmpl.Text;
            string otchEmpl = deistvieOtchEmpl.Text;
            DateTime dateEmpl = deistvieDateEmpl.SelectedDate.Value.Date;
            bool proverkaDateEmpl = Proverki.IsDateTimeNullorEmpty(dateEmpl);

            if (nameEmpl != "")
            {
                if (famEmpl != "")
                {
                    if (otchEmpl != "")
                    {
                        if (proverkaDateEmpl == false)
                        {
                            destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Name.ToLower().Contains(deistvieNameEmpl.Text.ToLower())
                            && a.Rabotniki.Familia.ToLower().Contains(deistvieFamEmpl.Text.ToLower()) && a.Rabotniki.Otchestvo.ToLower().Contains(deistvieOtchEmpl.Text.ToLower())
                            && a.Data.Month == dateEmpl.Month && a.Data.Day == dateEmpl.Day && a.Data.Year == dateEmpl.Year).ToList();
                        }
                        else
                            destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Name.ToLower().Contains(deistvieNameEmpl.Text.ToLower())
                            && a.Rabotniki.Familia.ToLower().Contains(deistvieFamEmpl.Text.ToLower()) && a.Rabotniki.Otchestvo.ToLower().Contains(deistvieOtchEmpl.Text.ToLower())).ToList();
                    }
                    else
                        destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Name.ToLower().Contains(deistvieNameEmpl.Text.ToLower())
                        && a.Rabotniki.Familia.ToLower().Contains(deistvieFamEmpl.Text.ToLower())).ToList();
                }
                else
                    destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Name.ToLower().Contains(deistvieNameEmpl.Text.ToLower())).ToList();
            }
            else if (famEmpl != "" && nameEmpl == "")
            {
                if (otchEmpl != "")
                {
                    if (proverkaDateEmpl == false)
                    {
                        destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Familia.ToLower().Contains(deistvieFamEmpl.Text.ToLower())
                        && a.Rabotniki.Otchestvo.ToLower().Contains(deistvieOtchEmpl.Text.ToLower()) && a.Data.Month == dateEmpl.Month && a.Data.Day == dateEmpl.Day && a.Data.Year == dateEmpl.Year).ToList();
                    }
                    else
                        destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Familia.ToLower().Contains(deistvieFamEmpl.Text.ToLower())
                        && a.Rabotniki.Otchestvo.ToLower().Contains(deistvieOtchEmpl.Text.ToLower())).ToList();
                }
                else
                    destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Familia.ToLower().Contains(deistvieFamEmpl.Text.ToLower())).ToList();
            }
            else if (otchEmpl != "" && nameEmpl == "" && famEmpl == "")
            {
                if (proverkaDateEmpl == false)
                {
                    destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Otchestvo.ToLower().Contains(deistvieOtchEmpl.Text.ToLower())
                    && a.Data.Month == dateEmpl.Month && a.Data.Day == dateEmpl.Day && a.Data.Year == dateEmpl.Year).ToList();
                }
                else
                    destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Rabotniki.Otchestvo.ToLower().Contains(deistvieOtchEmpl.Text.ToLower())).ToList();
            }
            else if (proverkaDateEmpl == false && otchEmpl == "" && nameEmpl == "" && famEmpl == "")
            {
                destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.Where(a => a.Data.Month == dateEmpl.Month && a.Data.Day == dateEmpl.Day && a.Data.Year == dateEmpl.Year).ToList();
            }
            else Update();
        }

        // ПРИХОД И РАСХОД
        private void savePrihodBtn_Click(object sender, RoutedEventArgs e)
        {
            Prihod();
        }

        public void Prihod()
        {
            if (articulBox.SelectedIndex != -1 && kolvoBox.Text != "" && skladIdBox.Text != "")
            {
                // Запись значений
                int kolvoTovars = int.Parse(kolvoBox.Text);
                int idTovars = articulBox.SelectedIndex;
                idTovars = idTovars + 1;
                int skladId = int.Parse(skladIdBox.Text);
                // Поиск строки
                var provArray = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDTovar == idTovars && a.IDSklad == skladId).ToArray().Length;
                if (provArray == 0)
                {
                    SkladTovar skladTovar = new SkladTovar();
                    skladTovar.IDSklad = skladId;
                    skladTovar.IDTovar = idTovars;
                    skladTovar.Kolvo = kolvoTovars;
                    skladTovar.NalichieBraka = false;

                    PP2022Entities.GetContext().SkladTovars.Add(skladTovar);
                    PP2022Entities.GetContext().SaveChanges();
                    MessageBox.Show("Товара ещё не было привезено, появилась новая строка в базе с привозом.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    ZapisDeistie.ZapisDeistiya(3, IDzapis);
                }
                else
                {
                    var tovarsArray = PP2022Entities.GetContext().SkladTovars.ToArray();
                    var array2 = tovarsArray.Where(a => a.IDTovar == idTovars && a.IDSklad == skladId).ToArray();
                    var rowId = array2[0].ID;

                    tovarsArray[rowId - 1].Kolvo = tovarsArray[rowId - 1].Kolvo + kolvoTovars;

                    PP2022Entities.GetContext().SkladTovars.AddOrUpdate(tovarsArray[rowId - 1]);
                    PP2022Entities.GetContext().SaveChanges();
                    MessageBox.Show("Выполнено успешно.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    ZapisDeistie.ZapisDeistiya(3, IDzapis);
                }
            }
            else MessageBox.Show("У вас есть пустые поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void saveRashodBtn_Click(object sender, RoutedEventArgs e)
        {
            Rashod();
        }

        public void Rashod()
        {
            if (articulBox2.SelectedIndex != -1 && kolvoBox2.Text != "" && skladIdBox2.Text != "")
            {
                // Запись значений
                int kolvoTovars = int.Parse(kolvoBox2.Text);
                int idTovars = articulBox2.SelectedIndex;
                idTovars = idTovars + 1;
                int skladId = int.Parse(skladIdBox2.Text);
                // Поиск строки
                var provArray = PP2022Entities.GetContext().SkladTovars.Where(a => a.IDTovar == idTovars && a.IDSklad == skladId).ToArray().Length;
                if (provArray == 0)
                {
                    MessageBox.Show("Товара ещё не было привезено, вычитать не имеет смысла.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var tovarsArray = PP2022Entities.GetContext().SkladTovars.ToArray();
                    var array2 = tovarsArray.Where(a => a.IDTovar == idTovars && a.IDSklad == skladId).ToArray();
                    var rowId = array2[0].ID;
                    var schet = tovarsArray[rowId - 1].Kolvo - kolvoTovars;
                    if (schet < 0)
                    {
                        tovarsArray[rowId - 1].Kolvo = 0;
                    }
                    else
                    {
                        tovarsArray[rowId - 1].Kolvo = tovarsArray[rowId - 1].Kolvo - kolvoTovars;
                    }
                    PP2022Entities.GetContext().SkladTovars.AddOrUpdate(tovarsArray[rowId - 1]);
                    PP2022Entities.GetContext().SaveChanges();
                    MessageBox.Show("Выполнено успешно.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    ZapisDeistie.ZapisDeistiya(2, IDzapis);
                }
            }
            else MessageBox.Show("У вас есть пустые поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // ДОП ФУНКЦИИ
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            ZapisDeistie.ZapisDeistiya(4, IDzapis);
            IDzapis = 0;
        }

        void Update()
        {
            TovarView.ItemsSource = null;
            ZacazView.ItemsSource = null;
            InfoZacaz.ItemsSource = null;
            RabotnikView.ItemsSource = PP2022Entities.GetContext().Rabotnikis.ToList();
            destvieView.ItemsSource = PP2022Entities.GetContext().Deistviyas.OrderByDescending(a => a.Data).ToList();
        }

        private void NumberZacaz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }

        private void NumberSklad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }

        private void addZacaz_Click(object sender, RoutedEventArgs e)
        {
            editingZacaz editingZacaz = new editingZacaz(1, IDzapis);
            editingZacaz.Show();
        }

        private void editZacaz_Click(object sender, RoutedEventArgs e)
        {
            editingZacaz editingZacaz = new editingZacaz(2, IDzapis);
            editingZacaz.Show();
        }

        private void kolvoBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }

        private void skladIdBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Proverki.controlNumbersInText(e);
        }

    }
}
