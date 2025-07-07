function validateCPF(cpf) {
    if (cpf.length !== 11 || /^(\d)\1+$/.test(cpf)) return false;

    // cálculo do dígito verificador do CPF
    let soma = 0;
    for (let i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }

    let resto = soma % 11;
    let digito1 = resto < 2 ? 0 : 11 - resto;

    if (parseInt(cpf.charAt(9)) !== digito1) return false;

    soma = 0;
    for (let i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }

    resto = soma % 11;
    let digito2 = resto < 2 ? 0 : 11 - resto;

    return parseInt(cpf.charAt(10)) === digito2;
}

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

function addLinhaNaTabelaBeneficiario(nome, cpf) {
    const tabela = document.querySelector('#table-beneficiarios tbody');
    const novaLinha = tabela.insertRow();

    novaLinha.innerHTML = `
            <td>${cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4')}</td>
            <td>${nome}</td>
            <td class="pull-right">
                <button class="btn btn-primary btn-sm btn-alterar">Alterar</button>
                <button class="btn btn-primary btn-sm btn-remover">Excluir</button>
            </td>
        `;

    novaLinha.setAttribute("data-cpf", cpf);

    $("#NomeBeneficiario").val("");
    $("#CPFBeneficiario").val("");
    $("#CPFBeneficiario").focus();

    novaLinha.querySelector(".btn-remover").addEventListener("click", function () {
        const cpfDaLinha = novaLinha.getAttribute('data-cpf');
        const index = listaBeneficiarios.findIndex(b => b.CPF === cpfDaLinha);

        if (listaBeneficiarios[index] && listaBeneficiarios[index].Id === undefined) {
            if (index > -1) {
                listaBeneficiarios.splice(index, 1);
            }
        } else {
            listaBeneficiarios[index].Excluir = true;
        }

        novaLinha.remove();
    });
}
