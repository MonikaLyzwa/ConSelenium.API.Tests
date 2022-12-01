using ConSelenium.API.Tests.RequestModels;
using ConSelenium.API.Tests.ResponseModels;
using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;

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
            // 3xA method
            //Arrange
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("api/v1/account/login");
            request.AddBody(new UserCredential
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
        public async Task WhenUserAddsProductShouldBeAdded()
        {

            //Arrange
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("api/v1/account/login");
            request.AddBody(new UserCredential
            {
                UserName = "admin",
                Password = "admin"
            });
            var tokenResponse = await client.ExecutePostAsync<Token>(request);

            var addProductRequest = new RestRequest("api/v1/products");

            var addProduct = new Product
            {
                Name = "Sukienka",
                Description = "Fajna",
                Price = 20.0,
                Stock = 100
            };
            
            addProductRequest.AddBody(addProduct);
            client.Authenticator = new JwtAuthenticator(tokenResponse.Data.AccessToken);

            //Act
            var productResponse = await client.ExecutePostAsync<Created>(addProductRequest);

            //Assert
            var id = productResponse?.Data?.Id;
            var getProductRequest = new RestRequest($"api/v1/products/{id}");
            var getProductResponse = await client.ExecuteGetAsync<ProductResponse>(getProductRequest);
            addProduct.Id = id;
            using (new AssertionScope())
            {
                productResponse?.StatusCode.Should().Be(HttpStatusCode.Created);
                getProductResponse.Data.Should().BeEquivalentTo(addProduct);
            }
        }

        [Test]
        public async Task WhenUserAddsUserShouldBeAdded()
        {
            //Arrange
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("api/v1/account/login");
            request.AddBody(new UserCredential
            {
                UserName = "admin",
                Password = "admin"
            });
            var tokenResponse = await client.ExecutePostAsync<Token>(request);

            var rand = new Random();

            var addUserRequest = new RestRequest("api/v1/users");
            var address = new UserAddress()
            {
                City = "Kraków",
                Street = "D³uga",
                HouseNumber = "5",
                Postalcode = "12345",
            };
          
            var addUser = new User
            {
                Name = "Monika",
                Surname = "Testowa",
                Username = $"monika-{rand.Next()}",
                Password = "Test",
                Email = $"xzyz-{rand.Next()}@xczx.pl",
                Address = address
            };
            addUserRequest.AddBody(addUser);

            client.Authenticator = new JwtAuthenticator(tokenResponse.Data.AccessToken);

            //Act
            var userResponse = await client.ExecutePostAsync<Created>(addUserRequest);

            //Assert
            var id = userResponse?.Data?.Id;
            var getUserRequest = new RestRequest($"api/v1/users/{id}");
            var getUserResponse = await client.ExecuteGetAsync<UserResponse>(getUserRequest);
            using (new AssertionScope())
            {
                userResponse?.StatusCode.Should().Be(HttpStatusCode.Created);
                getUserResponse.Data.Should().BeEquivalentTo(addUser);
            }
        }

        [TearDown] //wywo³ywane po ka¿dym zakoñczonym teœcie
        public void TearDown()
        {
            //Assert.Pass();
        }

        [OneTimeTearDown] //wywo³ywane po zakoñczeniu wszystkich testów
        public void OneTimeTearDown()
        {
            //Assert.Pass();
        }
    }
}