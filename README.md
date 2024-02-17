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

## Instruções para realização dos testes de funcionamento do endpoints

1. Siga a ordem de chamada dos endpoints usando a uma ferramenta de requisição http como o Postman ou outra similar ou chame o projeto usando o Visual Studio para obter a documentação gerada pelo Swagger 

Obs : Use idOwner = 1 (role Manager ) , IdOwner = 2 (role Administrator ) ou IdOwner = 3 (role SuportIT ).

a – Criar um projeto :

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/67e4bf01-854d-4804-be9d-c650a57de7dd)

b -  Buscar projetos pelo IdOwner :

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/58c92d84-05f3-4d0d-9e5a-5a579f5032d2)

c -  Excluir um projeto :

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/56e84e2f-eae4-4598-bf26-82e022b2da7a)

d - Criar uma tarefa :

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/18e1c375-1b4e-4023-a2bf-1c5a9bcb1436)

e – Atualizar uma tarefa :

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/1ffaeb4d-ad82-47a9-9371-4a33a023352a)

f – Excluir uma tarefa :

![image](https://github.com/102345/EclipseWorksChallenger/assets/31006716/e0a017cb-a7a4-4171-8bd6-b68eccc7a4de)









