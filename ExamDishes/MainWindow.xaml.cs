using Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Configuration;
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

namespace ExamDishes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Database.ListDishesEntities connection;
        public MainWindow()
        {
            InitializeComponent();

            //базочка любимая
            connection = new Database.ListDishesEntities();
            if (connection.Database.Exists() == false)
            {
                MessageBox.Show("Подключение к базе не выполнено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            connection.Dishes.Load();

            ListDishesFalse.ItemsSource = connection.Dishes.Local.ToBindingList();
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            Dish dish = new Dish();
            connection.Dishes.Add(dish);
            CreateIngredients createIngredients = new CreateIngredients();
            createIngredients.connection = connection;
            createIngredients.setDish(dish);
            createIngredients.ShowDialog();

        }

        private void Button_Ingredients_Click(object sender, RoutedEventArgs e)
        {
            CreateDishes createDishes = new CreateDishes();
            createDishes.ShowDialog();

        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ListDishesFalse.SelectedItem == null) return;

            CreateIngredients createIngredients = new CreateIngredients();
            createIngredients.connection = connection;
            createIngredients.setDish((Dish)ListDishesFalse.SelectedItem);
            createIngredients.ShowDialog();
        }

        private void Button_Click_Right(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извините, мы временнно не работаем, мы еще на реставрации");
        }

        private void Button_Click_Left(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извините, мы временнно не работаем, когда нибудт обязательно зараюотаем");
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Dish dish = (Dish)ListDishesFalse.SelectedItem;

            if (dish == null)
            {
                MessageBox.Show("Выберите блюдо!");
                return;
            }

            connection.Dishes.Remove(dish);
            connection.SaveChanges();
        }
    }
}
