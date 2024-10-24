# DeckIQ

## 1. Objetivo  
DeckIQ é um aplicativo de flashcards projetado para uso pessoal, focado em oferecer uma maneira eficiente de revisar e aprender. O diferencial é a capacidade de integrar com a API da OpenAI: você insere uma questão e a resposta correta, e o sistema gera automaticamente quatro respostas incorretas. Durante o estudo, as questões são embaralhadas por conteúdo, e as opções de resposta são sorteadas, garantindo uma experiência dinâmica e evitando padrões previsíveis.

## 2. Estrutura do Projeto  
O projeto está dividido em três principais componentes:

- **DeckIQ.Api:** Responsável por tudo que roda no servidor e manipula dados sensíveis.
  - Configurações necessárias:
    - ConnectionString para um banco MySQL.
    - API Key da OpenAI.
    - Escolha entre HTTPS ou HTTP (por padrão, o sistema está configurado para HTTPS).
  
- **DeckIQ.Core:** Atua como intermediário entre o front-end e o back-end.

- **DeckIQ.Web:** Aplicação Web Assembly.

## 3. Desenvolvimento  
Este projeto é uma aplicação prática dos conhecimentos adquiridos no curso da [Balta](https://balta.io/).

## 4. Banco de Dados  
DeckIQ utiliza um banco de dados SQL Server para armazenar todas as informações do usuário e flashcards.

## 5. Configuração do Banco de Dados e Migrations  
Para configurar o banco de dados e aplicar as migrations, execute os seguintes comandos:

````
dotnet ef migrations add v1
dotnet ef database update
````
