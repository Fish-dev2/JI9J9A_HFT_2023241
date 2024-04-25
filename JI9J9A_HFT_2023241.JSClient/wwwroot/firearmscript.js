let firearms = [];
let ammos = [];
let server = 'http://localhost:27031/firearm/'
let connection;

let firearmIdToUpdate = -1;

getdata();

setupSignalR();

async function getdata() {
    await fetch('http://localhost:27031/Ammo/')
        .then(z => z.json())
        .then(f => {
            ammos = f;
            displayammo();
            console.log(f);
        })
    await fetch(server)
        .then(x => x.json())
        .then(y => {
            firearms = y;
            console.log(y);
            display();
        });
}
function displayammo() {
    console.log(ammos.length);
    document.getElementById('ammo-type').innerHTML = '';
    ammos.forEach(t => {

        document.getElementById('ammo-type').innerHTML +=
            `<option value="${t.ammoId}">${t.name}</option>`

        document.getElementById('ammo-type-update').innerHTML +=
            `<option value="${t.ammoId}">${t.name}</option>`
    })
}

function display() {

    console.log("RUNNING")
    document.getElementById('resultarea').innerHTML = '';
    console.log("Length of firearms array:", firearms.length);
    firearms.forEach(t => {
        console.log("RUNNINGONE");
        document.getElementById('resultarea').innerHTML +=
            '<tr>' +
            '<td>' + t.gunId + '</td>' +
            '<td>' + t.name + '</td>' +
        '<td>' + t.manufacturer + '</td>' +
            '<td>' + t.fireRate + '</td>' +
            '<td>' + new Date(t.releaseDate).toDateString() + '</td>' +
            '<td>' + t.ammoId + '</td>' +
            '<td>' + toAmmo(t.ammoId) + '</td>' +
            `<td><button type="button" onclick="remove(${t.gunId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.gunId})">Update</button></td>` +
            '</tr>';
    })
}

function showupdate(id) {
    let form = document.getElementById('updateformdiv');
    form.classList.toggle('hidden');
    let firearm = firearms.find(t => t['gunId'] == id)
    document.getElementById('name-update').value = firearm['name'];
    document.getElementById('manufacturer-update').value = firearm['manufacturer'];
    document.getElementById('firerate-update').value = firearm['fireRate'];

    //day was off by one????
    var date = new Date(firearm['releaseDate']);
    var offset = date.getTimezoneOffset() * 60000;
    var localDate = new Date(date.getTime() - offset);
    //this fixes it

    document.getElementById('release-date-update').value = localDate.toISOString().split('T')[0];
    document.getElementById('ammo-type-update').value = firearm['ammoId'];

    firearmIdToUpdate = id;

}
function create() {
    let name = document.getElementById('name').value;
    let manufacturer = document.getElementById('manufacturer').value;
    let firerate = Number(document.getElementById('firerate').value);
    let releasedate = new Date(document.getElementById('release-date').value);
    let ammoid = Number(document.getElementById('ammo-type').value);

    fetch(server, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                manufacturer: manufacturer,
                fireRate: firerate,
                releaseDate: releasedate.toISOString(),
                ammoId: ammoid,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}
function update() {
    let name = document.getElementById('name-update').value;
    let manufacturer = document.getElementById('manufacturer-update').value;
    let firerate = Number(document.getElementById('firerate-update').value);
    let releasedate = new Date(document.getElementById('release-date-update').value);
    let ammoid = Number(document.getElementById('ammo-type-update').value);

    fetch(server, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                gunId: firearmIdToUpdate,
                name: name,
                manufacturer: manufacturer,
                fireRate: firerate,
                releaseDate: releasedate.toISOString(),
                ammoId: ammoid,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
    let form = document.getElementById('updateformdiv');
    form.classList.toggle('hidden');
}

function remove(id) {
    fetch(server + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function toAmmo(id) {
    let foundAmmo = ammos.find(ammo => ammo.ammoId === id);
    if (foundAmmo == null) {
        return "none";
    }
    return foundAmmo.name;
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27031/hub/")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    connection.on
        ('FirearmCreated', (user, message) => {
            console.log(user);
            console.log(message);
            getdata();
        });
    connection.on
        ('FirearmDeleted', (user, message) => {
            console.log(user);
            console.log(message);
            getdata();
        });
    connection.on
        ('FirearmUpdated', (user, message) => {
            console.log(user);
            console.log(message);
            getdata();
        });

    connection.onclose
        (async () => {
            await start();
        });
    start();

}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};