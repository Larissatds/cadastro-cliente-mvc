const listaBeneficiarios = [];

$(document).ready(function () {
    $('#CPFBeneficiario').mask('000.000.000-00', { reverse: true });
});

$("#incluirBeneficiario").click(function () {
    const nome = $("#NomeBeneficiario").val();
    const cpf = $("#CPFBeneficiario").val().replace(/\D/g, '');

    if (validateCPF(cpf)) {
        const existeCPFNaTabela = listaBeneficiarios.find(b => b.CPF === cpf);

        if (existeCPFNaTabela) {
            ModalDialog("Ocorreu um erro", "CPF de beneficiário já cadastrado para cliente.");
            return false;
        }

        listaBeneficiarios.push({ "Nome": nome, "CPF": cpf });
        addLinhaNaTabelaBeneficiario(nome, cpf);
    }
});
