﻿using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArcGISRuntime.Samples.Desktop
{
	/// <summary>
	/// This sample demonstrates how to add a GraphicsOverlay with Graphics and Symbols to the map in XAML.  The sample also shows how to add Polyline graphics to a GraphicsOverlay from the code-behind.
	/// </summary>
	/// <title>Graphics Layer</title>
	/// <category>Layers</category>
	/// <subcategory>Graphics Layers</subcategory>
	public partial class GraphicsLayerSample : UserControl
	{
		private GraphicsLayer _graphicsLayer;
		private MapPoint _ptStart;
		private MapPoint _ptEnd;

		/// <summary>Construct graphics layer sample control</summary>
		public GraphicsLayerSample()
		{
			InitializeComponent();

			_graphicsLayer = MyMapView.Scene.Layers["GraphicsLayer"] as GraphicsLayer;

			MyMapView.NavigationCompleted += MyMapView_NavigationCompleted;

		}

		void MyMapView_NavigationCompleted(object sender, System.EventArgs e)
		{
			MyMapView.NavigationCompleted -= MyMapView_NavigationCompleted;

			AddPointGraphics();
			AddPolyLineGraphics();
			AddPolygonGraphics();
		}

		private void AddPointGraphics()
		{
			var symbols = this.Resources.OfType<MarkerSymbol>();
			double x = -7000000;
			foreach (var symbol in symbols)
			{
				Graphic g = new Graphic(new MapPoint(x, 3900000, SpatialReferences.WebMercator), symbol);
				_graphicsLayer.Graphics.Add(g);
				x += 1000000;
			}

			_graphicsLayer.Graphics.Add(new Graphic(new MapPoint(-7000000, 3900000, SpatialReferences.WebMercator), (Symbol)Resources["RedMarkerSymbolCircle"]));
			_graphicsLayer.Graphics.Add(new Graphic(new MapPoint(-6000000, 3900000, SpatialReferences.WebMercator), (Symbol)Resources["RedMarkerSymbolCross"]));
			_graphicsLayer.Graphics.Add(new Graphic(new MapPoint(-5000000, 3900000, SpatialReferences.WebMercator), (Symbol)Resources["RedMarkerSymbolDiamond"]));
			_graphicsLayer.Graphics.Add(new Graphic(new MapPoint(-4000000, 3900000, SpatialReferences.WebMercator), (Symbol)Resources["RedMarkerSymbolSquare"]));
			_graphicsLayer.Graphics.Add(new Graphic(new MapPoint(-3000000, 3900000, SpatialReferences.WebMercator), (Symbol)Resources["RedMarkerSymbolTriangle"]));
			_graphicsLayer.Graphics.Add(new Graphic(new MapPoint(-2000000, 3900000, SpatialReferences.WebMercator), (Symbol)Resources["RedMarkerSymbolX"]));
		}

		private void AddPolyLineGraphics()
		{
			_ptStart = (MapPoint)_graphicsLayer.Graphics[0].Geometry;
			_ptEnd = (MapPoint)_graphicsLayer.Graphics[5].Geometry;

			var blueLineGeometry = new Polyline(
				new MapPoint[]
				{
					new MapPoint(_ptStart.X, _ptStart.Y + 1000000, SpatialReferences.WebMercator),
					new MapPoint(_ptEnd.X, _ptEnd.Y + 1000000, SpatialReferences.WebMercator)
				},
				_ptStart.SpatialReference);

			// Solid Blue line above point graphics
			Graphic blueLine = new Graphic()
			{
				Symbol = new SimpleLineSymbol() { Color = Colors.Blue, Style = SimpleLineStyle.Solid, Width = 4 },
				Geometry = blueLineGeometry
			};

			var greenLineGeometry = new Polyline(
				new MapPoint[]
				{
					new MapPoint(_ptStart.X, _ptStart.Y - 1000000, SpatialReferences.WebMercator),
					new MapPoint(_ptEnd.X, _ptEnd.Y - 1000000, SpatialReferences.WebMercator)
				},
				_ptStart.SpatialReference);

			// Dashed Green line below point graphics
			Graphic greenLine = new Graphic()
			{
				Symbol = new SimpleLineSymbol() { Color = Colors.Green, Style = SimpleLineStyle.Dash, Width = 4 },
				Geometry = greenLineGeometry
			};

			_graphicsLayer.Graphics.Add(blueLine);
			_graphicsLayer.Graphics.Add(greenLine);
		}

		private void AddPolygonGraphics()
		{
			_ptStart = (MapPoint)_graphicsLayer.Graphics[0].Geometry;
			_ptEnd = (MapPoint)_graphicsLayer.Graphics[5].Geometry;

			var purpleRectGeometry = new Polygon(
				new MapPoint[]
                    {
                        new MapPoint(_ptStart.X, _ptStart.Y - 2000000, SpatialReferences.WebMercator),
                        new MapPoint(_ptStart.X, _ptStart.Y - 2800000, SpatialReferences.WebMercator),
                        new MapPoint(_ptEnd.X, _ptEnd.Y - 2800000, SpatialReferences.WebMercator),
                        new MapPoint(_ptEnd.X, _ptEnd.Y - 2000000, SpatialReferences.WebMercator)

                    }, _ptStart.SpatialReference);

			// Purple rectangle below polyline graphic
			Graphic purpleRect = new Graphic()
			{
				Symbol = new SimpleFillSymbol() { Color = Colors.Purple, Style = SimpleFillStyle.Solid },
				Geometry = purpleRectGeometry
			};

			_graphicsLayer.Graphics.Add(purpleRect);
		}

	}

}
