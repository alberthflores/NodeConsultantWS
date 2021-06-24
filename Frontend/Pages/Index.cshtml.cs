using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoLayer;
using System.Net.Http;
using System.Net;
using System.Text.Json;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<NodeDto> Dtos { get; set; }
        public string DtosJson { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var url = "https://localhost:44393/api/Node";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseApi = JsonSerializer.Deserialize<ResponseApi<List<NodeDto>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
                    Dtos = responseApi.Content;
                    DtosJson =  JsonSerializer.Serialize(Dtos);
                }
            }
            return Page();
        }
    }
}
