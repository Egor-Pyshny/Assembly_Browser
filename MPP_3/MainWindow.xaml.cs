using AssemblyExplorer.Models;
using MPP_3.ViewModel;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MPP_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<AssemblyModel> Folders { get; set; } = new();


        public MainWindow()
        {
            InitializeComponent();
            Assembly assembly = Assembly.LoadFrom("C:\\Users\\user\\Downloads\\Telegram Desktop\\MPP_2.dll");
            Type[] types = assembly.GetTypes();
            var a = new AssemblyModel(assembly);
            MyViewModel v = new MyViewModel();
            v.add(a);
            v.add(a);
            //var assemblyViewModel = new AssemblyViewModel(a);
            DataContext = v;
        }

        private string CreateGenericTypeString(Type type)
        {
            string res = "";
            int len = type.Name.ElementAt(type.Name.IndexOf('`') + 1) - '0';
            var a = type.GetGenericTypeDefinition();
            if (a != null)
            {
                res += a.Name;
                Type[] t = type.GenericTypeArguments;
                while (res.Contains("`"))
                {
                    string s = "";
                    for (int i = 0; i < len; i++)
                    {
                        if (i == 0)
                            s = $"<{t[i].Name}";
                        else
                            s += $", {t[i].Name}";
                    }
                    s += ">";
                    foreach(Type tmp in t)
                    {
                        if (tmp.IsGenericType) {
                            s = s.Replace(tmp.Name, CreateGenericTypeString(tmp));
                        }
                    }
                    res = res.Replace($"`{len}", s);
                    
                }
            }
            return res;
        }
    }
}