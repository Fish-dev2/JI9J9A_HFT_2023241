let owners = [];
let server = 'http://localhost:27031/owner/'
let connection;


getdata();
setupSignalR();

async function getdata() {
   await fetch(server)
        .then(x => x.json())
        .then(y => {
            owners = y;
            display();
            console.log(y);
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = '';
    owners.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            '<tr>' +
            '<td>' + t.ownerId + '</td>' +
            '<td>' + t.firstName + '</td>' +
            '<td>' + t.lastName + '</td>' +
            '<td>' + new Date(t.licenceValidUntil).toDateString() + '</td>' +
        '<td>' + toLicence(t.licenceType) + '</td>' +
            `<td><button type="button" onclick="remove(${t.ownerId})">Delete</button></td>` +
            '</tr>';
    })
}
function create() {
    let firstname = document.getElementById('first-name').value;
    let lastname = document.getElementById('last-name').value;
    let validity = new Date(document.getElementById('validity').value);
    let licencetype = Number(document.getElementById('licence-type').value);

    fetch(server, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json',},
        body: JSON.stringify(
            {
                firstName: firstname,
                lastName: lastname,
                licenceValidUntil: validity.toISOString(),
                licenceType: licencetype,
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
function remove (id){
    fetch(server + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
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
        ('OwnerCreated', (user, message) => {
            console.log(user);
            console.log(message);
            getdata();
        });
    connection.on
        ('OwnerDeleted', (user, message) => {
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


function toLicence(type) {
    switch (type) {
        case 0:
            return "Self Defense";
        case 1:
            return "Hunting";
        case 2:
            return "Security";
        default:
            return "Error";
    }
}