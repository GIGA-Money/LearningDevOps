// Get form element
const form = document.getElementById('todoForm');

// Load saved checkbox states from localStorage
for (let i = 0; i < form.elements.length; i++) {
    let checkbox = form.elements[i];
    checkbox.checked = localStorage.getItem(checkbox.id) === "true" ? true : false;
    console.log(i + " form element");

    // Add event listener to save checkbox state on change
    checkbox.addEventListener('change', function() {
        localStorage.setItem(this.id, this.checked);
        console.log(this.id, this.checked)
    });
}
