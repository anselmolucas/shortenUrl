using B2EGroup.ShortenUrl.Dal.Repositories;
using B2EGroup.ShortenUrl.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace B2EGroup.ShortenUrl.WebService.Controllers
{
    public class UrlsController : ApiController
    {
        RepositoryUrl repositoryUrl = new RepositoryUrl(true);

        /// <summary>
        /// GET: /api/urls/[shortUrl]
        /// Endpoint 1: Deve retornar um 301 redirect para o endereço original da URL.
        /// Caso o id não existe no sistema, o retorno deverá ser um 404 Not Found.
        /// </summary>
        /// <param name="id">string com a shortUrl</param>
        /// <returns>retorna a url original (longUrl)</returns>
        [ResponseType(typeof(Url))]
        public IHttpActionResult GetUrl(string id)
        {            
            Url url = repositoryUrl.UrlHitsUpdate(shortUrl: id);

            if (url == null)
                return NotFound();

            return Ok(url.LongUrl);
        }

        /// <summary>
        /// DELETE: api/Urls/[shortUrl]
        /// Endpoint 6: Apaga uma URL do sistema. Deverá retornar vazio em caso de sucesso.
        /// </summary>
        /// <param name="id">shortUrl</param>
        [ResponseType(typeof(Url))]
        public IHttpActionResult DeleteUrl(string id)
        {           
            Url url = repositoryUrl.SearchUrl(urlToSearch: id, fieldToSearch:"shorturl");

            if (url == null)
                return NotFound();

            repositoryUrl.Delete(idToDelete: url.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }        
    }
}