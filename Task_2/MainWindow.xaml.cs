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

namespace Task_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListTasks.ItemsSource = AddData();
        }
        private void BtnMaximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.WindowState.Equals(WindowState.Normal))
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }

        }

        private void BtnMinimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnContent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button contBox = (Button)sender;
            TestingDate testingDate = AddData().Find((item) => item.Title.Contains(contBox.Content.ToString()));
            string str = "Title: " + testingDate.Title + "\n" +
                "FirstName: " + testingDate.FirstName + "\n" +
                "SecondName: " + testingDate.SecondName + "\n" +
                "Mail: " + testingDate.Mail + "\n" +
                "Age: " + testingDate.Age;     
            Info.Text = str;
            ControlPanel.Children.Clear();
            foreach (Control control in testingDate.Controls)
            {
                control.Margin = new Thickness(10, 0, 10, 0);
                ControlPanel.Children.Add(control);
            }
        }
        private void LayoutRootMouseLeft(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        List<TestingDate> AddData()
        {
            Button saveButton = new Button(); saveButton.Content = "Save";
            Button cutButton = new Button(); cutButton.Content = "Cut";
            CheckBox checkbox = new CheckBox(); checkbox.Content = "checkbox";
            RadioButton radioButton = new RadioButton(); radioButton.Content = "RadioBtn";
            Slider slider = new Slider(); slider.Value = 1; slider.IsSnapToTickEnabled = true;
            slider.Minimum = 0; slider.Maximum = 10; slider.Width = 200;
            List<Control> panel = new List<Control>();
            panel.Add(saveButton);

            List<TestingDate> items = new List<TestingDate>();
            items.Add(new TestingDate()
            {
                Title = "Task#1",
                FirstName = "One",
                SecondName = "S One",
                Mail = "Sone@i.ua",
                Age = 23,
                Controls = new List<Control> {saveButton, cutButton }
            });
            items.Add(new TestingDate()
            {
                Title = "Task#2",
                FirstName = "Two",
                SecondName = "Second Two",
                Mail = "two@i.ua",
                Age = 33,
                Controls = new List<Control>() { checkbox, saveButton, cutButton } 
            });
            items.Add(new TestingDate()
            {
                Title = "Task#3",
                FirstName = "Three",
                SecondName = "Second three",
                Mail = "three@org.ua",
                Age = 13,
                Controls = new List<Control>() { radioButton } 
            });
            items.Add(new TestingDate()
            {
                Title = "Task#4",
                FirstName = "",
                SecondName = "",
                Mail = "",
                Age = 23,
                Controls = new List<Control>() { slider } 
            });
            items.Add(new TestingDate()
            {
                Title = "Task#5",
                FirstName = "",
                SecondName = "",
                Mail = "",
                Age = 23,
                Controls = new List<Control>() { slider, radioButton } 
            });

            return items;
        }
    }
}
