(function ($) {

    $("[data-adv-search=button]").click(function () {
        $("[data-adv-search=modal],[data-adv-search=content]").addClass("active");
        $("body").addClass("no-scroll");
    });

    $("[data-adv-search=modal]").click(function () {
        $("[data-adv-search=modal],[data-adv-search=content]").removeClass("active");
        $("body").removeClass("no-scroll");
    });


    $("[data-request=button]").click(function () {
        $("[data-request=modal],[data-request=content]").addClass("active");
        $("body").addClass("no-scroll");
    });

    $("[data-request=modal]").click(function () {
        $("[data-request=modal],[data-request=content]").removeClass("active");
        $("body").removeClass("no-scroll");
    });


    // wwwroot/js/site.js
    function showCustomConfirm(title, message) {
        return new Promise((resolve) => {
            const dialog = document.getElementById('customConfirmModal');
            if (!dialog) {
                console.error("Confirm partial view HTML is missing from this page.");
                return resolve(false);
            }

            document.getElementById('confirmTitle').textContent = title;
            document.getElementById('confirmMessage').textContent = message;

            dialog.showModal();

            const yesBtn = document.getElementById('confirmYesBtn');
            const noBtn = document.getElementById('confirmNoBtn');

            const handleYes = () => { cleanup(); resolve(true); };
            const handleNo = () => { cleanup(); resolve(false); };

            function cleanup() {
                yesBtn.removeEventListener('click', handleYes);
                noBtn.removeEventListener('click', handleNo);
                dialog.close();
            }

            yesBtn.addEventListener('click', handleYes);
            noBtn.addEventListener('click', handleNo);
        });
    }

})(jQuery);
