using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;

namespace Task_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<D3Model> d3ModelsList = new List<D3Model>();
        DoubleAnimation anim = new DoubleAnimation();
        StopStoryboard storyboard = new StopStoryboard();
        public MainWindow()
        {
            InitializeComponent();
            AddData();
        }

        private void AddData()
        {
            ModelImporter importer = new ModelImporter();
            Model3D model = importer.Load("C:/Users/Zigo/Downloads/wwnnsthl4k-LibertyStatue/LibertyStatue/LibertStatue.obj");
            d3ModelsList.Add(new D3Model() { id = 1, Name ="test", ModelD3= model});
            Models.Content = model;
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
            D3Model d3Model = d3ModelsList.Find((item) => item.Name.Contains(contBox.Content.ToString()));
            clearContent.Content = d3Model.id;
            Models.Content = d3Model.ModelD3;
        }

        private void LayoutRootMouseLeft(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Obj files (*.obj)|*.obj";
            if (openFileDialog.ShowDialog() == true)
            {
                string name = Path.GetFileName(openFileDialog.FileName);

                Model3D device = getModel(openFileDialog.FileName);
              
                d3ModelsList.Add(new D3Model() { id = d3ModelsList.Count + 1, Name = name.Substring(0, name.Length - 4), ModelD3 = device });
                ListTasks.ItemsSource = null;
                ListTasks.ItemsSource = d3ModelsList;
                Models.Content = device;
            }


        }
        public Model3D getModel(string path)
        {
            Model3D device = null;
            try
            {
                ModelImporter import = new ModelImporter();
                device = import.Load(path);
            }
            catch (Exception e)
            { }
            return device;
        }
        private void BtnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {

                Models.Children.Clear();
                D3Model d3Model = d3ModelsList.Find((item) => item.id.Equals(clearContent.Content));
                if (d3Model != null)
                {
                    d3ModelsList.Remove(d3Model);                  
                    ListTasks.ItemsSource = null;
                    ListTasks.ItemsSource = d3ModelsList;
                }
            
        }
 
        private void BtnStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            double zmin = minZ.Value;
            double zmax = maxZ.Value;
            anim.From = zmin;
            anim.To = zmax;
            anim.Duration = TimeSpan.FromSeconds(3);
            anim.AutoReverse = true;
            anim.RepeatBehavior = new RepeatBehavior(100);      
            Z.BeginAnimation(AxisAngleRotation3D.AngleProperty, anim);

        }
        private void BtnStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            double zmin = minZ.Value;
            double zmax = maxZ.Value;
            anim.From = zmin;
            anim.To = zmax;
            anim.Duration = TimeSpan.FromSeconds(0);
            Z.BeginAnimation(AxisAngleRotation3D.AngleProperty, anim);
        }
    }
}

