fetchlink = "https://fakestoreapi.com/products";

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
                        <img src="${product.image}" class="card-img-top" alt="${product.title}">
                        <div class="card-body">
                            <h5 class="card-title">${product.title}</h5>
                            <p class="card-text">${product.description}</p>
                            <p class="card-text">$${product.price}</p>
                        </div>
                    </div>
                    <button type="button" class="btn btn-outline-primary">Add to cart</button>
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
                <img src="${product.image}" alt="${product.title}" style="max-width: 100%;">
                Name: ${product.title} <br>
                Price: ${product.price} <br>
                Description: ${product.description} <br>
                Id: ${id} `;
            });
          
            row.appendChild(productCard);
        });
    })
    .catch(error => {
        console.error('Помилка завантаження продуктів:', error);
    });
