
fetch('https://fakestoreapi.com/products')
    .then(res => res.json())
    .then(products => {
        const productList = document.getElementById('product-list');
        let row; 

        
        products.forEach((product, index) => {
         
            if (index % 3 === 0) {
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
           
            row.appendChild(productCard);
        });
    })
    .catch(error => {
        console.error('Помилка завантаження продуктів:', error);
    });
