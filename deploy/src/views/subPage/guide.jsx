import React from "react";
import { Container, Row, Col } from 'reactstrap';
import PropTypes from "prop-types";
import Header from "../../components/header/header.jsx";
import HeaderBanner from "../../components/banner/banner.jsx";

const Components = () => {
    return (
        <div id="main-wrapper">
            <Header />
            <div className="page-wrapper">
                <div className="container-fluid">
                <HeaderBanner />
                <Container>
                    <Row className="justify-content-center">
                        <Col md="12">
                        <h2>조작법 <br/></h2>
                        <ul>
                            <li> <p>이동 키 : WASD / 방향키</p></li>
                            <li> <p>상호작용 : f, 우클릭</p></li>
                            <li> <p>도구사용 키 : space, 왼클릭</p></li>
                            <li> <p>아이템 인벤토리 : i</p></li>
                            <li> <p>맵 : m  </p></li>
                            <li> <p>퀘스트 : l  // - 장비(착용 중인것) : e</p></li>
                            <li> <p>옵션 : esc</p></li>
                            <li> <p>장비변경 : 1~4 / q,e</p></li>
                            <li> <p>달리기 : L.shift</p></li>
                        </ul>
                        </Col>
                    </Row>
                </Container>
                </div>
            </div>
        </div>
    );
}

Components.propTypes = {
    classes: PropTypes.object
};

export default Components;
