let ammos = [];
let server = 'http://localhost:27031/Ammo/'
let connection;

let ammoIdToUpdate = -1;

getdata();

setupSignalR();

async function getdata() {
    await fetch(server)
        .then(x => x.json())
        .then(y => {
            ammos = y;
            display();
            console.log(y);
        })
}


function display() {

    document.getElementById('resultarea').innerHTML = '';
    ammos.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            '<tr>' +
            '<td>' + t.ammoId + '</td>' +
            '<td>' + t.name + '</td>' +
            '<td>' + t.diameter + '</td>' +
            '<td>' + t.length + '</td>' +
            '<td>' + t.bulletType + '</td>' +
            `<td><button type="button" onclick="remove(${t.ammoId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.ammoId})">Update</button></td>` +
            '</tr>';
    })
}

function showupdate(id) {
    let form = document.getElementById('updateformdiv');
    form.classList.toggle('hidden');
    let ammo = ammos.find(t => t['ammoId'] == id)
    document.getElementById('name-update').value = ammo['name'];
    document.getElementById('diameter-update').value = ammo['diameter'];
    document.getElementById('length-update').value = ammo['length'];


    
    document.getElementById('bullettype-update').value = ammo['ammoId'];

    ammoIdToUpdate = id;

}
function create() {
    let name = document.getElementById('name').value;
    let diameter = Number(document.getElementById('diameter').value);
    let length = Number(document.getElementById('length').value);
    let bullettype = document.getElementById('bullettype').value;

    fetch(server, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                diameter: diameter,
                length: length,
                bulletType: bullettype,
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
    let diameter = Number(document.getElementById('diameter-update').value);
    let length = Number(document.getElementById('length-update').value);
    let bullettype = document.getElementById('bullettype-update').value;

    fetch(server, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                ammoId: ammoIdToUpdate,
                name: name,
                diameter: diameter,
                length: length,
                bulletType: bullettype,
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