import React from 'react';
import Navigation from '../Navigation/Navigation'
import { Container, Row,Col } from 'react-bootstrap';
const layout = (props) => {
    console.log(props.location)
    return (
        <>
        <Navigation/>
           
            <Row fluid className = "vh-100" style ={{height: "100vw !important"}}>
            <Col md = {4} id = "sidebar" style ={{height: "100vw !important"}}>
               <h2 style ={{marginLeft:15}}>What happened on this particular date?</h2>
               <ul>
                   <li><a href = "/#about">About</a></li>
                   <li><a href = "/#example">API example</a></li>
                   <li><a href = "/#data">Data</a></li>
                   <li><a href = "/#license">License</a></li>
               </ul>
                </Col>
                <Col md = {8} >
                  
                    <main>
                        {props.children}
                    </main>
                </Col>

                </Row>
        
    </>
    )
}

export default layout;