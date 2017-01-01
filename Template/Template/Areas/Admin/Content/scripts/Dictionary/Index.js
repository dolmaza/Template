$(function() {
    $("#add-new").click(function () {
        DictionariesTree.StartEditNewNode();
        return false;
    });

    $("#collapse-all-nodes")
        .click(function () {
            DictionariesTree.CollapseAll();

            $(this).attr("disabled", "disabled");
            $("#expand-all-nodes").removeAttr("disabled");

        });

    $("#expand-all-nodes")
        .click(function () {
            DictionariesTree.ExpandAll();

            $(this).attr("disabled", "disabled");
            $("#collapse-all-nodes").removeAttr("disabled");
        });
});