import React from 'react';
import '../App.css'

const Header = (props) => (
    <header>
        <br/>
        <p>CAMPEONATO DE FILMES</p>
        <h1>{props.Fase}</h1>
        <hr/>
        <p>{props.Description}</p>
        <br/>
    </header>
)

export default Header;
