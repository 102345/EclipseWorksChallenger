# EclipseWorks Desafio

Segue um projeto com o uso das seguintes tecnologias : WebApi .NET 7 , ORM Dapper , FluentValidation , Mapper , Docker.
Padrões arquiteturais : Domain Driven Design , Dependency Injections , UnitOfWOrk , divisão de responsabilidade entre camadas da arquitetura entre outros
usados.

## Intruções para rodar o docker

1 – Abra o arquivo docker-compose.yml e altere o caminho da pasta onde se encontra o arquivo database.sql para o seu caminho de projeto de seu computador conforme a imagem a seguir.

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/85d31dcb-5da3-4416-a934-12d9a3013cd9)

2 – Abra o terminal de comando na raiz do projeto e execute o seguinte comando :  docker-compose up –build , conforme a imagem a seguir. 

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/ba6c4aa0-8d60-457b-8935-da9a76e7467c)

3 – Espere os serviços de containers fazerem o deploy e subirem por um tempo estimado de 2 min a 3 min. Se tudo ocorre bem você verá os containers inicializados conforme imagem vista na ferramenta Docker Desktop .

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/5bea2af5-ce23-4bb5-908d-d933523b851d)

## Instruções para realização dos testes de funcionamento dos endpoints

1. Siga a ordem de chamada dos endpoints usando uma ferramenta de requisição http como o Postman ou outra similar ou chame o projeto usando o Visual Studio para obter a documentação gerada pelo Swagger .
Acompanhe as ilustrações a seguir para ver os endpoints responsavéis por cada funcionalidade.

Obs : Use idOwner = 1 (role Manager ) , IdOwner = 2 (role Administrator ) ou IdOwner = 3 (role SuportIT ).

a – Criar um projeto :

POST : http://localhost:8090/eclipseworks/api/project

REQUEST BODY :

{
  "nameProject": "string",
  "observation": "string",
  "idOwner": 0
}

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/67e4bf01-854d-4804-be9d-c650a57de7dd)

b -  Buscar projetos pelo IdOwner :

GET : http://localhost:8090/eclipseworks/api/project?idOwner= ?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/58c92d84-05f3-4d0d-9e5a-5a579f5032d2)

c -  Excluir um projeto :

DELETE : http://localhost:8090/eclipseworks/api/project?idProject=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/56e84e2f-eae4-4598-bf26-82e022b2da7a)

d - Criar uma tarefa :

POST :  http://localhost:8090/eclipseworks/api/project/task


REQUEST BODY :
{
  "idPriority": 0,
  "status": 0,
  "idProject": 0,
  "idOwner": 0,
  "title": "string",
  "description": "string",
  "dueDate": "2024-02-19T12:58:52.146Z"
}

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/18e1c375-1b4e-4023-a2bf-1c5a9bcb1436)

e – Atualizar uma tarefa :

PUT :  http://localhost:8090/eclipseworks/api/project/task

REQUEST BODY :

{
  "idTask": 0,
  "status": 0,
  "idProject": 0,
  "idOwner": 0,
  "title": "string",
  "description": "string",
  "dueDate": "2024-02-19T12:59:51.248Z"
}



![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/1ffaeb4d-ad82-47a9-9371-4a33a023352a)

f – Excluir uma tarefa :

DELETE: http://localhost:8090/eclipseworks/api/project/task?idTask=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/e0a017cb-a7a4-4171-8bd6-b68eccc7a4de)

g -  Buscar tarefas por projeto :

GET: http://localhost:8090/eclipseworks/api/project/task?idProject=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/8e2c1eff-8906-4e8e-a906-02c600d4ee9e)

h – Adicionar um comentário em uma tarefa :

POST: http://localhost:8090/eclipseworks/api/project/task/comment

REQUEST BODY :

{
  "idTask": 0,
  "description": "string"
}


![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/1d6bf8de-2efe-4fa6-8c77-210d8b291f25)

i – Buscar dados de relatório gerencial de performance :

GET: http://localhost:8090//eclipseworks/api/reportmanager?idOwnerAuthorized=?&idProject=?&Status=?&idOwner=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/2876aaf1-99e1-4abd-8167-f8fcca073e50)

Obs : Apenas o parâmetro IdOwnerAuthorized é obrigatório para a pesquisa.

## Evidências de cobertura por testes unitários

Segue a imagem de relatório gerado do nivel de cobertura de testes unitários para as regras de negócios evidênciadas nas camadas de aplicação e de dominio.

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/16f138e5-26b8-47e2-8034-ec6a08ad2632)


## Refinamento para as próximas Sprints

1 - Não seria interessante ter um controle de bloqueio de usuários para acessar os endpoints em situações que estão de férias, afastados ou desligados? 

2 - A consulta de relatório gerencial não poderia ter um controle de permissão para apenas o Gerente responsavel por seu grupo de projetos visualizar
suas atividades sem ter acesso as informações de atividades de outro gerente de projeto ?

3 - Não seria interessante implementar uma funcionalidade de exclusão de comentarios associados a uma tarefa ?

4 - Outra sugestão : uma funcionalidade de transferências de conjuntos de projetos de um gerente para outro gerente assumir a condução destas atividades,

5 - Sugestão : Acrescentar um campo de registro de data de estimativa de inicio de projeto e um campo de data de finalização de projeto na criação de um novo projeto.

6 - Sugestão : Apenas usuários no papel de gerente podem criar , alterar  ou excluir projeto.

7 - Sugestão : acrescentar status de controle de situação do projeto : Em andamento , Em rescunho , Bloqueado , Finalizado ou Encerrado. Apenas usuarios no
papel de Gerente pode alterar o status.

8 - Sugestão : Todas vez que o gerente criar , alterar ou excluir um projeto deve ser disparado uma notificação de aviso para a área PMO ( Project Management
Office).

9 - Sugestão : Implementar status de situação de usuário no projeto. Exemplos : Em férias , Afastados , Desligados.

10 - Sugestão : Implementar funcionalidade de busca de todas as tarefas e comentarios relacionados ao usuário responsável por suas atividades

## Propostas de melhoria no projeto

### Visão de integração com ferramentas de apoio de infra-estrutura e melhores práticas

1- Usar plataforma de monitoramento (APM) para métricas de consumo de recursos de serviços contratados por cloud através de geração 
de dashbords e geração de relatórios dados de uso , gargalos de desempenho de chamadas de APIs e outros componentes de integração 
de uma arquitetura distribuida , principais chamadas de desempenho , tempo de latência e resposta de um processamento de requisição
de um serviço ou API. Exemplo de APM : Datadog , Splunk ou Azure Application Insights.

2- Integração do serviços de API Gateways para filtragem e separação de requisições entre as APIS de terceiros e a API interna do
projeto proposto em ambiente interno. Podemos citar alguns principais beneficios relacionados a esta solução :
- Único ponto de entrada no sistema‍
- Camada adicional de segurança
- Simplicidade e transparência para o consumidor final
- Evitar expor questões internas a clientes externos
- Separação de camada de aplicação
- Melhoria nos desenvolvimentos

3 - Usar serviços de notificação para disparo de alertas para eventos não programados ou possiveis falhas de funcionamento de
componentes da arquitetura da solução, através de uso de APIs especializadas. Podemos citar como exemplos: WhatsApp Business API ou API do Microsoft Teams.


### Visão de evolução da arquitetura da solução da aplicação 

1- Usar conceitos de CQRS para separação entre as requisições de consulta e as requisições transacionais através de uso de separação entre servidores de SGDB 
especializados e bancos de dados para cada caso.

2- Implementação de serviços automatizados em background (aplicações em processanto batch, Workers Services ou Windows Services) para sincronização de dados
gerados entre as bases de dados transacionais e as bases de dados responsaveis por geração de informações para consulta.

3 -Se aplicação escalar de forma evolucionaria ao longo de seu ciclo de vida , fazer uso de integração de serviços de mensageria para processamento assincrono
de dados das requisições transacionais. Exemplo de principais serviços : RabbitMQ , Azure Service Message Bus, Amazon Simple Queue Service entre outras soluções
disponiveis.

4- Usar serviços de cloud para armazenamento de segredos e dados sensiveis da aplicação como por exemplo AWS Secrets Manager ou Microsoft Azure KeyVault.

5- Implementar chamada de serviços de criptografia para dados sensiveis no trafego da requisição e também na gravação de informações nos bancos de dados .

6 - Para exposição de necessidades especificas de integração e consumo de dados através de chamadas das APIs pela camada de front ends , implementar uma camada intermediaria
para tratamento especificos das requsições relacionadas a autenticação e autorização de acesso, tradução dos dados que serão usados através de contratos customizados,
validação dos dados , gerenciamento e controle de erros gerados entre outros requisitos não funcionais. Este design arquitetural é provido por conceitos e praticas de BFF.

7 - Aplicar conceitos de redudância através de duplicação de componentes de infra-estrutura (servidores de SGDBS on Cloud ou On-Premisse , serviços de build de APIS e de host
on Cloud , Servidores de aplicação , Servidores de Message Brokers entre outros ) para prover alta disponibilidade de acesso.

8 - Aplicar tecnicas de resiliência  para tentativas de chamadas para processamento e integração de dados através de uso de algoritmos de tratamento de resiliência ou pacotes de terceiros
especializados na camada de infra-estrutura dos projetos associados a aplicação.

9 - Prover chamada de geração de logs através de serviços ou apis especializadas entre as camadas da arquitetura da aplicação e grava-los em algum banco de dados NOSQL no modelo Key-Value

10 - Em caso de consultas constantes no banco de dados para processamento transacionais de dados, fazer uso de algum pacote de controle de memory cache ( exemplo : Microsoft.Extensions.Caching.Memory)
ou em caso de requisições de alto volume e distribuidas entre varias plataformas consumidoras da API do projeto fazer uso de serviços especializados em controle de cache . Exemplos : Redis ,AWS 
Elastic Search , Azure Cache for Redis entre outras soluções.


















