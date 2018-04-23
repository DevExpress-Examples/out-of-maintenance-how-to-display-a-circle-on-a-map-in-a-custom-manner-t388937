using DevExpress.Map;
using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DXMap_CustomEllipses {
    public abstract class CircleCreator {
        protected internal VectorLayer vectorLayer;

        protected CircleCreator(VectorLayer vectorLayer) {
            this.vectorLayer = vectorLayer;
        }
        public MapPolyline CreateCircle(GeoPoint center, double radius) {
            MapPolyline ellipse = CreateCircleCore(center, radius);            
            return ellipse;
        }
        protected abstract MapPolyline CreateCircleCore(GeoPoint center, double radius);
    }
   
    public class EquidistantCircleCreator : CircleCreator {
        public EquidistantCircleCreator(VectorLayer vectorLayer)
            : base(vectorLayer) {
        }

        protected override MapPolyline CreateCircleCore(GeoPoint center, double radius) {
            MapPolyline ellipse = new MapPolyline();
            double step = Math.PI / 180.0;
            for (double i = 0; i < 2.0 * Math.PI; i += step) {
                Point distanceDelta = new Point(radius * Math.Cos(i), radius * Math.Sin(i));
                Size coordDelta = vectorLayer.KilometersToGeoSize(center, new Size(Math.Abs(distanceDelta.X), Math.Abs(distanceDelta.Y )));

                CoordPoint point = new GeoPoint(center.Latitude + coordDelta.Height * distanceDelta.Y.CompareTo(0), center.Longitude + coordDelta.Width * distanceDelta.X.CompareTo(0));
                ellipse.Points.Add(point);
            }
            return ellipse;
        }
    }
    public class ScreenCircleCreator : CircleCreator {
        public ScreenCircleCreator(VectorLayer vectorLayer)
            : base(vectorLayer) {
        }

        protected override MapPolyline CreateCircleCore(GeoPoint center, double radius) {
            Size delta = vectorLayer.KilometersToGeoSize(center, new Size(radius, 0));
            GeoPoint rightPoint = new GeoPoint(center.Latitude, center.Longitude + delta.Width);
            Point rightPointOnScreen = vectorLayer.GeoToScreenPoint(rightPoint);
            Point centerPointOnScreen = vectorLayer.GeoToScreenPoint(center);

            double screenRadius = rightPointOnScreen.X - centerPointOnScreen.X;

            MapPolyline ellipse = new MapPolyline();
            double step = Math.PI / 180.0;
            for (double i = 0; i < 2.0 * Math.PI; i += step) {
                Point point = new Point(centerPointOnScreen.X + screenRadius * Math.Cos(i), centerPointOnScreen.Y - screenRadius * Math.Sin(i));
                CoordPoint geoPoint = vectorLayer.ScreenToGeoPoint(point);
                ellipse.Points.Add(geoPoint);
            }
            return ellipse;
        }
    }

}
