/* eslint-disable */
import React, { useState } from 'react';
import { HashLink as Link } from 'react-router-hash-link';
import { Container, NavbarBrand, Navbar, Nav, NavItem, NavLink, NavbarToggler, Collapse } from 'reactstrap';

import logo2 from 'assets/images/logos/tmp.png';
import HeaderBanner from "../../components/banner/banner.jsx";
import Header from "../../components/header/header.jsx";
import Footer from "../../components/footer/footer.jsx";

import img01 from 'assets/images/introductionImg/01.JPG';
import img02 from 'assets/images/introductionImg/04.PNG';
import img03 from 'assets/images/introductionImg/05.png';

const HeaderComponent = () => {
    const [isOpen, setIsOpen] = useState(false);

    const toggle = () => setIsOpen(!isOpen);
    return (
        <div id="main-wrapper" className="bg-black">
            <Header />
            <div className="page-wrapper">
                <div className="container-fluid">
                    <HeaderBanner />
                    <div>
                        <div style={{ width: "80%", margin: "auto", textAlign: "center" }}>
                            <div className="pt-5">
                                <table>
                                    <tr>
                                        <td style={{ width: "50%" }}>
                                            <img src={img01} style={{ width: "90%" }}></img>
                                        </td>
                                        <td className="font-bold text-white" valign="top" colspan="2" style={{ widht: "50%", textAlign: "left", fontSize: "32px" }}>
                                            <br/>숲을 되살리고 숲속 마을을 재건하며<br /><br />
                                            각자의 속도에 맞추어 게임을 즐겨보세요!
                                        </td>
                                    </tr>
                                    <tr><br/><br/><br/></tr>
                                    <tr>
                                        <td className="font-bold text-white" valign="top" colspan="2" style={{ widht: "50%", textAlign: "right", fontSize: "32px" }}>
                                            <br />농사를 하며 일상에서 벗어나 여유를 즐겨요.<br/><br/>
                                            느려도 괜찮으니 차근차근 발전을 해보세요!!
                                        </td>
                                        <td>
                                            <img src={img02} style={{ width: "90%" }}></img>
                                        </td>
                                    </tr>
                                    <tr><br/><br/><br/></tr>
                                    <tr>
                                        <td>
                                            <img src={img03} style={{ width: "90%" }}></img>
                                        </td>
                                        <td className="font-bold text-white" valign="top" colspan="2" style={{ widht: "50%", textAlign: "left", fontSize: "32px" }}>
                                            <br />그 외에도 다양한 컨텐츠를 즐기며<br /><br />
                                            아르간디온을 즐겨보세요-!!
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                            <div className="pt-5 pb-3">
                                
                                <div style={{float:"left"}}>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default HeaderComponent;
