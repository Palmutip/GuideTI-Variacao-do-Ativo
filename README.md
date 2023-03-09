# Teste Pr�tico - Guide Investimentos 

Este documento detalha a solu��o de um desafio que consiste em consultar a varia��o do pre�o de um ativo escolhido nos �ltimos 30 preg�es. A solu��o apresentada neste documento utiliza a API do Yahoo Finance para consultar os pre�os do ativo escolhido. Foi implementada uma solu��o de backend com em .NET Core.

O objetivo principal � apresentar o percentual de varia��o de pre�o de um dia para o outro e o percentual desde o primeiro preg�o apresentado. Para isso, a solu��o busca os dados na API da Yahoo Finance e armazena as informa��es consultadas em uma base de dados SQLite local. A solu��o tamb�m implementa uma API que consulta essas informa��es na base de dados, retornando o valor do ativo nos �ltimos 30 preg�es e apresentando a varia��o do pre�o no per�odo, considerando o valor de abertura.

## Requisitos

1. Solu��o desenvolvida utilizando .NET 6.0 e Entity Framework Core. Sendo assim se faz necess�rio o download do ".NET SDK", conforme o ambiente em que a solu��o estiver operando. O download pode ser realizado atrav�s do link: https://dotnet.microsoft.com/en-us/download

2. Para armazenar os dados, utiliza-se o Banco de Dados SQLite para facilitar o acesso aos dados sem a obrigatoriedade de instala��o de outros softwares.
Caso o usu�rio deseje acessar os dados diretamente pelo arquivo 'sqlite.db', pode-se fazer um download de algum SGBD como o "DB Browser for SQLite". 
Link para o download da ferramenta: https://sqlitebrowser.org/dl/

3. Caso deseje publicar o projeto, certifique-se de instalar o ASP.NET Core Runtime 6.0. Para Windows � necess�rio o download e instala��o do 'Hosting Bundle' e 'x64/x86 Runtime' presentes no link: https://dotnet.microsoft.com/en-us/download/dotnet/6.0 

## Execu��o do Projeto

� de escolha do usu�rio executar o projeto diretamente pelo Visual Studio 2022 (que ser� um processo mais r�pido) ou publicar a solu��o em um site no IIS local. Abaixo segue o passo a passo para a execu��o do projeto diretamente pelo VS22:

1. O usu�rio deve clonar ou mesmo baixar o c�digo fonte pelo bot�o '<>code' dispon�vel nessa p�gina!
2. Abra a solu��o do projeto com o VS22. Esta � a vers�o suportada pelo .NET 6.0
3. Uma vez aberta a solu��o, o usu�rio j� pode execut�-la. Caso o Visual Studio solicite a aprova��o de confian�a do certificado SSL, o usu�rio deve confirmar o aceite para a execu��o do swagger.

## Publicar o Projeto

A seguir um passo a passo para publica��o da solu��o utilizando o Visual Studio 2022 no idioma pt-br.

1. Crie um novo site no IIS. Certifique-se de conceder as permiss�es necess�rias ao pool e ao usu�rio do Windows para n�o obter erros de permiss�o de acesso.
2. Para criar um perfil de publica��o o usu�rio pode clicar com o bot�o direito no projeto 'VariacaoDoAtivo' e escolher a op��o 'Publicar'. Ir� abrir uma janela com as op��es de publica��o. Escolha o Destino como 'Pasta' e clique em pr�ximo. Por padr�o, o visual studio preenche o 'Local da pasta', ent�o j� podemos clicar em Concluir.
3. Ap�s concluir ir� aparecer a janela de progresso e logo ir� mostrar um aviso de Perfil de publica��o criado. Clique em fechar. A janela que estar� aberta no fundo � o perfil de publica��o que criamos. Agora basta clicar no bot�o 'Publicar '
4. Clique na op��o 'Mostrar todas as configura��es'. Aguarde enquanto o Visual Studio carrega o Contexto de Dados. Ser�o mostradas as op��es 'Banco de Dados' e 'Migra��es do Entity Framework'. Expanda ambas as op��es e marque suas caixas de sele��o que representa o valor da ConnectionString do SQLite. Clique em Salvar. **OBS:**: caso seja apresentado algum erro de dotnet tool, certifique-se de que o dotnet tool est� instalado executando o seguinte comando no Console de Gerenciador de pacotes: 'dotnet tool install --global dotnet-ef'
5. Agora o perfil est� preparado para publica��o. O usu�ri pode clicar no bot�o 'Publicar' que est� na janela atual. 
6. Ap�s publicar, o usu�rio dever� copiar o conte�do que se encontra dentro do diret�rio que foi definido no etapa 2 deste passo-a-passo e colar no diret�rio onde foi criado o Site do IIS (na etapa 1).
7. Para acesso �s rotas configuradas no projeto, o usu�rio deve acessar o endere�o que foi configurado no IIS + o sufixo '/swagger/index.html'. Exemplo de acesso: http://localhost:7530/swagger/index.html

## Funcionamento do projeto

- Foram criadas rotas de GET, POST, PATCH e DELETE para a manipula��o do BD sqlite.
- Para popular o banco de dados, o usu�rio deve fazer um POST e informar o NOME do ativo que deseja obter o resultado no parametro 'IdentificacaoAtivo'. Exemplos: **NUBR33.SA** / **PETR4.SA**
- N�o � possivel popular o BD com dados de dois ativos simultaneamente, uma vez que essa API foi construida para listar os ultimos 30 preg�es de um unico ativo. Caso o usu�rio deseje consultar um ativo diferente, ele dever� utilizar a rota de DELETE para limpar os dados da tabela e em seguida executar um novo POST com o nome do ativo desejado.
- O m�todo de PATCH est� sem valida��o de conte�do pois a inten��o era somente demonstrar a aplica��o dos verbos Http.

## Informa��es Adicionais

- Este projeto foi criado para valida��o de conhecimento e serve de Teste Pr�tico para uma oferta de trabalho.
- O usu�rio deve ficar atento com o caminho fisico da pasta deste projeto, pois existe a chance de erro de execu��o do projeto por falta de permiss�o de execu��o dependendo do diret�rio em que se encontra.
- Em caso de d�vidas ou sugest�es, entre em contato comigo pelo meu e-mail: palmutip@hotmail.com


