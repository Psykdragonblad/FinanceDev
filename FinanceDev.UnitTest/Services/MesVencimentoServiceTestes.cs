using FinanceDev.Application.Services;
using FinanceDev.Domain.Entities;
using FinanceDev.Domain.Interface.Repository;
using FinanceDev.Domain.Shared;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDev.UnitTest.Services
{
    [TestFixture]
    public class MesVencimentoServiceTestes
    {
        private Mock<IMesVencimentoRepository> _repoMock;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IMesVencimentoRepository>();
        }

        [Test]
        public async Task GetAll_ReturnsFail_WhenEmpty()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(new List<MesVencimento>());
            var service = new MesVencimentoService(_repoMock.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Não foi encontrado a listagem de vencimentos", result.Message);
        }

        [Test]
        public async Task GetAll_ReturnsOk_WhenHasItems()
        {
            // Arrange
            var items = new List<MesVencimento> { new MesVencimento { Id = 1, Codigo = "A", Mes = "Janeiro" } };
            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(items);
            var service = new MesVencimentoService(_repoMock.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsTrue(result.Success);
            var list = result.Data?.ToList();
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("A", list[0].Codigo);
        }
    }
}
