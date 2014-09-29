using System;
using System.Threading.Tasks;
using System.Windows;

namespace _11___Excecoes
{
    public class TaskException : Exception
    {
        public TaskException(string msg)
            : base(msg)
        {

        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                throw new TaskException("Exceção em task");
            });

            try
            {
                task1.Wait();
            }
            catch (TaskException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Async_Click(object sender, RoutedEventArgs e)
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                throw new TaskException("Exceção em task");
            });

            try
            {
                await task1;
            }
            catch (TaskException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
