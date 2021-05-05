import React, { Component } from 'react';
import { Table, Col, Row } from 'react-bootstrap';
import Aux from '../../hoc/Auxiliary/Auxiliary';
import Event from './Event/Event';

import { connect } from 'react-redux';
import * as repositoryActions from '../../store/actions/repositoryActions';

class EventList extends Component {

    constructor(props) {
        super(props);    
        this.state = {
          searchTerm:  '',
        };
      }
    // componentDidMount = () => {
    //     let url = 'https://localhost:44350/api/events/getalleventsforday?day=august_7';
    //     this.props.onGetData(url, { ...this.props });
    // }
    search(){
        let {searchTerm } =this.state;
        const url=`https://localhost:44350/api/events/getalleventsforday?day=${searchTerm}`;

      
        this.props.onGetData(url, { ...this.props });
    }
    render() {
        let events = [];
        if (this.props.data && this.props.data.length > 0) {
            events = this.props.data.map((event) => {
                return (
                
                 
                    <Event key={event.id} event={event} {...this.props} />
                
                )
            })
        }

        return (
            <Aux>
                <Row>

                </Row>
                <br />
                <Row>
                    <Col md={12}>
                        
                    <span>Enter the day and month in the format 'month_day' (e.g. August_7):</span><br/>
                    <div class="input-group">
                     <input class = "form-control" placeholder="Enter day..." onChange={(e) => this.setState({ searchTerm: e.target.value })}/>
                     <div className="col-md-2">
                            <button type="submit" 
                            className="btn btn-primary btn-inline"
                            onClick={()=> this.search()}
                            >Search</button>
                        </div>
                   </div>
                        <Table responsive striped>
                            <thead>
                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {events}
                            </tbody>
                        </Table>
                       
                    </Col>
                </Row>
            </Aux>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        data: state.repository.data
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onGetData: (url, props) => dispatch(repositoryActions.getData(url, props))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(EventList);