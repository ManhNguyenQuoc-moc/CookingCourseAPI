// Ví dụ dữ liệu khóa học (bạn có thể fetch từ API của bạn)
const courses = [
    {
        title: "ReactJS Cơ bản",
        url: "https://www.youtube.com/embed/dGcsHMXbSOA"
    },
    {
        title: "NodeJS & Express",
        url: "https://www.youtube.com/embed/Oe421EPjeBE"
    },
    {
        title: "HTML & CSS từ cơ bản đến nâng cao",
        url: "https://www.youtube.com/embed/1RHDhtbNf1M"
    }
];

// Render ra giao diện
const courseContainer = document.getElementById('courses');
courses.forEach(course => {
    const courseCard = document.createElement('div');
    courseCard.className = 'course-card';
    courseCard.innerHTML = `
    <iframe src="${course.url}" allowfullscreen></iframe>
    <div class="course-content">
      <h3 class="course-title">${course.title}</h3>
    </div>
  `;
    courseContainer.appendChild(courseCard);
});
