using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;
using SourceData.Models;

namespace SourceData.Controllers
{
    [ApiController, Route("sourcedata/zasoby"), Produces("application/json")]
    public class ZasobyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ZasobyModel>> Get()
        {
            var result = Database.Zasoby.Values;
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ZasobyModel> Post([FromBody] ZasobyModel item)
        {
            if (!Database.Zasoby.TryAdd(item.Id, item))
            {
                return ValidationProblem($"Záznam s id '{item.Id}' se nepodařilo uložit.");
            }
            Database.Zasoby.TryGetValue(item.Id, out var result);
            Log.Logger.Information(" ----> Added: {item}", item);

            return Ok(result);
        }

        [HttpPut]
        public ActionResult<ZasobyModel> Put([FromBody] ZasobyModel item)
        {
            if (Database.Zasoby.TryGetValue(item.Id, out var result))
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    result.Name = item.Name;
                }

                if (item.SkladId != 0)
                {
                    result.SkladId = item.SkladId;
                }
            }
            else
            {
                return ValidationProblem($"Záznam s id '{item.Id}' se nepodařilo updatovat.");
            }

            Log.Logger.Information(" ----> Updated: {result}", result);

            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<ZasobyModel> Delete(int id)
        {
            Database.Zasoby.TryRemove(id, out var deleted);

            Log.Logger.Information(" ----> Updated: {deleted}", deleted);

            return Ok();
        }
    }
}
