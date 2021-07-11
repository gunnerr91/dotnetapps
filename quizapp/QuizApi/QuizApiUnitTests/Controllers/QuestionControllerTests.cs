using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using QuizApi.Controllers;
using QuizApi.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuizApiUnitTests.Controllers
{
    public class QuestionControllerTests
    {
        [Test]
        public async Task Post_Returns_Bad_Request_By_Default()
        {
            var controller = new QuestionController();
            var request = new QuestionRequest();

            var result = await controller.Post(request) as BadRequestResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }

    
}
