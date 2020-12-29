const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();
let cells = document.querySelectorAll('tbody td');
let i = 0;
let gameId = document.location.pathname.match(/\d+/)[0];


hubConnection.on("Notify", function (message) {
    let elem = document.createElement("p");
    elem.appendChild(document.createTextNode(message));
    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});


hubConnection.on("Receive", function (message,id,userName,counter) {
    document.getElementById(id).innerHTML = message;
    counter++;
    i = counter;
    console.log(i);
    
    if (isVictory(cells)) {
        alert('Победа ' + userName);
        location.href = 'https://localhost:44310/Game/AllGames';
    }
    if (i == 9) {
        alert('Ничья');        
        location.href = 'https://localhost:44310/Game/AllGames';
    }
    

});

function isVictory(cells) {
    let combs = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6],
    ];
    for (let comb of combs) {
        if (
            cells[comb[0]].innerHTML == cells[comb[1]].innerHTML &&
            cells[comb[1]].innerHTML == cells[comb[2]].innerHTML &&
            cells[comb[0]].innerHTML != ''
        ) {
            return true;
        }
    }

    return false;
}

for (let cell of cells) {
    
    cell.addEventListener('click', function step() {
        let id = this.id;
        //console.log(id);
       // i++;
       
        hubConnection.invoke("Send", id,gameId,i);      

    });
}

//function draw(cells) {
    

//    for (let cell of cells) {
//        cell.addEventListener('click', function step() {
//            let id = this.id;
//            //console.log(id);
//            let item = ['X', 'O'][i % 2];
//            this.innerHTML = item;
//            this.removeEventListener('click', step);
//            hubConnection.invoke("Send", item, id);
//            //if (isVictory(cells)) {
//            //    //alert(this.innerHTML);
//            //    console.log(this.innerHTML)
//            //}
//            //else if (i == 8) {
//            //    //alert("Draw");
//            //    console.log('Draw');
//            //}
//            i++;
            
//        });
//    }

//}



hubConnection.start();

    