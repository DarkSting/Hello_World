import 'dart:async';

import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:login_flutter/Models/SocketIO.dart';


class MapPage extends StatefulWidget {
  const MapPage({super.key});

  @override
  State<MapPage> createState() => _MapPageState();
}

class _MapPageState extends State<MapPage> {

  //set markers
  Set<Marker> markers = new Set<Marker>();

  //current marker index
  int currentIndex = 0;

  //goople api controller
  final Completer<GoogleMapController> _controller =
  Completer<GoogleMapController>();

  //socket
  TrainLiveSocket? socket;

  //initialize the markers
  @override
  initState(){

    socket?.connectToSocket();
    markers = createMarkers(5);
  }

  //current marker
  Marker _currentMarker = Marker(markerId: MarkerId('marker_1'),
  position: LatLng(6.9337, 79.8500),
    infoWindow: InfoWindow(title:'current position')
  );

  Marker _lastMarker = Marker(markerId: MarkerId('marker_1'),
      position: LatLng(6.9337, 79.8500),
      infoWindow: InfoWindow(title:'current position')
  );

  //create markers to track the directions
  Set<Marker> createMarkers(int count){
    //18.5 meters
    double difference = 0.01;

    Set<Marker> markers = Set<Marker>();

    for(int i=0;i<count;i++){
      LatLng ctLocation = LatLng(_lastMarker.position.latitude+difference, _currentMarker.position.longitude);
      Marker currentMarker = Marker(markerId: MarkerId('${i} marker'),
      position: ctLocation,
        infoWindow: InfoWindow(title: "${i} marker")
      );
      markers.add(currentMarker);
      _lastMarker = currentMarker;

    }
    return markers;
  }

  static const CameraPosition _jaffna = CameraPosition(
      bearing: 192.8334901395799,
      target: LatLng(9.6667, 80.0000),
      tilt: 59.440717697143555,
      zoom: 19.151926040649414);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: GoogleMap(
        mapType: MapType.normal,
        initialCameraPosition: _jaffna,
        onMapCreated: (GoogleMapController controller) {
          _controller.complete(controller);
        },
        markers:markers ,
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: (){

          print("this is the current index :"+currentIndex.toString());

          if(markers.length<=currentIndex){
            currentIndex =0;
            return;
          }
          _goToThePosition(markers.elementAt(currentIndex).position);
          currentIndex++;
        },
        label: const Text('Change Position'),
        icon: const Icon(Icons.train),
      ),
    );
  }

  Future<void> _goToThePosition(LatLng newPosition) async {
    final GoogleMapController controller = await _controller.future;

    await controller.animateCamera(CameraUpdate.newCameraPosition(CameraPosition(
      target: newPosition,
      zoom: 14.4746,
      tilt: 59.440717697143555
    )));


    setState(() {
      _currentMarker = markers.elementAt(currentIndex);

    });
  }

  @override
  void dispose() {
    // TODO: implement dispose
    super.dispose();
    socket?.disConnect();

  }
}
