using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace fabiostefani.io.WebApp.Api.Models
{
    public class AlunoDAO
    {
        string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<AlunoDto> ListarAlunosDb(int? id)
        {
            //string stringConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Desenv\Estudos\DotNet\CursoWebApi_Udemy\fabiostefani.io.WebApp\fabiostefani.io.WebApp.Api\App_Data\Database.mdf;Integrated Security=True";
            //string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
            var listaAlunos = new List<AlunoDto>();
            try
            {
                var selectCmd = conexao.CreateCommand();
                selectCmd.CommandText = "select * from alunos";
                if (id != null)
                {
                    selectCmd.CommandText = $"select * from alunos where id = {id}";
                }

                var resultado = selectCmd.ExecuteReader();


                while (resultado.Read())
                {
                    var aluno = new AlunoDto
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["Nome"]),
                        Sobrenome = Convert.ToString(resultado["Sobrenome"]),
                        Telefone = Convert.ToString(resultado["Telefone"]),
                        Ra = Convert.ToInt32(resultado["Ra"])
                    };
                    listaAlunos.Add(aluno);
                }
                conexao.Close();
                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            
        }

        public void InserirAlunoDb(AlunoDto aluno)
        {
            try
            {
                var comandInsert = conexao.CreateCommand();
                comandInsert.CommandText = "insert into alunos (nome, sobrenome, telefone, ra) values(@nome, @sobrenome, @telefone, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
                comandInsert.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
                comandInsert.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
                comandInsert.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("ra", aluno.Ra);
                comandInsert.Parameters.Add(paramRa);

                comandInsert.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            
        }

        public void AtualizarAlunoDb(AlunoDto aluno)
        {
            try
            {
                var comandUpdate = conexao.CreateCommand();
                comandUpdate.CommandText = @"update alunos 
                                            set nome = @nome,
                                                sobrenome = @sobrenome, 
                                                telefone = @telefone, 
                                                ra = @ra
                                           where id = @id";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
                comandUpdate.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
                comandUpdate.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
                comandUpdate.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("ra", aluno.Ra);
                comandUpdate.Parameters.Add(paramRa);

                IDbDataParameter paramId = new SqlParameter("id", aluno.Id);
                comandUpdate.Parameters.Add(paramId);

                comandUpdate.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            
        }

        public void DeletarAlunoDb(int id)
        {
            try
            {
                var comandDelete = conexao.CreateCommand();
                comandDelete.CommandText = @"delete alunos                                             
                                           where id = @id";

                IDbDataParameter paramId = new SqlParameter("id", id);
                comandDelete.Parameters.Add(paramId);

                comandDelete.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            
        }
    }
}