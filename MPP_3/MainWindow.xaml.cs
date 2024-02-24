using AssemblyExplorer.Models;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MPP_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Assembly assembly = Assembly.LoadFrom("C:\\Users\\Пользователь\\source\\repos\\MPP_2\\MPP_2\\bin\\Debug\\net8.0\\MPP_2.dll");
            Type[] types = assembly.GetTypes();
            var o = types[2].GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            var a = new AssemblyModel(assembly);
            var t = a.namespaces["-"].classes[2].methodsS;
        }
    }
}