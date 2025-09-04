document.addEventListener('DOMContentLoaded', function() {
    // Sample job data
    const jobs = [
        {
            id: 1,
            title: "Frontend Developer",
            company: "Tech Solutions Inc.",
            location: "New York, NY",
            salary: "$80,000 - $100,000",
            type: "Full-time",
            description: "We are looking for a skilled Frontend Developer to join our team. You will be responsible for building user interfaces and implementing design systems.",
            posted: "2 days ago",
            active: true
        },
        {
            id: 2,
            title: "UX Designer",
            company: "Creative Minds",
            location: "Remote",
            salary: "$70,000 - $90,000",
            type: "Full-time",
            description: "Join our design team to create beautiful and functional user experiences for our clients across various industries.",
            posted: "1 week ago",
            active: true
        },
        {
            id: 3,
            title: "Backend Engineer",
            company: "Data Systems LLC",
            location: "San Francisco, CA",
            salary: "$110,000 - $140,000",
            type: "Full-time",
            description: "We need an experienced Backend Engineer to develop and maintain our server infrastructure and APIs.",
            posted: "3 days ago",
            active: true
        },
        {
            id: 4,
            title: "Marketing Specialist",
            company: "Growth Marketing Co.",
            location: "Chicago, IL",
            salary: "$60,000 - $75,000",
            type: "Full-time",
            description: "Help us grow our client base through innovative marketing strategies and campaigns.",
            posted: "5 days ago",
            active: true
        }
    ];

    // Display jobs on the homepage
    const featuredJobsContainer = document.getElementById('featuredJobs');
    if (featuredJobsContainer) {
        displayJobs(jobs.slice(0, 4), featuredJobsContainer);
    }

    // Display jobs on the jobs listing page
    const jobListContainer = document.getElementById('jobList');
    if (jobListContainer) {
        displayJobs(jobs, jobListContainer);
        document.getElementById('jobCount').textContent = jobs.length;
    }

    // Job sorting functionality
    const sortSelect = document.getElementById('sortJobs');
    if (sortSelect) {
        sortSelect.addEventListener('change', function() {
            const sortedJobs = [...jobs];
            
            switch(this.value) {
                case 'newest':
                    // Already sorted by newest in our sample data
                    break;
                case 'oldest':
                    sortedJobs.reverse();
                    break;
                case 'salary-high':
                    sortedJobs.sort((a, b) => 
                        parseInt(b.salary.replace(/[^0-9]/g, '')) - parseInt(a.salary.replace(/[^0-9]/g, ''))
                    );
                    break;
                case 'salary-low':
                    sortedJobs.sort((a, b) => 
                        parseInt(a.salary.replace(/[^0-9]/g, '')) - parseInt(b.salary.replace(/[^0-9]/g, ''))
                    );
                    break;
            }
            
            displayJobs(sortedJobs, jobListContainer);
        });
    }

    // Pagination functionality
    const prevBtn = document.querySelector('.btn-prev');
    const nextBtn = document.querySelector('.btn-next');
    const pageNumbers = document.querySelectorAll('.page-numbers span');
    
    if (prevBtn && nextBtn && pageNumbers.length > 0) {
        let currentPage = 1;
        
        pageNumbers.forEach(number => {
            number.addEventListener('click', function() {
                if (this.textContent === '...') return;
                
                currentPage = parseInt(this.textContent);
                updatePagination();
                // In a real app, you would fetch jobs for this page
            });
        });
        
        prevBtn.addEventListener('click', function() {
            if (currentPage > 1) {
                currentPage--;
                updatePagination();
                // In a real app, you would fetch jobs for this page
            }
        });
        
        nextBtn.addEventListener('click', function() {
            if (currentPage < 10) { // Assuming 10 pages total
                currentPage++;
                updatePagination();
                // In a real app, you would fetch jobs for this page
            }
        });
        
        function updatePagination() {
            // Update active page number
            pageNumbers.forEach(number => {
                number.classList.remove('active');
                if (parseInt(number.textContent) === currentPage) {
                    number.classList.add('active');
                }
            });
            
            // Enable/disable prev/next buttons
            prevBtn.disabled = currentPage === 1;
            nextBtn.disabled = currentPage === 10; // Assuming 10 pages total
        }
    }
});

function displayJobs(jobs, container) {
    if (!container) return;
    
    container.innerHTML = '';
    
    jobs.forEach(job => {
        if (!job.active) return;
        
        const jobCard = document.createElement('div');
        jobCard.className = 'job-card';
        jobCard.innerHTML = `
            <div class="job-card-header">
                <div>
                    <h3 class="job-title">${job.title}</h3>
                    <p class="job-company">${job.company}</p>
                </div>
                <div>
                    <span class="job-type"><i class="fas fa-briefcase"></i> ${job.type}</span>
                </div>
            </div>
            <div class="job-details">
                <p class="job-location"><i class="fas fa-map-marker-alt"></i> ${job.location}</p>
                <p class="job-salary"><i class="fas fa-dollar-sign"></i> ${job.salary}</p>
            </div>
            <p class="job-description">${job.description}</p>
            <div class="job-footer">
                <span class="job-posted"><i class="far fa-clock"></i> ${job.posted}</span>
                <a href="detail.html?id=${job.id}" class="btn">View Details</a>
            </div>
        `;
        
        container.appendChild(jobCard);
    });
}