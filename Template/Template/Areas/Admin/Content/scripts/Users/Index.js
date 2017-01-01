$(function () {
    $("#add-new").click(function () {
        UsersGrid.AddNewRow();
        return false;
    });
});

function OnIsActiveCheckBoxInit(s, e) {
    if (UsersGrid.IsNewRowEditing()) {
        s.SetValue(false);
    }
}