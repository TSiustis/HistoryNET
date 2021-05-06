import React from 'react';
import Aux from '../../../hoc/Auxiliary/Auxiliary';

String.prototype.replaceUnderscore=function() {
    var a = this.split("_");
    return a.join(" ");
}

      
        
const death = (props) => {
    
    return (

        <Aux>
            <tr>
                <td>{props.death.day.replaceUnderscore()} {props.death.year}</td>
                <td>{props.death.content}</td>
                <td><a href= {`${props.death.url}`} target ='_blank'>{props.death.url}</a></td>
            </tr>
        </Aux>
    )
}

export default death;