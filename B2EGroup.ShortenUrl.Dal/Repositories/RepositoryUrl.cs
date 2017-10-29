using B2EGroup.ShortenUrl.Apps;
using B2EGroup.ShortenUrl.Dal.Interfaces;
using B2EGroup.ShortenUrl.Models;
using System.Linq;

namespace B2EGroup.ShortenUrl.Dal.Repositories
{
    public class RepositoryUrl : RepositoryBase<Url>, IRepositoryUrl
    {
        public RepositoryUrl(bool SaveChanges = true) : base(SaveChanges)
        {
        }
        
        public Url SearchUrl(string urlToSearch, string fieldToSearch = "shorturl")
        {
            using (var _contexto = new ShortenUrlContext())
            {
                Url retorno = null;

                if (fieldToSearch.ToLower() == "shorturl")
                    retorno =   (from u in _contexto.Urls
                                      where u.ShortUrl == urlToSearch.ToLower().Trim()
                                      select u
                                ).FirstOrDefault();

                else if (fieldToSearch.ToLower() == "longurl")
                    retorno = (from u in _contexto.Urls
                               where u.LongUrl == urlToSearch.ToLower().Trim()
                               select u
                                ).FirstOrDefault();

                return retorno;
            }
        }

        public Url UrlHitsUpdate(string shortUrl)
        {
            RepositoryUrl repositoryUrl = new RepositoryUrl(true);

            Url url = SearchUrl(urlToSearch:shortUrl, fieldToSearch:"shorturl");

            if (url != null)
            {
                url.Hits = url.Hits + 1;
                repositoryUrl.Save(objectToSave: url);
            }

            return url;
        }

        public Url PrepareObjectToAddNewUrl(string longUrl, int userId)
        {            
            Url urlObjNew = new Url()
            {
                LongUrl = longUrl,
                ShortUrl = HashCode.Random(4),
                UserId = userId,
                Hits = 1
            };

            return Save(objectToSave:urlObjNew);
        }
    }
}
