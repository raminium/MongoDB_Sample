using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB_Sample.Models;
using MongoDB_Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace MongoDB_Sample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogsService _logsService;

        public LogsController(LogsService logsService) =>
            _logsService = logsService;

        [HttpGet]
        public async Task<List<Log>> Get() =>
            await _logsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Log>> Get(string id)
        {
            var log = await _logsService.GetAsync(id);

            if (log is null)
            {
                return NotFound();
            }

            return log;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Log newLog)
        {
            await _logsService.CreateAsync(newLog);

            return CreatedAtAction(nameof(Get), new { id = newLog.Id }, newLog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog()
        {
            DateTime totTimeFrom = DateTime.Now;
            DateTime InsertTime;
            string strOut = "";

            for (int i = 0; i < 200; i++)
            {
                Log newLog = new Log();
                newLog.APIName = "Test API Log";
                newLog.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                newLog.MethodName = "No Functional Method";
                newLog.NoteID = i + 1;
                newLog.RequestDate = DateTime.Now;
                //System.Threading.Thread.Sleep(40);
                newLog.ResponseDate = DateTime.Now;
                newLog.RT = (newLog.ResponseDate - newLog.RequestDate).TotalMilliseconds;

                InsertTime = DateTime.Now;
                await _logsService.CreateAsync(newLog);
                strOut += (DateTime.Now - InsertTime).TotalMilliseconds.ToString() + Environment.NewLine;
            }

            strOut += "Totaly inserted 200 records in " + ((DateTime.Now - totTimeFrom).TotalSeconds.ToString()) + " seconds";
            return CreatedAtAction(nameof(Get), new { CycleCount = 50 }, strOut);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManyLog()
        {
            DateTime totTimeFrom = DateTime.Now;
            DateTime InsertTime;
            List<Log> logs = new List<Log>();
            string strOut = "";

            for (int i = 0; i < 200; i++)
            {
                Log newLog = new Log();
                newLog.APIName = "Test API Log";
                newLog.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                newLog.MethodName = "No Functional Method";
                newLog.NoteID = i + 1;
                newLog.RequestDate = DateTime.Now;
                //System.Threading.Thread.Sleep(40);
                newLog.ResponseDate = DateTime.Now;
                newLog.RT = (newLog.ResponseDate - newLog.RequestDate).TotalMilliseconds;

                logs.Add(newLog);
            }

            InsertTime = DateTime.Now;
            await _logsService.CreateManyAsync(logs);
            strOut += (DateTime.Now - InsertTime).TotalMilliseconds.ToString() + Environment.NewLine;

            strOut += "Totaly inserted 200 records in " + ((DateTime.Now - totTimeFrom).TotalSeconds.ToString()) + " seconds";
            return CreatedAtAction(nameof(Get), new { CycleCount = 200 }, strOut);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBulkLog()
        {
            DateTime totTimeFrom = DateTime.Now;
            DateTime InsertTime;
            List<Log> logs = new List<Log>();
            string strOut = "";

            for (int i = 0; i < 200; i++)
            {
                Log newLog = new Log();
                newLog.APIName = "Test API Log";
                newLog.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                newLog.MethodName = "No Functional Method";
                newLog.NoteID = i + 1;
                newLog.RequestDate = DateTime.Now;
                //System.Threading.Thread.Sleep(40);
                newLog.ResponseDate = DateTime.Now;
                newLog.RT = (newLog.ResponseDate - newLog.RequestDate).TotalMilliseconds;

                logs.Add(newLog);
            }

            InsertTime = DateTime.Now;
            await _logsService.CreateBulkAsync(logs);
            strOut += (DateTime.Now - InsertTime).TotalMilliseconds.ToString() + Environment.NewLine;

            strOut += "Totaly inserted 200 records in " + ((DateTime.Now - totTimeFrom).TotalSeconds.ToString()) + " seconds";
            return CreatedAtAction(nameof(Get), new { CycleCount = 200 }, strOut);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Log updatedLog)
        {
            var book = await _logsService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedLog.Id = book.Id;

            await _logsService.UpdateAsync(id, updatedLog);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _logsService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _logsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
