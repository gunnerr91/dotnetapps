using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(QuestionRequest request)
        {
            return BadRequest();
        }
    }
}
