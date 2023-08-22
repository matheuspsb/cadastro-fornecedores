using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static cadastro.Pages.Fornecedores.IndexModel;

namespace cadastro.Pages.Fornecedores
{
    public class CreateModel : PageModel
    {
        public FornecedorInfo listFornecedor = new FornecedorInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            listFornecedor.nome = Request.Form["nome"];
            listFornecedor.cnpj = Request.Form["cnpj"];
            listFornecedor.especialidade = Request.Form["especialidade"];
            listFornecedor.cep = Request.Form["cep"];
            listFornecedor.endereco = Request.Form["endereço"];

            if (listFornecedor.especialidade != "Comércio" && listFornecedor.especialidade != "Serviço" && listFornecedor.especialidade != "Indústria")
            {
                errorMessage = "Especialidade inválida";
                return;
            }

            if (listFornecedor.nome.Length == 0 || listFornecedor.cnpj.Length == 0 ||
                listFornecedor.cep.Length == 0 || listFornecedor.endereco.Length == 0 ) 
            {
                errorMessage = "Preencha todos os campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fornecedores;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection( connectionString )) 
                { 
                    connection.Open();
                    String sql = "INSERT INTO fornecedores " +
                                 "(nome, cnpj, especialidade, cep, endereco) VALUES " +
                                 "(@nome, @cnpj, @especialidade, @cep, @endereco);";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@nome", listFornecedor.nome);
                        command.Parameters.AddWithValue("@cnpj", listFornecedor.cnpj);
                        command.Parameters.AddWithValue("@especialidade", listFornecedor.especialidade);
                        command.Parameters.AddWithValue("@cep", listFornecedor.cep);
                        command.Parameters.AddWithValue("@endereco", listFornecedor.endereco);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch ( Exception ex )
            {
                errorMessage = ex.Message;
                return;
            }

            listFornecedor.nome = ""; 
            listFornecedor.cnpj = ""; 
            listFornecedor.especialidade = "";
            listFornecedor.cep = "";
            listFornecedor.endereco = "";

            successMessage = "Novo Fornecedor Adicionado com Sucesso!";

            Response.Redirect("/Fornecedores/Index");
        }
    }
}
