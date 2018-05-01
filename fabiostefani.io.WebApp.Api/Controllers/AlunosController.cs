using fabiostefani.io.WebApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fabiostefani.io.WebApp.Api.Controllers
{
    public class AlunosController : ApiController
    {
        // GET: api/Alunos
        public IEnumerable<string> Get()
        {
            return new string[] { "Fabio", "Evelini" };
        }

        // GET: api/Alunos/5
        public string Get(int id)
        {
            var alunos = new Alunos();
            return alunos.Get();
        }

        // POST: api/Alunos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Alunos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Alunos/5
        public void Delete(int id)
        {
        }
    }
}
