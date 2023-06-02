var tableProducts;
var price = 0;
var totalPrice = 0;

var discont = document.querySelector('#discont');
var totalInput = document.querySelector('#total-price');
var totalLabel = document.querySelector('#total-value');

document.addEventListener("DOMContentLoaded", function () {
    tableProducts = document.querySelectorAll('#product-table tbody tr');
    for (var i = 0; i < tableProducts.length; i++) {
        var tableRow = tableProducts[i];
        var priceItem = tableRow.querySelector('.product-price').textContent;
        var quantityItem = tableRow.querySelector('.product-quantity').textContent;

        price += (Number(priceItem) * Number(quantityItem));
    }
    var priceHtml = document.querySelector('#price');
    priceHtml.textContent = "Valor Bruto: " + price + " R$";
    totalInput.value = price;
});


discont.addEventListener("input", function () {

    if (discont.value < 0) {
        discont.value = 0;
    }
    if (discont.value > 100) {
        discont.value = 100;
    }

    var discontValue = Number(discont.value);
    totalPrice = price - (price * (discontValue / 100));
    ShowTotal(totalPrice);
});

function ShowTotal(total) {
    totalInput.value = total;
    totalLabel.textContent = `Total a pagar: ${total.toFixed(2)} R$`; 
}

function validateNumber(input) {
    if (isNaN(input.value)) {
        input.value = "";
    }
}

