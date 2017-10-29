# shortenUrl
webservice rest (c# web api)

EXCEPTIONS
Optei em manter as estruturas de exceções apenas nos repositórios, já que irá consumir os dados será um WebService que poderá os códigos de status de retorno do grupo  “500” em caso de erro ao acesso ao banco de dados.

Tentei no decorrer do projeto usar técnicas diferentes a fim de demonstrar um pouco a minha gama de conhecimento, peço que não julguem isso como falta de padrão.

Gosto de ler um programa como leio um livro, por isso em determinadas circunstâncias, prefiro abrir mão de soluções mais sofisticadas, caso as mesmas comprometam a fácil compreensão do código. 

Optei por não criar mais um projeto para separar as regras de negócio, que neste caso se resumiam apenas a estatísticas.
Achei por bem usar os campos userName, shorteUrl e longUrl em detrimento de seus respectivos ids (userId e urlId), para aprimorar a experiência do usuário do webService.
