const fetchlink = "http://localhost:8085/products";
const ordersEndpoint = "http://localhost:8085/orders";

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
      productCard.classList.add("col", "col-md-6", "mb-3", "position-relative");

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

      productCard.addEventListener("click", function () {
        const myModal = new bootstrap.Modal(
          document.getElementById("full_description_modal")
        );
        const textinside = document.getElementById("full-card-text");
        const id = product.id;

        myModal.show();
        textinside.innerHTML = `
        <div class="modal-window">
        <div class="element">
          <div class="image">
            <img src="${product.photoUrl}" alt="${product.title}" style="max-width: 100%;">
          </div>
        </div>
        <h3 class="title">${product.title}</h3>
        <div class="description">
          <p>${product.description}</p>
        </div>
        <div class="price">
          <p>${product.price} грн</p>
        </div>
        <button class="cart-button">В кошик</button>
        <div class="quantity-controls">
          <button class="quantity-button" id="decrease">-</button>
          <input type="text" id="quantity" value="1">
          <button class="quantity-button" id="increase">+</button>
        </div>
      </div>`;    

        const addToCartButton = document.querySelector(".add-to-cart-button");
        addToCartButton.addEventListener("click", () => addToCart(product));
      });

      row.appendChild(productCard);
    });

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
