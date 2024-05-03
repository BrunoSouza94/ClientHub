# Como executar

## Requisitos
```
 - .NET 8
 - Docker
 - SSMS (Ou similar para execução de script no banco de dados)
```

## Instruções
```
  1. Navegar através do Powershell até o diretório do projeto, onde está presente o arquivo docker-compose.yml
  2. Executar o comando: docker-compose up
  3. Após a conclusão, a aplicação estará em execução, tanto frontend, backend, banco de dados e serviço de caching.
  4. Realizar a conexão com o banco de dados, através do SSMS, com as seguintes informações:
    - Nome do Servidor: localhost,1433
    - Autenticação: Autenticação do SQL Server
    - Logon: sa
    - Senha: 84sd62aA!0
  5. Clicar em nova consulta e executar os comandos encontrados no arquivo 'init.sql', também presente no diretório do projeto.
  6. Acessar através do navegador 'http://localhost:9090' e explorar a aplicação

  Obs: Caso estiver utilizando Visual Studio, base abrir a solução, selecionar docker-compose como modo de execução e iniciar
```
