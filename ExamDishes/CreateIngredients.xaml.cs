using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using Database;

namespace ExamDishes
{
    /// <summary>
    /// Логика взаимодействия для CreateIngredients.xaml
    /// </summary>
    public partial class CreateIngredients : Window
    {

        public Database.ListDishesEntities connection;
        private Dish currentDish;

        public CreateIngredients()
        {
            InitializeComponent();
            connection = new Database.ListDishesEntities();
            connection.Ingredients.Load();
            connection.Dishes.Load();
            listOfIngredients.ItemsSource = connection.Ingredients.Local.ToBindingList();
        }

        public void setDish(Dish dish)
        {
            currentDish = dish;
            NameDishTextBox.Text = currentDish.nameDishes;
        }
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            currentDish.nameDishes = NameDishTextBox.Text;  
            connection.SaveChanges();
        }


        private void Button_Click_Right(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("На нашу реализацию не хватило времени :(");
        }

        private void Button_Click_Left(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Временные трудности");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Мы бы очень хотели что-то рассчитывать, но у нас нет возможности");
        }
    }
}
