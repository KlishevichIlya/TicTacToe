let cells = document.querySelectorAll('tbody td');
console.log(cells);
draw(cells);

function draw(cells) {
    let counter = 0;
    for (let cell of cells) {
        cell.addEventListener('click', function () {
            this.innerHTML = ['X', 'O'][counter % 2];
            counter++;
        });
    }
}









