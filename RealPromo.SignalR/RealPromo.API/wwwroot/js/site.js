
var connection = new signalR.HubConnectionBuilder().withUrl("/PromoHub").build();
debugger

connection.start().then(function () {
    console.info("Connected!")
}).catch(function (err) {
    console.error(err.toString())
});

var btnCadastrar = document.getElementById("BtnCadastrar")
debugger 

if (btnCadastrar != null) {
    btnCadastrar.addEventListener("click", function () {

        var empresa = document.getElementyById("Empresa").value;
        var chamada = document.getElementyById("Chamada").value;
        var regras = document.getElementyById("Regras").value;
        var enderecoUrl = document.getElementyById("EnderecoUrl").value;

        var promocao = { Empresa: empresa, Chamada: chamada, Regras: regras, EnderecoUrl: enderecoUrl };

        connection.invoke("CadastrarPromocao", promocao)
            .then(function () {
                console.log("Cadastrado com sucesso!")
            }).catch(function (err) {
                console.error(err.toString())
            });

    })
}