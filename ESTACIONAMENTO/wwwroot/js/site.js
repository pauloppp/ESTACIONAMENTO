// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function HabilitaStatus() {
    document.getElementById("status").disabled = false;
    document.getElementById("classificacao").disabled = false;
}

function HabilitaTodos() {
    document.getElementById("DataSaida").disabled = false;
    document.getElementById("DataEntrada").disabled = false;
    document.getElementById("Classificacao").disabled = false;
    document.getElementById("Status").disabled = false;
    document.getElementById("CarroModelo").disabled = false;
    document.getElementById("ManobristaNome").disabled = false;
    document.getElementById("fechar").submit();
}

function HabilitaTodosE() {
    document.getElementById("DataEntrada").disabled = false;
    document.getElementById("Classificacao").disabled = false;
    document.getElementById("Status").disabled = false;
    document.getElementById("editar").submit();
}