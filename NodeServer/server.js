net = require('net')
var ar = require('ar-drone');

var socket;
var drones = new Array();

var s = net.Server(function (client) {

    socket = client;

    socket.on('data', function (msg_sent) {
        var id = msg_sent[0];
        var msg = msg_sent[1]
        var x = msg_sent[2];
        var y = msg_sent[3];
        var z = msg_sent[4];

        if (id > 0) console.log("Drone " + id);

        switch (msg) {
            case 0: console.log('Connected'); break;
            case 1: console.log('New drone'); break;
            case 2: console.log('Removed drone'); break;
            case 3: console.log('Take off'); break;
            case 4: console.log('Land'); break;
            case 5: console.log('Move to ' + '(' + x + ', ' + y + ', ' + z + ')'); break;
            case 6: console.log('Stop'); break;
        }
    });

    socket.on('end', function () {
        process.exit();
    });
});

s.listen(8000);
console.log('System waiting at http://localhost:8000');

function sendInfo(id) {
    var s = new Array();
    s[0] = drones[id].id;
    s[1] = drones[id].position.x;
    s[2] = drones[id].position.y;
    s[3] = drones[id].position.z;
    socket.write(new Buffer.from(s));
}
