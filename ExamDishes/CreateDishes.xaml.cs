using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для CreateDishes.xaml
    /// </summary>
    public partial class CreateDishes : Window
    {

        private readonly Database.ListDishesEntities connection;
        Ingredient currentIngridient;

        public CreateDishes()
        {
            InitializeComponent();

            connection = new Database.ListDishesEntities();
            //List<Ingredient> ingredients = connection.Ingredients.Local.ToBindingList
            connection.Ingredients.Load();
            listOfIngredients.ItemsSource = connection.Ingredients.Local.ToBindingList();
        }

        private void Button_SaveIng_Click(object sender, RoutedEventArgs e)
        {
            if (currentIngridient == null)
            {
                MessageBox.Show("Для создания нажминете кнопку 'Создание', для редактирования выберете ингредиент!");
                return;
            }

            string ingredients = TextBoxIngredient.Text.Trim();
            float proteins = float.Parse(TextBoxProteins.Text.Trim());
            float fats = float.Parse(TextBoxFats.Text.Trim());
            float carbonydrates = float.Parse(TextBoxCarbonydrates.Text.Trim());

            currentIngridient.nameIngr = ingredients;
            currentIngridient.proteinsIngr = proteins;
            currentIngridient.fatsIngr = fats;
            currentIngridient.carbohydratesIngr = carbonydrates;

            connection.SaveChanges();
            listOfIngredients.Items.Refresh();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.nameIngr = "новый";
            connection.Ingredients.Add(ingredient);
            setCurrentRow(ingredient);
        }

        private void setCurrentRow(Ingredient ingr) { 
            currentIngridient = ingr;

            if (ingr == null)
            {
                TextBoxIngredient.Text = "";
                TextBoxProteins.Text ="";
                TextBoxFats.Text = "";
                TextBoxCarbonydrates.Text = "";
            }
            else
            {
                TextBoxIngredient.Text = ingr.nameIngr;
                TextBoxProteins.Text = Convert.ToDouble(ingr.proteinsIngr).ToString();
                TextBoxFats.Text = Convert.ToDouble(ingr.fatsIngr).ToString();
                TextBoxCarbonydrates.Text = Convert.ToDouble(ingr.carbohydratesIngr).ToString();
            }
        }

        private void listOfIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setCurrentRow((Ingredient)listOfIngredients.SelectedItem);
        }

        private void Button_DeleteIngr_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingr = (Ingredient)listOfIngredients.SelectedItem;
 
            if (ingr == null)
            {
                MessageBox.Show("Выберите ингредиент!");
                return;
            }

            connection.Ingredients.Remove(ingr);
            connection.SaveChanges();

        }
    }
}
