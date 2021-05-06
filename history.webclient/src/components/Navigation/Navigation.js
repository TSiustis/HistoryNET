import React from 'react';
import './Navigation.css';
import { Navbar, Nav} from 'react-bootstrap';
import {NavLink} from 'react-router-dom';
import {LinkContainer} from 'react-router-bootstrap';

const navigation = (props) => {
        return (
                  <Navbar className = "nav-fill w-100" >
                        <Navbar.Brand >
                            <NavLink to={'/'} exact >History Today</NavLink></Navbar.Brand>
                        <Navbar.Collapse>
                        <Nav>
                            <LinkContainer to={'/event-list'} exact>
                                <Nav.Link eventKey={1}>
                                    Events on this date 
                                </Nav.Link>
                            </LinkContainer>
                            <LinkContainer to={'/death-list'}>
                                <Nav.Link eventKey={2}>
                                    Deaths on this date
                                </Nav.Link>
                            </LinkContainer>
                            <LinkContainer to={'/birth-list'}>
                                <Nav.Link eventKey={3}>
                                    Births on this date
                                </Nav.Link>
                            </LinkContainer>
                        </Nav>
                    </Navbar.Collapse>
        </Navbar>
        )
    }


export default navigation;