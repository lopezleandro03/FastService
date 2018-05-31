function initAutocomplete() {
    autocomplete = new google.maps.places.Autocomplete(
        (document.getElementById("@ViewBag.InputId")),
        { types: ['geocode'] });

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete.addListener('place_changed', fillInAddress);
}

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAF44JdvJ96J5WyhIwxFRc646TTjhQ2dIY&libraries=places&callback=initAutocomplete"
    async defer></script>