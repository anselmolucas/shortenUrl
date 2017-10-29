using B2EGroup.ShortenUrl.Business;
using B2EGroup.ShortenUrl.Dal.Repositories;
using B2EGroup.ShortenUrl.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace B2EGroup.ShortenUrl.WebService.Controllers
{
    public class UsersController : ApiController
    {
        RepositoryUser repositoryUser = new RepositoryUser(true);

        /// <summary>
        /// POST api/users?id=[UserId]&url=[LongUrl]
        /// Endpoint 2: Cadastra uma nova url no sistema
        /// exemplo: http://localhost:60691/api/users?id=anselmo&url=http://www.estadao.com.br/
        /// </summary>
        /// <param name="id">nome do usuário</param>
        /// <param name="url">longUrl para encurtar</param>
        [ResponseType(typeof(Url))]
        public IHttpActionResult PostUsers(string id, string url)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = repositoryUser.SearchUserName(userName:id);

            if(user == null)
                return StatusCode(HttpStatusCode.NotFound);

            Url urlAdd = repositoryUser.AddANewUrlOnlyIfTheInformedLongUrlNotAlredyRegisteredForThatUser(userObj:user, longUrl:url);

            if (urlAdd == null)
                return StatusCode(HttpStatusCode.Conflict);
           
            return CreatedAtRoute("DefaultApi", new { id = id }, urlAdd);                      
        }

        /// <summary>
        /// GET api/users/[userName]
        /// Endpoint 4: Retorna estatísticas das urls de um usuário. O resultado é o mesmo que GET /stats mas com o escopo dentro de um usuário. Caso o usuário não exista o retorno deverá ser com código 404 Not Found
        /// </summary>
        /// <param name="id">userName</param>
        /// <returns>json com estatíticas do usuário (hists, qtd de urls cadastradas, top10 urls)</returns>
        public IHttpActionResult GetUrls(string id)
        {
            User user = repositoryUser.SearchUserName(userName: id);

            if (user == null)
                return NotFound();

            return Ok(Statistics.Summarize(userObj: user));
        }

        /// <summary>
        /// POST api/users
        /// Endpoint 7: Cria um usuário. O conteúdo do request deverá ser com código 201 Created e retornar um objeto com o conteúdo no seguinte formato.Caso já exista um usuário com o mesmo id retornar código 409 Conflict
        /// Requisição: { "id": "jibao" }
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ResponseType(typeof(Url))]
        public IHttpActionResult PostUsers(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User userVerify = repositoryUser.SearchUserName(userName: user.Name);

            if (userVerify != null || user.Id > 0)
                return StatusCode(HttpStatusCode.Conflict);

            user.Name = user.Name.ToLower().Trim();

            repositoryUser.Save(objectToSave:user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        /// <summary>
        /// DELETE api/users/[userName]
        /// Endpoint 8: Apaga um usuário.
        /// </summary>
        /// <param name="id">userName</param>
        public IHttpActionResult DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("o id deve ser preenchido");

            User user = repositoryUser.SearchUserName(userName: id);

            if (user == null)
                return NotFound();

            repositoryUser.Delete(idToDelete:user.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
