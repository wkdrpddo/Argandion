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
                        <Col md="10">
                        <h2>아이콘 출처 <br/></h2>
                        <ul>
                            <li> <p>모루 <a href="https://www.flaticon.com/kr/free-icons/" title="모루 아이콘">모루 아이콘 제작자: Freepik - Flaticon</a></p></li>
                            <li> <p>헛간 <a href="https://www.flaticon.com/free-icons/barn" title="barn icons">Barn icons created by Freepik - Flaticon</a></p></li>
                            <li> <p>공방 <a href="https://www.flaticon.com/free-icons/carpentry" title="carpentry icons">Carpentry icons created by berkahicon - Flaticon</a> </p></li>
                            <li> <p>발바닥 <a href="https://www.flaticon.com/free-icons/tiger" title="tiger icons">Tiger icons created by surang - Flaticon</a> </p></li>
                            <li> <p>튜닉 <a href="https://www.flaticon.com/free-icons/tunic" title="tunic icons">Tunic icons created by Freepik - Flaticon</a> </p></li>
                            <li> <p>물고기 <a href="https://www.flaticon.com/free-icons/fish" title="fish icons">Fish icons created by Freepik - Flaticon</a> </p></li>
                            <li> <p>상의 <a href="https://www.flaticon.com/free-icons/t-shirt" title="t-shirt icons">T-shirt icons created by gariebaldy - Flaticon</a> </p></li>
                            <li> <p>하의 <a href="https://www.flaticon.com/free-icons/pants" title="pants icons">Pants icons created by gariebaldy - Flaticon</a> </p></li>
                            <li> <p>모자 <a href="https://www.flaticon.com/free-icons/hat" title="hat icons">Hat icons created by Freepik - Flaticon</a> </p></li>
                            <li> <p>신발 <a href="https://www.flaticon.com/free-icons/boot" title="boot icons">Boot icons created by muhammad atho' - Flaticon</a> </p></li>

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
