function AddNodeSetting(s, type, GetIdPrevNode) {
 
   // if (type == 'add') { $('#ParentMenuName').val(""); };
   
    $('#MyForm').attr('action', "/Admin/Gallery/AddParentMenu?ParentId=" + s + "&type=" + type + "&Priority=" + GetIdPrevNode);
    $("#MenuInfoDetail").removeClass("DivMenuInfoDetailShow");
}

