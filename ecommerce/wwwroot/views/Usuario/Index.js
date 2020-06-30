

let index = {

    btnCadastrar: function () {
        console.log("oi")

        var nomeUsuario = document.getElementById("txtNome").value;
        var senha = document.getElementById("txtSenha").value;
        var senhaConf = document.getElementById("txtConfSenha").value;

        if (nomeUsuario.trim() == "") {

            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").innerHTML = "Informe o nome.";
        }
        else if (senha.trim() == "" || senhaConf.trim() == "") {

            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").innerHTML = "As senhas não informadas.";
        }
        else if (senha != senhaConf) {

            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").innerHTML = "As senhas não conferem.";
        }
        else {

            var dados = {
                nomeUsuario,
                senha,
                senhaConf
            }

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(dados)  //serializa
            };

            fetch("/Usuario/Criar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {

                    if (dadosObj.operacao == false) {
                        document.getElementById("divMsg").className = "alert alert-danger";
                        document.getElementById("divMsg").innerHTML = dadosObj.msg;
                    }
                    else if (dadosObj.operacao == true) {
                        let txt = "<span class='alert alert-success'>Cadastro efetuado com sucesso!</span>"
                        document.getElementById('divMsg').innerHTML = txt;
                        window.location.href = "/Default";
                    }
                })
                .catch(function () {

                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        }
    },

    btnPesquisarUsuario: function () {

        console.log("Oi");

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
            //body: JSON.stringify(dados)  //serializa
        };

        var usuario = encodeURIComponent(document.getElementById("txtUsuario").value);

        fetch("/Usuario/Pesquisar?usuario=" + usuario, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var tBodyUsuarios = document.getElementById("tBodyUsuarios");
                var linhas = "";

                for (var i = 0; i < dadosObj.length; i++) {

                    var template =
                        `<tr>
                            <td>${dadosObj[i].id}</td>
                            <td>${dadosObj[i].usuario}</td>
                            <td>
                                <a class="btn btn-primary btn-sm">Editar</a>
                                <a class="btn btn-danger btn-sm">Excluir</a>
                            </td>
                        </tr>`
                    linhas += template;
                }

                if (linhas == "") {
                    linhas = `<tr><td style="color:red;" colspan="3">Não encontramos nada para sua pesquisa!</td></tr>`;
                }

                document.getElementById("tbDados").style.display = "table";
                tBodyUsuarios.innerHTML = linhas;

                //dadosObj.forEach(exibir, index);

                //function exibir(dadosObj) {
                //    var td = "<td>" + dadosObj[index] + " " + dadosObj[index] + " " + "</td>";
                //    tBodyUsuarios.innerHTML = td;
                //}


                console.log(dadosObj);
            })
            .catch(function () {

                document.getElementById("divMsgAux").innerHTML = "deu erro";
            })
    },

}

    function selecionarPerfil(id, nome) {

        document.getElementById("hIdPerfil").value = id;

        document.getElementById("divPerfilNome").innerHTML = nome;

        $.fancybox.close();
    }



