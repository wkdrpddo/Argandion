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
                        <text className="font-bold pb-4 ml-5" style={{fontSize:"45px"}}>아이콘 출처</text>
                        <hr className="pb-4"></hr>
                                <table style={{fontSize:"16px", marginLeft:"40px", lineHeight:"40px"}}>
                                    <tr>
                                        <td>모루</td>
                                        <td><a href="https://www.flaticon.com/kr/free-icons/" title="모루 아이콘">모루 아이콘 제작자: Freepik - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>헛간</td>
                                        <td><a href="https://www.flaticon.com/free-icons/barn" title="barn icons">Barn icons created by Freepik - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>공방</td>
                                        <td><a href="https://www.flaticon.com/free-icons/carpentry" title="carpentry icons">Carpentry icons created by berkahicon - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>발바닥&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td><a href="https://www.flaticon.com/free-icons/tiger" title="tiger icons">Tiger icons created by surang - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>물고기&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td><a href="https://www.flaticon.com/free-icons/fish" title="fish icons">Fish icons created by Freepik - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>튜닉</td>
                                        <td><a href="https://www.flaticon.com/free-icons/tunic" title="tunic icons">Tunic icons created by Freepik - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>상의</td>
                                        <td><a href="https://www.flaticon.com/free-icons/t-shirt" title="t-shirt icons">T-shirt icons created by gariebaldy - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>하의</td>
                                        <td><a href="https://www.flaticon.com/free-icons/pants" title="pants icons">Pants icons created by gariebaldy - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>물방울</td>
                                        <td><a href="https://www.flaticon.com/free-icons/water" title="water icons">Water icons created by Freepik - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>물고기</td>
                                        <td><a href="https://www.flaticon.com/free-icons/fish" title="fish icons">Fish icons created by Freepik - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>괭이</td>
                                        <td><a href="https://www.flaticon.com/free-icons/hoe" title="hoe icons">Hoe icons created by Smashicons - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>낚싯대</td>
                                        <td><a href="https://www.flaticon.com/free-icons/fishing" title="fishing icons">Fishing icons created by Freepik - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>엘프</td>
                                        <td><a href="https://www.flaticon.com/free-icons/elf" title="elf icons">Elf icons created by Freepik - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>세계수</td>
                                        <td><a href="https://www.flaticon.com/free-icons/tree" title="tree icons">Tree icons created by Freepik - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>작물</td>
                                        <td><a href="https://www.flaticon.com/free-icons/plant" title="plant icons">Plant icons created by Smashicons - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>집</td>
                                        <td><a href="https://www.flaticon.com/free-icons/real-estate" title="real estate icons">Real estate icons created by Freepik - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>제단</td>
                                        <td><a href="https://www.flaticon.com/free-icons/archaeology" title="archaeology icons">Archaeology icons created by Vitaly Gorbachev - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>제단 포탈</td>
                                        <td><a href="https://www.flaticon.com/free-icons/portal" title="portal icons">Portal icons created by Andrejs Kirma - Flaticon</a></td>
                                    </tr>
                                    <tr>
                                        <td>건설 전</td>
                                        <td><a href="https://www.flaticon.com/free-icons/brick" title="brick icons">Brick icons created by Smashicons - Flaticon</a></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td></td>
                                        <td></td>
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
