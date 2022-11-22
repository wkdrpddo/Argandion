import React from 'react';
import { Container, Row, Col } from 'reactstrap';

// import img1 from 'assets/images/background/brain.jpg';
// import img2 from 'assets/images/background/e101bg.jpg';
// import img3 from 'assets/images/background/room.png';

// import tmp1 from 'assets/images/background/tmp1.jpg';
// import tmp2 from 'assets/images/background/tmp2.jpg';
// import tmp3 from 'assets/images/background/tmp3.jpg';

import Carousel1 from "../../components/carousel1.jsx";
import Carousel2 from "../../components/carousel2.jsx";
import Carousel3 from "../../components/carousel3.jsx";

const Images = () => {
    return (
        <div>
            <div className="spacer" id="imgs-component">
                <Container>
                    <Row className="justify-content-center">
                        <Col md="7" className="text-center">
                            {/* <h1 className="title font-bold">인게임 이미지</h1> */}
                            <h3 className="subtitle"> 아르간디온에서 다양한 체험을 통해</h3>

                            <h3 className="subtitle">나의 숲을 가꿔보세요.</h3>
                        </Col>
                    </Row>
                </Container>
            </div>
            <Container>
                <Row>
                    <Col lg="4" className="mt-3">
                        <Carousel1 />
                    </Col>
                    <Col lg="4" className="mt-3">
                        <Carousel2 />
                    </Col>
                    <Col lg="4" className="mt-3">
                        <Carousel3 />
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default Images;
