import React from 'react';
import { Container, Row, Col } from 'reactstrap';

const CallToAction = () => {
    return (
        <div className="coming-soon" id="coming">
            <Container className="py-5 mt-5">
                <Row>
                    <Col md="6">
                        <div className="d-flex align-items-center">
                            <div>
                                <h2 className="title text-white font-weight-bold">더 많은 컨텐츠가 업데이트 됩니다.</h2>
                                <h6 className="subtitle font-light text-white">우리는 더 많은 에피소드, 더 좋은 퀄리티의 컨텐츠를 업데이트를 통해 여러분의 즐거움을 만족시킵니다.</h6>
                                <a href="/#coming" className="btn btn-outline-light m-r-20 btn-md m-t-30 font-14">Comming Soon</a>
                            </div>
                        </div>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default CallToAction;
