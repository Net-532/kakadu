let checkTab = true;
let order = null;

function createCartItemElement(item) {
    const itemElement = document.createElement('div');
    itemElement.classList.add( 'card' , 'mb-3');
itemElement.innerHTML =`
<div class="row g-0 d-flex flex-nowrap p-2 "  >
  <div class="col-auto me-2">
    <img src="${item.photoUrl}" class="img-fluid rounded" id="cart-item-img" alt="${item.title}">
  </div>
  <div class="col">
      <h6 class="card-title fw-bold">${item.title}</h6>
      <p class="card-text" id="cart-text-small" >${item.description}</p>
      <p class="card-text" id="cart-item-price">${item.price} грн</p>
      <button class="remove-button btn btn-close mb-3" id="cart-remove-button"></button>
      <div class="quantity-controls">
        <button class="quantity-button" id="decrease">-</button>
        <input type="text" id="quantity" value="${item.quantity}" readonly>
        <button class="quantity-button" id="increase">+</button>
      </div>
  </div>
</div>

`;

  const incrementButton = itemElement.querySelector("#increase");
  incrementButton.addEventListener("click", function (event) {
    addToCart(item);
    renderCart();
  });

  const decrementButton = itemElement.querySelector("#decrease");
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

function formatPrice(price) {
    return parseFloat(price).toFixed(2);
}

function addToCart(product) {
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  let existingProductIndex = cartItems.findIndex(
    (item) => item.id === product.id
  );
  
  let quantity = parseInt(document.getElementById("quantity").value);

  if (existingProductIndex !== -1) {
    cartItems[existingProductIndex].quantity += quantity;
  } else {
    product.quantity = quantity;
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
  return formatPrice(totalSum)
}

function renderCart() {
  const offcanvasBody = document.querySelector("#offcanvasBottom .offcanvas-body");
  offcanvasBody.innerHTML = "";
  let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
  if (cartItems.length === 0 && checkTab == true) {
    const emptyCartContent = `
        <div class="empty-cart-container">
            <div class="empty-cart">
                <img src="assets/images/burger.png" alt="Empty Cart" class="empty-cart-image">
                <div class="empty-cart-text-container">
                    <div class="empty-cart-text">Ой, кошик порожній...</div>
                    <div class="empty-cart-message">Схоже, ви нічого не замовили.</div>
                </div>
            </div>
        </div>
    `;

    offcanvasBody.innerHTML = emptyCartContent;
  } else {
    cartItems.forEach((item) => {
      const itemElement = createCartItemElement(item);
      offcanvasBody.appendChild(itemElement);
    });
  }

  const totalSumElements = document.querySelectorAll(".cart-numb");
  totalSumElements.forEach((element) => {
    const totalSum = calculateTotalSum();
    element.textContent = `${totalSum} грн`;
  });

  const orderButton = document.getElementById("cart-button-order");
  orderButton.disabled = cartItems.length === 0;
}

function clearCart() {
  localStorage.removeItem("cartItems");
  renderCart();
}

function checkoutOrder() {
    const items = JSON.parse(localStorage.getItem("cartItems")) || [];
    const orderButton = document.getElementById("cart-button-order");

    const sendRequest = {
        items: items.map((item) => ({
        productId: item.id,
        quantity: item.quantity,
        })),
    };

    orderButton.disabled = true;
    ShowHideSpinner("cart-order-spinner" , 'inline-block');

    fetch(ordersEndpoint, {
        method: "POST",
        body: JSON.stringify(sendRequest),
    })
    .then((response) => response.json())
    .then((data) => {
        console.log("Замовлення виконано");
        DisplayOrder(false);
        order = data;
        clearCart();
        renderReceipt(data);
    })
    .catch((error) => {
        console.error("Помилка: ", error);
    })
    .finally(() => {
        orderButton.disabled = false;
        ShowHideSpinner("cart-order-spinner" , 'none');
    });
}

const printCheck = document.getElementById("cart-button-check");
printCheck.addEventListener("click", function (event) {
    const checkButton = document.getElementById("cart-button-check");
    ShowHideSpinner("cart-check-spinner" , "inline-block");

    fetch(`${printReceiptEndpoint}?orderId=${order.id}`, {
        method: "PUT",
    })
    .then((response) => response.json())
    .then((data) => {
        console.log("Чек надруковано!");
        localStorage.removeItem("cartItems");
        DisplayOrder(true);
        renderCart();
    })
    .catch((error) => {
        console.error("Помилка: ", error);
    })
    .finally(() => {
        checkButton.disabled = false;
        ShowHideSpinner("cart-check-spinner" , "none");
    });
});

function DisplayOrder(OrderTab) {
    const cartBottom = document.getElementById("cart-bottom");
    const cartCheck = document.getElementById("cart-button-check");
    const orderButton = document.getElementById("cart-button-order");
    const EmailButton = document.getElementById("cart-button-email");
    const EmailBox = document.getElementById("email-box");

    orderButton.disabled = false;
    EmailButton.disabled = true;
    cartBottom.style.display = OrderTab ? "block" : "none";
    EmailBox.style.display = OrderTab ? "none" : "block";
    EmailButton.style.display = OrderTab ? "none" : "block";
    cartCheck.style.display = OrderTab ? "none" : "block";

    checkTab = OrderTab;
}


function ShowHideSpinner(id, display) {
    const spinner = document.getElementById(id);
    spinner.style.display = display; 
}

function renderReceipt(order) {
    const header = `
      <p class="receipt-info">Kakadu</p>
      <p class="receipt-info">м. Чернівці, вул. Павла Каспрука 2</p>
      <p class="receipt-info">Чек # ${order.orderNumber}</p>
      <hr>
      <div class="container-receipt">
          <div class="name">
              <p>Дата: ${order.orderDate}</p>
              <p>Час: ${order.orderTime}</p>
          </div>
      </div>
      <hr>
  `;

    let body = ``;
    order.items.forEach((item) => {
        body += `
          <div class="container-receipt">
              <div class="name">${item.title}</div>
              <div class="price"> ${item.quantity} x  ${formatPrice(item.price)}</div>
          </div>
      `;
    });

    const footer = `
      <hr>
      <div class="container-receipt">
          <div class="name">Сума:</div> 
          <div class="price">${formatPrice(order.totalPrice)} грн</div>
      </div>
      <hr>
      <p class="receipt-thanks">Дякуємо за покупку!</p>
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

function sendEmail(orderId, recipientEmail) {
  const sendRequest = {
    recipient: recipientEmail,
    orderId: orderId
  };

  fetch(emailEndpoint, {
      method: "POST",
      body: JSON.stringify(sendRequest),
  })
  .then((response) => response.json())
  .then((data) => {
    console.log("виконано");
  })
  .catch((error) => {
      console.error("Помилка: ", error);
  });
}

function EmailButtonDisable() {
  const emailbox = document.getElementById("email-box");
  const emailBtn = document.getElementById("cart-button-email");
  const correctEmail = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;

  emailbox.addEventListener("input", function() {
    if (emailbox.value.trim() === "" || !correctEmail.test(emailbox.value.trim())) {
        emailBtn.disabled = true;
    } else {
        emailBtn.disabled = false;
    }
  });
};

document.getElementById('cart-button-email').addEventListener('click', function() {
  const recipientEmail = document.getElementById('email-box').value.trim();
  const emailbox = document.getElementById("email-box");
  emailbox.value = '';
  sendEmail(order.id, recipientEmail);
});