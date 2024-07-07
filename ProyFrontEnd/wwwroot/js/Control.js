var myModal;
function ShowMessageModal(nombrecontrol) {
    myModal = new bootstrap.Modal(document.getElementById(nombrecontrol), {
        keyboard: false
    })
    myModal.show();
}
function Reload(){
    location.reload();
}