# Gym Simulator — Backend

Repositório backend do projeto Gym Simulator (API em .NET 8 + EF Core).

Principais itens:
- Solução: GymSimulator.sln
- Projeto API: src/GymSimulator.API
- Infraestrutura/DB: src/GymSimulator.Infrastructure
- Domínio e Aplicação: src/GymSimulator.Domain, src/GymSimulator.Application

Arquivos importantes:
- Entrada da API: src/GymSimulator.API/Program.cs
- Contexto EF: src/GymSimulator.Infrastructure/Data/AppDbContext.cs
- Seeds: src/GymSimulator.Infrastructure/Data/Seeds/SeedData.cs
- Migrations: src/GymSimulator.API/Migrations/
- Banco de dev (SQLite): src/GymSimulator.API/gymDatabase.db
- Controllers: src/GymSimulator.API/Controllers/
- DTOs: src/GymSimulator.Application/DTOs/

Como rodar (desenvolvimento)
1. Restaurar e compilar
    cd src/GymSimulator.API
    dotnet restore
    dotnet build

2. Executar API
    dotnet run

### Como rodar via GUI do Visual Studio

-Abrir o projeto: Abra o Visual Studio.
-Vá em Arquivo > Abrir > Projeto/Solução...
-Selecione o arquivo:
```GymSimulator.sln```

-No Solution Explorer, clique com o botão direito sobre:
```GymSimulator.API```

-Escolha "Definir como projeto de inicialização" (Set as Startup Project).

O banco SQLite é usado no modo de desenvolvimento:

```src/GymSimulator.API/gymDatabase.db```
-Acesse Ferramentas > Gerenciador de Pacotes do Nuget > Console
-Execute o seguinte comando:
```Update-Database```

Executar o projeto:

No topo do Visual Studio, selecione Debug (ou Release, se preferir).

-Clique em Iniciar sem depuração (Ctrl + F5) ou Iniciar com depuração (F5).

-A API será iniciada e abrirá automaticamente no navegador

Notas
- Dados de exemplo são semeados via SeedData durante a inicialização (ver Program.cs).
- Para criar migrations: dotnet ef migrations add <Nome> -p src/GymSimulator.API -s src/GymSimulator.API

Estrutura resumida
- src/GymSimulator.API — API, controllers, migrations e configurações
- src/GymSimulator.Infrastructure — EF Core, contexto, seeds, serviços
- src/GymSimulator.Application — DTOs, abstrações e mapeamentos
- src/GymSimulator.Domain — Entidades e enums

Contribuindo
- Siga os padrões existentes: controllers finos, serviços para regras de negócio, DTOs para entrada/saída.
- Adicione tests e migrations conforme necessário.
- Inclua arquivo LICENSE se desejar especificar a licença.

Referências
- appsettings.json / appsettings.Development.json para configuração
- gym-simulator-web (repositório front-end separado) consome esta API
