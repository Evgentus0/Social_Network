function toGoogleString(str) {
    let replacedStr = str.replace(/\s+/g, '+').replace(',', '').replace('.', '');
    console.log(replacedStr);
    return replacedStr;
}

function httpGet(url) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, false); 
    xmlHttp.send(null);
    return xmlHttp.responseText;
}

 

function DisplayGoogleMap() {

    var destination = document.getElementById("Destination").value;
    var departure = document.getElementById("Departure").value;
    destination = toGoogleString(destination);
    departure = toGoogleString(departure);
    var destinationUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + destination + "&key=AIzaSyClsVpXKPq9St6ZLJSTvE5QJ3lTekqxOPM";
    var departureUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + departure + "&key=AIzaSyClsVpXKPq9St6ZLJSTvE5QJ3lTekqxOPM";
    var geoDestResponse = JSON.parse(httpGet(destinationUrl));
    var geoDeparResponse = JSON.parse(httpGet(departureUrl));

    var latDestCoord = geoDestResponse.results[0].geometry.location.lat;
    var lngDestCoord = geoDestResponse.results[0].geometry.location.lng;

    var latDepartCoord = geoDeparResponse.results[0].geometry.location.lat;
    var lngDepartCoord = geoDeparResponse.results[0].geometry.location.lng;

    var destAddress = new google.maps.LatLng(latDestCoord, lngDestCoord);
    var departAddress = new google.maps.LatLng(latDepartCoord, lngDepartCoord);
    var centerAdress = new google.maps.LatLng((latDestCoord + latDepartCoord) / 2, (lngDestCoord + lngDepartCoord) / 2);
    //Create Options or set different Characteristics of Google Map
    var mapOptions = {
        center: centerAdress,
        zoom: 5,
        minZoom: 5,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    //Display the Google map in the div control with the defined Options
    var map = new google.maps.Map(document.getElementById("myDiv"), mapOptions);

    //Set Marker on the Map
    var destMarker = new google.maps.Marker({
        position: destAddress,
        animation: google.maps.Animation.BOUNCE,
    });

    var departMarker = new google.maps.Marker({
        position: departAddress,
        animation: google.maps.Animation.BOUNCE,
    });
    destMarker.setMap(map);
    departMarker.setMap(map);
}