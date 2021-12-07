const http = require('http')
const web = require('websocket').server;
const server = http.createServer().listen(3000);

const websocket = new web({ httpServer: server });

var clients = new Array();


websocket.on("request", (req) => {

    var userId = req.resourceURL.query.uid;

    const cli = req.accept(null, req.origin);
    cli.userId = userId;

    clients.push(cli);

    console.log(clients.length + ". Client Connected");

    cli.sendUTF("Sunucumuza Hoşgeldiniz Lütfen Kurallara Uymaya Özen Gösteriniz");

    cli.on('close', () => {
        clients.splice(clients.indexOf(cli), 1);
        console.log("Client has left");
    });

    cli.on('message', (msgData) => {
        var message = msgData.utf8Data;
        
        clients.forEach(client => {
            if(client.userId==33){            
                // var data = JSON.parse(message);
                client.sendUTF(message);
            }else{
                client.sendUTF("Lütfen Kullanıcı Adınızı Kontrol Ediniz");
            }
        });

        // websocket.broadcastUTF(JSON.stringify(message));
        
        console.log("Sends To Room : " + message);

    });

});


console.log("Server Starting Port Is 3000")