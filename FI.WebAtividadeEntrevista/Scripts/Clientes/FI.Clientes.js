
$(document).ready(function () {
    $('#CPF').mask('000.000.000-00', { reverse: true });

    $('#formCadastro').submit(function (e) {
        var cpf = $(this).find("#CPF").val();
        e.preventDefault();

        if (CPFValidate(cpf)) {
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
                    "CPF": cpf
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
                    }
            });
        }
        else 
            ModalDialog("Ocorreu um erro", "CPF inválido, por favor verifique o CPF informado e tente novamente.");
    })

})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function CPFValidate(cpf) {
    //cpf = cpf.replace(/\D/g, '');
    //if (cpf.length !== 11 || /^(\d)\1+$/.test(cpf)) return false;

    //// cálculo do dígito verificador do CPF
    //let soma = 0;
    //for (let i = 0; i < 9; i++) {
    //    soma += parseInt(cpf.charAt(i)) * (10 - i);
    //}

    //let resto = soma % 11;
    //let digito1 = resto < 2 ? 0 : 11 - resto;

    //if (parseInt(cpf.charAt(9)) !== digito1) return false;

    //soma = 0;
    //for (let i = 0; i < 10; i++) {
    //    soma += parseInt(cpf.charAt(i)) * (11 - i);
    //}

    //resto = soma % 11;
    //let digito2 = resto < 2 ? 0 : 11 - resto;

    //    return parseInt(cpf.charAt(10)) === digito2;
    return true;
}

