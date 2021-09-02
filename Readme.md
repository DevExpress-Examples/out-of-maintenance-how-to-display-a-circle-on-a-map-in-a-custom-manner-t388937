<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128571462/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T388937)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [EllipseCreator.cs](./CS/DXMap_CustomEllipses/EllipseCreator.cs) (VB: [EllipseCreator.vb](./VB/DXMap_CustomEllipses/EllipseCreator.vb))
* [MainWindow.xaml](./CS/DXMap_CustomEllipses/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/DXMap_CustomEllipses/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/DXMap_CustomEllipses/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/DXMap_CustomEllipses/MainWindow.xaml.vb))
<!-- default file list end -->
# How to display a circle on a Map in a custom manner


<p>This example demonstrates how to create MapPolyline items, which allow displaying circles on a map. The circles' bounds are calculated using different rules:<br>The EquidistantCircleCreator class allows creating a polyline by calculating the distance from the center point on the Earth surface.<br>The ScreenCircleCreator class allows creating an polyline by calculating the distance from the center point by using the screen coordinates.<br>The bounds of the default <a href="https://documentation.devexpress.com/WPF/clsDevExpressXpfMapMapEllipsetopic.aspx">MapEllipse</a> item are calculated using complex logic that provides an intermediate result. <br><br>The difference of these algorithms is mostly noticeable if a circle with a big radius is displayed close to the poles.</p>

<br/>


