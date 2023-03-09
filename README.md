# Teste Prático - Guide Investimentos 

Este documento detalha a solução de um desafio que consiste em consultar a variação do preço de um ativo escolhido nos últimos 30 pregões. A solução apresentada neste documento utiliza a API do Yahoo Finance para consultar os preços do ativo escolhido. Foi implementada uma solução de backend com em .NET Core.

O objetivo principal é apresentar o percentual de variação de preço de um dia para o outro e o percentual desde o primeiro pregão apresentado. Para isso, a solução busca os dados na API da Yahoo Finance e armazena as informações consultadas em uma base de dados SQLite local. A solução também implementa uma API que consulta essas informações na base de dados, retornando o valor do ativo nos últimos 30 pregões e apresentando a variação do preço no período, considerando o valor de abertura.

## Requisitos

1. Solução desenvolvida utilizando .NET 6.0 e Entity Framework Core. Sendo assim se faz necessário o download do ".NET SDK", conforme o ambiente em que a solução estiver operando. O download pode ser realizado através do link: https://dotnet.microsoft.com/en-us/download

2. Para armazenar os dados, utiliza-se o Banco de Dados SQLite para facilitar o acesso aos dados sem a obrigatoriedade de instalação de outros softwares.
Caso o usuário deseje acessar os dados diretamente pelo arquivo 'sqlite.db', pode-se fazer um download de algum SGBD como o "DB Browser for SQLite". 
Link para o download da ferramenta: https://sqlitebrowser.org/dl/

3. Caso deseje publicar o projeto, certifique-se de instalar o ASP.NET Core Runtime 6.0. Para Windows é necessário o download e instalação do 'Hosting Bundle' e 'x64/x86 Runtime' presentes no link: https://dotnet.microsoft.com/en-us/download/dotnet/6.0 

## Execução do Projeto

Abaixo segue o passo a passo para a execução do Projeto:

1. O usuário deve clonar ou mesmo baixar o código fonte pelo botão '<>code' disponível nessa página!
2. Abra a solução do projeto com a versão 2022 do Visual Studio. Esta é a versão suportada pelo .NET 6.0
3. Uma vez aberta a solução, o usuário já pode executá-la. Caso o Visual Studio solicite a aprovação de confiança do certificado SSL, o usuário deve confirmar o aceite para a execução do swagger.

## Publicar o Projeto

1. Crie um novo site no IIS. Certifique-se de conceder as permissões necessárias ao pool e ao usuario do Windows para não obter erros de permissão de acesso.
2. Já existe um perfil de publicação criado nessa solução. O usuário pode clicar com o botão direito no projeto 'VariacaoDoAtivo' e escolher a opção 'Publicar'. Irá abrir uma janela com as configurações previamente realizada.
3. Após publicar, o usuario deverá copiar o conteúdo que se encontra dentro de 'bin\Release\net6.0\publish\' na pasta do projeto e colar no diretório onde foi criado o Site do IIS.
4. Para acesso às rotas configuradas no projeto, o usuario deve acessar o endereço que foi configurado no IIS + o sufixo '/swagger/index.html'. Exemplo de acesso: http://localhost:7530/swagger/index.html

## Funcionamento do projeto

- Foram criadas rotas de GET, POST, PATCH e DELETE para a manipulação do BD sqlite.
- Para popular o banco de dados, o usuário deve fazer um POST e informar o NOME do ativo que deseja obter o resultado no parametro 'IdentificacaoAtivo'. Exemplos: **NUBR33.SA** / **PETR4.SA**
- Não é possivel popular o BD com dados de dois ativos simultaneamente, uma vez que essa API foi construida para listar os ultimos 30 pregões de um unico ativo. Caso o usuário deseje consultar um ativo diferente, ele deverá utilizar a rota de DELETE para limpar os dados da tabela e em seguida executar um novo POST com o nome do ativo desejado.
- O método de PATCH está sem validação de conteúdo pois a intenção era somente demonstrar a aplicação dos verbos Http.

## Informações Adicionais

- Este projeto foi criado para validação de conhecimento e serve de Teste Prático para uma oferta de trabalho.
- O usuário deve ficar atento com o caminho fisico da pasta deste projeto, pois existe a chance de erro de execução do projeto por falta de permissão de execução dependendo do diretório em que se encontra.
- Em caso de dúvidas ou sugestões, entre em contato comigo pelo meu e-mail: palmutip@hotmail.com


