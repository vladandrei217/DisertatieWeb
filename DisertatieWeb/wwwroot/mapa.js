window.initMap = function (senzori) {
    const map = L.map('map').setView([44.4268, 26.1025], 13); // Setează locația și zoom-ul inițial al hărții

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18, // Limita zoom-ului
    }).addTo(map);

    senzori.forEach(s => {
        // Crează marker pentru fiecare senzor folosind coordonatele latitudine și longitudine
        const marker = L.marker([s.latitudine, s.longitudine]).addTo(map);

        // Eveniment de click pe marker
        marker.on('click', function () {

            DotNet.invokeMethodAsync('DisertatieWeb', 'SelecteazaSenzorInComponenta', s.id)
                .then(() => {
                    console.log('Senzor selectat: ' + s.Id); // Log mesaj pentru confirmare
                })
                .catch(error => {
                    console.error('Eroare la invocarea metodei .NET:', error);
                });
        });
    });
};
window.initZoneMap = function (zonePopulare) {
    const map = L.map('zoneMap').setView([44.4268, 26.1025], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
    }).addTo(map);

    zonePopulare.forEach(zona => {
        L.marker([zona.latitudine, zona.longitudine])
            .addTo(map)
            .bindPopup(`${zona.numeZona}<br>Vizitatori: ${zona.vizitatori}`);
    });
};

