Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace DXMap_CustomEllipses
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub


        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            itemsStorage.Items.Clear()

        End Sub


        Private Sub mapControl_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Keyboard.Modifiers = ModifierKeys.Shift Then

                Dim centerPoint As GeoPoint = vectorLayer.ScreenToGeoPoint(e.GetPosition(vectorLayer))
                Dim radius As Double = CDbl(seRadius.Value)

                Dim dot As New MapDot() With {.Location = centerPoint}
                dot.Size = 6
                itemsStorage.Items.Add(dot)


                Dim defaultEllipse As MapEllipse = MapEllipse.CreateByCenter(mapControl.CoordinateSystem, centerPoint, radius*2, radius*2)
                defaultEllipse.Fill = Brushes.Transparent
                defaultEllipse.Stroke = Brushes.White
                itemsStorage.Items.Add(defaultEllipse)

                Dim equalDistance As MapPolyline = (New EquidistantCircleCreator(vectorLayer)).CreateCircle(centerPoint, radius)
                equalDistance.Stroke = Brushes.GreenYellow
                itemsStorage.Items.Add(equalDistance)

                Dim ScreenCircle As MapPolyline = (New ScreenCircleCreator(vectorLayer)).CreateCircle(centerPoint, radius)
                ScreenCircle.Stroke = Brushes.Red
                itemsStorage.Items.Add(ScreenCircle)
            End If
        End Sub
    End Class


End Namespace
