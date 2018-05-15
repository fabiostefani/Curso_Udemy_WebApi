using fabiostefani.io.WebApp.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace fabiostefani.io.WebApp.Api.Controllers
{
    public class AlunosController : ApiController
    {
        // GET: api/Alunos
        public IEnumerable<Alunos> Get()
        {            
            return new Alunos().ListarAlunos();            
        }

        // GET: api/Alunos/5
        public IHttpActionResult Get(int id)
        {
            return Ok(new Alunos().ListarAlunos().FirstOrDefault(x => x.Id == id));
        }

        // POST: api/Alunos
        public List<Alunos> Post(Alunos aluno)
        {
            var alunos = new Alunos();
            alunos.Inserir(aluno);
            return alunos.ListarAlunos();
        }

        // PUT: api/Alunos/5
        public Alunos Put(int id, [FromBody]Alunos aluno)
        {
            return new Alunos().Atualizar(id, aluno);
        }

        // DELETE: api/Alunos/5
        public void Delete(int id)
        {
            new Alunos().Deletar(id);
        }
    }
}
