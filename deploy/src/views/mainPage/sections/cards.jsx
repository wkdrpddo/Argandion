/* eslint-disable */
import FileSaver from 'file-saver';
import React, {useCallback} from 'react';
import { Card, Button, CardTitle, CardText, Row, Col, Container } from 'reactstrap';
import "./css/cards.css";

const Cards = () => {
   

    return (
        <div >
            <Container>
                <Row className="justify-content-center">
                    <Col lg="8" md="6" className="align-self-center text-center mb-5">
                        {/* <h4 className="subtitle font-light">숲을 </h4> */}
                        <a id="downloadButton" href='./Argandion.zip' download>
                            <Button to="/#coming" className="btn btn-md m-t-30 btn-success font-14">
                            다운로드
                            </Button>
                            </a>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default Cards;