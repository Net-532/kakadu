var buttonElements = document.getElementsByClassName("add-to-cart-button");

for (var i = 0; i < buttonElements.length; i++) {
    buttonElements[i].addEventListener('click', function(event) {
        var idButton = this.parentNode.getAttribute("data-id");
        
        const product = {
          id: idButton,
          name: `Product ${idButton}`,
        };

        let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
        
        let existingProductIndex = cartItems.findIndex(item => item.id === idButton);
        
        if (existingProductIndex !== -1) {
            cartItems[existingProductIndex].quantity = (cartItems[existingProductIndex].quantity || 1) + 1;
        } else {
            product.quantity = 1;
            cartItems.push(product);
        }
        
        localStorage.setItem('cartItems', JSON.stringify(cartItems));

        console.log('Product added to cart!');
        
    });
}