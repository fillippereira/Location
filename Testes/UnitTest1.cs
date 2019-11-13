using NUnit.Framework;
using Location.Models;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TipoTeste()
        {
            var NovoTipo = new Tipo
            {
                TipoImovel = "Tipo Tste"
            };

            Assert.Pass();
        }

     
    }
}