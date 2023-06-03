$(document).ready(function () {
    $('#table-products').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.25/i18n/Portuguese-Brasil.json"
        }
    });
});


function AddProduct() {
    //encontra a linha do botão clicado.
    var button = event.target;
    var row = button.closest('tr');

    //obtem informacoes do produto da linha
    var id = row.querySelector('.product-id').textContent;
    var name = row.querySelector('.product-name').textContent;
    var brand = row.querySelector('.product-brand').textContent;
    var size = row.querySelector('.product-size').textContent;
    var price = row.querySelector('.product-price').textContent;
    var quantityEl = row.querySelector('.product-quantity');
    var quantity = parseInt(quantityEl.textContent);


    var product = {
        ProductId: id,
        Name: name,
        Brand: brand,
        Size: size,
        Price: price,
        Quantity: 1
    };
    if (quantity > 0) {

        var cartRows = document.querySelectorAll('#table-shopping-cart tbody tr');
        for (var i = 0; i < cartRows.length; i++) {
            var cartRow = cartRows[i];
            var cartId = cartRow.querySelector('.product-id').textContent;
            if (cartId === id) {
                var cartQuantity = cartRow.querySelector('.product-quantity');
                var cartQuantityHtml = parseInt(cartQuantity.textContent);
                cartQuantityHtml++;
                cartQuantity.textContent = cartQuantityHtml;
                quantityEl.textContent = quantity - 1;
                UpdateInputsForm()
                return;
            }
        }
    }

    if (quantity > 0) {

        quantityEl.textContent = quantity - 1;

        //Adicionando o produto na tabela do carrinho.

        var table = document.querySelector('#table-shopping-cart tbody');
        var row = table.insertRow();

        var cellId = row.insertCell();
        cellId.classList.add('product-id');
        cellId.name = 'ProductId';

        var cellName = row.insertCell();
        cellId.classList.add('product-name');
        cellName.name = 'ProductName';

        var cellBrand = row.insertCell();
        cellBrand.classList.add('product-brand');
        cellBrand.name = 'Brand';

        var cellSize = row.insertCell();
        cellSize.classList.add('product-size');
        cellSize.name = 'Size';

        var cellPrice = row.insertCell();
        cellPrice.classList.add('product-price');
        cellPrice.name = 'Price';

        var cellQuantity = row.insertCell();
        cellQuantity.classList.add('product-quantity');
        cellQuantity.name = 'Quantity';
        var cellButton = row.insertCell();

        cellId.innerHTML = product.ProductId;
        cellName.innerHTML = product.Name;
        cellBrand.innerHTML = product.Brand;
        cellSize.innerHTML = product.Size;
        cellPrice.innerHTML = product.Price;
        cellQuantity.innerHTML = product.Quantity;
        cellButton.innerHTML = '<a href="#" onclick="RemoveProduct()" class="button-table" id="remove-product" role="button" data-id="' + id + '">-</a>';
        UpdateInputsForm()
    }
}

function RemoveProduct() {
    var button = event.target;
    var row = button.closest('tr');

    //obtem informacoes do produto da linha
    var id = row.querySelector('.product-id').textContent;
    var name = row.querySelector('.product-name').textContent;
    var brand = row.querySelector('.product-brand').textContent;
    var size = row.querySelector('.product-size').textContent;
    var price = row.querySelector('.product-price').textContent;
    var quantityEl = row.querySelector('.product-quantity');
    var quantity = parseInt(quantityEl.textContent);

    if (quantity == 1) {

        var productRows = document.querySelectorAll('#table-products tbody tr');
        for (var i = 0; i < productRows.length; i++) {
            var productRow = productRows[i];
            var productId = productRow.querySelector('.product-id').textContent;
            if (productId === id) {
                var productQuantity = productRow.querySelector('.product-quantity');
                var productQuantityHtml = parseInt(productQuantity.textContent);
                productQuantityHtml++;
                productQuantity.textContent = productQuantityHtml;
            }
        }
        row.remove();
        UpdateInputsForm()

    } else {
        quantityEl.textContent = quantity - 1;

        var productRows = document.querySelectorAll('#table-products tbody tr');
        for (var i = 0; i < productRows.length; i++) {
            var productRow = productRows[i];
            var productId = productRow.querySelector('.product-id').textContent;
            if (productId === id) {
                var productQuantity = productRow.querySelector('.product-quantity');
                var productQuantityHtml = parseInt(productQuantity.textContent);
                productQuantityHtml++;
                productQuantity.textContent = productQuantityHtml;
                quantityEl.textContent = quantity - 1;
                UpdateInputsForm()
                return;
            }
        }
    }
}

function UpdateInputsForm() {
    var idList = document.querySelector('#id-list');
    var quantityList = document.querySelector('#quantity-list');

    idList.value = "";
    quantityList.value = "";
    var productRows = document.querySelectorAll('#table-shopping-cart tbody tr');
    for (var i = 0; i < productRows.length; i++) {

        var idProduct = productRows[i].querySelector('.product-id').textContent;
        var quantityProduct = productRows[i].querySelector('.product-quantity').textContent;

        if (idList.value == "") {
            idList.value += idProduct
            quantityList.value += quantityProduct
        } else {
            idList.value += " " + idProduct;
            quantityList.value += " " + quantityProduct;
        }
    }
}

const myTable = document.querySelector('#table-shopping-cart');
const myButton = document.querySelector('#confirm-purchase');

// MutationObserver - serve para verificar alterações no DOM. Aqui está verificando se existe ou não elementos em minha tabela de carrinho de compras.
const observer = new MutationObserver(() => {
    
    if (myTable.tBodies[0].rows.length > 0) {
        
        myButton.disabled = false;
    } else {
        
        myButton.disabled = true;
    }
});
observer.observe(myTable.tBodies[0], { childList: true });
myButton.disabled = true;
