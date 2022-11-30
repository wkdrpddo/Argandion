/* eslint-disable */
import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import questionIcon from 'assets/images/iconimg/question04.png';

const Footer = () => {
    return (
        <div className="footer4 b-t spacer" >
            <Container>
                <Row className="justify-content-around">
                    <Col lg="3" md="6" className="m-b-30">
                        <h5 className="m-b-20">Contact</h5>
                            <h6><i className="fa fa-envelope"></i> argandionworld@gmail.com</h6>
                    </Col>
                    <Col lg="3" md="6" className="m-b-30">
                        <h5 className="m-b-20">Review & Bug Report</h5>
                        <div className="round-social light">
                            <a href="https://forms.gle/vDvco92rb9qXGYsu9" target="_blank" className="link"><i className="fa fa-question"></i></a>
                        </div>
                    </Col>
                    <Col lg="3" md="6">
                        <h5 className="m-b-20">Social</h5>
                        <div className="round-social light">
                            <a href="https://github.com/bgpjh/Argandion" target="_blank" className="link"><i className="fa fa-github"></i></a>
                            <a href="https://www.youtube.com/channel/UCoVmDqSBLRaj4Wj-c5QvtoQ" target="_blank" className="link"><i className="fa fa-youtube-play"></i></a>
                        </div>
                    </Col>
                </Row>
                <div className="f4-bottom-bar">
                    <Row>
                        <Col md="12">
                            <div className="d-flex font-14">
                                <div className="m-t-10 m-b-10 copyright">All Rights Reserved by Argandion.</div>
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
