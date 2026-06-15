let slideIndex = 0;

function updateCarouselView() {
    const slides = document.querySelectorAll('.slide');
    const dots = document.querySelectorAll('.dot');

    // 1. Reset loop logic if index goes out of bounds
    if (slideIndex >= slides.length) { slideIndex = 0; }
    if (slideIndex < 0) { slideIndex = slides.length - 1; }

    // 2. Remove the 'active' class from all slides and dots
    slides.forEach(slide => slide.classList.remove('active'));
    dots.forEach(dot => dot.classList.remove('active'));

    // 3. Add the 'active' class back to the target index elements
    slides[slideIndex].classList.add('active');
    dots[slideIndex].classList.add('active');
}

// Handler for the Next/Prev buttons
function moveSlide(direction) {
    slideIndex += direction;
    updateCarouselView();
}

// Handler for direct indicator dot selection clicks
function currentSlide(index) {
    slideIndex = index;
    updateCarouselView();
}

// Optional: Add automatic sliding every 5 seconds
setInterval(() => {
    moveSlide(1);
}, 5000);


// To add keyboard arrow key navigation, you need to listen for the native keydown event on the global window object.By checking the e.key property, you can catch whenever a desktop user presses the left or right arrow keys and change slides accordingly.1.The Updated JavaScript CodeAdd this event listener block anywhere in your main JavaScript file.It bridges into your existing moveSlide architecture:

// --- NEW: Keyboard Event Listener for Desktop Accessibility ---
window.addEventListener('keydown', (e) => {
    // 1. Check if the pressed key is the Right Arrow
    if (e.key === 'ArrowRight') {
        moveSlide(1);
    }
    // 2. Check if the pressed key is the Left Arrow
    else if (e.key === 'ArrowLeft') {
        moveSlide(-1);
    }
});

// 2. Complete, Production - Ready Script LifecycleHere is how your entire standalone script looks now, combining indicator loops, mobile touch gestures, and desktop accessibility keyboard listener blocks:

let slideIndex = 0;
let touchStartX = 0;
let touchEndX = 0;
const swipeThreshold = 50;

function updateCarouselView() {
    const slides = document.querySelectorAll('.slide');
    const dots = document.querySelectorAll('.dot');

    if (slideIndex >= slides.length) { slideIndex = 0; }
    if (slideIndex < 0) { slideIndex = slides.length - 1; }

    slides.forEach(slide => slide.classList.remove('active'));
    dots.forEach(dot => dot.classList.remove('active'));

    slides[slideIndex].classList.add('active');
    dots[slideIndex].classList.add('active');
}

function moveSlide(direction) {
    slideIndex += direction;
    updateCarouselView();
}

function currentSlide(index) {
    slideIndex = index;
    updateCarouselView();
}

// Global Event Initialization Setup
document.addEventListener('DOMContentLoaded', () => {
    const container = document.querySelector('.carousel-container');

    // MOBILE SWIPE TRIGGERS
    container.addEventListener('touchstart', (e) => {
        touchStartX = e.changedTouches[0].screenX;
    }, { passive: true });

    container.addEventListener('touchmove', (e) => {
        touchEndX = e.changedTouches[0].screenX;
    }, { passive: true });

    container.addEventListener('touchend', () => {
        handleSwipeGesture();
    }, { passive: true });

    // DESKTOP KEYBOARD ARROW TRIGGER
    window.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowRight') moveSlide(1);
        if (e.key === 'ArrowLeft') moveSlide(-1);
    });
});

function handleSwipeGesture() {
    const distanceX = touchEndX - touchStartX;
    if (Math.abs(distanceX) > swipeThreshold) {
        if (distanceX < 0) moveSlide(1);  // Swipe Left -> Next
        else moveSlide(-1);               // Swipe Right -> Prev
    }
    touchStartX = 0;
    touchEndX = 0;
}

// Key Accessibility ConsiderationsScope Isolation: Listening directly to the global window object means arrow keys will change your carousel slides no matter where the user focuses on the web page.Input Field Conflict Warning: If your page contains form elements(like text inputs, dropdown selectors, or comment textareas), you might want to prevent the carousel from switching slides when users press arrow keys inside those text boxes.You can easily add a safety check:

window.addEventListener('keydown', (e) => {
    // Ignore keys if the user is currently typing in an input or textarea
    if (e.target.tagName === 'INPUT' || e.target.tagName === 'TEXTAREA') {
        return;
    }
    if (e.key === 'ArrowRight') moveSlide(1);
    if (e.key === 'ArrowLeft') moveSlide(-1);
});

/* how both pleaseHere is the complete solution implementing both features: making your carousel fully screen reader(ARIA) compliant and adding a pause - on - hover effect for the automatic sliding interval. 

1. The Accessible HTML Structure(ARIA Compliant)To make a carousel accessible to screen readers, you need to use semantic ARIA properties.This tells assistive technologies that the elements are part of a slider widget and prevents unexpected behavior.

<div class="carousel-container"
    role="region"
    aria-roledescription="carousel"
    aria-label="Product Showcase Gallery">

    <!-- Screen reader announcement zone for slide changes -->
    <div class="sr-only" aria-live="polite" id="carousel-announcer"></div>

    <!-- Image Slides -->
    <div class="carousel-slides">
        <div class="slide active" role="group" aria-roledescription="slide" aria-label="Slide 1 of 3">
            <img src="https://picsum.photos" alt="A beautiful mountain sunrise over a foggy valley">
        </div>
        <div class="slide" role="group" aria-roledescription="slide" aria-label="Slide 2 of 3" aria-hidden="true">
            <img src="https://picsum.photos" alt="A close up shot of green palm tree leaves">
        </div>
        <div class="slide" role="group" aria-roledescription="slide" aria-label="Slide 3 of 3" aria-hidden="true">
            <img src="https://picsum.photos" alt="Abstract glowing geometric shapes in neon blue and purple">
        </div>
    </div>

    <!-- Navigation Buttons with Accessible Labels -->
    <button class="carousel-btn prev-btn" aria-label="Previous Slide" onclick="moveSlide(-1)">&#10094;</button>
    <button class="carousel-btn next-btn" aria-label="Next Slide" onclick="moveSlide(1)">&#10095;</button>

    <!-- Indicator Dots with Interactive Roles -->
    <div class="carousel-dots" role="tablist" aria-label="Slides select controls">
        <button class="dot active" role="tab" aria-selected="true" aria-label="Go to slide 1" onclick="currentSlide(0)"></button>
        <button class="dot" role="tab" aria-selected="false" aria-label="Go to slide 2" onclick="currentSlide(1)"></button>
        <button class="dot" role="tab" aria-selected="false" aria-label="Go to slide 3" onclick="currentSlide(2)"></button>
    </div>
</div> 


(Note: Indicator dots have been changed from passive <span> tags to interactive <button> elements to naturally support keyboard tab focus).

2. Added CSS Accessibility RuleAdd this utility helper class to your CSS file to hide screen reader announcement text from visual desktop screens:

.sr-only {
    position: absolute;
    width: 1px;
    height: 1px;
    padding: 0;
    margin: -1px;
    overflow: hidden;
    clip: rect(0, 0, 0, 0);
    border: 0;
}

3. Complete JavaScript (ARIA updates & Pause-On-Hover)This updated JavaScript handles the interactive visibility properties for screen readers and manages the state of the automatic setInterval timer when a user hovers over the screen.

let slideIndex = 0;
let touchStartX = 0;
let touchEndX = 0;
const swipeThreshold = 50; 

// Timer reference variables
let autoSlideTimer = null;
const slideIntervalDuration = 5000; 

function updateCarouselView() {
    const slides = document.querySelectorAll('.slide');
    const dots = document.querySelectorAll('.dot');
    const announcer = document.getElementById('carousel-announcer');

    if (slideIndex >= slides.length) { slideIndex = 0; }
    if (slideIndex < 0) { slideIndex = slides.length - 1; }

    slides.forEach((slide, idx) => {
        if (idx === slideIndex) {
            slide.classList.add('active');
            slide.removeAttribute('aria-hidden'); // Make visible to screen readers
        } else {
            slide.classList.remove('active');
            slide.setAttribute('aria-hidden', 'true'); // Hide inactive slides
        }
    });

    dots.forEach((dot, idx) => {
        if (idx === slideIndex) {
            dot.classList.add('active');
            dot.setAttribute('aria-selected', 'true');
        } else {
            dot.classList.remove('active');
            dot.setAttribute('aria-selected', 'false');
        }
    });

    // Speak the change politely to non-visual screen readers
    if (announcer) {
        announcer.textContent = `Showing slide ${slideIndex + 1} of ${slides.length}`;
    }
}

function moveSlide(direction) {
    slideIndex += direction;
    updateCarouselView();
}

function currentSlide(index) {
    slideIndex = index;
    updateCarouselView();
}

// --- TIMER AUTOMATION CONTROL METHODS ---
function startAutoSliding() {
    if (autoSlideTimer === null) {
        autoSlideTimer = setInterval(() => {
            moveSlide(1);
        }, slideIntervalDuration);
    }
}

function stopAutoSliding() {
    if (autoSlideTimer !== null) {
        clearInterval(autoSlideTimer);
        autoSlideTimer = null; // Clear the memory layout trace
    }
}

// Global Lifecycle Initializations
document.addEventListener('DOMContentLoaded', () => {
    const container = document.querySelector('.carousel-container');

    // 1. Start the auto slider loop on page load
    startAutoSliding();

    // 2. PAUSE-ON-HOVER & FOCUS LISTENERS
    // Pauses when mouse enters or when a desktop keyboard user tabs into controls
    container.addEventListener('mouseenter', stopAutoSliding);
    container.addEventListener('focusin', stopAutoSliding);
    
    // Resumes when mouse leaves or when keyboard focus leaves the carousel
    container.addEventListener('mouseleave', startAutoSliding);
    container.addEventListener('focusout', startAutoSliding);

    // 3. MOBILE SWIPE INTERACTION TRIGGERS
    container.addEventListener('touchstart', (e) => {
        touchStartX = e.changedTouches.screenX;
    }, { passive: true });

    container.addEventListener('touchmove', (e) => {
        touchEndX = e.changedTouches.screenX;
    }, { passive: true });

    container.addEventListener('touchend', () => {
        const distanceX = touchEndX - touchStartX;
        if (Math.abs(distanceX) > swipeThreshold) {
            if (distanceX < 0) moveSlide(1);
            else moveSlide(-1);
        }
        touchStartX = 0; touchEndX = 0;
    }, { passive: true });

    // 4. GLOBAL KEYBOARD ACCESSIBILITY LISTENERS
    window.addEventListener('keydown', (e) => {
        if (e.target.tagName === 'INPUT' || e.target.tagName === 'TEXTAREA') return;
        if (e.key === 'ArrowRight') moveSlide(1);
        if (e.key === 'ArrowLeft') moveSlide(-1);
    });
});


Key Framework Features Explainedfocusin & focusout Compatibility: Pausing on mouse hover is great for mouse users, but keyboard-only users rely on the tab focus tracking layer. Incorporating focusin ensures that if a user tabs onto a control dot, the timer halts immediately so the slide doesn't change unexpectedly while they try to interact with it.aria-hidden="true" Toggle: Simply setting an item's opacity to 0 using CSS hides it visually, but a screen reader can still read the underlying text. Setting aria-hidden="true" tells screen readers to ignore the inactive slides entirely.


You are very welcome! It is designed to be lightweight, secure, and drop-in ready, so it should integrate into your ASP.NET Core views smoothly.If you run into any compilation bugs, routing mismatches, or issues when tying the front-end JavaScript loops to your C# actions down the road, just paste the error snippet right back here.Have fun building your application! Clear skies ahead with your project. Turn off the computer and take a well-deserved break!

*/

