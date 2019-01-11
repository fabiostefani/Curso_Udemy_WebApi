using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace fabiostefani.io.WebApp.Api.Models
{
    public class Alunos
    {
        public Alunos(int id, string nome, string sobrenome, string telefone, int ra, string data)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
            Ra = ra;
            Data = data;
        }
        public Alunos()
        {

        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public int Ra { get; set; }
        public string Data { get; set; }

        public List<Alunos> ListarAlunos()
        {
            string caminhoArquivo = RecuperarCaminhoBancoDados();
            string json = File.ReadAllText(caminhoArquivo);
            return JsonConvert.DeserializeObject<List<Alunos>>(json);
        }

        

        public List<Alunos> ListarAlunosDb()
        {
            //string stringConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Desenv\Estudos\DotNet\CursoWebApi_Udemy\fabiostefani.io.WebApp\fabiostefani.io.WebApp.Api\App_Data\Database.mdf;Integrated Security=True";
            //string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
            string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
            IDbConnection conexao;

            conexao = new SqlConnection(stringConexao);
            conexao.Open();
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

        private bool ReescreverArquivo(List<Alunos> listaAlunos)
        {
            string caminhoArquivo = RecuperarCaminhoBancoDados();
            string json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        private static string RecuperarCaminhoBancoDados()
        {
            return HostingEnvironment.MapPath(@"~/App_Data/Base.json");
        }

        public Alunos Inserir(Alunos aluno)
        {
            var listaAluno = ListarAlunos();
            var maxId = listaAluno.Max(x => x.Id) + 1;
            aluno.Id = maxId;
            listaAluno.Add(aluno);
            ReescreverArquivo(listaAluno);
            return aluno;
        }

        public Alunos Atualizar(int id, Alunos aluno)
        {
            var listaAluno = ListarAlunos();
            var itemIndex = listaAluno.FindIndex(x => x.Id == aluno.Id);
            if (itemIndex >= 0)
            {
                aluno.Id = id;
                listaAluno[itemIndex] = aluno;
            }
            else
            {
                return null;
            }
            ReescreverArquivo(listaAluno);
            return aluno;
        }

        public bool Deletar(int id)
        {
            var listaAluno = ListarAlunos();
            var itemIndex = listaAluno.FindIndex(x => x.Id == id);
            if (itemIndex >= 0)
            {

                listaAluno.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }
            ReescreverArquivo(listaAluno);
            return true;
        }
    }
}