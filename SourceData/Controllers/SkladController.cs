using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SourceData.Models;

namespace SourceData.Controllers
{
    [ApiController, Route("sourcedata/sklad"), Produces("application/json")]
    public class SkladController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<SkladModel>> Get()
        {
            var result = Database.Sklady.Values;
            return Ok(result);
        }
    }
}
