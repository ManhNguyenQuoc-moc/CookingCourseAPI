async function registerUser() {
    const name = document.getElementById("regName").value;
    const email = document.getElementById("regEmail").value;
    const password = document.getElementById("regPassword").value;
    const confirmPassword = document.getElementById("regConfirmPassword").value;

    const messageEl = document.getElementById("message");

    // Kiểm tra mật khẩu khớp nhau
    if (password !== confirmPassword) {
        messageEl.innerText = "Mật khẩu không khớp!";
        messageEl.style.color = "red";
        return;
    }

    // Kiểm tra email hợp lệ
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailRegex.test(email)) {
        messageEl.innerText = "Email không hợp lệ!";
        messageEl.style.color = "red";
        return;
    }

    const user = {
        name: name,
        email: email,
        password: password
    };

    try {
        const response = await fetch("http://localhost:5043/api/auth/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(user)
        });

        if (response.ok) {
            const data = await response.json();
            messageEl.innerText = data.message || "Đăng ký thành công!";
            messageEl.style.color = "green";
        } else {
            const error = await response.text();
            messageEl.innerText = error || "Đã có lỗi xảy ra. Vui lòng thử lại.";
            messageEl.style.color = "red";
        }
    } catch (error) {
        messageEl.innerText = "Đã có lỗi xảy ra. Vui lòng thử lại.";
        messageEl.style.color = "red";
    }
}

    async function loginUser() {
    const email = document.getElementById("loginEmail").value;
    const password = document.getElementById("loginPassword").value;

    const user = {
        email: email,
    password: password
    };

    try {
        const response = await fetch("http://localhost:5043/api/auth/login", {
        method: "POST",
    headers: {
        "Content-Type": "application/json"
            },
    body: JSON.stringify(user)
        });

    if (response.ok) {
            const data = await response.json();

    // ✅ Hiển thị thông báo
    document.getElementById("message").innerText = "Đăng nhập thành công!";
    document.getElementById("message").style.color = "green";

    // ✅ Hiển thị console
    console.log("Login success:", data);

    // ✅ Đóng modal đăng nhập
    const loginModalEl = document.getElementById('loginModal');
    const modalInstance = bootstrap.Modal.getInstance(loginModalEl);
    modalInstance.hide();

        } else {
            const error = await response.text();
    document.getElementById("message").innerText = error;
    document.getElementById("message").style.color = "red";
        }
    } catch (error) {
        console.error(error);
    document.getElementById("message").innerText = "Đã có lỗi xảy ra. Vui lòng thử lại.";
    document.getElementById("message").style.color = "red";
    }
}

