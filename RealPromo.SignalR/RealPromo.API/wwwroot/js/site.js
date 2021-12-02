
var btnCadastrar = document.getElementyById("BtnCadastrar");

if (btnCadastrar != null) {
    btnCadastrar.addEventListener("click", function () {

        var empresa = document.getElementyById("Empresa").value;
        var chamada = document.getElementyById("Chamada").value;
        var regras = document.getElementyById("Regras").value;
        var enderecoUrl = document.getElementyById("EnderecoUrl").value;

        var promocao = { Empresa: empresa, Chamada: chamada, Regras: regras, EnderecoUrl: enderecoUrl };
    })
}