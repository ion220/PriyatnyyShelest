
using PriyatnyyShelest.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PriyatnyyShelest
{
    /// <summary>
    /// Логика взаимодействия для AgentsListPage.xaml
    /// </summary>
    public partial class AgentsListPage : Page
    {
        private int _currentPage = 1;
        private int _maxPage;
        public AgentsListPage()
        {
            InitializeComponent();

            var context = PriyatnyyShelestDBEntities.GetContext();
            var agents = context.VW_AgentDetails.ToList();

            _maxPage = agents.Count / 10;
            if(agents.Count % 10 > 0)
                _maxPage++;

            UpdateContext(1);
        }

        private void UpdateContext(int page)
        {
            //костыыль
            if (FirstBtn is null)
                return;
            
            var context = PriyatnyyShelestDBEntities.GetContext();
            
            //фильрация и поиск
            var SoretdAgents = context.VW_AgentDetails.Where
                (x => x.Наименование_агента.Contains(SearchQueryText.Text)).ToList();
            if (FilterComboBox.SelectionBoxItem.ToString() != "Все типы")
                SoretdAgents = SoretdAgents.Where
                    (x => x.Тип_агента.Contains(FilterComboBox.SelectionBoxItem.ToString())).ToList();
            
            _maxPage = SoretdAgents.Count / 10;
            if (SoretdAgents.Count % 10 > 0)
                _maxPage++;
            
            if (AgentsList.SelectedItems.Count != 0)
            {
                ChangePriorityBtn.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriorityBtn.Visibility = Visibility.Hidden;
            }

            AgentsList.ItemsSource = SoretdAgents.Skip((10 * page) - 10).Take(10);

            //изменение кнопок пагинации
            if (page == 1)
            {
                FirstBtn.Content = page.ToString();
                SecondBtn.Content = (page + 1).ToString();
                ThirdBtn.Content = (page + 2).ToString();
                FourBtn.Content = (page + 3).ToString();

                FirstBtn.FontWeight = FontWeights.Bold;
                SecondBtn.FontWeight = FontWeights.Normal;
                ThirdBtn.FontWeight = FontWeights.Normal;
                FourBtn.FontWeight = FontWeights.Normal;
            }
            else
            {
                switch (_maxPage - page)
                {
                    case 0:
                        FirstBtn.Content = (page - 3).ToString();
                        SecondBtn.Content = (page - 2).ToString();
                        ThirdBtn.Content = (page - 1).ToString();
                        FourBtn.Content = page.ToString();

                        FirstBtn.FontWeight = FontWeights.Normal;
                        SecondBtn.FontWeight = FontWeights.Normal;
                        ThirdBtn.FontWeight = FontWeights.Normal;
                        FourBtn.FontWeight = FontWeights.Bold;
                        break;
                    case 1:
                        FirstBtn.Content = (page - 2).ToString();
                        SecondBtn.Content = (page - 1).ToString();
                        ThirdBtn.Content = page.ToString();
                        FourBtn.Content = (page + 1).ToString();

                        FirstBtn.FontWeight = FontWeights.Normal;
                        SecondBtn.FontWeight = FontWeights.Normal;
                        ThirdBtn.FontWeight = FontWeights.Bold;
                        FourBtn.FontWeight = FontWeights.Normal;
                        break;
                    default:
                        FirstBtn.Content = (page - 1).ToString();
                        SecondBtn.Content = page.ToString();
                        ThirdBtn.Content = (page + 1).ToString();
                        FourBtn.Content = (page + 2).ToString();

                        FirstBtn.FontWeight = FontWeights.Normal;
                        SecondBtn.FontWeight = FontWeights.Bold;
                        ThirdBtn.FontWeight = FontWeights.Normal;
                        FourBtn.FontWeight = FontWeights.Normal;
                        break;
                }
            }
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
                _currentPage--;
            UpdateContext(_currentPage);
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _maxPage)
                _currentPage++;
            UpdateContext(_currentPage);
        }

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = Int32.Parse((sender as Button).Content.ToString());
            UpdateContext(_currentPage);
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }

        private void SearchQueryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }

        private void ChangePriorityBtn_Click(object sender, RoutedEventArgs e)
        {
            //берет только первый выделенный элемент для редактирования
            if (AgentsList.SelectedItems.Count == 1)
            {
                var selectedItems = AgentsList.SelectedItems;
                var win = new AgentEditPriority(selectedItems[0] as VW_AgentDetails);
                win.ShowDialog();
                UpdateContext(_currentPage);
            }
        }

        private void AgentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }
    }
}
