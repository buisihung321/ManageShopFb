
$('#spinner').bind('ajaxStart', function () {
    console.log("Ajax starting..");
    $(this).show();
}).bind('ajaxStop', function () {
    console.log("Ajax Stopping..");
    $(this).hide();
});

$("#btnSearch").click(function() {
    let searchBy = $(`select[name="searchBy"]`).val();
    let searchValue = $(`input[name="searchValue"]`).val();
    console.log("SearchBy:" + searchBy);
    if (!searchBy || !searchValue)
        return;


    const searchContainer = $('#search-result-container');
    //Show Loading spinnerr
    let spinner = `<div id="spinner" class="d-flex justify-content-center"
                         style="display: none">
                        <div class="spinner-border" role="status">
                            <span class="sr-only">Loading...</span>
                        </div>
                    </div>`;
    searchContainer.empty().append(spinner);
    $.ajax({
        type: "POST",
        url: "/Product/Search",
        content: "application/json; charset=utf-8",
        data: {
            searchBy: searchBy,
            searchValue: searchValue
        },
        success: (res) => {

            searchContainer.empty().html(res);
            searchContainer.find("#spinner").remove();

            //setTimeout( () =>
            //{
            //    searchContainer.empty().html(res);
            //    searchContainer.find("#spinner").remove();
            //}, 3000);

        }
    });

});

