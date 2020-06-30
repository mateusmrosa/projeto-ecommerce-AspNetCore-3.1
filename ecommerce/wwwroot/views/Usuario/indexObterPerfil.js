var indexObterPerfil = {


    btnPesquisarOnClick: function () {
        document.getElementById("tbPerfis").style.display = "table";

        var tbodyPerfils = document.getElementById("tbodyPerfils");
        tbodyPerfils.innerHTML = `<img class="lds-hourglass keyframes" src=\"/imgs/ajax-loader.gif"\ />`
        document.getElementById("btnPesquisar").disabled = "disabled";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };
        var nome = encodeURIComponent(document.getElementById("txtNome").value);

        fetch("/Usuario/ObterPerfis?nome=" + nome, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                for (var i = 0; i < dadosObj.length; i++) {

                    var template =
                        `<tr data-id="${dadosObj[i].id}">
                            <td>${dadosObj[i].id}</td>
                            <td>
                                ${dadosObj[i].nome}
                            </td>
                            <td>
                                <a href="javascript:;" onclick="indexObterPerfil.selecionar(${dadosObj[i].id},'${dadosObj[i].nome}')">selecionar</a>
                            </td>
                         </tr>`
                    linhas += template;
                }

                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }

                tbodyPerfils.innerHTML = linhas;
            })
            .catch(function () {
                tbodyPerfils.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
            .finally(function () {

                document.getElementById("btnPesquisar").disabled = "";
            });


    },

    selecionar: function (id, nome) {

        //window = navegador, parent = janela modal que é acessado pelo "window"
        window.parent.selecionarPerfil(id, nome);

    },

    
}

