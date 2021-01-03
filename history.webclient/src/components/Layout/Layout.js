import React from 'react';
import Navigation from '../Navigation/Navigation'
import { Container, Row } from 'react-bootstrap';
import {Col, Navbar, Nav, NavItem,NavDropdown} from 'react-bootstrap';
import {NavLink} from 'react-router-dom';
import {LinkContainer} from 'react-router-bootstrap';
const layout = (props) => {
    return (
        <Container>
            <Row>
                 <Navigation/>
            </Row>
            <main>
                {props.children}
            </main>
        </Container>
    )
}

export default layout;