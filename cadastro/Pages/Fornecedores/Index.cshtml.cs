using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace cadastro.Pages.Fornecedores
{
    public class IndexModel : PageModel
    {
        public List<FornecedorInfo> listFornecedor = new List<FornecedorInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fornecedores;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM fornecedores";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader render = command.ExecuteReader())
                        {
                            while (render.Read()) 
                            { 
                                FornecedorInfo fornecedorInfo = new FornecedorInfo();
                                fornecedorInfo.id = "" + render.GetInt32(0);
                                fornecedorInfo.nome = render.GetString(1);
                                fornecedorInfo.cnpj = render.GetString(2);
                                fornecedorInfo.especialidade = render.GetString(3);
                                fornecedorInfo.cep = render.GetString(4);
                                fornecedorInfo.endereco = render.GetString(5);

                                listFornecedor.Add(fornecedorInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public class FornecedorInfo
        {
            public String id;
            public String nome;
            public String cnpj;
            public String especialidade;
            public String cep;
            public String endereco;
        }
    }
}
