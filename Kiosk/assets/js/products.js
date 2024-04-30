const fetchlink = "http://localhost:8085/products";

fetch(fetchlink)
    .then(res => res.json())
    .then(products => {
        const productList = document.getElementById('product-list');
        let row; 
        var column = 3;

        products.forEach((product, index) => {
         
            if (index % column === 0) {
                row = document.createElement('div');
                row.classList.add('row');
                productList.appendChild(row);
            }

        
            const productCard = document.createElement('div');
            productCard.classList.add('col');
            productCard.innerHTML = `
                <div class="card product" style="width: 18rem;" data-id="${product.id}">
                    <div class="card-pre-body">
                        <img src="${product.photoUrl}" class="card-img-top" alt="${product.title}">
                        <div class="card-body">
                            <h5 class="card-title">${product.title}</h5>
                            <p class="card-text">${product.description}</p>
                            <p class="card-text">$${product.price}</p>
                        </div>
                    </div>
                    <button data-id="${product.id}" type="button" class="add-to-cart-button btn btn-outline-primary">Add to cart</button>
                </div>
            `;

            const cardPreBody = productCard.querySelector('.card-pre-body');
            cardPreBody.addEventListener('click', function(event) {
                var id = product.id;
                cardPreBody.disabled = true;
                var myModal = new bootstrap.Modal(document.getElementById('full_description_modal'));
                
                myModal.show();
                var textinside = document.getElementById('full-card-text');
                textinside.innerHTML = `
                <img src="${product.photoUrl}" alt="${product.title}" style="max-width: 100%;">
                Name: ${product.title} <br>
                Price: ${product.price} <br>
                Description: ${product.description} <br>
                Id: ${id} `;
            });
            const addToCartButton = productCard.querySelector('.add-to-cart-button');
            addToCartButton.addEventListener('click', function(event) {
                var idButton = this.parentNode.getAttribute("data-id");
                console.log('button clicked', idButton);
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

            row.appendChild(productCard);
        });
    })
    .catch(error => {
        console.error('Помилка завантаження продуктів:', error);
    });
