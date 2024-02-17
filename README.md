# EclipseWorks Desafio

Segue um projeto com o uso das seguintes tecnologias : WebApi .NET 7 , ORM Dapper , FluentValidation , Mapper , Docker.
Padrões arquiteturais : Domain Driven Design , Dependency Injections , UnitOfWOrk , divisão de responsabilidade entre camadas da arquitetura entre outras
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

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/67e4bf01-854d-4804-be9d-c650a57de7dd)

b -  Buscar projetos pelo IdOwner :

GET : http://localhost:8090/eclipseworks/api/project?idOwner= ?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/58c92d84-05f3-4d0d-9e5a-5a579f5032d2)

c -  Excluir um projeto :

DELETE : http://localhost:8090/eclipseworks/api/project?idProject=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/56e84e2f-eae4-4598-bf26-82e022b2da7a)

d - Criar uma tarefa :

POST :  http://localhost:8090/eclipseworks/api/project/task

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/18e1c375-1b4e-4023-a2bf-1c5a9bcb1436)

e – Atualizar uma tarefa :

PUT :  http://localhost:8090/eclipseworks/api/project/task

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/1ffaeb4d-ad82-47a9-9371-4a33a023352a)

f – Excluir uma tarefa :

DELETE: http://localhost:8090/eclipseworks/api/project/task?idTask=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/e0a017cb-a7a4-4171-8bd6-b68eccc7a4de)

g -  Buscar tarefas por projeto :

GET: http://localhost:8090/eclipseworks/api/project/task?idProject=?

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/8e2c1eff-8906-4e8e-a906-02c600d4ee9e)

h – Adicionar um comentário em uma tarefa :

POST: http://localhost:8090/eclipseworks/api/project/task/comment

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













