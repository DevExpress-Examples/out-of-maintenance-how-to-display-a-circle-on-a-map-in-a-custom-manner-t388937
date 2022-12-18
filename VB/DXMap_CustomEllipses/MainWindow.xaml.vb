Imports DevExpress.Xpf.Map
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media

Namespace DXMap_CustomEllipses

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.itemsStorage.Items.Clear()
        End Sub

        Private Sub mapControl_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Keyboard.Modifiers = ModifierKeys.Shift Then
                Dim centerPoint As GeoPoint = Me.vectorLayer.ScreenToGeoPoint(e.GetPosition(Me.vectorLayer))
                Dim radius As Double = CDbl(Me.seRadius.Value)
                Dim dot As MapDot = New MapDot() With {.Location = centerPoint}
                dot.Size = 6
                Me.itemsStorage.Items.Add(dot)
                Dim defaultEllipse As MapEllipse = MapEllipse.CreateByCenter(Me.mapControl.CoordinateSystem, centerPoint, radius * 2, radius * 2)
                defaultEllipse.Fill = Brushes.Transparent
                defaultEllipse.Stroke = Brushes.White
                Me.itemsStorage.Items.Add(defaultEllipse)
                Dim equalDistance As MapPolyline = New EquidistantCircleCreator(CType(Me.vectorLayer, VectorLayer)).CreateCircle(centerPoint, radius)
                equalDistance.Stroke = Brushes.GreenYellow
                Me.itemsStorage.Items.Add(equalDistance)
                Dim ScreenCircle As MapPolyline = New ScreenCircleCreator(CType(Me.vectorLayer, VectorLayer)).CreateCircle(centerPoint, radius)
                ScreenCircle.Stroke = Brushes.Red
                Me.itemsStorage.Items.Add(ScreenCircle)
            End If
        End Sub
    End Class
End Namespace
