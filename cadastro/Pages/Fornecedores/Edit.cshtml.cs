using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using static cadastro.Pages.Fornecedores.IndexModel;

namespace cadastro.Pages.Fornecedores
{
    public class EditModel : PageModel
    {
        public FornecedorInfo listFornecedor = new FornecedorInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fornecedores;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM fornecedores WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                listFornecedor.id = "" + reader.GetInt32(0);
                                listFornecedor.nome = reader.GetString(1);
                                listFornecedor.cnpj = reader.GetString(2);
                                listFornecedor.especialidade = reader.GetString(3);
                                listFornecedor.cep = reader.GetString(4);
                                listFornecedor.endereco = reader.GetString(5);
                            }
                        }
                    }
                }

            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
            listFornecedor.id = Request.Form["id"];
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

            if (listFornecedor.id.Length == 0 || listFornecedor.nome.Length == 0 || listFornecedor.cnpj.Length == 0 ||
                listFornecedor.cep.Length == 0 || listFornecedor.endereco.Length == 0)
            {
                errorMessage = "Preencha todos os campos";
                return;
            }

            try 
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fornecedores;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE fornecedores " +
                                 "SET nome=@nome, cnpj=@cnpj, especialidade=@especialidade, cep=@cep, endereco=@endereco " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", listFornecedor.nome);
                        command.Parameters.AddWithValue("@cnpj", listFornecedor.cnpj);
                        command.Parameters.AddWithValue("@especialidade", listFornecedor.especialidade);
                        command.Parameters.AddWithValue("@cep", listFornecedor.cep);
                        command.Parameters.AddWithValue("@endereco", listFornecedor.endereco);
                        command.Parameters.AddWithValue("@id", listFornecedor.id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Fornecedores/Index");
        }

    }
}
