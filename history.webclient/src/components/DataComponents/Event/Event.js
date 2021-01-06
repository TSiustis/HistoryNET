import React from 'react';
import Aux from '../../../hoc/Auxiliary/Auxiliary';

String.prototype.replaceUnderscore=function() {
    var a = this.split("_");
    return a.join(" ");
}

      
        
const event = (props) => {
    
const url=`https://localhost:44350/api/events/${props.event.id}/eventlinks`;
    return (

        <Aux>
            <tr>
                <td>{props.event.day.replaceUnderscore()} {props.event.year}</td>
                <td>{props.event.content}</td>
                <td><a href= {`${props.event.url}`} target ='_blank'>{props.event.url}</a></td>
            </tr>
        </Aux>
    )
}

export default event;