using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DXMap_CustomEllipses {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            itemsStorage.Items.Clear();

        }


        private void mapControl_MouseDown(object sender, MouseButtonEventArgs e) {
            if(Keyboard.Modifiers == ModifierKeys.Shift)
            {
                
                GeoPoint centerPoint = vectorLayer.ScreenToGeoPoint(e.GetPosition(vectorLayer));
                double radius = (double)seRadius.Value;

                MapDot dot = new MapDot() { Location = centerPoint };
                dot.Size = 6;
                itemsStorage.Items.Add(dot);


                MapEllipse defaultEllipse = MapEllipse.CreateByCenter(mapControl.CoordinateSystem, centerPoint, radius*2, radius*2);
                defaultEllipse.Fill = Brushes.Transparent;
                defaultEllipse.Stroke = Brushes.White;
                itemsStorage.Items.Add(defaultEllipse);

                MapPolyline equalDistance = new EquidistantCircleCreator(vectorLayer).CreateCircle(centerPoint, radius);
                equalDistance.Stroke = Brushes.GreenYellow;
                itemsStorage.Items.Add(equalDistance);

                MapPolyline ScreenCircle = new ScreenCircleCreator(vectorLayer).CreateCircle(centerPoint, radius);
                ScreenCircle.Stroke = Brushes.Red;
                itemsStorage.Items.Add(ScreenCircle);
            }
        }
    }


}
