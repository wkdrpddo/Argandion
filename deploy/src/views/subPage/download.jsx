import React from "react";
import { Container, Row, Col } from 'reactstrap';
import PropTypes from "prop-types";

import HeaderBanner from "../../components/banner/banner.jsx";
import Header from "../../components/header/header.jsx";
import requirements from 'assets/images/background/requirements.png';

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
                        <h2>시스템 요구사양  </h2>
                <img src={requirements} alt="img" className="img-responsive img-thumbnail" width="100%" height="100%" />

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
