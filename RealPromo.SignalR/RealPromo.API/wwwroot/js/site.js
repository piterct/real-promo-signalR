
var connection = new signalR.HubConnectionBuilder().withUrl("/PromoHub").build();

connection.start().then(function () {
    console.info("Connected!")
}).catch(function (err) {
    console.error(err.toString())
});

connection.on("CadastradoSucesso", function () {
    var mensagem = document.getElementById("Mensagem");
    mensagem.innerHTML = "Cadastro de promoção realizado com sucesso!";
})

connection.on("ReceberPromocao", function (promocao) {
    console.info(promocao);
})

var btnCadastrar = document.getElementById("BtnCadastrar")


if (btnCadastrar != null) {
    btnCadastrar.addEventListener("click", function () {

        var empresa = document.getElementById("Empresa").value;
        var chamada = document.getElementById("Chamada").value;
        var regras = document.getElementById("Regras").value;
        var enderecoUrl = document.getElementById("EnderecoUrl").value;

        var promocao = { Empresa: empresa, Chamada: chamada, Regras: regras, EnderecoUrl: enderecoUrl };

        connection.invoke("CadastrarPromocao", promocao)
            .then(function () {
                console.log("Cadastrado com sucesso!")
            }).catch(function (err) {
                console.error(err.toString())
            });

    })
}