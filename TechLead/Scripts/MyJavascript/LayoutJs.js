// Hide navbar when user scrolls down and show it when user scrolls up
// class for navbar => .navbar-inverse

console.log("Got into layout.js");
var prevScrollPos = window.pageYOffset;
document.write();
window.onscroll = function () {
    var currentScrollPos = window.pageYOffset;
    if (currentScrollPos < prevScrollPos && currentScrollPos < 400) {
        document.getElementById("nvb").style.top = "0";
    } else if (currentScrollPos>200){
        document.getElementById("nvb").style.top = "-50px";
    }

    prevScrollPos = window.pageYOffset;
}