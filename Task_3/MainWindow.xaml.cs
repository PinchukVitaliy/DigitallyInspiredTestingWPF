using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;

namespace Task_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<D3Model> d3ModelsList = new List<D3Model>();
        public MainWindow()
        {
            InitializeComponent();
            ListTasks.ItemsSource = AddData();       
        }

        private List<D3Model> AddData()
        {
            d3ModelsList = new List<D3Model>();
            HelixViewport3D helixViewport3D1 = new HelixViewport3D() { Name = "One" };
            HelixViewport3D helixViewport3D2 = new HelixViewport3D() { Name = "Three" };
            HelixViewport3D helixViewport3D3 = new HelixViewport3D() { Name = "Four" };
           
            helixViewport3D1.Children.Add(new DefaultLights());
            helixViewport3D1.Children.Add(new Teapot());
            d3ModelsList.Add(new D3Model() { id = 1, Name = helixViewport3D1.Name, helixViewport3D = helixViewport3D1 });
            helixViewport3D2.Children.Add(new DefaultLights());
            d3ModelsList.Add(new D3Model() { id = 2, Name = helixViewport3D2.Name, helixViewport3D = helixViewport3D2 });
            helixViewport3D3.Children.Add(new Teapot());
            d3ModelsList.Add(new D3Model() { id = 3, Name = helixViewport3D3.Name, helixViewport3D = helixViewport3D3 });
            return d3ModelsList;
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
            Model.Children.Clear();          
            Model.Children.Add(d3Model.helixViewport3D);
        }
       
        private void LayoutRootMouseLeft(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            D3Model d3 = new D3Model();
            openFileDialog.Filter = "Obj files (*.obj)|*.obj";
            if (openFileDialog.ShowDialog() == true)
            {
                string name = Path.GetFileName(openFileDialog.FileName);
                HelixViewport3D hekixModel = new HelixViewport3D();
                ModelVisual3D device = new ModelVisual3D();
                device.Content = getModel(hekixModel, openFileDialog.FileName);
                hekixModel.Children.Add(device);
                Model.Children.Add(hekixModel);

                d3ModelsList.Add(new D3Model() { id = d3ModelsList.Count + 1, Name = name.Substring(0, name.Length - 4), helixViewport3D = hekixModel });
                ListTasks.ItemsSource = null;
                ListTasks.ItemsSource = d3ModelsList;
            }
         
          
        }
        public Model3D getModel(HelixViewport3D viewport3D, string path)
        {
            Model3D device = null;
            try
            {
                viewport3D.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter import = new ModelImporter();
                device = import.Load(path);
            }
            catch (Exception e)
            { }
            return device;
        }
        private void BtnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (Model.Children.Count > 0)
            {
                Model.Children.Clear();
                D3Model d3Model = d3ModelsList.Find((item) => item.id.Equals(clearContent.Content)); 
                if (d3Model != null)
                {
                    d3ModelsList.Remove(d3Model);
                    ListTasks.ItemsSource = null;
                    ListTasks.ItemsSource = d3ModelsList;
                }
            }
        }
    }
}
