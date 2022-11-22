import React from 'react';
// import { HashLink as Link } from 'react-router-hash-link';
import { Container, Row, Col, Button } from 'reactstrap';

const HeaderBanner = () => {
    return (
        <div className="static-slider-head">
            <Container>
                <Row className="justify-content-center">
                    <Col lg="8" md="6" className="align-self-center text-center">
                        <h1 className="title">Argandion </h1>
                        {/* <h4 className="subtitle font-light">숲을 </h4> */}
                        <a id="downloadButton" href='./Argandion.zip' download>
                            <Button to="/#coming" className="btn btn-md m-t-30 btn-success-gradiant font-16">
                            무료 다운로드
                            </Button>
                            </a>
                        {/* <Button to="/#coming" className="btn btn-md m-t-30 btn-info-gradiant font-14">
                            <a id="downloadButton" href='./detective.apk' download>다운로드</a>
                        </Button> */}
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default HeaderBanner;
