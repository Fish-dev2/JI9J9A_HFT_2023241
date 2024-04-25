let registers = [];
let server = 'http://localhost:27031/register/'
let connection;

let regIdToUpdate = -1;

getdata();
setupSignalR();

async function getdata() {
    await fetch(server)
        .then(x => x.json())
        .then(y => {
            registers = y;
            display();
            console.log(y);
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = '';
    registers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            '<tr>' +
            '<td>' + t.id + '</td>' +
            '<td>' + t.ownerId + '</td>' +
            '<td>' + t.firearmId + '</td>' +
            '<td>' + new Date(t.registrationDate).toDateString() + '</td>' +
            `<td><button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button></td>` +
            '</tr>';
    })
}

function showupdate(id) {
    let form = document.getElementById('updateformdiv');
    form.classList.toggle('hidden');
    let reg = registers.find(t => t['id'] == id)
    document.getElementById('owner-id-update').value = reg['ownerId'];
    document.getElementById('firearm-id-update').value = reg['firearmId'];

    //day was off by one????
    var date = new Date(reg['registrationDate']);
    var offset = date.getTimezoneOffset() * 60000;
    var localDate = new Date(date.getTime() - offset);
    //this fixes it

    document.getElementById('registration-date-update').value = localDate.toISOString().split('T')[0];

    regIdToUpdate = id;

}
function create() {
    let ownerid = document.getElementById('owner-id').value;
    let firearmid = document.getElementById('firearm-id').value;
    let registrationDate = new Date(document.getElementById('registration-date').value);

    fetch(server, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                ownerId: ownerid,
                firearmId: firearmid,
                registrationDate: registrationDate.toISOString(),
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
    let ownerid = document.getElementById('owner-id-update').value;
    let firearmid = document.getElementById('firearm-id-update').value;
    let registrationDate = new Date(document.getElementById('registration-date-update').value);

    fetch(server, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: regIdToUpdate,
                ownerId: ownerid,
                firearmId: firearmid,
                registrationDate: registrationDate.toISOString(),
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
        ('RegisterCreated', (user, message) => {
            console.log(user);
            console.log(message);
            getdata();
        });
    connection.on
        ('RegisterDeleted', (user, message) => {
            console.log(user);
            console.log(message);
            getdata();
        });
    connection.on
        ('RegisterUpdated', (user, message) => {
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
