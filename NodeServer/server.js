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
        console.log("Ending");
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


/*function Drone(ip, id) {
    var self = this;

    this.client = ar.createClient({'ip' : "192.168.1." + ip});
    this.ip = ip;
    this.id = id;

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
        sendInfo(self.id);
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
}*/
