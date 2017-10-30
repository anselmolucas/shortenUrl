# shortenUrl web api
webservice rest (c# web api)

Esse documento visa guiar a instalação do projeto ShortenUrls, mantendo-se aderente ao documento de requisitos apresentado pela equipe da B2E Group.

O projeto trata-se de um web service api padrão no rest, cujo objetivo é receber inputs de urls e retornar uma url curta de 4 caracteres.

Para inserir urls, é necessário antes adicionar um nome de usuário. O nome de usuário cadastrado será usado em conjunto com a url a ser encurtada para se obter a url encurtada.

O acesso será através da url formada pelo endereço ou ip do servidor + o sufixo do controller.


1. EndPoints disponíveis

1.1. Stats

 Usado para se obter dados estatísticos do web service. Métodos:

1.1.1.  [GET]  GET api/Stats  - irá retornar a totalização de hits, o número de urls cadastradas e uma relação com as 10 urls mas visitadas.
Ex: http://localhost:59754/api/stats/

1.1.2. [GET] api/Stats/{id}, onde {id}  é a “shortUrl” a ser pesquisado e irá retornar os dados da url (longUrl, userId e número de hits)
Ex: http://localhost:59754/api/stats/79ys


1.2. Users 

 Cadastramento, consulta, exclusão de usuários e cadastramento de urls.Métodos:

1.2.1. [POST] api/Users/{id}?url={url}
 cadastramento de usuários, onde {id} é o nome do usuário e {url} a url a ser encurtada

Ex: http://localhost:59754/api/users?id=joana&url=https://www.terra.com.br/noticias/

1.2.2. [POST] api/Users, cadastramento de usuários, requer o nome do usuário no corpo da requisição. 

Ex: http://localhost:59754/api/users e no corpo da requisição: { "Name" : "matilda" } – formato json

1.2.3. [GET] api/Users/{id}, consulta para obter o número de urls do usuário {id}, bem como o total de hits e a relação de suas 10 urls com mais hits

Ex: http://localhost:59754/api/users/joana

1.2.4. [DELETE] api/Users/{id}, deleta o usuário {id}

Ex: http://localhost:59754/api/users/matilda


1.3. Urls 

Usado para se obter a url original (longUrl) e para exclusão de urls.

1.3.1. GET api/Urls/{id}, consulta uma url, onde {id} é as shortUrl cadastrada e o retorno é a longUrl (url original)
Ex: http://localhost:59754/api/urls/2wcw

1.3.2. DELETE api/Urls/{id}, deleta uma url, onde {id} é a shortUrl a ser exclu
Ex: http://localhost:59754/api/urls/66na


2. Site

Como o objetivo desse projeto é o WebService, os recursos de interface com usuário, estão no padrão do template visual studido 2017. Nenhuma técnica de customização ou framework de frontend foi implementado.


2.1. Home do projeto

Acessada através da url do servidor, por exemplo, http://localhost:59754/Home/Index

2.1.1. Limpar as tabelas, essa opção permite realizar um “truncate table” nas tabelas do sistema, dessa forma todos os registros serão apagados. Após esse processo, basta usar novamente as opções de cadastramento de usuários e urls para inserir novos dados. Para executar esse procedimento basta teclar o botão “executar limpeza”.
Obs.: preparei scrpts para inclusão de novos registros, via SQL Server (vide seção Scripts).


2.2. Informações

2.2.1 Resumo estatístico do webservice, fornece o total de urls, hits e a lista das 10 urls mais acessadas.


2.2.Documentação

Informações sobre os endpoints do projeto (gerado automáticamente pelo visual Studio).


3. Ambiente de desenvolvimento

- Asp.net web api rest – C# versão .Net framework 4.6.1
- ORM: EntityFramework 6.2.0
- Ide: Visual Studio Community 2017 versão 15.3.5
- SQL Server 2014 - Management Studio
- IIS Window 10


4. Dados

Achei por bem usar os campos dbo.Users.Name, dbo.Urls.shortUrl e dbo.Urls.longUrl como chaves nas pesquisas em detrimento de seus respectivos PKs (dbo.Users.Id e dbo.Urls.Id), para aprimorar a experiência do usuário do webService. Por essa razão, esses campos foram indexados. Além disso, os campos dbo.Urls.LongUrl e dbo.Users.Name são do tipo “unique” para garantir que não haverão chaves duplicadas.

Obs.: preparei scrpts para inclusão de novos registros, via SQL Server (vide seção Scripts).

Em resumo, o Banco de Dados “ShortenUrl” tem 2 (duas) tabelas, “Urls” e “Users”, conforme a relação de campos abaixo:


4.1. Dbo.Urls

Campos: Id(pk), LongUrl (índex, unique), UserId (index), Hits (índex).


4.2. Dbo.Users

Campos: Id(pk), Name (índex, unique).


4.3. Triggers / Procedures

O único procedimento inserido via SQL Server é um trigger disparado para excluir as respectivas urls de um usuário excluído (vide seção Scripts).
.

5. Configurações do Projeto

5.1. Arquivo de recursos

É necessário alterar o arquivo de recursos do projeto para inserir a “baseurl” com a url raiz usada para exibição da página “informações” (da view  controller Infos/Index)

5.1.1. Config.resx: 
Namespace: B2EGroup.ShortenUrl.WebService – pasta Models
Ex: baseurl = http://localhost:59754/


5.2. ConnectionString 

A conexão com o banco de dados precisa ser ajustada no arquivo web.config situado na raiz do namespace B2EGroup.ShortenUrl.WebService.
exemplo:

<connectionStrings>
<add name="ShortenUrl" connectionString="Data Source=I7_AEL;Initial Catalog=ShortenUrl;Integrated Security=True" providerName="System.Data.SqlClient" />
  </connectionStrings>

Obs.: inserir esse trecho na seção <configuration>


6. Dependências do projeto

- Informar a baseurl no arquivo config.resx (vide detalhes no item configurações)

- Connection string (vide detalhes no item configurações)

- Tabelas: a tabela Urls depende da tabela Users, pois existe o campo userId em sua estrutura, dessa forma é necessário primeiro criar ao menos um usuário para depois inserir as urls


7. Entrega do projeto

- Github: https://github.com/anselmolucas/shortenUrl

- Arquivo .zip enviado por email



8.Testes

Acho importante utilizar TDD para direcionar um projeto desde o seu início e também usar testes de unidade para garantir a funcionalidade plena do mesmo. Como tema testes não foi mencionado no documento de requisitos e o tempo gasto na formulação do projeto acabou excedendo a minha expectativa inicial somado a um prazo de entrega muito curto, considerando-se o número de requisitos apresentados, optei por não incluir testes automatizados nesse projeto.


9. Comentários finais

Gosto de ler um programa como leio um livro, por isso em determinadas circunstâncias, prefiro abrir mão de soluções mais sofisticadas, caso as mesmas comprometam a fácil compreensão do código. 

Por esse projeto se tratar de um WebService, optei em manter as estruturas de exceções isoladas nas classes dos repositórios, pois as exceções serão retornadas com códigos do grupo  “500” em caso de erro ao acesso ao banco de dados e deverão ser tratadas pelo sistema que for consumir esse serviço.


10. Agradecimentos

Agradeço ao pessoal da B2E Group e a equipe da WTime por me convidarem para participar desse importante processo.


 
