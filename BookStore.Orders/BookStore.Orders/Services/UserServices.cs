using BookStore.Book.Entity;
using BookStore.Orders.Interface;
using BookStore.Orders.Model;
using BookStore.User.Interface;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Orders.Services
{
    public class UserServices:IUser
    {
        public  async Task<BookEntity> GetBookDetailsById(int bookId)
        {

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://localhost:44393/api/Book/GetBookById?id=" + bookId);

            if (response.IsSuccessStatusCode) 
            {
                string strResponse = await response.Content.ReadAsStringAsync();

                ResponseModel apiResponse = JsonConvert.DeserializeObject<ResponseModel>(strResponse);
                BookEntity book = JsonConvert.DeserializeObject<BookEntity>(apiResponse.Data.ToString());
                return book;
            }
            else
                return null;
        }

        //public async Task<UserEntity> GetUser(string token)
        //{
        //    HttpClient client = new HttpClient();

        //}
        public async Task<UserEntity> GetUser(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await client.GetAsync("https://localhost:44384/api/User/GetUserById");  // Replace with the correct API endpoint.
            if (response.IsSuccessStatusCode)
            {
                string strResponse = await response.Content.ReadAsStringAsync();
                ResponseModel apiResponse = JsonConvert.DeserializeObject<ResponseModel>(strResponse);

                UserEntity user = JsonConvert.DeserializeObject<UserEntity>(apiResponse.Data.ToString());
                return user;
            }
            else
            {
                return null;
            }
        }

    }
}
