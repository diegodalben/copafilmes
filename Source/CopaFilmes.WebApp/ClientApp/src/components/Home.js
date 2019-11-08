import React, { Component } from 'react';
import Header from './Header';
import '../App.css'

export class Home extends Component {
  displayName = Home.name

  constructor(props) {
    super(props);
    this.state = { movies: [], loading: true, counter: 0, selectedMovies: [] };

    fetch('http://localhost:50006/api/v1/competition')
      .then(response => response.json())
      .then(data => {
        if(data.Message !== undefined){
            throw Error(data.Message);
        }
        this.setState({ movies: data, loading: false });
      })
      .catch(function(error) {
        alert(error);
      });
  }

  onSelectMovie(movie) {
    var count;
    var chk = document.getElementById(movie.Id);
    var arrMovies = this.state.selectedMovies.slice();
    
    if(chk.checked) {
      count = this.state.counter + 1;
      arrMovies.push(movie);
    }
    else {
      count = this.state.counter - 1;
      arrMovies.splice(arrMovies.indexOf(movie));
    }

    this.setState({
      counter: count,
      selectedMovies: arrMovies
    })
  }

  renderMoviesTable(movies) {
    return (
      <div>
        <table className='table'>
            <thead>
              <tr>
                <th>Sel.</th>
                <th>Título</th>
                <th>Ano Lançamento</th>
              </tr>
            </thead>
            <tbody>
              {movies.map(movie =>
                <tr key={movie.Id}>
                  <td><input type="checkbox" id={movie.Id} onChange={() => {this.onSelectMovie(movie)}} /></td>
                  <td>{movie.PrimaryTitle}</td>
                  <td>{movie.Year}</td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Aguarde, carregando filmes...</em></p>
      : this.renderMoviesTable(this.state.movies);

    return (
      <div>
        <div id='header'>
          <Header Fase="Fase de Seleção" Description="Selecione 16 filmes que você deseja que entrem na competição e depois pressione o botão Gerar Meu Campeonato para prosseguir." />
          <br/>
          <div className="counter">{this.state.counter} FILMES SELECIONADOS.</div>
          <div className='right'>
            <button onClick={() => {this.props.history.push({
              pathname: '/Result',
              state: {
                movies: this.state.selectedMovies
              }
            })}}>GERAR MEU CAMPEONATO</button>
          </div>
        </div>
        <div id="body">
          {contents}
        </div>
      </div>      
    );
  }
}
