
    function addNestedForm(container, counter, ticks, content) {
        var nextIndex = $(container + " " + counter).length;
        var pattern = new RegExp(ticks, "gi");
        content = content.replace(pattern, nextIndex);
        $(container).append(content);
        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form");
    }