const fetchlink = "http://192.168.1.143:8085/products";
const ordersEndpoint = "http://192.168.1.143:8085/orders";
const printReceiptEndpoint='http://192.168.1.143:8085/print';

fetch(fetchlink)
  .then((res) => res.json())
  .then((products) => {
    const productList = document.getElementById("product-list");
    let row;
    const column = 2;

    products.forEach((product, index) => {
      if (index % column === 0) {
        row = document.createElement("div");
        row.classList.add("row");
        productList.appendChild(row);
      }

      const productCard = document.createElement("div");
      productCard.classList.add("col", "col-md-6", "mb-3", "d-flex", "justify-content-center");

      const cardContent = `
        <div class="card product shadow p-1 position-relative">
          <span class="badge bg-dark position-absolute top-0 end-0 product-price p-2">${product.price}</span>
          <div class="card-pre-body d-flex align-items-center justify-content-center ">
            <img src="${product.photoUrl}" alt="${product.title}"> 
          </div>
          <div class="d-flex flex-grow-1 align-items-center justify-content-center">
            <span class="card-title fw-bold text-center">${product.title}</span>
          </div>
        </div>
      `;

      productCard.innerHTML = cardContent;

      const cardProduct = productCard.querySelector(".card.product");
      cardProduct.addEventListener("click", function () {
        const myModal = new bootstrap.Modal(
          document.getElementById("product-description-modal-dialog")   
        );
        const content = document.getElementById("product-description");
        const id = product.id;

        myModal.show();
        content.innerHTML = `
        <div class="image">
            <img class="image" src="${product.photoUrl}" alt="${product.title}"">
        </div>
        <h3 class="title">${product.title}</h3>
        <p class="description">${product.description}</p>
        <p class="product-description-price">${product.price} грн</p>
        <button data-id="${product.id}" class="cart-button">В кошик</button>
          <div class="quantity-controls">
            <button class="quantity-button" id="decrease">-</button>
            <input type="text" id="quantity" value="1" readonly>
            <button class="quantity-button" id="increase">+</button>
          </div>
        `;
      
        document.getElementById("increase").addEventListener("click", increment);
        document.getElementById("decrease").addEventListener("click", decrement);
        
        const addToCartButton = document.querySelector(".cart-button");
        addToCartButton.addEventListener("click", () => { addToCart(product); myModal.hide(); });

        document.getElementById("product-description-modal-dialog").addEventListener("hidden.bs.modal", resetQuantity);
      });
      
      row.appendChild(productCard);
    });

    document.getElementById('cart-clear-button').addEventListener('click', clearCart);
    document.getElementById('cart-button-order').addEventListener('click', checkoutOrder);
    document.getElementById('cart-close-button').addEventListener('click', function () { DisplayOrder(true); });
    const myOffcanvas = document.getElementById('offcanvasBottom');
    myOffcanvas.addEventListener('hidden.bs.offcanvas', function () { DisplayOrder(true); });
    document.getElementById("open-cart").addEventListener("click", function () {
      renderCart();
      const bsOffcanvas = new bootstrap.Offcanvas(
        document.getElementById("offcanvasBottom")
      );
      bsOffcanvas.show();
    });
  })
  .catch((error) => {
    console.error("Error loading products:", error);
  }); 

  let quantity = 1;
  function increment() {
    quantity++;
    document.getElementById("quantity").value = quantity;
  }
  
  function decrement() {
    if (quantity > 1) {
      quantity--;
      document.getElementById("quantity").value = quantity;
    }
  }

  function resetQuantity() {
    quantity = 1;
    document.getElementById("quantity").value = quantity;
  }