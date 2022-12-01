using RestSharp;

namespace ConSelenium.API.Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task WhenUserLoginShouldTokenReturned()
        {
            //Arrange
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("api/v1/account/login");
            request.AddBody(new
            {
                UserName = "admin",
                Password = "admin"
            });

            // Act
            var response = await client.ExecutePostAsync(request);

            /*
            var response = client.PostAsync(request); //tutaj dodajemy await
            var response = client.ExecutePost(request); wys³anie posta
            var response = client.Execute(request);
            var response = client.Post<NameClass>(request); deserializacja do obiektu
            var response = client.Post(request); wys³anie posta - bez wyj¹tku
            var response = client.PostAsync<NameClass>(request); deserializacja do obiektu
            var response = client.ExecutePostAsync<NameClass>(); deserializacja do obiektu
            */

            //Assert
            Assert.NotNull(response.Content);
        }

        [Test]
        public void Test2()
        {
            Assert.Pass();
        }

        [TearDown] //wywo³ywane po ka¿dym zakoñczonym teœcie
        public void TearDown()
        {
            //Assert.Pass();
        }

        [OneTimeTearDown] //wywo³ywane po zakoñczeniu wszystkich testów
        public void OneTimeTearDown()
        {
            Assert.Pass();
        }
    }
}