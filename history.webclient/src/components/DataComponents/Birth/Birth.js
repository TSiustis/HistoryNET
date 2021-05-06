import React from 'react';
import Aux from '../../../hoc/Auxiliary/Auxiliary';

String.prototype.replaceUnderscore=function() {
    var a = this.split("_");
    return a.join(" ");
}

      
        
const birth = (props) => {
    
    return (

        <Aux>
            <tr>
                <td>{props.birth.day.replaceUnderscore()} {props.birth.year}</td>
                <td>{props.birth.content}</td>
                <td><a href= {`${props.birth.url}`} target ='_blank'>{props.birth.url}</a></td>
            </tr>
        </Aux>
    )
}

export default birth;