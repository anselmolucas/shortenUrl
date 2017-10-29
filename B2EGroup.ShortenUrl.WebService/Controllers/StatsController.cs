using B2EGroup.ShortenUrl.Business;
using B2EGroup.ShortenUrl.Dal.Repositories;
using B2EGroup.ShortenUrl.Models;
using B2EGroup.ShortenUrl.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace B2EGroup.ShortenUrl.WebService.Controllers
{
    public class StatsController : ApiController
    {
        RepositoryUrl repositoryUrl = new RepositoryUrl(true);

        /// <summary>
        /// GET: /api/stats
        /// Endpoint 3: Retorna estatísticas globais do sistema
        /// </summary>
        /// <returns>estatíticas e top 10 urls</returns>
        public StatusResult GetUrls() //
        {
            //return repositoryUser.Statistics(null);
            return Statistics.Summarize(null);
        }

        /// <summary>
        /// GET: /api/stats/[shortUrl]
        /// Endpoint 5: Retorna estatísticas de uma URL específica 
        /// </summary>
        /// <param name="id">shortUrl</param>
        /// <returns>o registro da url com o número de hits (short url)</returns>
        [ResponseType(typeof(Url))]
        public IHttpActionResult GetUrl(string id)
        {
            Url url = repositoryUrl.UrlHitsUpdate(shortUrl: id);

            if (url == null)
                return NotFound();

            return Ok(url);
        }        
    }
}