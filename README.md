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


 
11. Scripts

11.1. Criação da tabela dbo.Users

CREATE TABLE [dbo].[Users] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);



11.1.1. Criar índices (lembrando que os índices são criados através do EntityFramework)

GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Users]([Name] ASC);


11.1.2. Criação do trigger

GO
CREATE TRIGGER excluirUrlsDeUserDeletado
ON dbo.users
AFTER DELETE 
AS
BEGIN
	DECLARE
	@UserId int
	Select @UserId = Id from deleted

	DELETE FROM Urls
	WHERE UserId = @UserId
END


11.1.3. Inserir registros manualmente para massa de dados de testes (opcional)

USE ShortenUrl;
GO

INSERT INTO dbo.Users (Name) VALUES 
('marcela'),
('sophia'),
('gabriela'),
('barbara'),
('guta'),
('dolores'),
('irma'),
('celia'),
('manuela'),
('anselmo'),
('joana'),
('rubia'),
('maira'),
('elpidio'),
('melinda'),
('solange'),
('clelia'),
('lucas'),
('ophelia'),
('margarida'),
('rosa'),
('rosangela'),
('patricia'),
('ana'),
('cassilda'),
('marjorie'),
('sade'),
('diana'),
('vilma'),
('dani');

GO


11.2. Criação da tabela dbo.Urls

CREATE TABLE [dbo].[Urls] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [LongUrl]  NVARCHAR (200) NULL,
    [ShortUrl] NVARCHAR (10)  NULL,
    [UserId]   INT            NULL,
    [Hits]     INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Urls] PRIMARY KEY CLUSTERED ([Id] ASC)
);

11.2.1. Criar índices (lembrando que os índices são criados através do EntityFramework)

GO
CREATE NONCLUSTERED INDEX [IX_ShortUrl]
    ON [dbo].[Urls]([ShortUrl] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_LongUrl]
    ON [dbo].[Urls]([LongUrl] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Urls]([UserId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Hits]
    ON [dbo].[Urls]([Hits] ASC);


11.2.2. Inserir registros manualmente para massa de dados de testes (opcional)

USE ShortenUrl;
GO
INSERT INTO dbo.Urls  (LongUrl,ShortUrl,UserId,Hits ) VALUES

('https://www.terra.com.br/noticias/', 'b0a0', 19, 22),
('https://noticias.uol.com.br/', 'lh1i', 17, 6),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'b2go', 5, 1),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'g25d', 18, 4),
('https://noticias.r7.com/', 'xksj', 19, 23),
('http://www.estadao.com.br/', 'gl53', 3, 5),
('https://www.terra.com.br/noticias/', 'scb1', 5, 26),
('http://g1.globo.com/', 'gi1i', 5, 43),
('http://www.estadao.com.br/', 'thlb', 15, 50),
('https://www.terra.com.br/noticias/', 'owih', 5, 40),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '1be7', 12, 45),
('http://g1.globo.com/', 'cghc', 11, 4),
('https://br.noticias.yahoo.com/', 'eakc', 9, 1),
('https://noticias.r7.com/', '2jow', 11, 45),
('http://g1.globo.com/', 'oj9b', 19, 5),
('http://g1.globo.com/', 'olc4', 8, 15),
('https://www.terra.com.br/noticias/', 'e6d3', 7, 40),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'xs90', 2, 45),
('https://www.terra.com.br/noticias/', 'jccd', 10, 36),
('https://br.noticias.yahoo.com/', 'mde0', 9, 5),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '2cga', 2, 35),
('http://www.estadao.com.br/', '9d4j', 9, 32),
('https://www.terra.com.br/noticias/', 'dhcj', 8, 39),
('https://www.terra.com.br/noticias/', '66gh', 13, 4),
('http://www.estadao.com.br/', 'msd1', 17, 5),
('https://www.terra.com.br/noticias/', 'g5kd', 10, 4),
('https://br.noticias.yahoo.com/', '5jbj', 18, 2),
('https://br.noticias.yahoo.com/', '6eks', 15, 4),
('http://g1.globo.com/', 'eegj', 16, 4),
('http://g1.globo.com/', 'aeiw', 16, 3),
('https://noticias.uol.com.br/', '2f1d', 18, 5),
('https://noticias.uol.com.br/', 'kwjx', 17, 2),
('https://www.terra.com.br/noticias/', 'j9eh', 16, 2),
('https://www.terra.com.br/noticias/', 'je0x', 19, 3),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'm45c', 10, 5),
('https://br.noticias.yahoo.com/', 'dh7b', 11, 2),
('https://www.terra.com.br/noticias/', '2gki', 8, 4),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'jfkg', 14, 1),
('http://g1.globo.com/', 'o8ho', 10, 4),
('https://noticias.uol.com.br/', 'ccb4', 11, 5),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '5673', 12, 2),
('https://noticias.uol.com.br/', '1ejd', 6, 3),
('https://www.terra.com.br/noticias/', 'd8st', 1, 2),
('https://noticias.uol.com.br/', 'jot8', 19, 2),
('https://www.terra.com.br/noticias/', '7lke', 14, 1),
('https://www.terra.com.br/noticias/', 'mghx', 14, 4),
('http://www.estadao.com.br/', '5at2', 9, 2),
('https://noticias.uol.com.br/', 'abf2', 14, 5),
('https://www.terra.com.br/noticias/', 'tcb8', 19, 4),
('https://www.terra.com.br/noticias/', '1dxx', 1, 5),
('https://www.terra.com.br/noticias/', 'fkjc', 4, 5),
('https://br.noticias.yahoo.com/', 'tigd', 10, 4),
('http://g1.globo.com/', 'l0a9', 20, 4),
('http://g1.globo.com/', '29jd', 12, 7),
('http://g1.globo.com/', 't1ke', 11, 4),
('http://www.estadao.com.br/', '8goo', 13, 4),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'daem', 1, 17),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'e3mo', 5, 20),
('https://noticias.uol.com.br/', 'jlbb', 7, 9),
('http://www.estadao.com.br/', 'a9ol', 7, 11),
('https://www.terra.com.br/noticias/', 'c233', 3, 13),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '1cbo', 12, 20),
('https://www.terra.com.br/noticias/', 'dkgd', 13, 6),
('https://br.noticias.yahoo.com/', 'lbgg', 13, 10),
('https://www.terra.com.br/noticias/', 'sm54', 2, 5),
('http://www.estadao.com.br/', 'imxt', 20, 14),
('https://br.noticias.yahoo.com/', 'lga8', 10, 18),
('https://www.terra.com.br/noticias/', 'deg0', 4, 5),
('https://www.terra.com.br/noticias/', 'w77c', 13, 3),
('https://www.terra.com.br/noticias/', 'jfba', 14, 18),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'bbig', 11, 18),
('http://g1.globo.com/', 'tdbd', 3, 2),
('https://www.terra.com.br/noticias/', 'f3jk', 6, 6),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'jg2m', 9, 9),
('https://br.noticias.yahoo.com/', 'i38m', 17, 8),
('https://noticias.r7.com/', 'i88j', 12, 17),
('http://www.estadao.com.br/', 'a5to', 12, 9),
('https://br.noticias.yahoo.com/', '9wcj', 8, 8),
('https://br.noticias.yahoo.com/', 'g0se', 9, 18),
('https://www.terra.com.br/noticias/', 'g0k7', 5, 6),
('https://br.noticias.yahoo.com/', 'jbwa', 4, 5),
('https://www.terra.com.br/noticias/', 'tesb', 5, 1),
('http://g1.globo.com/', '8kif', 3, 15),
('https://noticias.uol.com.br/', 'ag05', 3, 2),
('https://www.terra.com.br/noticias/', 'w8jb', 12, 7),
('https://noticias.uol.com.br/', 'hd0o', 8, 13),
('http://www.estadao.com.br/', 'i0mk', 19, 16),
('http://g1.globo.com/', '81hd', 8, 1),
('https://www.terra.com.br/noticias/', '6fgd', 9, 4),
('http://g1.globo.com/', 'cwoj', 15, 1),
('http://www.estadao.com.br/', 'g3ao', 13, 2),
('https://noticias.uol.com.br/', 'hhg1', 10, 2),
('https://br.noticias.yahoo.com/', 'ggjx', 12, 1),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '6fg4', 9, 2),
('https://www.terra.com.br/noticias/', 'dja3', 20, 1),
('https://noticias.uol.com.br/', '5s7h', 2, 3),
('https://noticias.r7.com/', 'ogw8', 9, 1),
('https://www.terra.com.br/noticias/', 'hwkl', 18, 5),
('https://br.noticias.yahoo.com/', 'o0e1', 16, 3),
('https://www.terra.com.br/noticias/', 'd1jo', 13, 3),
('http://g1.globo.com/', 'ce72', 2, 41),
('https://www.terra.com.br/noticias/', 'ei5x', 19, 11),
('https://www.terra.com.br/noticias/', '3ag1', 13, 1),
('http://www.estadao.com.br/', 'ef4t', 12, 42),
('https://br.noticias.yahoo.com/', 'jawt', 13, 25),
('https://noticias.r7.com/', 'gmjd', 14, 35),
('https://br.noticias.yahoo.com/', 'w6gc', 3, 14),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'idam', 12, 48),
('https://noticias.r7.com/', 'x31b', 13, 14),
('http://g1.globo.com/', 'b7b1', 2, 17),
('http://www.estadao.com.br/', 'kjgh', 18, 40),
('https://br.noticias.yahoo.com/', 'sbes', 9, 31),
('https://br.noticias.yahoo.com/', 'exgh', 14, 38),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'sxio', 3, 38),
('https://br.noticias.yahoo.com/', '15dl', 17, 6),
('https://br.noticias.yahoo.com/', 'j8dc', 8, 6),
('https://www.terra.com.br/noticias/', 'teab', 16, 17),
('https://www.terra.com.br/noticias/', 'o1sh', 3, 34),
('https://noticias.uol.com.br/', 'a5lg', 1, 21),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '6djm', 1, 29),
('http://www.estadao.com.br/', 'txwb', 14, 41),
('https://www.terra.com.br/noticias/', 'odc9', 7, 14),
('http://www.estadao.com.br/', 'chch', 1, 22),
('http://www.estadao.com.br/', 'hllb', 19, 4),
('http://g1.globo.com/', '8faf', 10, 1),
('https://noticias.uol.com.br/', 'wbe0', 5, 2),
('https://br.noticias.yahoo.com/', 'a74b', 19, 4),
('https://br.noticias.yahoo.com/', 'meo7', 7, 4),
('https://www.terra.com.br/noticias/', 'ag68', 2, 5),
('http://g1.globo.com/', '63hs', 16, 5),
('https://www.terra.com.br/noticias/', 'asfg', 12, 3),
('https://www.terra.com.br/noticias/', 'btj7', 8, 2),
('http://g1.globo.com/', '7g56', 8, 1),
('https://www.terra.com.br/noticias/', 'eo35', 19, 4),
('https://noticias.r7.com/', '5ll1', 10, 4),
('http://g1.globo.com/', 'at5b', 1, 5),
('https://noticias.uol.com.br/', 'cbll', 5, 4),
('https://www.terra.com.br/noticias/', 'tgtw', 13, 2),
('https://www.terra.com.br/noticias/', 'e2fd', 10, 2),
('https://www.terra.com.br/noticias/', 'h0wb', 19, 2),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'gllc', 6, 1),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'ifk1', 15, 1),
('https://noticias.r7.com/', '44aj', 2, 1),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'x2hi', 17, 5),
('https://noticias.uol.com.br/', '5b12', 20, 1),
('http://www.estadao.com.br/', 'lbjt', 16, 2),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'bw1j', 11, 3),
('https://noticias.uol.com.br/', 'b7e7', 6, 3),
('https://noticias.uol.com.br/', 'l134', 10, 3),
('https://noticias.uol.com.br/', 'cho4', 15, 1),
('https://www.terra.com.br/noticias/', 'cgek', 12, 5),
('https://noticias.uol.com.br/', 'd0xj', 19, 5),
('https://br.noticias.yahoo.com/', 'dgih', 18, 1),
('http://www.estadao.com.br/', 'kj6b', 5, 12),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'f8mf', 3, 18),
('http://g1.globo.com/', 'i69d', 3, 2),
('https://www.terra.com.br/noticias/', '4d97', 10, 20),
('https://noticias.uol.com.br/', 'aa0b', 20, 13),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '9a44', 8, 3),
('http://g1.globo.com/', '50c3', 13, 1),
('https://www.terra.com.br/noticias/', 'k92c', 18, 12),
('http://g1.globo.com/', '5dh6', 12, 13),
('https://www.terra.com.br/noticias/', 'gxj6', 15, 1),
('http://g1.globo.com/', 'e6hx', 18, 4),
('https://www.terra.com.br/noticias/', 'bbo5', 15, 18),
('https://br.noticias.yahoo.com/', '5g32', 13, 19),
('https://www.terra.com.br/noticias/', 'echa', 11, 15),
('https://br.noticias.yahoo.com/', 'cd1j', 4, 12),
('https://br.noticias.yahoo.com/', 'te92', 16, 11),
('https://noticias.uol.com.br/', 'c37b', 7, 13),
('https://noticias.r7.com/', 'h9aa', 19, 17),
('https://noticias.uol.com.br/', 'i34i', 14, 17),
('https://noticias.uol.com.br/', 'ecst', 14, 6),
('http://g1.globo.com/', '1jct', 5, 1),
('http://www.estadao.com.br/', '13jg', 8, 17),
('http://www.estadao.com.br/', '68ce', 11, 11),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'ccfs', 1, 8),
('https://www.terra.com.br/noticias/', 'dj4b', 7, 2),
('https://www.terra.com.br/noticias/', '8t41', 8, 19),
('https://www.terra.com.br/noticias/', 'sth0', 19, 17),
('https://www.terra.com.br/noticias/', '2dsg', 1, 2),
('https://noticias.uol.com.br/', '1oil', 10, 14),
('http://g1.globo.com/', 'kbxa', 18, 16),
('https://www.terra.com.br/noticias/', 'hm5e', 14, 16),
('http://www.estadao.com.br/', 'ea3j', 9, 6),
('https://noticias.uol.com.br/', '3he4', 8, 11),
('http://www.estadao.com.br/', 'iah7', 2, 20),
('https://noticias.uol.com.br/', '4b87', 19, 5),
('https://noticias.r7.com/', '30sm', 12, 4),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'j05h', 17, 1),
('https://noticias.r7.com/', 'fowg', 18, 1),
('https://noticias.uol.com.br/', 'e7ke', 19, 4),
('https://noticias.r7.com/', 'd762', 1, 3),
('http://www.estadao.com.br/', 'mf0i', 11, 5),
('https://noticias.uol.com.br/', '09lt', 14, 4),
('http://www.estadao.com.br/', 'a5jo', 9, 3),
('https://noticias.r7.com/', '4gkb', 10, 5),
('https://noticias.r7.com/', '4eaj', 14, 3),
('https://www.terra.com.br/noticias/', '6cw3', 20, 2),
('http://www.estadao.com.br/', 'fmwd', 16, 2),
('https://www.terra.com.br/noticias/', '2klb', 12, 25),
('https://noticias.uol.com.br/', 'jsje', 4, 13),
('https://noticias.uol.com.br/', 'mfsw', 17, 34),
('http://g1.globo.com/', '6i92', 16, 31),
('https://br.noticias.yahoo.com/', 'jt0e', 7, 43),
('http://www.estadao.com.br/', 'iib7', 14, 45),
('https://www.terra.com.br/noticias/', '2fh0', 20, 11),
('http://www.estadao.com.br/', '634j', 17, 31),
('http://www.estadao.com.br/', 'ebhm', 5, 33),
('https://noticias.r7.com/', '58b0', 8, 32),
('https://www.terra.com.br/noticias/', 'd5la', 2, 2),
('https://www.terra.com.br/noticias/', '8ds2', 9, 10),
('https://noticias.r7.com/', '0ejb', 19, 41),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '0a8j', 17, 28),
('http://g1.globo.com/', 'ahci', 12, 46),
('http://www.estadao.com.br/', '5jai', 6, 38),
('https://br.noticias.yahoo.com/', 'kkb3', 14, 26),
('https://br.noticias.yahoo.com/', 'deck', 20, 41),
('https://br.noticias.yahoo.com/', '3hxg', 18, 14),
('http://www.estadao.com.br/', 'b94h', 17, 22),
('https://www.terra.com.br/noticias/', 'jg2o', 11, 7),
('https://www.terra.com.br/noticias/', '2w8w', 5, 25),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'ojxx', 7, 24),
('http://www.estadao.com.br/', 'b4fh', 17, 1),
('https://www.terra.com.br/noticias/', 'dkda', 1, 4),
('http://www.estadao.com.br/', 'oo19', 17, 1),
('https://noticias.r7.com/', '2jjg', 14, 1),
('https://noticias.uol.com.br/', 'iib1', 6, 2),
('https://www.terra.com.br/noticias/', 'gh5c', 1, 2),
('https://noticias.uol.com.br/', 'bgdx', 1, 1),
('http://g1.globo.com/', '8e40', 12, 1),
('https://br.noticias.yahoo.com/', 'w0fo', 16, 5),
('https://br.noticias.yahoo.com/', 'gdhj', 20, 2),
('https://www.terra.com.br/noticias/', 'd93f', 19, 3),
('https://noticias.uol.com.br/', 'keca', 1, 5),
('https://www.terra.com.br/noticias/', 'sfff', 5, 5),
('https://www.terra.com.br/noticias/', 'hai0', 16, 1),
('http://www.estadao.com.br/', 'jwah', 8, 4),
('http://www.estadao.com.br/', '7dbj', 18, 5),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'cjja', 8, 4),
('https://br.noticias.yahoo.com/', 'jjdw', 20, 5),
('https://noticias.r7.com/', 'mklt', 5, 5),
('https://br.noticias.yahoo.com/', 'taic', 12, 2),
('https://br.noticias.yahoo.com/', '0hmd', 1, 4),
('http://www.estadao.com.br/', '58m2', 12, 5),
('https://www.terra.com.br/noticias/', 'wimf', 16, 2),
('https://br.noticias.yahoo.com/', 'b125', 11, 5),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', '2g4c', 19, 3),
('http://g1.globo.com/', 'cblc', 20, 3),
('https://br.noticias.yahoo.com/', 'jae6', 1, 3),
('http://www.estadao.com.br/', '6hk9', 1, 1),
('https://noticias.uol.com.br/', '0d1g', 1, 5),
('https://www.terra.com.br/noticias/', 'k7cs', 13, 5),
('https://www.terra.com.br/noticias/', '5kde', 20, 15),
('https://www.terra.com.br/noticias/', 'badj', 18, 8),
('https://br.noticias.yahoo.com/', 'd9bb', 1, 10),
('https://noticias.r7.com/', '0em9', 15, 9),
('https://www.terra.com.br/noticias/', 'abh3', 20, 12),
('http://www.estadao.com.br/', '0xee', 12, 8),
('https://noticias.uol.com.br/', 'aab0', 4, 2),
('https://noticias.r7.com/', 'gb6d', 6, 7),
('https://noticias.uol.com.br/', '14bd', 19, 6),
('https://noticias.r7.com/', 'tg3j', 3, 16),
('http://www.estadao.com.br/', '21mw', 1, 5),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'dmig', 13, 7),
('https://www.terra.com.br/noticias/', '6b9j', 15, 8),
('https://www.terra.com.br/noticias/', 'dhbt', 10, 2),
('https://noticias.uol.com.br/', 'kf19', 15, 3),
('https://noticias.uol.com.br/', 'ebah', 11, 8),
('http://www.estadao.com.br/', 'dajx', 6, 15),
('https://www.terra.com.br/noticias/', 'a62m', 1, 1),
('https://br.noticias.yahoo.com/', 'gdce', 7, 10),
('https://noticias.r7.com/', 'jdij', 16, 1),
('https://noticias.r7.com/', 'c6bi', 5, 6),
('http://www.estadao.com.br/', 'gble', 1, 4),
('https://noticias.uol.com.br/', 't556', 7, 12),
('https://www.terra.com.br/noticias/', 's9t5', 13, 18),
('http://www.estadao.com.br/', 'jih8', 12, 9),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'sej3', 4, 8),
('https://noticias.uol.com.br/', '5a2k', 5, 19),
('http://www.estadao.com.br/', '0385', 4, 15),
('http://g1.globo.com/', '4hh8', 7, 14),
('https://noticias.uol.com.br/', '4x65', 9, 5),
('https://www.terra.com.br/noticias/', 'so8i', 15, 13),
('http://g1.globo.com/', '4dje', 16, 9),
('https://www.terra.com.br/noticias/', 'l5xk', 18, 3),
('https://br.noticias.yahoo.com/', 'adaa', 17, 15),
('https://br.noticias.yahoo.com/', 'edx2', 10, 4),
('https://www.terra.com.br/noticias/', '2ghi', 5, 5),
('https://www.terra.com.br/noticias/', 'efdo', 5, 4),
('http://www.estadao.com.br/', 'gdjt', 3, 3),
('https://br.noticias.yahoo.com/', 'h2gj', 18, 4),
('https://noticias.r7.com/', 'j8hi', 11, 4),
('https://www.terra.com.br/noticias/', '8t18', 12, 3),
('https://www.terra.com.br/noticias/', '9h9h', 8, 3),
('https://www.terra.com.br/noticias/', 'co91', 20, 5),
('https://noticias.uol.com.br/cotidiano/ultimas-noticias/2017/10/27/operacao-policial-cerca-quatro-morros-no-centro-do-rio.htm', 'xa4c', 5, 2),
('http://g1.globo.com/', 'geeg', 3, 5),
('http://www.estadao.com.br/', '2gfb', 11, 4),
('https://br.noticias.yahoo.com/', 'f14c', 1, 1);


