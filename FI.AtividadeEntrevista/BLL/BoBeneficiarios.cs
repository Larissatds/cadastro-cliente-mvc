using System.Collections.Generic;
using System.Linq;
using FI.AtividadeEntrevista.Utils;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiarios
    {
        // <summary>
        /// Inclui um novo beneficiários
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiarios beneficiario)
        {
            DAL.DaoBeneficiarios benef = new DAL.DaoBeneficiarios();
            return benef.Incluir(beneficiario);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiarios benef = new DAL.DaoBeneficiarios();
            benef.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiários
        /// </summary>
        /// <param name="idCliente">idCliente do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiarios> Listar(long idCliente)
        {
            DAL.DaoBeneficiarios benef = new DAL.DaoBeneficiarios();
            return benef.Listar(idCliente);
        }

        /// <summary>
        /// Verifica se CPF beneficiário está válido
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public OperationResult ValidarCPFBeneficiario(string CPF, long idCliente)
        {
            DAL.DaoBeneficiarios benef = new DAL.DaoBeneficiarios();
            OperationResult result = new OperationResult() { Sucess = true };

            if (!CpfValidator.IsValid(CPF))
                return new OperationResult()
                {
                    Sucess = false,
                    message = "CPF inválido."
                };

            if (benef.VerificarExistencia(CPF, idCliente))
                return new OperationResult()
                {
                    Sucess = false,
                    message = "CPF de beneficiário já cadastrado para cliente."
                };

            return result;
        }
    }
}
