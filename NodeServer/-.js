net = require('net')
var ar = require('ar-drone');

var socket;
var drones = new Array();

// Create a TCP socket listener
var s = net.Server(function (client) {

    socket = client;

    socket.on('data', function (msg_sent) {
        var msg = msg_sent[0];
        var id = msg_sent[1];
        var ip = msg_sent[2];

        if (id > 0) console.log("drone " + id);

        if (msg == 0) {
            console.log('Connected');
        }
        if (msg == 1) {
            console.log('Drone takeoff');
            sendInfo(id);
            drones[id].client.takeoff();
        }
        if (msg == 2) {
            console.log('Drone land');
            drones[id].client.land();
        }
        if (msg == 3) {
            console.log('Drone stop');
            drones[id].client.stop();
        }
        if (msg == 31) {
            console.log('Drone stop X');
            drones[id].client.left(0);
            drones[id].client.right(0);
        }
        if (msg == 32) {
            console.log('Drone stop Y');
            drones[id].client.front(0);
            drones[id].client.back(0);
        }
        if (msg == 33) {
            console.log('Drone stop Z');
            drones[id].client.up(0);
            drones[id].client.down(0);
        }
        if (msg == 4) {
            console.log('Drone move forward');
            drones[id].client.front(0.5);
        }
        if (msg == 5) {
            console.log('Drone move backward');
            drones[id].client.back(0.5);
        }
        if (msg == 6) {
            console.log('Drone move left');
            drones[id].client.left(0.5);
        }
        if (msg == 7) {
            console.log('Drone move right');
            drones[id].client.right(0.5);
        }
        if (msg == 8) {
            console.log('Drone move up');
            drones[id].client.up(0.5);
        }
        if (msg == 9) {
            console.log('Drone move down');
            drones[id].client.down(0.5);
        }
        if (msg == 10) {
            drones[id] = new Drone(ip);
        }
    });

    socket.on('end', function () {
        return;
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

function Drone(ip) {
    var self = this;

    this.client = ar.createClient({'ip' : "192.168.1." + ip});
    this.ip = ip;

    this.parallel = 0;
    this.transverse = 0;
    this.lateral = 0;
    this.vertical = 0;

    this.droneData = new Object();
    this.deltaTime;
    this.prevTime;
    this.curTime = Date.now();

    this.position = {
        x: 0,
        y: 0,
        z: 0
    }

    this.updateData = function(navData) {
        //console.log("Receiving data from ", ip);
        //update drone data
        self.droneData = navData;
        //calculate delta
        self.prevTime = self.curTime;
        self.curTime = Date.now();
        self.deltaTime = (self.curTime - self.prevTime) / 1000;

        //update position
        if (self.droneData.droneState.flying) {
            var yawRadians = self.droneData.demo.rotation.yaw * 180 / Math.PI;
            var localX = self.droneData.demo.xVelocity * self.deltaTime;
            var localY = self.droneData.demo.yVelocity * self.deltaTime;
            self.position.z = self.droneData.demo.altitude;
            self.position.x += Math.cos(yawRadians)*localX + Math.sin(yawRadians)*localY;
            self.position.y += Math.sin(yawRadians)*localX + Math.cos(yawRadians)*localY;
            // self.position.x += localX;
            // self.position.y += localY;
        }
    }
    this.client.on('navdata', this.updateData);

    this.move = function(parallel, transverse, rotational) {
        if (parallel >= 0) {
            self.client.front(parallel);
        } else {
            self.client.back(-parallel);
        }
        if (transverse >= 0) {
            self.client.right(transverse);
        } else {
            self.client.left(-transverse);
        }
        if (rotational >= 0) {
            self.client.clockwise(rotational);
        } else {
            self.client.counterClockwise(-rotational)
        }

        if (parallel == 0 && transverse == 0 && rotational == 0) {
            self.client.stop();
        }
    }
}
