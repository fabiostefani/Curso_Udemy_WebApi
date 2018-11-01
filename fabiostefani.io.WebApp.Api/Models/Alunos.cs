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
        public Alunos(int id, string nome, string sobrenome, string telefone, int ra, DateTime data)
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
        public DateTime Data { get; set; }

        public List<Alunos> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = File.ReadAllText(caminhoArquivo);
            var listaAlunos = JsonConvert.DeserializeObject<List<Alunos>>(json);
            return listaAlunos;
        }

        private bool ReescreverArquivo(List<Alunos> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);

            return true;
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