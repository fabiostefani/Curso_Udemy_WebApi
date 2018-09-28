using fabiostefani.io.WebApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace fabiostefani.io.WebApp.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Alunos")]
    public class AlunosController : ApiController
    {
        // GET: api/Alunos
        //[HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(new Alunos().ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                return Ok(new Alunos().ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

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
