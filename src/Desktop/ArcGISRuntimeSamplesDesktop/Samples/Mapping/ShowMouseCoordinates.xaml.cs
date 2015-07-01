﻿using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArcGISRuntime.Samples.Desktop
{
	/// <summary>
	/// This sample includes a Map and a single ArcGIS Server layer. MouseMove events on the Map are handled to return the mouse cursor location over the map. The location is displayed in pixels and map units.
	/// </summary>
	/// <title>Show Mouse Coordinates</title>
	/// <category>Mapping</category>
	public partial class ShowMouseCoordinates : UserControl
	{
		public ShowMouseCoordinates()
		{
			InitializeComponent();
		}

		private void MyMapView_MouseMove(object sender, MouseEventArgs e)
		{
			if (MyMapView.GetCurrentViewpoint(ViewpointType.BoundingGeometry) == null || MyMapView.GetCurrentViewpoint(ViewpointType.BoundingGeometry).TargetGeometry.Extent == null)
				return;

			System.Windows.Point screenPoint = e.GetPosition(MyMapView);
			ScreenCoordsTextBlock.Text = string.Format("Screen Coords: X = {0}, Y = {1}",
				screenPoint.X, screenPoint.Y);

			MapPoint mapPoint = MyMapView.ScreenToLocation(screenPoint);
			if (MyMapView.WrapAround)
				mapPoint = GeometryEngine.NormalizeCentralMeridian(mapPoint) as MapPoint;
			MapCoordsTextBlock.Text = string.Format("Map Coords: X = {0}, Y = {1}",
					Math.Round(mapPoint.X, 4), Math.Round(mapPoint.Y, 4));
		}

	    private void MySceneView_MouseMove(object sender, MouseEventArgs e)
	    {
		    System.Windows.Point screenPoint = e.GetPosition(MySceneView);
		    var sc = string.Format("Screen Coords: X = {0}, Y = {1}",
			    screenPoint.X, screenPoint.Y);
		    System.Diagnostics.Debug.WriteLine(sc);
		    MapPoint mapPoint = MySceneView.ScreenToLocation(screenPoint);
		    if (mapPoint != null)
		    {
			    var mp = string.Format("Map Coords: X = {0}, Y = {1}",
				    Math.Round(mapPoint.X, 4), Math.Round(mapPoint.Y, 4));
			    System.Diagnostics.Debug.WriteLine(mp);
		    }
	    }
	}
}
