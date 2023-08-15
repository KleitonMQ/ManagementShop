function showDateTime() {
    var d = new Date();
    var date = d.getUTCDate() + " / " + (d.getUTCMonth() + 1) + " / " + d.getUTCFullYear();
    var time = d.getUTCHours() + ":" + d.getUTCMinutes();
    var dateTime = date + " " + time;
    document.getElementById("currentDateTime").innerHTML = dateTime;
}

showDateTime();
setInterval(showDateTime, 1000);

function formatValue(input) {
    // Remove todos os caracteres, exceto números e vírgula
    let valor = input.value.replace(/[^\d,]/g, '');

    // Remove vírgulas duplicadas
    valor = valor.replace(/(,.*?),/g, '$1');

    // Formata os centavos (últimos dois dígitos após a vírgula)
    valor = valor.replace(/(\d+),(\d{0,2}).*/, '$1,$2');

    // Atualiza o valor no campo de entrada
    input.value = valor;
}