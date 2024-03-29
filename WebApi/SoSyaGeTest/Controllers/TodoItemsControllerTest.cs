﻿using SoSyaGeApp;
using SoSyaGeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace SoSyaGeAppTest.Controllers
{
    public class TodoItemsControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public TodoItemsControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test()
        {
            Debug.WriteLine("Test");

            var todoItem = new TodoItem
            {
                Id = 1, // 更新するTodoItemのIDを設定
                Name = "Updated Name", // 新しい名前
                IsComplete = true // 新しい状態
            };

            // Arrange
            var client = _factory.CreateClient();

            // Post
            var url = "api/TodoItems";
            //var response = await client.GetAsync(url);
            var response = await client.PostAsJsonAsync(url, todoItem);
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode); // ステータスコードが201であることを確認
            var responseString = await response.Content.ReadAsStringAsync();

            // Get
            url = "api/TodoItems/1";
            response = await client.GetAsync(url);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();

            // Put
            todoItem.Name = "namePut";
            url = "api/TodoItems/1";
            //var response = await client.GetAsync(url);
            response = await client.PutAsJsonAsync(url, todoItem);
            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Delete
            url = "api/TodoItems/1";
            response = await client.DeleteAsync(url);
            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();

            // Get
            url = "api/TodoItems/1";
            response = await client.GetAsync(url);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();

            // Delete
            url = "api/TodoItems/1";
            response = await client.DeleteAsync(url);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();

            Debug.WriteLine("Test end");

        }
    }
}
