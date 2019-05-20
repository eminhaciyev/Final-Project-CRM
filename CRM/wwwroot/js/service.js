$(document).ready(function () {
    $(".your-info").click(function () {
        //ashagidaki kodun sonuna fadeOut() metodu qoyacaqsan..
        $(".your-info").prev(".form_info").fadeOut()
        //---

        var form = $(this).prev(".form_info");
        form.fadeIn("fast");

        $(".form_modal_inner").append($(form));
    })

    // preloader
    $('.preloader').delay(400).fadeOut(500);
    //-------  
    
    $(function () {
        // Scroll 50 den boyuk olduqda "Top Scroll" buttonu "display: block" olacaq
        window.onscroll = function () { scrollFunction() };

        function scrollFunction() {
            if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
                document.getElementById("up_scroll").style.display = "block";
            } else {
                document.getElementById("up_scroll").style.display = "none";
            }
        }

        // Scroll "50" den kicik olduqda "Top Scroll" buttonu "display: none" olacaq ve Scroll "0" olacaq
        $("#up_scroll").on("click", function () {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        })
        //function topFunction() {
            
        //}

        $(window).scroll(function () {
            if ($(this).scrollTop() >= 300) {                
                $(".menu").css(
                    {
                        "position": "fixed",
                        "z-index": "2",
                        "opacity": "0.9"
                    }
                )
                $(".responsive-nav").css({
                    "margin-left": "0px "

                })
                /////
                $("section").click(function () {
                    $(".menu").css({
                        "display": "none"
                    })
                })
                $(window).scroll(function () {
                    $(".menu").css({
                        "display": "block"
                    })
                })
                ////////
            } else if ($(this).scrollTop() <= 200) {
                $(".menu").css({
                    "position": "relative"

                })

                $(".responsive-nav").css({
                    "margin-left": "-15px "
                })
            }
        });
    });
})