/* eslint-disable */
import React, { useState } from 'react';
import { HashLink as Link } from 'react-router-hash-link';
import { Container, NavbarBrand, Navbar, Nav, NavItem, NavLink, NavbarToggler, Collapse } from 'reactstrap';

import logo2 from 'assets/images/logos/tmp.png';

const HeaderComponent = () => {
    const [isOpen, setIsOpen] = useState(false);

    const toggle = () => setIsOpen(!isOpen);
    return (
        <div id="section">
            <div className="header1 po-relative bg-dark">
                <Container>
                    <Navbar className="navbar-expand-lg h2-nav">
                        <NavbarBrand href="#"><img src={logo2} alt="wrapkit" /></NavbarBrand>
                        <NavbarToggler onClick={toggle}><span className="ti-menu text-white"></span></NavbarToggler>
                        <Collapse isOpen={isOpen} navbar id="header1">
                            <Nav navbar className="ml-auto mt-2 mt-lg-0">
                                <NavItem>
                                    <Link className="nav-link font-weight-bold" to={"download"}>
                                        다운로드
                                    </Link>
                                </NavItem>
                                <NavItem>
                                    <Link className="nav-link font-weight-bold" to={"introduction"}>
                                        게임 소개
                                    </Link>
                                </NavItem>
                                <NavItem>
                                    <Link className="nav-link font-weight-bold" to={"guide"}>
                                        조작법 및 사용 가이드
                                    </Link>
                                </NavItem>
                                <NavItem>
                                    <Link className="nav-link font-weight-bold" to={"copyright"}>
                                        저작권
                                    </Link>
                                </NavItem>
                            </Nav>
                        </Collapse>
                    </Navbar>
                </Container>
            </div>
        </div>
    );
}

export default HeaderComponent;
