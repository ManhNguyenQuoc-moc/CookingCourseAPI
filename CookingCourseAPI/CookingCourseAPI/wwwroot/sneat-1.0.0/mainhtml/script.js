const API_BASE_URL = 'http://localhost:5043/api';

// ========== EVENT LISTENERS ==========
document.addEventListener("DOMContentLoaded", () => {
    loadCourses();
    handleCourseEnrollButtons();
    const token = sessionStorage.getItem('token');
    if (token) {
        loginSuccess(token);
    } else {
        document.getElementById('loginBtn')?.classList.remove('d-none');
        document.getElementById('userDropdown')?.classList.add('d-none');
    }

    const user = JSON.parse(sessionStorage.getItem('user') || '{}');
    const payload = token ? JSON.parse(atob(token.split('.')[1])) : {};
    const role = payload.role || payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    console.log("Token:", token);
    console.log("User:", user);
    console.log("Payload:", payload);
    console.log("Role:", role);

    document.querySelectorAll('.menu-item > a').forEach(link => {
        link.addEventListener('click', function () {
            document.querySelectorAll('.menu-item').forEach(item => item.classList.remove('active'));
            this.parentElement.classList.add('active');
        });
    });

    setupAuthForms();
    setupProfile();
    setupImagePreview();
    setupLogout();
    setupCreatePostModal();
});

document.getElementById('loginBtn')?.addEventListener('click', () => {
    window.location.href = 'auth-login-basic.html';
});

// ========== SETUP FUNCTIONS ==========
function setupAuthForms() {
    document.getElementById('formAuthentication')?.addEventListener('submit', (e) => {
        e.preventDefault();
        loginUser();
    });
    document.getElementById('formAuthenticationreg')?.addEventListener('submit', (e) => {
        e.preventDefault();
        registerUser();
    });
}

function setupLogout() {
    document.getElementById('logoutBtn')?.addEventListener('click', handleLogout);
}

async function setupProfile() {
    const token = sessionStorage.getItem('token');
    if (!token) return;

    try {
        const res = await fetch(`${API_BASE_URL}/Users/profile`, {
            method: 'GET',
            headers: { 'Authorization': `Bearer ${token}` }
        });

        if (!res.ok) throw new Error("Failed to fetch user profile");

        const user = await res.json();
        sessionStorage.setItem('user', JSON.stringify(user));

        document.getElementById('nameuser').textContent = user.name;
        const payload = JSON.parse(atob(token.split('.')[1]));
        const role = payload.role || payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        document.getElementById('roleuser').textContent = role;
    } catch (err) {
        console.error('Error fetching user profile:', err);
    }
}

function setupCreatePostModal() {
    document.getElementById('createPostBtn')?.addEventListener('click', async () => {
        const token = sessionStorage.getItem('token');
        if (!token) return $('#loginPromptModal').modal('show');

        $('#postModal').modal('show');

        const user = JSON.parse(sessionStorage.getItem('user') || '{}');
        document.getElementById('avatar-user').src = user.avatarUrl || "/img/default-avatar.png";
        document.getElementById('name-user').innerText = user.name || "Người dùng";
        document.getElementById('displayUsername').innerText = `@${user.username || "unknown"}`;

        await loadTopicsToSelect();

        document.getElementById("submitPostBtn").onclick = async () => {
            await createPost();
        };
    });
}

function setupImagePreview() {
    document.getElementById('postImage')?.addEventListener('change', (event) => {
        const file = event.target.files[0];
        const preview = document.getElementById('previewImage');

        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                preview.style.display = 'block';
                preview.src = e.target.result;
            };
            reader.readAsDataURL(file);
        } else {
            preview.style.display = 'none';
        }
    });
}

// ========== AUTH FUNCTIONS ==========
async function registerUser() {
    const name = document.getElementById('regName').value.trim();
    const email = document.getElementById('regEmail').value.trim();
    const password = document.getElementById('regPassword').value;
    const confirmPassword = document.getElementById('regConfirmPassword').value;
    const messageEl = document.getElementById('message');

    if (!messageEl) {
        alert('Không tìm thấy phần hiển thị thông báo.');
        return;
    }

    if (password !== confirmPassword) {
        return showMessage(messageEl, 'Mật khẩu không khớp!', 'red');
    }

    const emailRegex = /^[\w.-]+@[a-zA-Z\d.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailRegex.test(email)) {
        return showMessage(messageEl, 'Email không hợp lệ!', 'red');
    }

    try {
        const response = await fetch(`${API_BASE_URL}/auth/register`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, email, password }),
        });

        const data = await response.json();
        if (response.ok) {
            showMessage(messageEl, data.message || 'Đăng ký thành công!', 'green');
            setTimeout(() => window.location.href = '/sneat-1.0.0/mainhtml/admin.html', 1000);
        } else {
            showMessage(messageEl, data.message || 'Đăng ký thất bại.', 'red');
        }
    } catch (err) {
        console.error('Error during registration:', err);
        showMessage(messageEl, 'Đã có lỗi xảy ra.', 'red');
    }
}

async function loginUser() {
    const email = document.getElementById('loginEmail').value.trim();
    const password = document.getElementById('loginPassword').value;
    const messageEl = document.getElementById('message');

    try {
        const response = await fetch(`${API_BASE_URL}/auth/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password }),
        });

        const data = await response.json();
        if (response.ok) {
            const token = data.token;
            sessionStorage.setItem('token', token);

            const payload = JSON.parse(atob(token.split('.')[1]));
            const role = payload.role;

            console.log('Login - Token Role:', role);

            await loginSuccess(token);

            showMessage(messageEl, data.message || 'Đăng nhập thành công', 'green');

            if (role === 'Admin') {
                setTimeout(() => window.location.href = '/sneat-1.0.0/mainhtml/admin.html', 1000);
            } else {
                setTimeout(() => window.location.href = '/sneat-1.0.0/mainhtml/index.html', 1000);
            }
        } else {
            showMessage(messageEl, data.message || 'Đăng nhập thất bại.', 'red');
        }
    } catch (err) {
        console.error('Login error:', err);
        showMessage(messageEl, 'Đã có lỗi xảy ra.', 'red');
    }
}

async function loginSuccess(token) {
    try {
        const res = await fetch(`${API_BASE_URL}/Users/profile`, {
            method: 'GET',
            headers: { 'Authorization': `Bearer ${token}` }
        });
        if (!res.ok) throw new Error('Không thể lấy thông tin người dùng');
        const user = await res.json();
        sessionStorage.setItem('user', JSON.stringify(user));
        sessionStorage.setItem('userId', user.id);
        document.getElementById('loginBtn')?.classList.add('d-none');
        document.getElementById('avataruser1')?.setAttribute('src', user.avatarUrl);
        document.getElementById('useravatar')?.setAttribute('src', user.avatarUrl);
        document.getElementById("nameuser").innerText = user.name;
        document.getElementById('userDropdown')?.classList.remove('d-none');
        alert('Chào mừng bạn đã đến với khóa học của chúng tôi');
    } catch (err) {
        console.error('Login success error:', err);
        alert('Không thể lấy thông tin người. Vui lòng thử lại!');
    }
}

function handleLogout() {
    sessionStorage.clear();
    document.getElementById('loginBtn')?.classList.remove('d-none');
    document.getElementById('userDropdown')?.classList.add('d-none');
    window.location.href = '/sneat-1.0.0/mainhtml/index.html';
}

// ========== UTILS ==========
function showMessage(el, message, color) {
    el.innerText = message;
    el.style.color = color;
}

// ========== COURSE FUNCTIONS ==========
function loadCourses() {
    fetch(`${API_BASE_URL}/Courses`)
        .then(res => res.json())
        .then(res => {
            if (res.success && res.data && res.data.$values) {
                renderCourses(res.data.$values);
            } else {
                console.error("Không tìm thấy dữ liệu khóa học.");
            }
        })
        .catch(error => console.error("Lỗi khi gọi API:", error));
}

function renderCourses(coursesFromApi) {
    const paidCoursesContainer = document.getElementById("paid-courses");
    const freeCoursesContainer = document.getElementById("free-courses");
    if (!paidCoursesContainer || !freeCoursesContainer) return;

    paidCoursesContainer.innerHTML = "";
    freeCoursesContainer.innerHTML = "";

    const token = sessionStorage.getItem('token');
    const payload = token ? JSON.parse(atob(token.split('.')[1])) : {};
    const role = payload.role || payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    coursesFromApi.forEach(course => {
        const isPaid = !course.isFree;
        const isAdmin = role === 'Admin';

        const buttons = isAdmin
            ? `
                <button class="btn btn-warning btn-sm me-1 btn-edit-course" data-id="${course.id}">Sửa</button>
                <button class="btn btn-danger btn-sm btn-delete-course" data-id="${course.id}">Xóa</button>
              `
            : `
                <button class="btn btn-${isPaid ? 'primary' : 'secondary'} btn-enroll" 
                    data-course-id="${course.id}" 
                    data-course-name="${course.name}" 
                    data-course-price="${course.price}">
                    ${isPaid ? 'Đăng ký' : 'Học miễn phí'}
                </button>
              `;

        const courseCard = `
            <div class="col-md-6 col-xl-4 mb-4">
                <div class="card shadow-sm border-0 overflow-hidden">
                    <div class="position-relative" style="height: 200px; overflow: hidden;">
                        <img src="${course.imageUrl || 'https://chuphinhmenu.com/wp-content/uploads/2018/03/chup-hinh-mon-an-menu-nha-trang-khanh-hoa-0008.jpg'}" 
                            alt="${course.name}" 
                            class="w-100 h-100" 
                            style="object-fit: cover;">
                        <div class="position-absolute top-0 end-0 m-2">
                            <span class="badge ${isPaid ? 'bg-danger' : 'bg-success'}">${isPaid ? 'Paid' : 'Free'}</span>
                        </div>
                    </div>
                    <div class="card-body bg-white text-center">
                        <h5 class="card-title text-dark fw-bold mb-2">${course.name}</h5>
                        <p class="card-text text-muted small mb-2">${course.description}</p>
                        ${isPaid ? `<p class="card-price fw-semibold text-danger mb-3">Giá: ${course.price.toLocaleString()} VNĐ</p>` : ''}
                        ${buttons}
                    </div>
                </div>
            </div>
        `;

        if (isPaid) {
            paidCoursesContainer.innerHTML += courseCard;
        } else {
            freeCoursesContainer.innerHTML += courseCard;
        }
    });

    if (!role || role !== 'Admin') {
        handleCourseEnrollButtons();
    }

    document.querySelectorAll('.btn-edit-course').forEach(button => {
        button.addEventListener('click', (e) => handleEditCourse(e.target.dataset.id));
    });

    document.querySelectorAll('.btn-delete-course').forEach(button => {
        button.addEventListener('click', (e) => handleDeleteCourse(e.target.dataset.id));
    });
}

function handleCourseEnrollButtons() {
    const enrollButtons = document.querySelectorAll('.btn-enroll');
    enrollButtons.forEach(button => {
        button.removeEventListener('click', handleEnrollClick);
        button.addEventListener('click', handleEnrollClick);
    });
}

async function handleEnrollClick(e) {
    const courseId = e.target.getAttribute('data-course-id');
    const courseName = e.target.getAttribute('data-course-name');
    const coursePrice = parseFloat(e.target.getAttribute('data-course-price'));
    const token = sessionStorage.getItem('token');

    if (!token) {
        $('#loginPromptModal').modal('show');
        sessionStorage.setItem('pendingCourseId', courseId);
        return;
    }

    const userId = sessionStorage.getItem('userId');

    try {
        const checkResponse = await fetch(`${API_BASE_URL}/Courses/check-enrollment?userId=${userId}&courseId=${courseId}`, {
            method: 'GET',
            headers: {
                'Accept': '*/*',
                'Authorization': `Bearer ${token}`
            }
        });

        const checkData = await checkResponse.json();

        if (checkData.isEnrolled) {
            window.location.href = `learning.html?courseId=${courseId}`;
            return;
        }

        if (coursePrice > 0) {
            showPaymentConfirmation(courseId, courseName, coursePrice);
        } else {
            await enrollInCourse(courseId, courseName);
        }
    } catch (err) {
        console.error('Error checking enrollment:', err);
        alert('Đã có lỗi xảy ra khi kiểm tra trạng thái đăng ký');
    }
}

function showPaymentConfirmation(courseId, courseName, coursePrice) {
    let paymentModal = document.getElementById('paymentConfirmationModal');
    if (!paymentModal) {
        paymentModal = document.createElement('div');
        paymentModal.id = 'paymentConfirmationModal';
        paymentModal.className = 'modal fade';
        paymentModal.innerHTML = `
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Xác nhận thanh toán</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>Bạn đang đăng ký khóa học: <strong id="confirmCourseName"></strong></p>
                        <p>Giá: <strong id="confirmCoursePrice"></strong> VND</p>
                        <p>Bạn có chắc chắn muốn tiếp tục thanh toán?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Hủy</button>
                        <button type="button" class="btn btn-primary" id="confirmPaymentBtn">Xác nhận</button>
                    </div>
                </div>
            </div>`;
        document.body.appendChild(paymentModal);
    }

    document.getElementById('confirmCourseName').innerText = courseName;
    document.getElementById('confirmCoursePrice').innerText = coursePrice.toLocaleString('vi-VN');

    const modalInstance = new bootstrap.Modal(document.getElementById('paymentConfirmationModal'));
    modalInstance.show();

    document.getElementById('confirmPaymentBtn').onclick = () => {
        modalInstance.hide();
        window.location.href = `/sneat-1.0.0/mainhtml/payment.html?courseId=${courseId}`;
    };
}

async function enrollInCourse(courseId, courseName) {
    const token = sessionStorage.getItem('token');
    const userId = sessionStorage.getItem('userId');

    try {
        const response = await fetch(`${API_BASE_URL}/Courses/enroll`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, courseId })
        });

        const data = await response.json();
        if (response.ok) {
            alert(`Đã đăng ký khóa học ${courseName}`);
            window.location.href = `learning.html?courseId=${courseId}`;
        } else {
            alert(`Không thể đăng ký khóa học: ${data.message}`);
        }
    } catch (err) {
        console.error("Lỗi khi đăng ký khóa học:", err);
        alert("Đã có lỗi xảy ra khi đăng ký khóa học");
    }
}

// Hiển thị modal tạo khóa học
document.getElementById('add-course-btn')?.addEventListener('click', () => {
    const modal = new bootstrap.Modal(document.getElementById('addCourseModal'));
    modal.show();
});

// ================== VIDEO FORM CREATION ==================
// Hàm tạo form video, hỗ trợ điền dữ liệu từ API
// Hàm tạo form video, hỗ trợ điền dữ liệu từ API
function createVideoForm(index, video = {}) {
    const recipe = video.recipe || {};
    return `
        <div class="border p-3 mb-3 video-item">
            <h6>Video ${index + 1}</h6>
            <div class="mb-2">
                <label>Tiêu đề video</label>
                <input type="text" class="form-control" name="title" value="${video.title || ''}" required>
            </div>
            <div class="mb-2">
                <label>URL</label>
                <input type="text" class="form-control" name="url" value="${video.url || ''}" required>
            </div>
            <div class="mb-2">
                <label>Tên công thức</label>
                <input type="text" class="form-control" name="recipeTitle" value="${recipe.title || ''}" required>
            </div>
            <div class="mb-2">
                <label>Nguyên liệu</label>
                <textarea class="form-control" name="ingredients" rows="2" required>${recipe.ingredients || ''}</textarea>
            </div>
            <div class="mb-2">
                <label>Hướng dẫn</label>
                <textarea class="form-control" name="instructions" rows="2" required>${recipe.instructions || ''}</textarea>
            </div>
            <div class="mb-2">
                <label>Mẹo nấu</label>
                <textarea class="form-control" name="cookingTips" rows="2">${recipe.cookingTips || ''}</textarea>
            </div>
            <button type="button" class="btn btn-danger btn-sm remove-video-btn">Xóa video</button>
        </div>`;
}
// ================== THÊM KHÓA HỌC ==================
document.getElementById('add-video-btn')?.addEventListener('click', () => {
    const container = document.getElementById('videos-container');
    const index = container.children.length;
    container.insertAdjacentHTML('beforeend', createVideoForm(index));
});

document.getElementById('videos-container')?.addEventListener('click', (e) => {
    if (e.target.classList.contains('remove-video-btn')) {
        e.target.closest('.video-item').remove();
    }
});

document.getElementById('addCourseForm')?.addEventListener('submit', async (e) => {
    e.preventDefault();
    const form = e.target;

    const formData = new FormData();
    formData.append('Name', form.name.value);
    formData.append('Description', form.description.value);
    formData.append('Price', parseFloat(form.price.value) || 0);
    formData.append('IsFree', form.isFree.checked ? 'true' : 'false');

    document.querySelectorAll('#videos-container .video-item').forEach((videoDiv, index) => {
        formData.append(`Videos[${index}].Title`, videoDiv.querySelector('input[name="title"]').value);
        formData.append(`Videos[${index}].Url`, videoDiv.querySelector('input[name="url"]').value);
        formData.append(`Videos[${index}].Recipe.Title`, videoDiv.querySelector('input[name="recipeTitle"]').value);
        formData.append(`Videos[${index}].Recipe.Instructions`, videoDiv.querySelector('textarea[name="instructions"]').value);
        formData.append(`Videos[${index}].Recipe.Ingredients`, videoDiv.querySelector('textarea[name="ingredients"]').value);
        formData.append(`Videos[${index}].Recipe.CookingTips`, videoDiv.querySelector('textarea[name="cookingTips"]').value || '');
    });

    if (form.image.files.length > 0) {
        formData.append('Image', form.image.files[0]);
    }

    try {
        const res = await fetch(`${API_BASE_URL}/Courses`, {
            method: 'POST',
            body: formData
        });

        const data = await res.json();

        if (res.ok) {
            alert('Tạo khóa học thành công!');
            form.reset();
            document.getElementById('videos-container').innerHTML = '';
            bootstrap.Modal.getInstance(document.getElementById('addCourseModal')).hide();
            loadCourses();
        } else {
            console.log('API lỗi:', data);
            alert(`Thất bại: ${data.message || 'Không xác định'}`);
        }
    } catch (err) {
        console.error('Lỗi tạo khóa học:', err);
        alert('Lỗi khi gọi API');
    }
});


// Xử lý chỉnh sửa khóa học
async function handleEditCourse(courseId) {
    const token = sessionStorage.getItem('token');
    if (!token) {
        alert('Vui lòng đăng nhập để chỉnh sửa khóa học.');
        return;
    }

    try {
        const modalElement = document.getElementById('editCourseModal');
        const form = document.getElementById('editCourseForm');
        const videosContainer = document.getElementById('editVideosContainer');
        const imagePreview = document.getElementById('editCourseImagePreview');

        if (!modalElement || !form || !videosContainer || !imagePreview) {
            alert('Lỗi giao diện: Không tìm thấy các phần tử cần thiết.');
            return;
        }

        const response = await fetch(`${API_BASE_URL}/Courses/${courseId}`, {
            headers: { 'Authorization': `Bearer ${token}` }
        });

        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Không thể tải thông tin khóa học.');
        }

        const course = await response.json();
        const data = course.data;

        // Hiển thị modal
        const editCourseModal = new bootstrap.Modal(modalElement);
        editCourseModal.show();

        // Đổ dữ liệu vào form
        form.querySelector('input[name="courseId"]').value = data.id || '';
        form.querySelector('input[name="name"]').value = data.name || '';
        form.querySelector('textarea[name="description"]').value = data.description || '';
        form.querySelector('input[name="price"]').value = data.price || 0;
        form.querySelector('input[name="isFree"]').checked = data.isFree || false;

        const imageInput = form.querySelector('input[name="image"]');
        if (data.imageUrl) {
            imagePreview.src = data.imageUrl;
            imagePreview.style.display = 'block';
        } else {
            imagePreview.src = '';
            imagePreview.style.display = 'none';
        }

        imageInput.addEventListener('change', (e) => {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    imagePreview.src = e.target.result;
                    imagePreview.style.display = 'block';
                };
                reader.readAsDataURL(file);
            } else {
                imagePreview.style.display = 'none';
            }
        }, { once: true });

        // Render lại danh sách video
        videosContainer.innerHTML = '';
        const videos = data.videos?.$values || [];
        videos.forEach((video, index) => {
            videosContainer.insertAdjacentHTML('beforeend', createVideoForm(index, video));
        });

        // Handle submit
        form.onsubmit = async (e) => {
            e.preventDefault();

            const videoItems = document.querySelectorAll('#editVideosContainer .video-item');
            if (videoItems.length === 0) {
                alert('Vui lòng thêm ít nhất một video.');
                return;
            }

            const formData = new FormData();
            formData.append('Name', form.querySelector('input[name="name"]').value || '');
            formData.append('Description', form.querySelector('textarea[name="description"]').value || '');
            formData.append('Price', parseFloat(form.querySelector('input[name="price"]').value) || 0);
            formData.append('IsFree', form.querySelector('input[name="isFree"]').checked);

            videoItems.forEach((videoDiv, index) => {
                formData.append(`Videos[${index}].Title`, videoDiv.querySelector('input[name="title"]').value);
                formData.append(`Videos[${index}].Url`, videoDiv.querySelector('input[name="url"]').value);
                formData.append(`Videos[${index}].Recipe.Title`, videoDiv.querySelector('input[name="recipeTitle"]').value);
                formData.append(`Videos[${index}].Recipe.Instructions`, videoDiv.querySelector('textarea[name="instructions"]').value);
                formData.append(`Videos[${index}].Recipe.Ingredients`, videoDiv.querySelector('textarea[name="ingredients"]').value);
                formData.append(`Videos[${index}].Recipe.CookingTips`, videoDiv.querySelector('textarea[name="cookingTips"]').value || '');
            });

            if (imageInput.files.length > 0) {
                formData.append('Image', imageInput.files[0]);
            }

            try {
                const res = await fetch(`${API_BASE_URL}/Courses/update/${courseId}`, {
                    method: 'PUT',
                    body: formData
                });

                if (res.ok) {
                    alert('Cập nhật khóa học thành công!');
                    editCourseModal.hide();
                    loadCourses();
                } else {
                    const error = await res.json();
                    console.error('Lỗi từ API:', error);
                    alert(`Không thể cập nhật khóa học: ${error.message || 'Lỗi không xác định'}`);
                }
            } catch (err) {
                console.error('Lỗi khi cập nhật khóa học:', err);
                alert('Đã có lỗi xảy ra khi cập nhật khóa học.');
            }
        };
    } catch (err) {
        console.error('Lỗi khi tải thông tin khóa học:', err);
        alert('Đã có lỗi xảy ra khi tải thông tin khóa học.');
    }
}


// Thêm video cho form chỉnh sửa
document.getElementById('editAddVideoBtn')?.addEventListener('click', () => {
    const container = document.getElementById('editVideosContainer');
    if (!container) {
        console.error('Không tìm thấy #editVideosContainer');
        return;
    }
    const index = container.children.length;
    container.insertAdjacentHTML('beforeend', createVideoForm(index));
});

// Xóa video trong form chỉnh sửa
document.getElementById('editVideosContainer')?.addEventListener('click', (e) => {
    if (e.target.classList.contains('remove-video-btn')) {
        e.target.closest('.video-item').remove();
    }
});
// ================== XÓA KHÓA HỌC ==================
async function handleDeleteCourse(courseId) {
    if (!confirm('Bạn có chắc chắn muốn xóa khóa học này?')) return;

    const token = sessionStorage.getItem('token');
    try {
        const res = await fetch(`${API_BASE_URL}/Courses/${courseId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (res.ok) {
            alert('Xóa khóa học thành công!');
            loadCourses();
        } else {
            const error = await res.json();
            alert(`Không thể xóa khóa học: ${error.message}`);
        }
    } catch (err) {
        console.error('Lỗi khi xóa khóa học:', err);
        alert('Đã có lỗi xảy ra khi xóa khóa học.');
    }
}

// ================== THÊM VIDEO CHO FORM CHỈNH SỬA ==================
document.getElementById('editAddVideoBtn')?.addEventListener('click', () => {
    const container = document.getElementById('editVideosContainer');
    const index = container.children.length;
    container.insertAdjacentHTML('beforeend', createVideoForm(index));
});

document.getElementById('editVideosContainer')?.addEventListener('click', (e) => {
    if (e.target.classList.contains('remove-video-btn')) {
        e.target.closest('.video-item').remove();
    }
});