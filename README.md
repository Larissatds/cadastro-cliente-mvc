# Projeto de Cadastro de Clientes e Beneficiários

O objetivo desse projeto é implementar funcionalidades de cadastro de clientes e seus beneficiários, incluindo a validação e formatação do CPF.

## Funcionalidades

### 1. Cadastro de Clientes

- **Campo CPF**: Um novo campo denominado CPF foi adicionado à tela de cadastramento/alteração de clientes.
  - O campo é obrigatório.
  - Deve seguir a formatação padrão de CPF (999.999.999-99).
  - O sistema valida se o CPF informado é válido, utilizando o cálculo padrão de verificação do dígito verificador.
  - Não é permitido o cadastramento de um CPF já existente no banco de dados.

### 2. Cadastro de Beneficiários

- **Botão Beneficiários**: Um novo botão foi adicionado à tela de cadastramento/alteração de clientes.
  - Ao clicar no botão, um pop-up é exibido para inclusão do CPF e Nome do beneficiário.
  - Um grid exibe os beneficiários já cadastrados, permitindo a manutenção (alteração e exclusão).
  - O CPF do beneficiário deve seguir a formatação padrão (999.999.999-99) e ser validado.
  - Não é permitido o cadastramento de mais de um beneficiário com o mesmo CPF para o mesmo cliente.
  - Os dados do beneficiário são gravados na base de dados ao acionar o botão “Salvar”.

## Estrutura do Banco de Dados

### Tabela CLIENTES

- **CPF**: Novo campo adicionado para armazenar o CPF do cliente.

### Tabela BENEFICIARIOS

- **ID**: Identificador único do beneficiário.
- **CPF**: CPF do beneficiário.
- **NOME**: Nome do beneficiário.
- **IDCLIENTE**: Identificador do cliente ao qual o beneficiário está associado.

## Localização do Banco de Dados

O banco de dados interno está localizado no caminho:


## Tecnologias Utilizadas
- **Visual Studio 2022**  
  - Pacote de direcionamento do .NET Framework 4.8  
  - SDK do .NET Framework 4.8  
  - SQL Server Express 2019 LocalDB  
- **ASP.NET MVC 5**  
- **Entity Framework**
- **Bootstrap 3** para layout e estilos  
- **jQuery** para manipulação de DOM e AJAX  
- **SQL Server LocalDB** (`.mdf` em App_Data)

## Como Executar o Projeto

1. **Clone** este repositório.  
2. Abra o **Visual Studio** e carregue a solução `.sln`.  
3. Certifique-se de que a pasta **App_Data** esteja presente.  
4. Execute a aplicação (F5).  
   - Na primeira execução, o LocalDB criará o arquivo `.mdf`.  
   - A aplicação inicializa a tabela **CLIENTES** e **BENEFICIARIOS** via scripts C#.  
5. Acesse **/Cliente/Cadastrar** para testar o cadastro de Clientes e Beneficiários.

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request.
