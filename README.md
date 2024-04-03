Esta aplicação contém tanto uma camada de backend(Api) quanto uma camada de frontend(Contacts)

Caso a aplicação dê erro devido à conexão de dados ou por alguma inconsistência da primeira migração efetuada (ela foi feita em uma base de dados SQL Server local para fins de teste), talvez seja necessário rodar novamente os comandos:

dotnet ef migrations add InitialCreation
dotnet ef database update
