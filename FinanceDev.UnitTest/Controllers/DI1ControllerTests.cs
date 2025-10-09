using FinanceDev.Application.DTO;
using FinanceDev.Application.Interface;
using FinanceDev.Controllers;
using FinanceDev.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.UnitTest.Controllers
{
    [TestFixture]
    public class DI1ControllerTests
    {
        private readonly Mock<IDI1CurvaService> _curvaServiceMock;
        private readonly DI1Controller _controller;

        public DI1ControllerTests()
        {
            _curvaServiceMock = new Mock<IDI1CurvaService>();
            _controller = new DI1Controller(_curvaServiceMock.Object);
        }

        [Test]
        public async Task GetAction_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var request = new GerarCargaRequest(DateTime.Today);
            var serviceResponse = ResultResponse<IEnumerable<DI1CurvaDto>>.Ok(new List<DI1CurvaDto>());

            _curvaServiceMock.Setup(s => s.GetByDataAsync(request.Data))
                             .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAction(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreSame(serviceResponse, okResult.Value);
        }

    [Test]
    public async Task GetAction_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var request = new GerarCargaRequest(DateTime.Today);
            var serviceResponse = ResultResponse<IEnumerable<DI1CurvaDto>>.Fail("Erro");

            _curvaServiceMock.Setup(s => s.GetByDataAsync(request.Data))
                             .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAction(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequest = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreSame(serviceResponse, badRequest.Value);
        }

    [Test]
    public async Task GerarCarga_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var request = new GerarCargaRequest(DateTime.Today);
            var serviceResponse = ResultResponse.Ok();

            _curvaServiceMock.Setup(s => s.Add(request.Data))
                             .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GerarCarga(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreSame(serviceResponse, okResult.Value);
        }

    [Test]
    public async Task GerarCarga_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var request = new GerarCargaRequest(DateTime.Today);
            var serviceResponse = ResultResponse.Fail("Erro");

            _curvaServiceMock.Setup(s => s.Add(request.Data))
                             .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GerarCarga(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequest = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreSame(serviceResponse, badRequest.Value);
        }

    [Test]
    public async Task GetCurva_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var request = new GerarCargaRequest(DateTime.Today);
            var dto = new DI1CurvaRelatorioDto(0.0, 0.0, 0.0, 0);
            var serviceResponse = ResultResponse<IEnumerable<DI1CurvaRelatorioDto>>.Ok(new List<DI1CurvaRelatorioDto> { dto });

            _curvaServiceMock.Setup(s => s.CurvaDI1(request.Data))
                             .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetCurva(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreSame(serviceResponse, okResult.Value);
        }

    [Test]
    public async Task GetCurva_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var request = new GerarCargaRequest(DateTime.Today);
            var serviceResponse = ResultResponse<IEnumerable<DI1CurvaRelatorioDto>>.Fail("Erro");

            _curvaServiceMock.Setup(s => s.CurvaDI1(request.Data))
                             .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetCurva(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequest = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreSame(serviceResponse, badRequest.Value);
        }
    }
}
