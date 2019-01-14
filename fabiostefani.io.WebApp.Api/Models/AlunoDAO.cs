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

        public List<Alunos> ListarAlunosDb()
        {
            //string stringConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Desenv\Estudos\DotNet\CursoWebApi_Udemy\fabiostefani.io.WebApp\fabiostefani.io.WebApp.Api\App_Data\Database.mdf;Integrated Security=True";
            //string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from alunos";

            IDataReader resultado = selectCmd.ExecuteReader();

            var listaAlunos = new List<Alunos>();
            while (resultado.Read())
            {
                var aluno = new Alunos();
                aluno.Id = Convert.ToInt32(resultado["Id"]);
                aluno.Nome = Convert.ToString(resultado["Nome"]);
                aluno.Sobrenome = Convert.ToString(resultado["Sobrenome"]);
                aluno.Telefone = Convert.ToString(resultado["Telefone"]);
                aluno.Ra = Convert.ToInt32(resultado["Ra"]);
                listaAlunos.Add(aluno);
            }
            conexao.Close();
            return listaAlunos;
        }

        public void InserirAlunoDb(Alunos aluno)
        {
            IDbCommand comandInsert = conexao.CreateCommand();
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
    }
}