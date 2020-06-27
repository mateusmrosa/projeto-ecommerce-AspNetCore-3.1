

let index = {

    //usuario: "",
    btnEntrarOnClick: function () {

        var usuario = document.getElementById("txtUsuario").value;
        var senha = document.getElementById("txtSenha").value;

        var dados = {
            usuario,
            senha
        }

        if (dados.usuario.trim() == "" && dados.senha.trim() == "") {

            let txt = '<div id="divMsg" class="alert alert-danger"><b> Preencha os Campos!</b> </div>';

            document.getElementById("divMsgAux").innerHTML = txt;
            setTimeout(function () {
                document.getElementById("divMsg").remove();

            }, 1500);

            return false;
        }


        var config = {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include',
            body: JSON.stringify(dados)  //serializa
        };

        fetch("/Default/Logar", config)
            .then(function (dadosJson) {
                //console.log(dadosJson);
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao == false) {
                    document.getElementById("divMsgAux").className = "alert alert-danger";
                    document.getElementById("divMsgAux").innerHTML = dadosObj.msg;
                }
                else if (dadosObj.operacao == true) {
                    document.getElementById("divMsgAux").className = "alert alert-success";
                    document.getElementById("divMsgAux").innerHTML = dadosObj.msg;
                    window.location.href = "/Produto";
                }
            })
            .catch(function () {

                document.getElementById("divMsgAux").innerHTML = "<span class='alert alert-success'>Erro</span>";
            })
    }

}