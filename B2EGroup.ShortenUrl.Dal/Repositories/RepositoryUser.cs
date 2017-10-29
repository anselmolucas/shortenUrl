using B2EGroup.ShortenUrl.Dal.Interfaces;
using B2EGroup.ShortenUrl.Models;
using System.Linq;

namespace B2EGroup.ShortenUrl.Dal.Repositories
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser 
    {
        public RepositoryUser(bool SaveChanges = true) : base(SaveChanges)
        {
        }
        
        public User SearchUserName(string userName)
        {
            using (var _contexto = new ShortenUrlContext())
            {
                return (
                        from u in _contexto.Users
                        where u.Name == userName.ToLower().Trim()
                        select u
                        ).FirstOrDefault();
            }
        }

        public Url AddANewUrlOnlyIfTheInformedLongUrlNotAlredyRegisteredForThatUser(User userObj, string longUrl)
        {
            Url urlVerify = null;
            RepositoryUrl repositoryUrl = new RepositoryUrl(true);

            urlVerify = CheckUrlDuplicity(userId:userObj.Id, longUrl:longUrl);

            if (urlVerify != null)
                return null;

            return repositoryUrl.PrepareObjectToAddNewUrl(longUrl: longUrl, userId: userObj.Id);                                      
        }

        private static Url CheckUrlDuplicity(int userId, string longUrl)
        {
            Url urlVerify;
            using (var _contexto = new ShortenUrlContext())
            {
                urlVerify = (from u in _contexto.Urls
                             where u.UserId == userId && u.LongUrl == longUrl
                             select u
                            ).FirstOrDefault();
            }

            return urlVerify;
        }               
    }
}
