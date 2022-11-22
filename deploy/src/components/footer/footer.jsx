/* eslint-disable */
import React from 'react';
import { Container, Row, Col } from 'reactstrap';

const Footer = () => {
    return (
        <div className="footer4 b-t spacer">
            <Container>
                <Row>
                    <Col lg="3" md="6" className="m-b-30">
                        <h5 className="m-b-20">Address</h5>
                        <p>부산광역시 강서구 녹산산업중로 333 105팀</p>
                    </Col>
                    <Col lg="3" md="6" className="m-b-30">
                        <h5 className="m-b-20">Email</h5>
                        <p>Office :  <a href="#" className="link">example@gmail.com</a> <br />Site :  <a href="#" className="link">www.example.com</a></p>
                    </Col>
                    <Col lg="3" md="6" className="m-b-30">
                        <h5 className="m-b-20">리뷰 및 버그 리포트</h5>
                        <a href="https://forms.gle/vDvco92rb9qXGYsu9" target="_blank" className="link"><i className="fa fa-question"></i></a>
                    </Col>
                    <Col lg="3" md="6">
                        <h5 className="m-b-20">Social</h5>
                        <div className="round-social light">
                            <a href="https://lab.ssafy.com/s07-final/S07P31E105" target="_blank" className="link"><i className="fa fa-gitlab"></i></a>
                        </div>
                    </Col>
                </Row>
                <div className="f4-bottom-bar">
                    <Row>
                        <Col md="12">
                            <div className="d-flex font-14">
                                <div className="m-t-10 m-b-10 copyright">All Rights Reserved by SSAFY.</div>
                                <div className="links ml-auto m-t-10 m-b-10">
                                    <a href="#" className="p-10 p-l-0">Terms of Use</a>
                                    <a href="#" className="p-10">Legal Disclaimer</a>
                                    <a href="#" className="p-10">Privacy Policy</a>
                                </div>
                            </div>
                        </Col>
                    </Row>
                </div>
            </Container>
        </div>
    );
}
export default Footer;
