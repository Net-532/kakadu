function addToCart(product) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    let existingProductIndex = cartItems.findIndex(item => item.id === product.id);
    if (existingProductIndex !== -1) {
        cartItems[existingProductIndex].quantity = (cartItems[existingProductIndex].quantity || 1) + 1;
    } else {
        product.quantity = 1;
        cartItems.push(product);
    }

    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    console.log('Product added to cart!');
    const bsOffcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasBottom'));
    bsOffcanvas.show();
    const offcanvasBody = document.querySelector('#offcanvasBottom .offcanvas-body');
    offcanvasBody.innerHTML = '';
    cartItems.forEach(item => {
        offcanvasBody.innerHTML += `<div>Product Title: ${item.title}</div><div>Price: ${item.price}</div><div>Quantity: ${item.quantity}</div><hr>`;
    });
}

window.onload = function() {
    const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    const offcanvasBody = document.querySelector('#offcanvasBottom .offcanvas-body');
    cartItems.forEach(item => {
        offcanvasBody.innerHTML += `<div>Product Title: ${item.title}</div><div>Price: ${item.price}</div><div>Quantity: ${item.quantity}</div><hr>`;
    });
};
