const fetchlink = "http://localhost:8085/products";
const ordersEndpoint = "http://localhost:8085/orders";

fetch(fetchlink)
    .then(res => res.json())
    .then(products => {
        const productList = document.getElementById('product-list');
        let row;
        const column = 2

        products.forEach((product, index) => {

            if (index % column === 0) {
                row = document.createElement('div');
                row.classList.add('row');
                productList.appendChild(row);
            }

            const productCard = document.createElement('div');
            productCard.classList.add('col', 'col-md-6', 'mb-3', 'position-relative');

            const cardContent = `
                <div class="card product shadow">
                    <div class="card-pre-body d-flex align-items-center justify-content-center" style="aspect-ratio: 1;">
                        <img src="${product.photoUrl}" class="card-img-top img-fluid" alt="${product.title}" style="max-width: 80%;"> 
                    </div>
                    <div class="card-body text-center mt-2"> <!-- Додали відступ зверху -->
                        <h5 class="card-title fw-bold">${product.title}</h5> <!-- Зробили текст жирним -->
                    </div>
                    <button class="btn btn-dark btn-price btn-lg position-absolute top-0 end-0">${product.price}</button>
                </div>
            `;

            productCard.innerHTML = cardContent;

            productCard.addEventListener('click', function() {
                const myModal = new bootstrap.Modal(document.getElementById('full_description_modal'));
                const textinside = document.getElementById('full-card-text');
                const id = product.id;

                myModal.show();
                textinside.innerHTML = `
                    <img src="${product.photoUrl}" alt="${product.title}" style="max-width: 100%;">
                    Name: ${product.title} <br>
                    Price: ${product.price} <br>
                    Description: ${product.description} <br>
                    Id: ${id} `;
            });

            row.appendChild(productCard);
        });

        document.getElementById('open-cart').addEventListener('click', function() {
            renderCart();
            const bsOffcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasBottom'));
            bsOffcanvas.show();
        });
    })
    .catch(error => {
        console.error('Error loading products:', error);
    });

    