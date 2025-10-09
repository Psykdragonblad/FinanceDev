using FinanceDev.API.Controllers;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Service;
using FinanceDev.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDev.UnitTest.Controllers
{
    [TestFixture]
    public class MesVencimentoControllerTestes
    {
        private Mock<IMesVencimentoService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IMesVencimentoService>();
        }

        [Test]
        public void GetAll_ReturnsOk_WithFailResult()
        {
            // Arrange
            var fail = ResultResponse<IEnumerable<MesVencimento>>.Fail("Erro");
            _serviceMock.Setup(s => s.GetAll()).ReturnsAsync(fail);

            var controller = new MesVencimentoController(_serviceMock.Object);

            // Act
            var actionResult = controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var ok = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(ok);
            // The controller returns the Task<ResultResponse<...>> as value, so unwrap
            var task = ok.Value as Task<ResultResponse<IEnumerable<MesVencimento>>>;
            Assert.IsNotNull(task);
            Assert.IsFalse(task.Result.Success);
            Assert.AreEqual("Erro", task.Result.Message);
        }

        [Test]
        public void GetAll_ReturnsOk_WithOkResult()
        {
            // Arrange
            var items = new List<MesVencimento> { new MesVencimento { Id = 1, Codigo = "A", Mes = "Jan" } };
            var okResult = ResultResponse<IEnumerable<MesVencimento>>.Ok(items);
            _serviceMock.Setup(s => s.GetAll()).ReturnsAsync(okResult);

            var controller = new MesVencimentoController(_serviceMock.Object);

            // Act
            var actionResult = controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var ok = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(ok);
            var task = ok.Value as Task<ResultResponse<IEnumerable<MesVencimento>>>;
            Assert.IsNotNull(task);
            Assert.IsTrue(task.Result.Success);
            var data = task.Result.Data?.ToList();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count);
        }
    }
}
