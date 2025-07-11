﻿using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiarios boBenef = new BoBeneficiarios();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var validarCPF = bo.ValidarCPF(model.CPF);

                if (!validarCPF.Sucess)
                {
                    Response.StatusCode = 400;
                    return Json(validarCPF.message);
                }

                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                foreach (var b in model.Beneficiarios)
                {
                    var validarCPFBenef = boBenef.ValidarCPFBeneficiario(b.CPF, model.Id);

                    if (!validarCPFBenef.Sucess)
                    {
                        Response.StatusCode = 400;
                        return Json(validarCPFBenef.message);
                    }

                    boBenef.Incluir(new Beneficiarios()
                    {
                        CPF = b.CPF,
                        Nome = b.Nome,
                        IdCliente = model.Id
                    });
                }

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiarios boBenef = new BoBeneficiarios();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {           
                var cliente = bo.Consultar(model.Id);
                if(cliente.CPF != model.CPF)
                {
                    var validarCPF = bo.ValidarCPF(model.CPF);

                    if (!validarCPF.Sucess)
                    {
                        Response.StatusCode = 400;
                        return Json(validarCPF.message);
                    }
                }

                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                foreach (var b in model.Beneficiarios)
                {
                    if (b.Excluir)
                    {
                        boBenef.Excluir(b.Id);
                    }
                    else if (b.Id == 0)
                    {
                        var validarCPFBenef = boBenef.ValidarCPFBeneficiario(b.CPF, model.Id);

                        if (!validarCPFBenef.Sucess)
                        {
                            Response.StatusCode = 400;
                            return Json(validarCPFBenef.message);
                        }

                        boBenef.Incluir(new Beneficiarios()
                        {
                            CPF = b.CPF,
                            Nome = b.Nome,
                            IdCliente = model.Id
                        });
                    }
                }

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiarios boBenef = new BoBeneficiarios();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;
            List<Beneficiarios> beneficiarios = boBenef.Listar(id);

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                    Beneficiarios = beneficiarios.Select(x => new BeneficiariosModel()
                    {
                        Id = x.Id,
                        CPF = x.CPF,
                        Nome = x.Nome,
                        IdCliente = x.IdCliente
                    }).ToList()
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}