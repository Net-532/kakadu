let checkTab = true;
let order = null;

function createCartItemElement(item) {
    const itemElement = document.createElement('div');
    itemElement.classList.add( 'card' , 'mb-3');
itemElement.innerHTML =`
<div class="row g-0 d-flex flex-nowrap p-2"  >
  <div class="col-auto">
    <img src="${item.photoUrl}" class="img-fluid rounded-start" id="cart-item-img" alt="${item.title}">
  </div>
  <div class="col">
      <h6 class="card-title fw-bold">${item.title}</h6>
      <p class="card-text" id="cart-text-small" >${item.description}</p>
      <p class="card-text" id="cart-item-price">${item.price} грн</p>
      <button class="remove-button btn btn-close mb-3" id="cart-remove-button"></button>
      <div id="cart-item-buttons">
         <button class="decrement-button" id="cart-button-change">-</button>
         <div id="cart-item-quantity">${item.quantity}</div>
         <button class="increment-button" id="cart-button-change">+</button>
    </div>
  </div>
</div>

`;

  const incrementButton = itemElement.querySelector(".increment-button");
  incrementButton.addEventListener("click", function (event) {
    addToCart(item);
    renderCart();
  });

  const decrementButton = itemElement.querySelector(".decrement-button");
  decrementButton.addEventListener("click", function (event) {
    removeFromCart(item);
    renderCart();
  });

  const removeButton = itemElement.querySelector(".remove-button");
  removeButton.addEventListener("click", function (event) {
    removeItemFromCart(item);
  });

  return itemElement;
}

function addToCart(product) {
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  let existingProductIndex = cartItems.findIndex(
    (item) => item.id === product.id
  );
  if (existingProductIndex !== -1) {
    cartItems[existingProductIndex].quantity =
      (cartItems[existingProductIndex].quantity || 1) + 1;
  } else {
    product.quantity = 1;
    cartItems.push(product);
  }

  localStorage.setItem("cartItems", JSON.stringify(cartItems));
  displayAlert("success", product.title, "додано в кошик");
}

function removeFromCart(product) {
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  let existingProductIndex = cartItems.findIndex(
    (item) => item.id === product.id
  );
  if (existingProductIndex !== -1) {
    cartItems[existingProductIndex].quantity -= 1;
    if (cartItems[existingProductIndex].quantity === 0) {
      cartItems.splice(existingProductIndex, 1);
    }
  }

  localStorage.setItem("cartItems", JSON.stringify(cartItems));
  renderCart();
}

function removeItemFromCart(product) {
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  let existingProductIndex = cartItems.findIndex(
    (item) => item.id === product.id
  );
  if (existingProductIndex !== -1) {
    cartItems.splice(existingProductIndex, 1);
  }

  localStorage.setItem("cartItems", JSON.stringify(cartItems));
  renderCart();
}

function calculateTotalSum() {
  let totalSum = 0;
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  cartItems.forEach((item) => {
    totalSum += item.price * item.quantity;
  });
  return totalSum;
}

function renderCart() {
  const offcanvasBody = document.querySelector(
    "#offcanvasBottom .offcanvas-body"
  );
  offcanvasBody.innerHTML = "";
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  cartItems.forEach((item) => {
    const itemElement = createCartItemElement(item);
    offcanvasBody.appendChild(itemElement);
  });

  if (cartItems.length === 0 && checkTab == true) {
    const emptyCart = document.createElement("div");
    emptyCart.textContent = "Кошик порожній";
    offcanvasBody.appendChild(emptyCart);
  }

  const totalSumElements = document.querySelectorAll(".cart-numb");
  totalSumElements.forEach((element) => {
    const totalSum = calculateTotalSum();
    element.textContent = `${totalSum} грн`;
  });
}

function clearCart() {
  localStorage.removeItem("cartItems");
  renderCart();
}

function checkoutOrder() {
  const items = JSON.parse(localStorage.getItem("cartItems")) || [];
  const sendRequest = {
    items: items.map((item) => ({
      productId: item.id,
      quantity: item.quantity,
    })),
  };
  fetch(ordersEndpoint, {
    method: "POST",
    body: JSON.stringify(sendRequest),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Замовлення виконано");
      DisplayCheckTab();
      order = data;
      clearCart();
      renderReceipt(data);
    })
    .catch((error) => {
      console.error("Помилка: ", error);
    });
}

const printCheck = document.getElementById("cart-button-check");
printCheck.addEventListener("click", function (event) {
  fetch(`${printReceiptEndpoint}?orderId=${order.id}`, {
    method: "PUT",
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Чек надруковано!");
      localStorage.removeItem("cartItems");
      DisplayOrderTab();
      renderCart();
    })
    .catch((error) => {
      console.error("Помилка: ", error);
    });
});

function DisplayCheckTab() {
  const cartBottom = document.getElementById("cart-bottom");
  cartBottom.style.display = "none";
  const cartCheck = document.getElementById("cart-button-check");
  cartCheck.style.display = "block";
  checkTab = false;
}

function DisplayOrderTab() {
  const cartBottom = document.getElementById("cart-bottom");
  cartBottom.style.display = "block";
  const cartCheck = document.getElementById("cart-button-check");
  cartCheck.style.display = "none";
  checkTab = true;
}
function renderReceipt(order) {
  const header = `
        <center>
        <p>Kakadu</p>
        <p>Адреса: вул. Павла Каспрука 2</p>
        <p>Чек # ${order.orderNumber}</p>
        <hr>
        <div class="container-receipt">
        <div class="name"><p>Дата:${order.orderDate}</p>
        <p>Час:${order.orderTime}</p></div>
        </div>
        <hr>
        `;
  let body = ``;
  order.items.forEach((item) => {
    body += `
    <center>
    <div class="container-receipt">
                      <div class="name">${item.title}</div>
                     <div class="price"> ${item.quantity}x${item.price}</div>
            </div>
            `;
  });
  const footer = `
  <center>
        <hr>
        <div class="container-receipt">
  <div class="name">Сума:</div> 
  <div class="price">${order.totalPrice} грн</div>
    </div>
        <hr>
        <p>Дякуємо за покупку!</p>
    `;
  let check = document.getElementById("cart-main-container");
  const itemElement = document.createElement("div");
  itemElement.classList.add("receipt");
  itemElement.innerHTML = header + body + footer;
  check.appendChild(itemElement);
}

function displayAlert(type, item, message) {
  let alert = `
        <div class="alert alert-${type} alert-dismissible" id="add-alert" role="alert">
            <div>${item} ${message}</div>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

  let alertContainer = document.getElementById("alert-container");
  if (alertContainer) {
    alertContainer.innerHTML = alert;
  }
}
