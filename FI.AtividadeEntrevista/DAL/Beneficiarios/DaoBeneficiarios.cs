using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiários
    /// </summary>
    internal class DaoBeneficiarios : AcessoDados
    {
        /// <summary>
        /// Inclui um novo Beneficiários
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(Beneficiarios beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiarios", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        internal bool VerificarExistencia(string CPF, long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("idCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarioPorCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Lista todos os Beneficiários
        /// </summary>
        /// <param name="idCliente">idCliente do cliente</param>
        internal List<Beneficiarios> Listar(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarios", parametros);
            List<Beneficiarios> benef = Converter(ds);

            return benef;
        }

        /// <summary>
        /// Excluir Beneficiários
        /// </summary>
        /// <param name="id">Id do beneficiario</param>
        internal void Excluir(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", id));

            base.Executar("FI_SP_DelBeneficiarios", parametros);
        }

        private List<Beneficiarios> Converter(DataSet ds)
        {
            List<Beneficiarios> lista = new List<Beneficiarios>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiarios benef = new Beneficiarios();
                    benef.Id = row.Field<long>("Id");
                    benef.CPF = row.Field<string>("CPF");
                    benef.Nome = row.Field<string>("Nome");
                    benef.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(benef);
                }
            }

            return lista;
        }
    }
}
