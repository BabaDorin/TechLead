class TypeWriter {
    constructor(txtElement, words, wait = 3000) {
        this.txtElement = txtElement;
        this.words = words;
        this.txt = '';
        this.wordIndex = 0;
        this.wait = parseInt(wait, 10);
        this.type();
        this.IsDeleting = false;
    }

    type() {
        //Current index of the word
        const current = this.wordIndex % this.words.length;
        //Get full text of current word
        const fullTxt = this.words[current];

        //Check if deleting
        if (this.isDeleting) {
            //remove char   
            this.txt = fullTxt.substring(0, this.txt.length - 1);
        } else {
            //Add char
            this.txt = fullTxt.substring(0, this.txt.length + 1);
        }

        //Insert txt into element
        this.txtElement.innerHTML = `<span class = "txt">${this.txt}</span>`;

        let typespeed = 80;
        if (this.isDeleting) {
            typespeed /= 2;
        }
        //if word is complete
        if (!this.isDeleting && this.txt === fullTxt) {
            //Make a pause at end and set is deleteing to true
            typespeed = this.wait;
            this.isDeleting = true;
        } else if (this.isDeleting && this.txt === '') {
            this.isDeleting = false;
            //Move to the next word
            this.wordIndex++;

            //Pause a little bit before typing again
            typespeed = 80;
        }

        if (current == 4) {
            typespeed = 35;
        }

        setTimeout(() => this.type(), typespeed)
    }


}


document.addEventListener('DOMContentLoaded', init);

//Init App
function init() {
    const txtElement = document.querySelector('.txt-type');
    const word = JSON.parse(txtElement.getAttribute('data-words'));
    const wait = txtElement.getAttribute('data-wait');

    //init typewriter
    new TypeWriter(txtElement, word, wait);
}