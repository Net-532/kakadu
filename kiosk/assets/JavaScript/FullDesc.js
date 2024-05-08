const elements = document.getElementsByClassName("card-pre-body");

for (let i = 0; i < elements.length; i++) {
        elements[i].addEventListener('click', function(event) {
            const id = this.parentNode.getAttribute("data-id");
            fetch('https://fakestoreapi.com/products/' + id)
                .then(res => res.json())
                .then(json => {
                    const myModal = new bootstrap.Modal(document.getElementById('exampleModal'));
                    myModal.show();
                    const textinside = document.getElementById('full-card-text');
                    textinside.innerHTML = "Name: " + json.title + "<br> Price: " + json.price + "<br> Desc: " + json.description + "<br> Id: " + id;
                });
        });
    }