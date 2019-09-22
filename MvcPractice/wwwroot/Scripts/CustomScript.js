function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmSpanDelete = 'confirmDeleteSpan_' + uniqueId;
    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmSpanDelete).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmSpanDelete).hide();
    }
}