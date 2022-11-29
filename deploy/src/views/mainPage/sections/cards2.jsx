/* eslint-disable */
// 간략한 스토리를 담는 카드
import React from 'react';
import { Card, Button, CardTitle, CardText, Row, Col, Container } from 'reactstrap';
import "./css/cards.css";

const Cards = () => {


    return (
        <div>
            <div className="spacer" id="card-component">
                <Container>
                    <Row className="justify-content-center">
                        <Col md="7" className="text-center">
                            <h2 className="text-light font-bold">배경 스토리</h2><br/>
                            <h5 className="subtitle">
                                이기심으로 이어진 전쟁과 약탈<br/> <br/>
                                황폐해진 땅과 삶의 터전을 잃은 숲의 주민 엘프들 <br/> <br/>
                                숲의 마지막 희망인 세계수와 <br /><br />
                                선택받은 바로 당신!! <br/><br/>
                                황폐해진 숲을 되살리고 마을을 재건하라!!
                            </h5>
                        </Col>
                    </Row>
                </Container>
            </div>            
        </div>
    );
}

export default Cards;