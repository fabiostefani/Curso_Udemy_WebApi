using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            try
            {
                return new AlunoDAO().ListarAlunosDb();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar alunos. Erro => {ex.Message} ");
            }
            
        }

        //public List<Alunos> ListarAlunos()
        //{
        //    string caminhoArquivo = RecuperarCaminhoBancoDados();
        //    string json = File.ReadAllText(caminhoArquivo);
        //    return JsonConvert.DeserializeObject<List<Alunos>>(json);
        //}





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

        public void Inserir(Alunos aluno)
        {
            //var listaAluno = ListarAlunos();
            //var maxId = listaAluno.Max(x => x.Id) + 1;
            //aluno.Id = maxId;
            //listaAluno.Add(aluno);
            //ReescreverArquivo(listaAluno);
            //return aluno;

            try
            {
                new AlunoDAO().InserirAlunoDb(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir alunos. Erro => {ex.Message} ");
            }
        }

        public void Atualizar(Alunos aluno)
        {
            try
            {
                new AlunoDAO().AtualizarAlunoDb(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o aluno. Erro => {ex.Message} ");
            }
        }

        public void Deletar(int id)
        {
            //var listaAluno = ListarAlunos();
            //var itemIndex = listaAluno.FindIndex(x => x.Id == id);
            //if (itemIndex >= 0)
            //{

            //    listaAluno.RemoveAt(itemIndex);
            //}
            //else
            //{
            //    return false;
            //}
            //ReescreverArquivo(listaAluno);
            //return true;
            try
            {
                new AlunoDAO().DeletarAlunoDb(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o aluno. Erro => {ex.Message} ");
            }
        }
    }
}