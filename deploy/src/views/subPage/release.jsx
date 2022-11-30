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
                            <Col md="10">
                            <hr className="pb-2"></hr>
                                <text className="font-bold" style={{ fontSize: "30px" }}>v.1.0.0</text>
                                <p className="text-end mr-4">2022. 11. 18</p>
                                <h5 style={{ color: "white" }}>New</h5>
                                <ul>
                                    <li>베타 버전 출시</li>
                                </ul>
                            </Col>
                            <Col md="10">
                                <hr className="pb-2"></hr>
                                <text className="font-bold" style={{ fontSize: "30px" }}>v.1.1.0</text>
                                <p className="text-end mr-4">2022. 11. 28</p>
                                <h5 style={{ color: "white" }}>New</h5>

                                <ul>
                                    <li>저장/불러오기 기능 추가</li>
                                    <li>인벤토리에서 마우스 좌클릭 상호작용 기능 추가</li>
                                    <li>인벤토리에서 커서를 올림으로써 아이템 설명 표시 추가</li>
                                    <li>월/일 과 시/분 표시 기능 추가</li>
                                    <li>ESC 버튼으로 패널 닫기 기능 추가</li>
                                    <li>침대 상호작용으로 하루를 마무리하는 기능 추가</li>
                                    <li>마우스 커서 아이콘 추가</li>
                                    <li>장비 사용 시 스테미나 감소 추가</li>
                                </ul>
                                <h5 style={{ color: "white" }}>Fix</h5>
                                <ul>
                                    <li>밸런스 패치 - 전반적인 아이템 가격 조정, 달리기 스태미너 감소율 조정</li>
                                    <li>'리데이사의 가호' 아이콘이 정상적으로 사라지지 않는 현상 수정</li>
                                    <li>건물 건설 시 필요한 자원이 정상적으로 표기되지 않는 현상 수정</li>
                                    <li>인벤토리의 아이템이 - 개수로 내려가는 현상 수정</li>
                                    <li>아이템 사용 시 데이터 값이 UI와 동기되지 않는 현상 수정</li>
                                    <li>미끼 사용 시, UI 동기 오류 수정</li>
                                    <li>인벤토리가 가득 찬 상태에서 드랍된 아이템과 접촉 시 정상적으로 동작하지 않는 현상 수정</li>
                                    <li>창고의 아이템이 정상적으로 반입/반출되지 않는 현상 수정</li>
                                    <li>일부 지형의 표면 수정</li>
                                    <li>UI 화면의 종료가 비정상적으로 작동하는 현상 수정</li>
                                    <li>일부 누락된 이미지 수정</li>
                                    <li>창고의 아이템 반출 시 이미지가 검게 물드는 현상 수정</li>
                                </ul>
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
