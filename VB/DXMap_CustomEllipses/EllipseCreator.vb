Imports DevExpress.Map
Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows

Namespace DXMap_CustomEllipses
	Public MustInherit Class CircleCreator
		Protected Friend vectorLayer As VectorLayer

		Protected Sub New(ByVal vectorLayer As VectorLayer)
			Me.vectorLayer = vectorLayer
		End Sub
		Public Function CreateCircle(ByVal center As GeoPoint, ByVal radius As Double) As MapPolyline
			Dim ellipse As MapPolyline = CreateCircleCore(center, radius)
			Return ellipse
		End Function
		Protected MustOverride Function CreateCircleCore(ByVal center As GeoPoint, ByVal radius As Double) As MapPolyline
	End Class

	Public Class EquidistantCircleCreator
		Inherits CircleCreator

		Public Sub New(ByVal vectorLayer As VectorLayer)
			MyBase.New(vectorLayer)
		End Sub

		Protected Overrides Function CreateCircleCore(ByVal center As GeoPoint, ByVal radius As Double) As MapPolyline
			Dim ellipse As New MapPolyline()
			Dim [step] As Double = Math.PI / 180.0
			Dim i As Double = 0
			Do While i < 2.0 * Math.PI
				Dim distanceDelta As New Point(radius * Math.Cos(i), radius * Math.Sin(i))
				Dim coordDelta As Size = vectorLayer.KilometersToGeoSize(center, New Size(Math.Abs(distanceDelta.X), Math.Abs(distanceDelta.Y)))

				Dim point As CoordPoint = New GeoPoint(center.Latitude + coordDelta.Height * distanceDelta.Y.CompareTo(0), center.Longitude + coordDelta.Width * distanceDelta.X.CompareTo(0))
				ellipse.Points.Add(point)
				i += [step]
			Loop
			Return ellipse
		End Function
	End Class
	Public Class ScreenCircleCreator
		Inherits CircleCreator

		Public Sub New(ByVal vectorLayer As VectorLayer)
			MyBase.New(vectorLayer)
		End Sub

		Protected Overrides Function CreateCircleCore(ByVal center As GeoPoint, ByVal radius As Double) As MapPolyline
			Dim delta As Size = vectorLayer.KilometersToGeoSize(center, New Size(radius, 0))
			Dim rightPoint As New GeoPoint(center.Latitude, center.Longitude + delta.Width)
			Dim rightPointOnScreen As Point = vectorLayer.GeoToScreenPoint(rightPoint)
			Dim centerPointOnScreen As Point = vectorLayer.GeoToScreenPoint(center)

			Dim screenRadius As Double = rightPointOnScreen.X - centerPointOnScreen.X

			Dim ellipse As New MapPolyline()
			Dim [step] As Double = Math.PI / 180.0
			Dim i As Double = 0
			Do While i < 2.0 * Math.PI
				Dim point As New Point(centerPointOnScreen.X + screenRadius * Math.Cos(i), centerPointOnScreen.Y - screenRadius * Math.Sin(i))
				Dim geoPoint As CoordPoint = vectorLayer.ScreenToGeoPoint(point)
				ellipse.Points.Add(geoPoint)
				i += [step]
			Loop
			Return ellipse
		End Function
	End Class

End Namespace
