fetch('https://fakestoreapi.com/products')
          .then(res => res.json())
        .then(json => {console.log(json);
        var div = document.getElementById('product-list');
        div.innerHTML= `<div data-id="1" class="card product" style="width: 18rem;">
            <img src="https://placehold.co/600x400" class="card-img-top" alt="...">
            <div class="card-body">
              <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
            </div>
            <button type="button" class="btn btn-outline-primary">Add to cart</button>
          </div>`; // append 
        })