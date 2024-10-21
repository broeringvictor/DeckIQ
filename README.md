# DeckIQ

1. **Objetivo:** O DeckIQ é um aplicativo de flashcards voltado para uso pessoal, permitindo revisar e aprender de forma eficiente.

2. **Decisão de Front-end:** Ainda estou avaliando se a renderização será feita no servidor ou diretamente no navegador.

3. **Desenvolvimento:** Estou desenvolvendo este aplicativo para aplicar os conhecimentos adquiridos no curso da [Balta](https://balta.io/).

4. **Banco de Dados:** O aplicativo utiliza um banco de dados SQL Server.

5. **Migrations e Banco de Dados:**
   - Para configurar o banco, basta rodar os comandos:
     ```bash
     dotnet ef migrations add v1
     dotnet ef database update
     ```

6. **String de Conexão:** Lembre-se de inicializar as secrets do projeto para proteger a string de conexão com o banco SQL Server, executando:
   ```bash
   dotnet user-secrets init
