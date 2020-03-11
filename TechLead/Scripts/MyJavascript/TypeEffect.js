"use strict";

function _instanceof(left, right) { if (right != null && typeof Symbol !== "undefined" && right[Symbol.hasInstance]) { return !!right[Symbol.hasInstance](left); } else { return left instanceof right; } }

function _classCallCheck(instance, Constructor) { if (!_instanceof(instance, Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

var TypeWriter = /*#__PURE__*/function () {
    function TypeWriter(txtElement, words) {
        var wait = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 3000;

        _classCallCheck(this, TypeWriter);

        this.txtElement = txtElement;
        this.words = words;
        this.txt = '';
        this.wordIndex = 0;
        this.wait = parseInt(wait, 10);
        this.type();
        this.IsDeleting = false;
    }

    _createClass(TypeWriter, [{
        key: "type",
        value: function type() {
            var _this = this;

            //Current index of the word
            var current = this.wordIndex % this.words.length; //Get full text of current word

            var fullTxt = this.words[current]; //Check if deleting

            if (this.isDeleting) {
                //remove char   
                this.txt = fullTxt.substring(0, this.txt.length - 1);
            } else {
                //Add char
                this.txt = fullTxt.substring(0, this.txt.length + 1);
            } //Insert txt into element


            this.txtElement.innerHTML = "<span class = \"txt\">".concat(this.txt, "</span>");
            var typespeed = 80;

            if (this.isDeleting) {
                typespeed /= 2;
            } //if word is complete


            if (!this.isDeleting && this.txt === fullTxt) {
                //Make a pause at end and set is deleteing to true
                typespeed = this.wait;
                this.isDeleting = true;
            } else if (this.isDeleting && this.txt === '') {
                this.isDeleting = false; //Move to the next word

                this.wordIndex++; //Pause a little bit before typing again

                typespeed = 80;
            }

            if (current == 4) {
                typespeed = 35;
            }

            setTimeout(function () {
                return _this.type();
            }, typespeed);
        }
    }]);

    return TypeWriter;
}();

document.addEventListener('DOMContentLoaded', init); //Init App

function init() {
    var txtElement = document.querySelector('.txt-type');
    var word = JSON.parse(txtElement.getAttribute('data-words'));
    var wait = txtElement.getAttribute('data-wait'); //init typewriter

    new TypeWriter(txtElement, word, wait);
}