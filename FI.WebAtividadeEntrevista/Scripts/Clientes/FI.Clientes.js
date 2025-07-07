
$(document).ready(function () {
    $('#CPF').mask('000.000.000-00', { reverse: true });

    $('#formCadastro').submit(function (e) {
        const cpf = $(this).find("#CPF").val().replace(/\D/g, '');
        e.preventDefault();

        if (validateCPF(cpf)) {
            $.ajax({
                url: urlPost,
                method: "POST",
                data: {
                    "NOME": $(this).find("#Nome").val(),
                    "CEP": $(this).find("#CEP").val(),
                    "Email": $(this).find("#Email").val(),
                    "Sobrenome": $(this).find("#Sobrenome").val(),
                    "Nacionalidade": $(this).find("#Nacionalidade").val(),
                    "Estado": $(this).find("#Estado").val(),
                    "Cidade": $(this).find("#Cidade").val(),
                    "Logradouro": $(this).find("#Logradouro").val(),
                    "Telefone": $(this).find("#Telefone").val(),
                    "CPF": cpf,
                    "beneficiarios": listaBeneficiarios
                },
                error:
                    function (r) {
                        if (r.status == 400)
                            ModalDialog("Ocorreu um erro", r.responseJSON);
                        else if (r.status == 500)
                            ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    },
                success:
                    function (r) {
                        ModalDialog("Sucesso!", r)
                        $("#formCadastro")[0].reset();
                        listaBeneficiarios.length = 0;
                    }
            });
        }
        else
            ModalDialog("Ocorreu um erro", "CPF inválido, por favor verifique o CPF informado e tente novamente.");
    })

})


