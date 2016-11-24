net = require('net')
var ar = require('ar-drone');
var c = ar.createClient();
c.createRepl();

var socket;

// Create a TCP socket listener
var s = net.Server(function (client) {
    
    socket = client;
    
    socket.on('data', function (msg_sent) {
        if (msg_sent == 0) {
            console.log('Conncected');
        }
        if (msg_sent == 1) {
            console.log('Drone takeoff');
            c.takeoff();
        }
        if (msg_sent == 2) {
            console.log('Drone land');
            c.land();
        }
        if (msg_sent == 3) {
            console.log('Drone stop');
            c.stop();
        }
        if (msg_sent == 31) {
            console.log('Drone stop X');
            c.left(0);
            c.right(0);
        }
        if (msg_sent == 32) {
            console.log('Drone stop Y');
            c.front(0);
            c.back(0);
        }
        if (msg_sent == 33) {
            console.log('Drone stop Z');
        }
        if (msg_sent == 4) {
            console.log('Drone move forward');
            c.front(0.1);
        }
        if (msg_sent == 5) {
            console.log('Drone move backward');
            c.back(0.1);
        }
        if (msg_sent == 6) {
            console.log('Drone move left');
            c.left(0.1);
        }
        if (msg_sent == 7) {
            console.log('Drone move right');
            c.right(0.1);
        }
        if (msg_sent == 8) {
            console.log('Drone move up');
        }
        if (msg_sent == 9) {
            console.log('Drone move down');
        }
    });
    
    socket.on('end', function () {
        return;
    });
});

s.listen(8000);
console.log('System waiting at http://localhost:8000');