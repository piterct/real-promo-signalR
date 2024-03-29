﻿getToken();

//var connection = new signalR.HubConnectionBuilder().withUrl("/PromoHub").build();

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/PromoHub", {
        accessTokenFactory: async () => {
            if (connection.connection.accessToken === null || 'undefined') {
                connection.accessToken = getItemLocalStorage("tokenRealPromo");
            }
            return connection.accessToken;
        }
    })
    .build();

start();

connection.onclose(async () => {
    alert('Connection Closed');
    await start();
})

connection.on("CadastradoSucesso", function () {
    var mensagem = document.getElementById("Mensagem");
    mensagem.innerHTML = "Cadastro de promoção realizado com sucesso!";
})

connection.on("ReceberPromocao", function (promocao) {

    var containerLogin = document.getElementById("container-login");

    var containerPromo = document.createElement("div");
    containerPromo.setAttribute("class", "container-promo");

    var containerChamada = document.createElement("div");
    containerChamada.setAttribute("class", "container-chamada");

    var h1Titulo = document.createElement("h1");
    h1Titulo.innerText = promocao.empresa;

    var p1 = document.createElement("p");
    p1.innerText = promocao.chamada;
    var p2 = document.createElement("p");
    p2.innerText = promocao.regras;

    var containerBotao = document.createElement("div");
    containerBotao.setAttribute("class", "container-botao")

    var link = document.createElement("a");
    link.setAttribute("href", promocao.enderecoURL);
    link.setAttribute("target", "_blank");
    link.innerText = "Pegar";


    containerChamada.appendChild(h1Titulo);
    containerChamada.appendChild(p1);
    containerChamada.appendChild(p2);

    containerBotao.appendChild(link);

    containerPromo.appendChild(containerChamada);
    containerPromo.appendChild(containerBotao);

    containerLogin.appendChild(containerPromo);

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

async function start() {
    connection.start().then(async function () {
        console.info("Connected!")
    }).catch(function (err) {
        console.error(err.toString())
        setInterval(() => start(), 5000);
    });
}



async function getToken() {

    const json = { email: "signalR@teste.com.br", password: "signalRTeste" };

    await fetch('https://localhost:44391/api/Auth/autentica', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(json)

    }).then(async function (response) {

        // The API call was successful!
        if (response.ok) {
            return response.json();
        } else {
            return Promise.reject(response);
        }
    }).then(async function (responseData) {

        // This is the JSON from our response
        var token = responseData.data.accessToken;
        setLocalStorage("tokenRealPromo", token);
        console.log(responseData);
    }).catch(function (err) {
        // There was an error
        console.warn('Something went wrong.', err);
    });

}

function setLocalStorage(name, value) {
    window.localStorage.setItem(name, value);
}

function getItemLocalStorage(name) {
    return window.localStorage.getItem(name);
}


