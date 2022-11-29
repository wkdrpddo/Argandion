import React from "react";
import { Container, Row, Col } from 'reactstrap';
import PropTypes from "prop-types";

import HeaderBanner from "../../components/banner/banner.jsx";
import Header from "../../components/header/header.jsx";
import Footer from "../../components/footer/footer.jsx";

const Components = () => {
    return (
        <div id="main-wrapper" className="bg-black">
            <Header />
            <div className="page-wrapper">
                <div className="container-fluid">
                <HeaderBanner />
                <div>
                    <Row className="justify-content-center bg-white pt-5 pb-5">
                        <Col md="12">
                            <h1 className="font-bold pb-4 ml-5" style={{textAlign:"center"}}>시스템 요구사양</h1>
                                <hr className="pb-4"></hr>
                                <table style={{fontSize:"24px", marginLeft:"auto", marginRight:"auto", lineHeight:"50px"}}>
                                    <tr>
                                        <th style={{textAlign:"right"}}>운영 체제: </th>
                                        <th>&nbsp;Windows 10 Pro 64비트 (10.0, 빌드 19042)</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>언어: </th>
                                        <th>&nbsp;한국어 (국가별 설정: 한국어)</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>시스템 제조업체: </th>
                                        <th>&nbsp;SAMSUNG ELECTRONICS CO., LTD.</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>시스템 모델: </th>
                                        <th>&nbsp;850XCJ</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>BIOS: </th>
                                        <th>&nbsp;P02AJK.038.200702.SH</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>프로세서: </th>
                                        <th>&nbsp;Intel(R) Core(TM) i7-10750H CPU @ 2.60GHz (12 CPUs), ~2.6GHz</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>메모리: </th>
                                        <th>&nbsp;16384MB RAM</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>페이지 파일: </th>
                                        <th>&nbsp;13494MB 사용됨, 9978MB 사용 가능</th>
                                    </tr>
                                    <tr>
                                        <th style={{textAlign:"right"}}>DirectX 버전: </th>
                                        <th>&nbsp;DirectX 12</th>
                                    </tr>
                                </table>
                        </Col>
                    </Row>
                </div>
                </div>
            </div>
            <Footer />
        </div>
    );
}

Components.propTypes = {
    classes: PropTypes.object
};

export default Components;
