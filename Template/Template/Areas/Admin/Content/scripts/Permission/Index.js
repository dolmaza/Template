$(function () {
    $("#add-new").click(function () {
        PermissionsTree.StartEditNewNode();
        return false;
    });

    $("#collapse-all-nodes")
        .click(function() {
            PermissionsTree.CollapseAll();

            $(this).attr("disabled", "disabled");
            $("#expand-all-nodes").removeAttr("disabled");

        });

    $("#expand-all-nodes")
        .click(function () {
            PermissionsTree.ExpandAll();

            $(this).attr("disabled", "disabled");
            $("#collapse-all-nodes").removeAttr("disabled");
        });
});