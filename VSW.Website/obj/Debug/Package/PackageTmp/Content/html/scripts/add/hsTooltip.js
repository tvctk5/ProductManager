var text1 = "The tooltip is an easy way of interaction for the visitors in a web page ";
var text2 = "For webhosting, please contact us at support@hiox.com";

//This is the text to be displayed on the tooltip.

if (document.images) {
    pic1 = new Image();
    pic1.src = '../add/img/htooltip/bubble_top.gif';
    pic2 = new Image();
    pic2.src = '/{CPPath}/Content/add/img/htooltip/bubble_middle.gif';
    pic3 = new Image();
    pic3.src = '/{CPPath}/Content/add/img/htooltip/bubble_bottom.gif';
}

function showToolTip(e, text) {
    if (document.all) e = event;
    var obj = document.getElementById('bubble_tooltip');
    var obj2 = document.getElementById('bubble_tooltip_content');
    obj2.innerHTML = text;
    obj.style.display = 'block';
    var st = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
    if (navigator.userAgent.toLowerCase().indexOf('safari') >= 0) st = 0;
    var leftPos = e.clientX - 2;
    if (leftPos < 0) leftPos = 0;
    obj.style.left = leftPos + 'px';
    obj.style.top = e.clientY - obj.offsetHeight + 2 + st + 'px';
}
function hideToolTip() {
    document.getElementById('bubble_tooltip').style.display = 'none';
}