net = require('net')
var ar = require('ar-drone');
var c = ar.createClient();
c.createRepl();

var socket;

// Create a TCP socket listener
var s = net.Server(function (client) {

    socket = client;
    
    socket.on('data', function (msg_sent) {
        var msg = msg_sent[0];
        var id = msg_sent[1];
        if (id > 0) console.log("drone " + id);

        if (msg == 0) {
            console.log('Connected');
        }
        if (msg == 1) {
            console.log('Drone takeoff');
            c.takeoff();
        }
        if (msg == 2) {
            console.log('Drone land');
            c.land();
        }
        if (msg == 3) {
            console.log('Drone stop');
            c.stop();
        }
        if (msg == 31) {
            console.log('Drone stop X');
            c.left(0);
            c.right(0);
        }
        if (msg == 32) {
            console.log('Drone stop Y');
            c.front(0);
            c.back(0);
        }
        if (msg == 33) {
            console.log('Drone stop Z');
        }
        if (msg == 4) {
            console.log('Drone move forward');
            c.front(0.1);
        }
        if (msg == 5) {
            console.log('Drone move backward');
            c.back(0.1);
        }
        if (msg == 6) {
            console.log('Drone move left');
            c.left(0.1);
        }
        if (msg == 7) {
            console.log('Drone move right');
            c.right(0.1);
        }
        if (msg == 8) {
            console.log('Drone move up');
        }
        if (msg == 9) {
            console.log('Drone move down');
        }
    });
    
    socket.on('end', function () {
        return;
    });
});

s.listen(8000);
console.log('System waiting at http://localhost:8000');

function hex2a(hexx) {
    var hex = hexx.toString();//force conversion
    var str = '';
    for (var i = 0; i < hex.length; i += 2)
        str += String.fromCharCode(parseInt(hex.substr(i, 2), 16));
    return str;
}