function createCartItemElement(item) {
    const itemElement = document.createElement('div');
    itemElement.innerHTML = `

    <img style="width: 70px" src="${item.photoUrl}">
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
        addToCart(item);
        renderCart();
    });

    const decrementButton = itemElement.querySelector('.decrement-button');
    decrementButton.addEventListener('click', function(event) {
        removeFromCart(item);
        renderCart();
    });

    const removeButton = itemElement.querySelector('.remove-button');
    removeButton.addEventListener('click', function(event) {
        removeItemFromCart(item);
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
    displayAlert("success", product.title, "додано в кошик");
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
    const offcanvasBody = document.querySelector('#offcanvasBottom .offcanvas-body');
    offcanvasBody.innerHTML = '';
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    cartItems.forEach(item => {
        const itemElement = createCartItemElement(item);
        offcanvasBody.appendChild(itemElement);
    });

    if (cartItems.length === 0) {
        const emptyCart = document.createElement('div');
        emptyCart.textContent = 'Кошик порожній';
        offcanvasBody.appendChild(emptyCart);
    }

    const clearCartButton = document.getElementById('clear-cart');
    clearCartButton.addEventListener('click', function(event) {
        localStorage.removeItem('cartItems');
        renderCart();
    });

    const checkoutButton = document.createElement('button');
    checkoutButton.textContent = 'Оформити замовлення';
    checkoutButton.classList.add('btn', 'btn-success', 'fixed-bottom', 'mx-auto', 'd-block');
    checkoutButton.addEventListener('click', function(event) {
        const items = JSON.parse(localStorage.getItem('cartItems')) || [];
        const sendRequest = {
            items: items.map(item => ({
                productId: item.id,
                quantity: item.quantity
            }))
        };
        fetch(ordersEndpoint, {
            method: 'POST',
            body: JSON.stringify(sendRequest)
        })
        .then(response => response.json())
        .then(data => {
            console.log('Замовлення виконано');
            localStorage.removeItem('cartItems');
            renderCart();
        })
        .catch(error => {
            console.error('Помилка: ', error);
        });
    });

    offcanvasBody.appendChild(checkoutButton);
}



function displayAlert(type, item, message) {
    let alert = `
        <div class="alert alert-${type} alert-dismissible" id="add-alert" role="alert">
            <div>${item} ${message}</div>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

    let alertContainer = document.getElementById('alert-container');
    if (alertContainer) {
        alertContainer.innerHTML = alert;
    } 
}
