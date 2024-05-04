const fetchlink = "http://localhost:8085/products";
const ordersEndpoint = "http://localhost:8085/orders";

fetch(fetchlink)
    .then(res => res.json())
    .then(products => {
        const productList = document.getElementById('product-list');
        let row;
        const column = 3;

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
                const id = product.id;
                cardPreBody.disabled = true;
                const myModal = new bootstrap.Modal(document.getElementById('full_description_modal'));

                myModal.show();
                const textinside = document.getElementById('full-card-text');
                textinside.innerHTML = `
                <img src="${product.photoUrl}" alt="${product.title}" style="max-width: 100%;">
                Name: ${product.title} <br>
                Price: ${product.price} <br>
                Description: ${product.description} <br>
                Id: ${id} `;
            });
            const addToCartButton = productCard.querySelector('.add-to-cart-button');
            addToCartButton.addEventListener('click', () => addToCart(product));
            row.appendChild(productCard);
        });

        document.getElementById('open-cart').addEventListener('click', function() {
            const bsOffcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasBottom'));
            bsOffcanvas.show();
            renderCart();
        });
    })
    .catch(error => {
        console.error('Помилка завантаження продуктів:', error);
    });

    