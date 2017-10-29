using B2EGroup.ShortenUrl.Dal.Repositories;
using B2EGroup.ShortenUrl.Models;
using B2EGroup.ShortenUrl.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace B2EGroup.ShortenUrl.Business
{
    public class Statistics
    {
        /// <summary>
        /// sumarizar as informações do site (estatísticas)
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public static StatusResult Summarize(User userObj)
        {
            List<Url> urls = null;
            RepositoryUrl repositoryUrl = new RepositoryUrl(true);

            int hits, urlsCount = 0;
            StatusResult statusResult = null;
            List<Url> topUrls = null;

            using (var _contexto = new ShortenUrlContext())
            {
                if (userObj == null)
                    urls = repositoryUrl.SelectAll();
                else
                    urls = _contexto.Urls.Where(h => h.UserId == userObj.Id).ToList();

                if (urls != null)
                {
                    hits = urls.Sum(h => h.Hits);
                    urlsCount = urls.Count();
                    topUrls = urls.OrderByDescending(h => h.Hits).Take(10).ToList();

                    statusResult = new StatusResult()
                    {
                        Hits = hits,
                        UrlsCount = urlsCount,
                        Urls = topUrls
                    };
                }
            }

            return statusResult;
        }
    }
}
