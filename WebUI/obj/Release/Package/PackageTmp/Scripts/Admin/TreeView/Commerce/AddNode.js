function AddNodeSetting(s, type, GetIdPrevNode) {
 
   // if (type == 'add') { $('#ParentMenuName').val(""); };
   
    $('#MyForm').attr('action',
        "/Admin/Commerce/AddParentMenu?ParentId=" + s + "&type=" + type + "&Priority=" + GetIdPrevNode);
    $("#MenuInfoDetail").removeClass("DivMenuInfoDetailShow");
}

