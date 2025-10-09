using ClosedXML.Excel;
using FinanceDev.Application.DTO;
using FinanceDev.Application.Services;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDev.UnitTest.Services
{
    [TestFixture]
    public class DI1ServiceTestes
    {
        private Mock<IDI1CurvaRepository> _di1RepoMock;
        private Mock<IReferenciaCurvaRepository> _refRepoMock;
        private Mock<IFeriadoRepository> _feriadoRepoMock;
        private Mock<IMesVencimentoRepository> _mesRepoMock;
        private Mock<IConfiguration> _configMock;

        [SetUp]
        public void Setup()
        {
            _di1RepoMock = new Mock<IDI1CurvaRepository>();
            _refRepoMock = new Mock<IReferenciaCurvaRepository>();
            _feriadoRepoMock = new Mock<IFeriadoRepository>();
            _mesRepoMock = new Mock<IMesVencimentoRepository>();
            _configMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task GetByDataAsync_ReturnsFail_WhenNoData()
        {
            // Arrange
            var date = DateTime.Today;
            _di1RepoMock.Setup(r => r.GetByDataAsync(date)).ReturnsAsync(new List<DI1Curva>());

            var service = new DI1Service(_di1RepoMock.Object, _refRepoMock.Object, _feriadoRepoMock.Object, _mesRepoMock.Object, _configMock.Object);

            // Act
            var result = await service.GetByDataAsync(date);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Dados não encontrado", result.Message);
        }

        [Test]
        public async Task GetByDataAsync_ReturnsOk_WithMappedDtos()
        {
            // Arrange
            var date = DateTime.Today;
            var items = new List<DI1Curva> { new DI1Curva { Id = 1, Vencimento = "A1", Ajuste = 123.45 } };
            _di1RepoMock.Setup(r => r.GetByDataAsync(date)).ReturnsAsync(items);

            var service = new DI1Service(_di1RepoMock.Object, _refRepoMock.Object, _feriadoRepoMock.Object, _mesRepoMock.Object, _configMock.Object);

            // Act
            var result = await service.GetByDataAsync(date);

            // Assert
            Assert.IsTrue(result.Success);
            var data = result.Data?.ToList();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(1, data[0].Id);
            Assert.AreEqual("A1", data[0].Vencimento);
            Assert.AreEqual(123.45, data[0].Ajuste);
        }

        [Test]
        public async Task Add_ReturnsFail_WhenExcelHasNoRows()
        {
            // Arrange
            var date = new DateTime(2020, 1, 1);
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            try
            {
                var filePath = Path.Combine(tempDir, $"DI1-{date:dd-MM-yyyy}.xlsx");
                // Create an empty workbook
                using (var wb = new XLWorkbook())
                {
                    wb.AddWorksheet("Sheet1");
                    wb.SaveAs(filePath);
                }

                _configMock.Setup(c => c["Arquivos:DI1Curva"]).Returns(tempDir);

                var service = new DI1Service(_di1RepoMock.Object, _refRepoMock.Object, _feriadoRepoMock.Object, _mesRepoMock.Object, _configMock.Object);

                // Act
                var result = await service.Add(date);

                // Assert
                Assert.IsFalse(result.Success);
                StringAssert.Contains("Nenhum dado encontrado", result.Message);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public async Task CurvaDI1_ReturnsOk_WhenDataValid()
        {
            // Arrange
            var date = new DateTime(2024, 10, 1);

            var di1Regs = new List<DI1Curva>
            {
                new DI1Curva { Vencimento = "A25", Ajuste = 1.5 }
            };

            var mesList = new List<MesVencimento> { new MesVencimento { Id = 10, Codigo = "A", Mes = "Outubro" } };
            var feriados = new List<Feriado> { new Feriado { Data = new DateTime(2024, 10, 15), DiaSemana = "3", Nome = "Teste" } };

            _di1RepoMock.Setup(r => r.GetByDataAsync(date)).ReturnsAsync(di1Regs);
            _mesRepoMock.Setup(r => r.GetAll()).ReturnsAsync(mesList);
            _feriadoRepoMock.Setup(r => r.GetAll()).ReturnsAsync(feriados);

            var service = new DI1Service(_di1RepoMock.Object, _refRepoMock.Object, _feriadoRepoMock.Object, _mesRepoMock.Object, _configMock.Object);

            // Act
            var result = await service.CurvaDI1(date);

            // Assert
            Assert.IsTrue(result.Success);
            var list = result.Data?.ToList();
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
            Assert.GreaterOrEqual(list[0].DiasCorridos, 0);
        }
    }
}
