
let index = {

    btnCadProduto: function () {
        console.log("oi")
        var nome = document.getElementById("txtNome").value;
        var descricao = document.getElementById("txtDescricao").value;
        var qtde = document.getElementById("txtQtde").value;
        var preco = document.getElementById("txtPreco").value;

        var dados = {
            nome,
            descricao,
            qtde,
            preco
        }

        if (dados.nome.trim() == "" && dados.descricao.trim() == "" && dados.qtde.trim() == "" && dados.preco.trim() == "") {

            let txt = "<div id='divMsg' class='alert alert-danger'><b> Preencha os Campos! </b></div>";

            document.getElementById("divMsgAux").innerHTML = txt;

            setTimeout(function () {
                document.getElementById("divMsg").remove();

            }, 1000)

            return false;
        }

        if (document.getElementById("foto").files.length == 0) {

            let txt = '<div id="divMsg" class="alert alert-danger"> <b> Selecione uma imagem!</b> </div>';

            document.getElementById("divMsgAux").innerHTML = txt;

            setTimeout(function () {
                document.getElementById("divMsg").remove();

            }, 3000);

            return false;

        }

        var config = {
            method: "POST",
            headers: {
                "Content-Type": "application/Json; charset=utf-8"
            },
            credentials: 'include',
            body: JSON.stringify(dados) //serializa
        };

        fetch("/Produto/Cadastrar", config)
            .then(function (dadosJson) {

                var obj = dadosJson.json(); //deserializa
                return obj;
            })
            .then(function (dadosObj) {

                var id = dadosObj.id;

                var fd = new FormData();
                fd.append("foto", document.getElementById("foto").files[0]);
                fd.append("id", id);

                var configFD = {
                    method: "POST",
                    headers: {
                        "Accept": "application/json"
                    },
                    body: fd
                }

                fetch("/Produto/Foto", configFD)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json();
                        return obj;
                    })
                    .then(function (dadosObj) {

                        if (dadosObj.operacao == false) {

                            let txt = '<div id="divMsg" class="alert alert-danger"> <b>' + dadosObj.msg + ' </b> </div>';

                            document.getElementById("divMsgAux").innerHTML = txt;

                            setTimeout(function () {
                                document.getElementById("divMsg").remove();

                            }, 3000);
                        }
                        else {

                            let txt = '<div id="divMsg" class="alert alert-success"> <b>' + dadosObj.msg + ' </b> </div>';

                            document.getElementById("divMsgAux").innerHTML = txt;

                            setTimeout(function () {
                                document.getElementById("divMsg").remove();

                            }, 3000);

                            //window.location.href = "/Produto/Catalogo";
                        }

                    })
                    .catch(function () {
                        document.getElementById("divMsgAux").innerHTML = "<span class='alert alert-danger'>Deu Ruim!</span>";
                    })


            })
            .catch(function () {
                document.getElementById("divMsgAux").innerHTML = "<span class='alert alert-danger'>Deu Ruim!</span>";
            })


    },

    ObterProdutos: function () {
        console.log("oi");
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        var produto = encodeURIComponent(document.getElementById("txtProduto").value);

        fetch("/Produto/ObterProdutos?produto=" + produto, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj == "" || dadosObj == null) {
                    let txt = "<div id='divMsg' class='alert alert-danger col-12'><b> Nenhum produto encontrado! </b></div>";

                    document.getElementById("cardProdutos").innerHTML = txt;

                    setTimeout(function () {
                        document.getElementById("divMsg").remove();

                    }, 1000)

                    return false;
                }
                
                var linhas = "";

                for (var i = 0; i < dadosObj.length; i++) {

                    var template =
                        `
                        <div class="col-sm-4" style="margin-bottom:50px;">
                            <div class="card" style="width: 18rem;box-shadow: 5px 5px 5px rgba(0,0,0,0.1);">
                                <img src="/Produto/ObterFoto?id=${dadosObj[i].id}" class="card-img-top" style="width:285px" height="161px" alt="Imagem do Produto">
                                <div class="card-body">
                                <h5 class="card-title">${dadosObj[i].nome}</h5>
                                <p class="card-text">${dadosObj[i].descricao}</p>
                                <h5 class="card-title">R$${dadosObj[i].preco},00</h5>
                                <p class="card-text">Quantidade: ${dadosObj[i].qtde}</p>
                                <a href="#" class="btn btn-primary">Editar</a>
                                <a href="#" class="btn btn-danger">Excluir</a>
                            </div>
                            </div>
                        </div>
                        
                        `
                    linhas += template;
                }

                cardProdutos.innerHTML = linhas;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }
}



