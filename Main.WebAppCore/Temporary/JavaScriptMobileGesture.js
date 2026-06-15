// To add mobile touch gestures, you need to listen for three native JavaScript touch events on the carousel container: touchstart, touchmove, and touchend.By calculating the difference between where the user first presses down and where they lift their finger, you can detect if they swiped left or right.Here is the updated JavaScript code that seamlessly integrates swipe gestures into your existing carousel framework.

// 1. Updated JavaScript CodeReplace your previous JavaScript code with this updated version:

let slideIndex = 0;

// Variables to track swipe coordinates
let touchStartX = 0;
let touchEndX = 0;

// Minimum horizontal distance (in pixels) required to trigger a slide change
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

// --- NEW: Touch Gesture Event Listeners ---
document.addEventListener('DOMContentLoaded', () => {
    const container = document.querySelector('.carousel-container');

    // 1. Capture the initial position when the finger touches the screen
    container.addEventListener('touchstart', (e) => {
        touchStartX = e.changedTouches[0].screenX;
    }, { passive: true });

    // 2. Continually update the position as the finger moves
    container.addEventListener('touchmove', (e) => {
        touchEndX = e.changedTouches[0].screenX;
    }, { passive: true });

    // 3. Evaluate the swipe direction when the finger is lifted
    container.addEventListener('touchend', () => {
        handleSwipeGesture();
    }, { passive: true });
});

// --- NEW: Calculate Swipe Direction ---
function handleSwipeGesture() {
    const distanceX = touchEndX - touchStartX;

    // Verify if the swipe distance is long enough to avoid accidental touches
    if (Math.abs(distanceX) > swipeThreshold) {
        if (distanceX < 0) {
            // Swiped Left -> Show Next Slide
            moveSlide(1);
        } else {
            // Swiped Right -> Show Previous Slide
            moveSlide(-1);
        }
    }

    // Reset tracker coordinates for the next interaction
    touchStartX = 0;
    touchEndX = 0;
}

// Optional: Automatic sliding
setInterval(() => { moveSlide(1); }, 5000);





// CSS (Mobile)
// 2. Required CSS OptimizationAdd the following CSS property to your existing.carousel - container class. This prevents the default mobile browser behaviors(like accidental page pulling or refreshing) from interrupting the smooth touch gesture calculation:


// .carousel - container {
//     /* ... your existing carousel-container styles ... */

//     /* Prevents default browser panning/scrolling actions over the carousel area */
//     touch - action: pan - y;
// }


// Key Technical Details{ passive: true }: Passed into the event listeners to tell the browser the code will not call preventDefault().This optimizes scrolling performance and prevents jagged animations on mobile device threads.swipeThreshold: Setting a threshold of 50px prevents your carousel from accidentally switching images when a user simply taps the screen or scrolls vertically down the page.e.changedTouches[0]: Multi - touch screens track multiple inputs simultaneously.Using index[0] ensures your carousel locks directly onto the user's primary interactive finger stream.