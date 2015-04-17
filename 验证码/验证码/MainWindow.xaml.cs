using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;
using ImageLibrary;
using System.Drawing.Imaging;
using 验证码.Model;
namespace 验证码
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageInfo img = new ImageInfo();
            img.RecImageInfo();
            Dispatcher.BeginInvoke((Action) delegate(){
                this.info.DataContext = img;
            },null);
        }

       
    }
}
