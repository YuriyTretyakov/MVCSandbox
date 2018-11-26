//(function () {
//    var ele = $("#textholder");
//    ele.text("Super Cat!");


//    var main = $("#main");
//    main.on("mouseenter", function () {
//        main.style.background = "#88888d";
//    });

//    main.on("mouseleave", function () {
//        main.style.background = "lightgray"
//    });

//    var menuItems = $("ul.menu li a");
//    menuItems.on("click", function ()
//        {
//        var me = $(this);
//        me.style = this.text.style = this.style.bo
//}
//    ))
//}) ();

var $sidebarAndWrapper = $("#sidebar,#wrapper");

$("#toggle").on("click", function () {
    $icon = $("#toggle>i.fa")
    $sidebarAndWrapper.toggleClass("hide-sidebar")
    if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
        $icon.removeClass("fa-chevron-left");
        $icon.addClass("fa-chevron-right");
    }

    else {
        $icon.removeClass("fa-chevron-right");
        $icon.addClass("fa-chevron-left");
    }
}
)