function changeLanguage(language) {
    $.ajax({
        url: changeLanguageUrl + language,
        type: 'GET',
        dataType: "json",
        success: function (result) {
            window.location = window.location.href;
        }
    });
}


function getAspNetCoreCulture() {

    const name = ".AspNetCore.Culture=";

    const decodedCookie = decodeURIComponent(document.cookie);

    const cookieArray = decodedCookie.split(';');

    let cultureCookieValue = "";

    for (let i = 0; i < cookieArray.length; i++) {

        let cookie = cookieArray[i].trim();

        if (cookie.indexOf(name) === 0) {

            cultureCookieValue = cookie.substring(name.length, cookie.length);

            break;
        }
    }


    if (!cultureCookieValue)
        return null;

    const match = cultureCookieValue.match(/c=([a-zA-Z\-]+)/);

    return match ? match[1] : null;

}


$(function () {

    function getUpdateCurrentLanguageDropdown()
    {

        const currentLang = getAspNetCoreCulture();
        // Outputs: "en-US", "es-ES", etc.

        if (currentLang == "en-US")
        {
            $("#imgNigeriaFlag").hide();
            $("#spnNigeriaFlag").hide();
            $("#imgBanglaFlag").hide();
            $("#imgEnglishFlag").show();
            $("#spnBanglaFlag").hide();
            $("#spnEnglishFlag").show();
        }
        else if (currentLang == "bn-BD")
        {
            $("#imgNigeriaFlag").hide();
            $("#spnNigeriaFlag").hide();
            $("#imgEnglishFlag").hide();
            $("#imgBanglaFlag").show();
            $("#spnEnglishFlag").hide();
            $("#spnBanglaFlag").show();
        }
        else if (currentLang == "ng")
        {
            $("#imgEnglishFlag").hide();
            $("#imgBanglaFlag").hide();
            $("#spnEnglishFlag").hide();
            $("#spnBanglaFlag").hide();
            $("#imgNigeriaFlag").show();
            $("#spnNigeriaFlag").show();
        }
    }


    getUpdateCurrentLanguageDropdown();


    $(document).on("click", "#ancorBanglaLanguage", function () {
        changeLanguage("bn-BD");
    });


    $(document).on("click", "#ancorEnglishLanguage", function () {
        changeLanguage("en-US");
    });

});