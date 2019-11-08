import React, { Component } from 'react';
import Header from './Header';
import '../App.css'

export class Result extends Component    {
    displayName = Result.name

    constructor(props) {
        super(props);
        this.state = { 
            resultData: null,
            firstPlace: null,
            secondPlace: null,
            thirdPlace: null,
            loading: true,
            error: false
        };

        fetch('http://localhost:50006/api/v1/competition', {
            method:'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.props.location.state.movies)
        })
        .then(response => response.json())
        .then(data => {
            if(data.Message !== undefined){
                this.setState({loading: false, error: true});
                throw Error(data.Message);
            }

            this.setState({ 
                resultData: data,
                firstPlace: data.CompetitionResult.FirstPlace.PrimaryTitle,
                secondPlace: data.CompetitionResult.SecondPlace.PrimaryTitle,
                thirdPlace: data.CompetitionResult.ThirdPlace.PrimaryTitle,
                loading: false 
            });
        })
        .catch(function(error) {
            alert(error);
        });
      }

    renderResult() {
        return (
            <div>
                <div className="border">
                    <div className="item">1º</div>
                    <div className="value">{this.state.firstPlace}</div>
                </div>
                <br/>
                <div className="border">
                    <div className="item">2º</div>
                    <div className="value">{this.state.secondPlace}</div>
                </div>
                <br/>
                <div className="border">
                    <div className="item">3º</div>
                    <div className="value">{this.state.thirdPlace}</div>
                </div>
                <br/>
                <p><b>JSON Completo do Resultado:</b></p>
                <textarea>{JSON.stringify(this.state.resultData, undefined, 4)}</textarea>
            </div>
        );
    }

    render(){
        let contents = this.state.loading
            ? <p><em>Aguarde, competição em andamento...</em></p>
            : !this.state.error ? this.renderResult() : '';

        return (
            <div>
                <div id='header'>
                    <Header Fase="Resultado Final" Description="Veja o resultado final do Campeonato de filmes de forma e simples e rápida" />
                    <br/>
                </div>
                <div id="body">
                    {contents}
                    <div className='right'>
                        <button onClick={() => {this.props.history.push('/')}}>NOVA COMPETIÇÃO</button>
                    </div>
                </div>                
            </div>
        );
    }
}