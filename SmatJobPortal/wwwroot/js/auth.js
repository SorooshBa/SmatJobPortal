document.addEventListener('DOMContentLoaded', function() {
    // Login Form Handling
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const email = document.getElementById('email').value;
            const password = document.getElementById('password').value;
            const remember = document.getElementById('remember').checked;
            
            // Simple validation
            if (!email || !password) {
                alert('Please fill in all fields');
                return;
            }
            
            // Here you would typically send data to the server
            console.log('Login attempt:', { email, password, remember });
            
            // Simulate successful login
            setTimeout(() => {
                window.location.href = '../dashboard/seeker.html'; // Or employer.html based on role
            }, 1000);
        });
    }
    
    // Role Selection
    const roleOptions = document.querySelectorAll('.role-option');
    if (roleOptions.length > 0) {
        roleOptions.forEach(option => {
            option.addEventListener('click', function() {
                roleOptions.forEach(opt => opt.classList.remove('active'));
                this.classList.add('active');
                const input = this.querySelector('input');
                if (input) input.checked = true;
            });
        });
    }
});