
$newPwd = document.getElementById('newPwd');
$newPwdConfirm = document.getElementById('newPwdConfirm');
$newPwdConfirm.addEventListener('input', e => {

    var newPwdConfirm = e.target.value;
    if (newPwdConfirm != "") {

        if (newPwdConfirm === $newPwd.value && $newPwdConfirm.value.length >= 5) {
            $btnChangePwd = document.getElementById('btnChangePwd').disabled = false;
        }
        else {
            $btnChangePwd = document.getElementById('btnChangePwd').disabled = true;

        }
    }
});

