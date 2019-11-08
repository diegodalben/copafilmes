# Campeonato de Filmes

Este projeto tem como objetivo a realização de um campeonato entre os filmes selecionados pelo jogador.

Inicialmente são listados 64 filmes, onde o jogador deve selecionar 16 candidatos para a realização do campeonato. Cada filme possui uma pontuação já determinada, que é usada como critério de apuração e classificação para as fases seguintes.


## Pré-Requisitos

Para o real funcionamento da solução, é necessário um pré-requisito mínimo com as seguintes ferramentas e Frameworks:

* [.Net Core 2.1](https://www.microsoft.com/net/download)
* [Node.js v8.11.3](https://nodejs.org/en/download/)
* Visual Studio 2017 (15.7) ou Visual Studio Code

## Instalando a aplicação

Para que a solução fique pronta para utilização, são necessários os seguintes passos para instalação:

* Compilar a solução para que sejam restaurados os pacotes e dependências dos projetos e também gerados os arquivos binários. Abra o Prompt de Comando, aponte para a pasta da solução *".sln" e execute o seguinte comando:

	> dotnet build

* É necessário executar uma API que é base para o Backend da aplicação.
Ainda no Prompt de Comando e na pasta da solução, navegue até a pasta do projeto da API (*"./Source/CopaFilmes.WebApi"*) e execute o seguinte comando:

	> dotnet run

	A API ficará disponível na porta 50006: http://localhost:50006

* Para executar a aplicação que dá acesso ao campeonato, ainda no Prompt de Comando, navegue até a pasta da aplicação (*"./Source/CopaFilmes.WebApp/ClientApp"*) e execute os comandos na sequencia abaixo:

	> npm install
	 
	> npm start

	A aplicação será aberta em seu navegador padrão e estará pronta para ser usada.

## Passos para utilização e realização do campeonato

Logo na página inicial serão listados 64 filmes com possibilidade de seleção. Escolha e selecione exatos 16 filmes, independentemente da ordem.

Após selecionados os filmes, clique no botão *Gerar Meu Campeonato* para dar início a competição entre os filmes escolhidos.

Após as batalhas e apuração dos resultados, será aberta uma nova página contendo o resultado final do campeonato.

Caso deseje realizar um novo campeonato, clique no botão *Nova Competição*.

## Rodando testes automatizados

A solução dispõe de testes de unidade, que podem ser executados seguindo os passos abaixo:

* Para executar os testes a nível da solução, abra o Prompt de Comando, navegue até a pasta da solução e execute o arquivo *coverlet.bat*.

* Para executar os testes a nível de projeto, abra o Prompt de Comando, navegue até a pasta do projeto desejado (contidas dentro da pasta *Test*) e execute o arquivo coverlet.bat.

Todos os projetos de testes possuem análise de cobertura de código, que são exibidos no Console após a sua execução.
Quando executado o arquivo *coverlet.bat* de cada projeto, ao final da execução é gerada uma pasta chamada *Report* na raiz do projeto, contendo o relatório de cobertura de código, gerado pelo *ReportGenerator*. Para abri-lo, basta executar o arquivo *index.htm*.