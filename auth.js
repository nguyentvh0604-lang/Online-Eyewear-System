function register(e) {
    if (e) e.preventDefault();

    let name = document.getElementById("name").value.trim();
    let user = document.getElementById("user").value.trim();
    let password = document.getElementById("password").value.trim();
    let repassword = document.getElementById("repassword").value.trim();

    if (name === "" || user === "" || password === "" || repassword === "") {
        alert("Vui lòng nhập đầy đủ thông tin!");
        return false;
    }

    if (password !== repassword) {
        alert("Mật khẩu nhập lại không khớp!");
        return false;
    }

    let users = JSON.parse(localStorage.getItem("users")) || [];

    let check = users.find(function(u) {
        return u.user === user;
    });

    if (check) {
        alert("Tên đăng nhập đã tồn tại!");
        return false;
    }

    users.push({
        name: name,
        user: user,
        password: password
    });

    localStorage.setItem("users", JSON.stringify(users));

    alert("Đăng ký thành công!");
    window.location.href = "login.html";

    return false;
}



function login(e) {
    if (e) e.preventDefault();

    let user = document.getElementById("user").value.trim();
    let password = document.getElementById("password").value.trim();

    let users = JSON.parse(localStorage.getItem("users")) || [];

    let foundUser = users.find(function(item) {
        return item.user === user && item.password === password;
    });

    if (foundUser) {
        localStorage.setItem("currentUser", JSON.stringify(foundUser));

        alert("Đăng nhập thành công!");

        window.location.href = "index.html";
    } else {
        alert("Sai tên đăng nhập hoặc mật khẩu!");
    }

    return false;
}



function logout() {
    localStorage.removeItem("currentUser");
    window.location.href = "login.html";
}