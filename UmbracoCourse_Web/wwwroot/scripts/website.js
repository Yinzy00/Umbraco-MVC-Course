done = false;
$(function () {
    $('#doc-history').click(function () {
        var id = $(this).attr('data-doc-id');
        $.get("/umbraco/api/DocumentationHistory/Versions/" + id,
            function (data) {
                console.log(data);
                if (done === false) {
                    $('#doc-history').parent().append("<hr/><ul id=\"doc-history-list\">");
                    $(data).each(function (i, item) {
                        var d = new Date(item.publishDate);
                        $("#doc-history-list")
                            .append("<li><a class=\"revision-history\" data-version='" + item.versionId + "'>" + item.name + "</a> " + "<small> " + d.getUTCDate() + "/" + (d.getUTCMonth() + 1) + " - " + d.getHours() + ":" + addZero(d.getMinutes()) + " - " + d.getFullYear() + "</small></li>")
                    });
                    $('#doc-history').parent().append("</ul>");
                    bindRollbackClickEvents();
                    done = true;
                }
            });
        return false;
    });
});

function bindRollbackClickEvents() {

    $('.revision-history').click(function () {
        alert('revision clicked');
        //get version from data attribute of clicked item
        var versionId = $(this).data("version");
        var contentId = $("#doc-history").attr('data-doc-id');

        // maybe add some kind of are you sure you want to roll back to some kind of older version ?
        $.get("/umbraco/api/DocumentationHistory/PublishVersion/?contentId=" + contentId + "&versionId=" + versionId,
            function (data) {
                // we've triggered the roll back to the previous version
                // we now need to reload the page, the url might have changed 
                window.location.href = data;
            });
    });
}

function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}