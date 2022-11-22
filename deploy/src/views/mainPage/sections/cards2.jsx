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
                            <h2 className="title font-bold">배경 스토리</h2>
                            <h5 className="subtitle">
                                여러 종족 간의 전쟁으로 황폐화된 숲 <br/> <br/>
                                숲의 주인인 엘프에게 남은 마지막 희망 세계수 <br/> <br/>
                                선택받은 당신! <br/>
                                황폐해진 숲을 살리고 마을을 재건하자!<br/>
                            </h5>
                        </Col>
                    </Row>
                </Container>
            </div>            
        </div>
    );
}

export default Cards;