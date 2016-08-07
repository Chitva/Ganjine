function AddNodeSetting(s, type, GetIdPrevNode) {
    $('#MyForm').attr('action', "/Admin/Product/AddParentMenu?ParentId=" + s + "&type=" + type + "&Priority=" + GetIdPrevNode);
    $("#MenuInfoDetail").removeClass("DivMenuInfoDetailShow");
}

