function createCartItemElement(item) {
    const itemElement = document.createElement('div');
    itemElement.innerHTML = `

    <img style="width: 70px" src="${item.image}">
    <button class="remove-button btn btn-close"></button>
    <div>Product Title: ${item.title}</div>
    <div>Price: ${item.price}</div>
    <div>Quantity: ${item.quantity}</div>
    <button class="increment-button btn btn-primary">+</button>
    <button class="decrement-button btn btn-primary">-</button>
    
    <hr>
    `;

    const incrementButton = itemElement.querySelector('.increment-button');
    incrementButton.addEventListener('click', function(event) {
        addToCart({ id: item.id });
    });

    const decrementButton = itemElement.querySelector('.decrement-button');
    decrementButton.addEventListener('click', function(event) {
        removeFromCart({ id: item.id });
    });

    const removeButton = itemElement.querySelector('.remove-button');
    removeButton.addEventListener('click', function(event) {
        removeItemFromCart({ id: item.id });
    });

    return itemElement;
}

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
    renderCart();
}

function removeFromCart(product) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    let existingProductIndex = cartItems.findIndex(item => item.id === product.id);
    if (existingProductIndex !== -1) {
        cartItems[existingProductIndex].quantity -= 1;
        if (cartItems[existingProductIndex].quantity === 0) {
            cartItems.splice(existingProductIndex, 1);
        }
    }

    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    renderCart();
}

function removeItemFromCart(product) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    let existingProductIndex = cartItems.findIndex(item => item.id === product.id);
    if (existingProductIndex !== -1) {
        cartItems.splice(existingProductIndex, 1);
    }

    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    renderCart();
}

function renderCart() {
    const offcanvasElement = document.getElementById('offcanvasBottom');
    const bsOffcanvas = new bootstrap.Offcanvas(offcanvasElement);
    const offcanvasBody = document.querySelector('#offcanvasBottom .offcanvas-body');
    offcanvasBody.innerHTML = '';
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    cartItems.forEach(item => {
        const itemElement = createCartItemElement(item);
        offcanvasBody.appendChild(itemElement);
    });

    const checkoutButton = document.createElement('button');
    checkoutButton.textContent = 'Оформити замовлення';
    checkoutButton.classList.add('btn', 'btn-success', 'fixed-bottom', 'mx-auto', 'd-block');
    checkoutButton.addEventListener('click', function(event) {
      ///
    });

    offcanvasBody.appendChild(checkoutButton);


    if (!offcanvasElement.classList.contains('show')) {
        bsOffcanvas.show();
    }
}
