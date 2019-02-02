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
                return Ok(new Alunos().ListarAlunos(null));
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
                return Ok(new Alunos().ListarAlunos(null));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET: api/Alunos/5
        [HttpGet]
        [Route("Recuperar/{id:int}/{nome?}")]
        public IHttpActionResult Get(int id, string nome = null)
        {
            try
            {
                return Ok(new Alunos().ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // GET: api/Alunos/5
        [HttpGet]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                var aluno = new Alunos();
                IEnumerable<AlunoDto> alunos = aluno.ListarAlunos(null).Where(x => x.Data == data || x.Nome == nome);
                if (!alunos.Any())
                {
                    return NotFound();
                }
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [HttpPost]
        // POST: api/Alunos
        public IHttpActionResult Post(AlunoDto aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var alunos = new Alunos();
                alunos.Inserir(aluno);
                return Ok(alunos.ListarAlunos(null));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }

        [HttpPut]
        // PUT: api/Alunos/5
        public IHttpActionResult Put(int id, [FromBody]AlunoDto aluno)
        {
            try
            {
                var alunos = new Alunos();
                aluno.Id = id;
                alunos.Atualizar(aluno);
                return Ok(alunos.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }

        [HttpDelete]
        // DELETE: api/Alunos/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                new Alunos().Deletar(id);
                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }
    }
}
