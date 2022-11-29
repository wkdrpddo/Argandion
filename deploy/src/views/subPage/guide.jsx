import React from "react";
import { Container, Row, Col } from 'reactstrap';
import PropTypes from "prop-types";
import Header from "../../components/header/header.jsx";
import HeaderBanner from "../../components/banner/banner.jsx";
import Footer from "../../components/footer/footer.jsx";

const Components = () => {
    return (
        <div id="main-wrapper" className="bg-black">
            <Header />
            <div className="page-wrapper">
                <div className="container-fluid">
                <HeaderBanner />
                <Container>
                    <Row className="mt-5 justify-content-center text-white pb-5">
                        <Col md="12">
                                <text className="font-bold pb-4 ml-5" style={{fontSize:"45px"}}>조작법</text>
                                <hr className="pb-4"></hr>
                                <table style={{fontSize:"24px", marginLeft:"40px", lineHeight:"50px"}}>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;이동키</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;WASD | 방향키</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;상호작용</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;F | 우클릭</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;도구사용</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;SPACE | 좌클릭</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;인벤토리</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;I</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;맵</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;M</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;옵션</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;ESC</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;장비변경</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;Number 1 ~ 7 | Q (좌) | E (우)</td>
                                    </tr>
                                    <tr>
                                        <td>▶&nbsp;&nbsp;달리기</td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;:</td>
                                        <td>&nbsp;&nbsp;&nbsp;L.shift</td>
                                    </tr>
                                </table>
                        </Col>
                    </Row>
                </Container>
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
