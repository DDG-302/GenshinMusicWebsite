// 要操作的元素
const body=document.body;
const eye = document.getElementById('eye');
const eye_confirm = document.getElementById("confirm_eye")
const beam = document.querySelector('.beam1');
const passwordInput = document.getElementById('password');
const passwordInput_confirm = document.getElementById('confirm_password');

// 为body绑定鼠标移动事件
body.addEventListener('mousemove',function(e){
    let rect=beam.getBoundingClientRect();
    let mouseX=rect.right+(rect.width/2);
    let mouseY=rect.top+(rect.height/2);
    let rad=Math.atan2(mouseX-e.pageX,mouseY-e.pageY);
    let deg=(rad*(20/Math.PI)*-1)-350;
    // 设置CSS自定义属性--beam-deg（灯光射线角度）
    body.style.setProperty('--beam1-deg',deg+'deg');
})
console.log(eye)
// 为密码框小眼睛绑定点击事件
eye.addEventListener('click',function(e){
    e.preventDefault();
    body.classList.toggle('show-password');
    passwordInput.type=passwordInput.type==='password'?'text':'password';
    passwordInput_confirm.type = passwordInput_confirm.type === 'password' ? 'text' : 'password';
    eye.className = 'eye fa ' + (passwordInput.type === 'password' ? 'fa-eye-slash' : 'fa-eye');
    eye_confirm.className = 'eye fa ' + (passwordInput_confirm.type === 'password' ? 'fa-eye-slash' : 'fa-eye');
    passwordInput.focus();
})

eye_confirm.addEventListener('click', function (e) {
    e.preventDefault();
    body.classList.toggle('show-password');
    passwordInput.type = passwordInput.type === 'password' ? 'text' : 'password';
    passwordInput_confirm.type = passwordInput_confirm.type === 'password' ? 'text' : 'password';
    eye_confirm.className = 'eye fa ' + (passwordInput_confirm.type === 'password' ? 'fa-eye-slash' : 'fa-eye');
    eye.className = 'eye fa ' + (passwordInput.type === 'password' ? 'fa-eye-slash' : 'fa-eye');
    passwordInput_confirm.focus();
})