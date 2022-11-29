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
        <div className="bg-white" style={{justifyContent:"center", paddingBottom:"100px"}}>
            <div className="spacer" id="imgs-component">
                <Container>
                    <Row className="justify-content-center">
                        <Col md="7" className="text-center">
                            <h3 className="fw-bold primary mb-4"> 아르간디온에서 세계수와 함께</h3>
                            <h3 className="fw-bold primary mb-4">다양한 경험을 하며</h3>
                            <h3 className="fw-bold primary">숲의 마을을 되살리세요!</h3>
                        </Col>
                    </Row>
                </Container>
            </div>
            <hr style={{width:"90%", margin:"auto", backgroundColor:"gray", height:"4px"}}></hr>
            <div className="pt-5" style={{margin:"auto", maxWidth:"1440px"}}>
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
            </div>
        </div>
    );
}

export default Images;
