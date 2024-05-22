const fetchlink = "http://192.168.88.139:8085/products";
const ordersEndpoint = "http://192.168.88.139:8085/orders";
const printReceiptEndpoint='http://192.168.88.139:8085/print';

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
          document.getElementById("full_description_modal")
        );
        const textinside = document.getElementById("full-card-text");
        const id = product.id;

        myModal.show();
        textinside.innerHTML = `
                    <img src="${product.photoUrl}" alt="${product.title}" style="max-width: 100%;"> <br>
                    Name: ${product.title} <br>
                    Price: ${product.price} <br>
                    Description: ${product.description} <br>
                    Id: ${id} <br>
                    <button data-id="${product.id}" type="button" class="add-to-cart-button btn btn-outline-primary">Add to cart</button> `;

        const addToCartButton = document.querySelector(".add-to-cart-button");
        addToCartButton.addEventListener("click", () => addToCart(product));
      });

      row.appendChild(productCard);
    });
    document.getElementById('cart-clear-button').addEventListener('click', clearCart);
    document.getElementById('cart-button-order').addEventListener('click', checkoutOrder);
    document.getElementById('cart-close-button').addEventListener('click', function () { DisplayOrderTab(); });
    const myOffcanvas = document.getElementById('offcanvasBottom');
    myOffcanvas.addEventListener('hidden.bs.offcanvas', function () { DisplayOrderTab(); });
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


