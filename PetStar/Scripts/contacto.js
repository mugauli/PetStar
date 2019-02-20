
var map;

function newLocation(newLat, newLng) {
    map.setCenter({
        lat: newLat,
        lng: newLng
    });
    var uluru = { lat: newLat, lng: newLng };
    var marker = new google.maps.Marker({ position: uluru, map: map });
}

// Initialize and add the map
function initMap() {
    // The location of Uluru
    var uluru = { lat: -25.344, lng: 131.036 };
    // The map, centered at Uluru
    map = new google.maps.Map(document.getElementById('map'), { zoom: 10, center: uluru });
    // The marker, positioned at Uluru
    var marker = new google.maps.Marker({ position: uluru, map: map });
}

//google.maps.event.addDomListener(window, 'load', initialize);

//Setting Location with jQuery
$(document).ready(function () {
    $("#sltSucursal").change(function () {
        
        var value = dict[$(this).val()];
        var lat = value.split(",")[0];
        var long = value.split(",")[1];
        newLocation(parseFloat(lat),parseFloat(long));
    });

  
});

